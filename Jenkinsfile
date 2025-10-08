pipeline {
  agent any

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
