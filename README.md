[AWS Toolkit for Visual Studio Documentation](https://docs.aws.amazon.com/aws-toolkit-visual-studio/index.html)

# Commands

- Build and deploy using the Amazon.Lambda.Tools
  - dotnet tool install -g Amazon.Lambda.Tools
- To install Amazon.Lambda.Templates
  - dotnet new -i Amazon.Lambda.Templates
- Deploy function (publish)
  - dotnet lambda deploy-function MyFunction
- Execute lambda function
  - dotnet lambda invoke-function MyFunction --payload "Just Checking If Everything is OK"
