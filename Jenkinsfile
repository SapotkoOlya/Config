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

    stage('Publish Test Results') {
    steps {
        // Преобразование TRX → JUnit XML (например, с помощью trx2junit)
        bat 'trx2junit test_results.trx'

        // Публикация отчёта
        junit 'test_results.xml'
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
