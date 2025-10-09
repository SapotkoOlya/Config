pipeline {
  agent any
  
  stages {
    stage('Clean') {
	  steps {
	    steps {
		  script {
		    deleteDir()
	      }
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
        bat 'dotnet test --no-build --configuration Release --logger:"trx;LogFileName=test-result.trx"'	
      }
    }
  }
  
  post {
    always {
	  archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
	}
    failure {
      echo 'Test run is failed!'
    }
    success {
      echo 'SUCCESS!!!'
    }
  }
}