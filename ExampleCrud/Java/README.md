# 1. Lambda configuration

- Composition: `PackageName` + `ClassName` + `Method`
  - `com.insertbooks.lambda.BookHandler::saveBook`

# 1. To test directly into Lambda

```
  {
    "body": "{\n\t\"name\": \"Clean architecture\",\n\t\"rating\": 5,\n\t\"author\": \"Uncle BOB\",\n\t\"price\": 49.99\n}"
  }
```
