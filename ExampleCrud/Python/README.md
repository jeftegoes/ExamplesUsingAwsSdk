# 1. To test directly into Lambda

    ```
        {
        "resource": "/product",
        "httpMethod": "POST",
        "body": "{\n\t\"name\": \"Clean architecture\",\n\t\"rating\": 5,\n\t\"author\": \"Uncle BOB\",\n\t\"price\": 49.99,\n\t\"fileName\": \"Clean architecture.jpg\"\n}"
        }
    ```

# 2. Packages

- pip install python-multipart
- pip install boto3
