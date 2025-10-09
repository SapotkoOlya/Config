pipeline {
  agent any
  
  parameters {
    choice(
	  name:'ENVIRONMENT',
	  choices:['dev', 'staging', 'prod'],
	  description: 'Set ENV'
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