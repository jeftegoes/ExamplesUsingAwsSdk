# 1. Lambda configuration

- Lambda handler:
  - Composition: `AssemblyName` + `Namespace.ClassName` + `Method`
    - `InsertBooks.Lambda::BookControoler::SaveBook`
- `dotnet lambda package .\InsertBooks.Lambda.sln -o Book.LambdaConsole.zip`

# 2. To test directly into Lambda

```
    {
        "resource": "/product",
        "httpMethod": "POST",
        "body": "{\n\t\"Name\": \"Clean architecture\",\n\t\"Rating\": 5,\n\t\"Author\": \"Uncle BOB\",\n\t\"Price\": 49.99\n}"
    }
```
