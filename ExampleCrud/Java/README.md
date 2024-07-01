# 1. Lambda configuration

- Composition: `PackageName` + `ClassName` + `Method`
  - `com.insertbooks.lambda.BookHandler::saveBook`

# 2. To test directly into Lambda

- To insert method

```
	{
		"body": "{\n\t\"name\": \"Clean architecture\",\n\t\"rating\": 5,\n\t\"author\": \"Uncle BOB\",\n\t\"price\": 49.99\n}"
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
