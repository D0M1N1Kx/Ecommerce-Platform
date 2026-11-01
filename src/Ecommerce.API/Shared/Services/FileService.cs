using SkiaSharp;

namespace Ecommerce.API.Shared.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string folder);
    Task DeleteFileAsync(string relativePath);
    Task<string> ReplaceFileAsync(IFormFile file, string directory, string? oldPath);
    bool Exists(string relativePath);
    string? GetValidFilePath(string? path);
}

public class FileService : IFileService
{
    private readonly string _publicPath; // public directory path
    
    private readonly string[] _allowedImageExtensions = [ ".jpg", ".jpeg", ".png", ".webp" ];
    private readonly string[] _allowedImageMimeTypes = [ "image/jpeg", "image/png", "image/webp" ];

    private readonly string[] _allowedVideoExtensions = [ ".mp4", ".webm", ".mov" ];
    private readonly string[] _allowedVideoMimeTypes = ["video/mp4", "video/webm", "video/quicktime"];

    private readonly string[] _allowedAudioExtensions = [ ".mp3", ".m4a", ".flac", ".ogg", ".wav" ];

    private readonly string[] _allowedAudioMimeTypes =
    [
        "audio/mpeg", "audio/m4a", "audio/x-m4a", "audio/mp4", "audio/flac", "audio/x-flac", "audio/ogg", "audio/wav",
        "audio/x-wav"
    ];

    private IEnumerable<string> AllowedExtensions => _allowedImageExtensions.Concat(_allowedVideoExtensions).Concat(_allowedAudioExtensions);
    
    private static readonly (string PreferredPrefix, string[] LegacyPrefixes)[] _pathAliases =
    [
        ("user/avatars/", ["avatars/", "user/avatar/"]),
        ("user/badges/", ["badges/"]),
        ("post/attachments/", ["post/attachment/"]),
        ("game-server/logos/", ["game-server/"]),
        ("game-server/banners/", ["game-server/"]),
        ("ads/advertiser/avatars/", ["avatars/", "user/avatars/"])
    ];

    public FileService()
    {
        _publicPath = Path.Combine(AppContext.BaseDirectory, "Public");
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folder)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var mimeType = file.ContentType.ToLowerInvariant();

        var isImage = _allowedImageExtensions.Contains(extension) && _allowedImageMimeTypes.Contains(mimeType);
        var isVideo = _allowedVideoExtensions.Contains(extension) && _allowedVideoMimeTypes.Contains(mimeType);
        var isAudio = _allowedAudioExtensions.Contains(extension) && _allowedAudioMimeTypes.Contains(mimeType);

        if (!isImage && !isVideo && !isAudio)
            throw new InvalidOperationException($"[Error] -> File type '{extension}' is not allowed.");

        var uploadPath = Path.Combine(_publicPath, folder);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var uniqueName = $"{Guid.NewGuid()}";

        if (isVideo || isAudio)
        {
            var filePath = Path.Combine(uploadPath, uniqueName + extension);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folder, uniqueName + extension).Replace("\\", "/");
        }
        
        var originalPath = Path.Combine(uploadPath, uniqueName + extension);

        // save original file
        await using (var stream = new FileStream(originalPath, FileMode.Create))
            await file.CopyToAsync(stream);

        // convert to webp if not already
        if (extension == ".webp") return Path.Combine(folder, uniqueName + ".webp").Replace("\\", "/");
        var webpPath = Path.Combine(uploadPath, uniqueName + ".webp");

        await using (var inputStream = System.IO.File.OpenRead(originalPath))
        using (var bitmap = SKBitmap.Decode(inputStream))
        await using (var outputStream = new FileStream(webpPath, FileMode.Create))
        {
            bitmap.Encode(outputStream, SKEncodedImageFormat.Webp, 85);
        }

        System.IO.File.Delete(originalPath);
            
        GC.Collect();
        GC.WaitForPendingFinalizers();

        return Path.Combine(folder, uniqueName + ".webp").Replace("\\", "/");
    }

    private static readonly string[] _protectedFolders =
    [
        "user/avatars/DefaultAvatar",
        "avatar/defaults",
        "banner/defaults"
    ];
    public Task DeleteFileAsync(string relativePath)
    {
        var normalizedPath = relativePath.Replace("\\", "/").TrimStart('/');

        if (_protectedFolders.Any(p => normalizedPath.Equals(p, StringComparison.OrdinalIgnoreCase) ||
                                       normalizedPath.StartsWith(p + "/", StringComparison.OrdinalIgnoreCase)))
            return Task.CompletedTask;
        
        var fullPath = Path.Combine(_publicPath, relativePath);

        if (!System.IO.File.Exists(fullPath))
            throw new FileNotFoundException($"[Error] -> File '{relativePath}' not found.");
        
        System.IO.File.Delete(fullPath);
        return Task.CompletedTask;
    }

    public async Task<string> ReplaceFileAsync(IFormFile file, string directory, string? oldPath)
    {
        if (!string.IsNullOrWhiteSpace(oldPath))
        {
            try
            {
                await DeleteFileAsync(BuildRelativePath(directory, oldPath));
            }
            catch (FileNotFoundException) {}
        }

        var newPath = await UploadFileAsync(file, directory);

        return newPath;
    }

    public bool Exists(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return false;
        
        var normalizedPath = NormalizeRelativePath(relativePath);
        var fullPath = Path.Combine(_publicPath, normalizedPath);

        if (System.IO.File.Exists(fullPath))
            return true;

        return ExistsWithAllowedExtensions(normalizedPath);
    }

    public string? GetValidFilePath(string? path)
    {
        if (string.IsNullOrWhiteSpace(path)) return null;
        if (path.StartsWith("http://") || path.StartsWith("https://")) return path;

        try
        {
            var normalizedPath = path.Replace("\\", "/").TrimStart('/');

            foreach (var candidatePath in GetCandidatePaths(normalizedPath))
            {
                if (Exists(candidatePath))
                    return candidatePath;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    private bool ExistsWithAllowedExtensions(string relativePath)
    {
        var directory = Path.GetDirectoryName(relativePath)?.Replace("\\", "/") ?? string.Empty;
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(relativePath);

        if (string.IsNullOrWhiteSpace(fileNameWithoutExtension))
            return false;

        foreach (var extension in AllowedExtensions)
        {
            var candidatePath = string.IsNullOrWhiteSpace(directory)
                ? fileNameWithoutExtension + extension
                : Path.Combine(directory, fileNameWithoutExtension + extension).Replace("\\", "/");

            if (System.IO.File.Exists(Path.Combine(_publicPath, candidatePath)))
                return true;
        }

        return false;
    }

    private static string NormalizeRelativePath(string relativePath)
    {
        return relativePath.Replace("\\", "/").TrimStart('/');
    }

    private static string BuildRelativePath(string directory, string fileNameOrPath)
    {
        var normalizedPath = NormalizeRelativePath(fileNameOrPath);

        if (normalizedPath.Contains('/'))
            return normalizedPath;

        return $"{NormalizeRelativePath(directory)}/{normalizedPath}";
    }

    private static IEnumerable<string> GetCandidatePaths(string relativePath)
    {
        var candidatePaths = new List<string> { relativePath };

        foreach (var (preferredPrefix, legacyPrefixes) in _pathAliases)
        {
            if (relativePath.StartsWith(preferredPrefix, StringComparison.OrdinalIgnoreCase))
            {
                var suffix = relativePath[preferredPrefix.Length..].TrimStart('/');

                foreach (var legacyPrefix in legacyPrefixes)
                    candidatePaths.Add(legacyPrefix + suffix);

                continue;
            }

            foreach (var legacyPrefix in legacyPrefixes)
            {
                if (!relativePath.StartsWith(legacyPrefix, StringComparison.OrdinalIgnoreCase))
                    continue;

                var suffix = relativePath[legacyPrefix.Length..].TrimStart('/');

                candidatePaths.Add(preferredPrefix + suffix);

                foreach (var alternateLegacyPrefix in legacyPrefixes)
                {
                    if (!string.Equals(alternateLegacyPrefix, legacyPrefix, StringComparison.OrdinalIgnoreCase))
                        candidatePaths.Add(alternateLegacyPrefix + suffix);
                }
            }
        }

        return candidatePaths.Distinct(StringComparer.OrdinalIgnoreCase);
    }
}