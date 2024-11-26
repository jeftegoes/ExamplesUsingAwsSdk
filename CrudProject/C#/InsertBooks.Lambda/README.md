# 1. Lambda configuration

- Lambda handler:
  - Composition: `AssemblyName` + `Namespace.ClassName` + `Method`
    - `InsertBooks.Lambda::BookControoler::SaveBook`
- `dotnet lambda package .\InsertBooks.Lambda.sln -o Book.LambdaConsole.zip`

# 2. To test directly into Lambda

- To insert method

```
    {
        "body": "{\n\t\"Name\": \"Clean architecture\",\n\t\"Rating\": 5,\n\t\"Author\": \"Uncle BOB\",\n\t\"Price\": 49.99\n}"
    }
```

- To getById method

```
	{
		"pathParameters": {
			"bookId": "9dcd68fe-1805-42ee-824a-13d8e1b60d85"
		}
	}
```
