# 1. Toolkit

[AWS Toolkit for Visual Studio Documentation](https://docs.aws.amazon.com/aws-toolkit-visual-studio/index.html)

# 2. Dotnet commands

- Install dotnet lambda global tools, this is necessary to build and deploy using the Amazon.Lambda.Tools
  - dotnet tool install -g Amazon.Lambda.Tools
- If already have it installed, upgrade using the following command
  - dotnet tool update -g Amazon.Lambda.Tools
- To install Amazon Template for Dotnet Amazon.Lambda.Templates
  - dotnet new -i Amazon.Lambda.Templates
- Deploy function (publish)
  - dotnet lambda deploy-function `<name_function>`
- Execute lambda function
  - dotnet lambda invoke-function `<name_function>` --payload "Just Checking If Everything is OK"
- Deploy function (publish) locally
  - dotnet lambda package `<.\sln_file_name.sln>` -o `<output_file_name.zip>`
