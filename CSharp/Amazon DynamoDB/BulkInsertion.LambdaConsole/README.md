- Lambda handler:
  - Composition: `AssemblyName` + `Namespace.ClassName` + `Method`
    - `BulkInsertion.LambdaConsole::BulkInsertion::FunctionHandler`
- `dotnet lambda package .\BulkInsertion.LambdaConsole.csproj -o BulkInsertion.LambdaConsole.zip`
- To test:
  ```
  {
    "NumberOfItems": 10
  }
  ```
