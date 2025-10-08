pipeline {
  agent any

  parameters {
    choice(
            name: 'ENVIRONMENT',
            choices: ['dev', 'staging', 'prod'],
            description: 'Выберите окружение для запуска'
        )
  }

  stages {
    stage('Clean') {
            steps {
                script {
                    deleteDir()
                }
            }
        }

    stage('Checkout') {
      steps {
        checkout scm
      }
    }

    stage('Restore') {
      steps {
        bat 'dotnet restore'
      }
    }

    stage('Build') {
      steps {
        bat 'dotnet build --configuration Release'
      }
    }

    stage('Test') {
      steps {
        echo 'Select ENV: ${parameters.ENVIRONMENT}' 
        bat 'dotnet test --no-build --configuration Release --logger:"trx;LogFileName=test-results.trx"'
      }
    }
  }

  post {
    always {
      archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
    }
    failure {
      echo 'Сборка или тесты завершились с ошибкой.'
    }
    success {
      echo 'Все стадии прошли успешно.'
    }
  }
}