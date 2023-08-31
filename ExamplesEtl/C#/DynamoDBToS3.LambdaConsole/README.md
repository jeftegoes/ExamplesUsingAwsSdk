- Lambda handler:
  - Composition: `AssemblyName` + `Namespace.ClassName` + `Method`
    - `DynamoDBToS3.LambdaConsole::DynamoDBToS3::FunctionHandler`
- `dotnet lambda package .\DynamoDBToS3.LambdaConsole.csproj -o DynamoDBToS3.LambdaConsole.zip`
- To test:
  ```
  {
    "AnyoneParameter": 10
  }
  ```
