pipeline {
  agent any

  parameters {
    choice(
            name: 'ENVIRONMENT',
            choices: ['dev', 'staging', 'prod'],
            description: 'Select environment for test run'
        )
    string(
            name: 'TEST_TAG',
            defaultValue: "Web",
            description: 'Run tests with tag'
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
        echo "Select ENV: ${params.ENVIRONMENT}"
        bat "dotnet test --filter \"Category=${params.TEST_TAG}\" --logger:\"trx;LogFileName=test-results.trx\""
      }
    }
  }

  post {
    always {
      archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
    }
    failure {
      echo 'FAILURE'
    }
    success {
      echo 'SUCCESS'
    }
  }
}