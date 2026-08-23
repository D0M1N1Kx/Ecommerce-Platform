pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build Docker Image') {
            steps {
                // Létrehozzuk a Docker képet az API-hoz
                sh 'docker build -t ecommerce-api:latest .'
            }
        }

        stage('Deploy Container') {
            steps {
                sh 'docker stop ecommerce-api || true'
                sh 'docker rm ecommerce-api || true'

                withCredentials([
                        string(credentialsId: 'DB_CONNECTION_STRING', variable: 'DB_CONN'),
                        string(credentialsId: 'JWT_SECRET_KEY', variable: 'JWT_SECRET')
                    ]) {
                    sh """
            docker run -d --name ecommerce-api --network app-net -p 8081:8080 -e ASPNETCORE_ENVIRONMENT="Development" -e ConnectionStrings__DefaultConnection="${DB_CONN}" -e JwtSettings__SecretKey="${JWT_SECRET}" -e JwtSettings__Issuer="EcommerceAPI" -e JwtSettings__Audience="EcommerceFrontend" ecommerce-api:latest
            """
                }
            }
        }
    }
}