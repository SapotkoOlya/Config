pipeline {
  agent any

  stages {
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
        // �������, ��� ���������� ������ "Publish NUnit test result report"
        nunit testResultsPattern: '**/*.trx'
      }
    }
  }

  post {
    always {
      archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
    }
    failure {
      echo '������ ��� ����� ����������� � �������.'
    }
    success {
      echo '��� ������ ������ �������.'
    }
  }
}
