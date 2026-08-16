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
                // Leállítjuk a régi konténert ha fut, és elindítjuk az újat az app-net hálózaton
                sh 'docker stop ecommerce-api || true'
                sh 'docker rm ecommerce-api || true'
                sh 'docker run -d --name ecommerce-api --network app-net -e ConnectionStrings__DefaultConnection="Host=ecommerce-db;Database=ecommercedb;Username=postgres;Password=secretpassword" ecommerce-api:latest'
            }
        }
    }
}