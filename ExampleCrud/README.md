# Baby steps

1.  Create role arn:aws:iam::XXXXXXXXXXX:role/CrudProductLambdaConsoleManualExecutionRole
    1.  AmazonDynamoDBFullAccess
    2.  AmazonS3FullAccess
    3.  AWSLambdaExecute
    4.  AWSLambdaBasicExecutionRole
    5.  AWSXrayWriteOnlyAccess
2.  Create `books-images-XXXXXXXXXXX` and `front-end` buckets.
    1. Enable option Block all public access.
    2. Configure bucket policy
       ```
        {
            "Version": "2012-10-17",
            "Statement": [
                {
                    "Sid": "AllowEveryoneReadOnlyAccess",
                    "Effect": "Allow",
                    "Principal": "*",
                    "Action": "s3:GetObject",
                    "Resource": "arn:aws:s3:::books-images-XXXXXXXXXXX/*"
                },
                {
                    "Sid": "AllowLambdaAccess",
                    "Effect": "Allow",
                    "Principal": {
                        "AWS": "arn:aws:iam::XXXXXXXXXXX:role/CrudProductLambdaConsoleManualExecutionRole"
                    },
                    "Action": [
                        "s3:PutObject",
                        "s3:PutObjectAcl",
                        "s3:GetObject",
                        "s3:GetObjectVersion",
                        "s3:DeleteObject",
                        "s3:DeleteObjectVersion"
                    ],
                    "Resource": "arn:aws:s3:::books-images-XXXXXXXXXXX/*"
                }
            ]
        }
       ```
3.  Create books table into DynamoDb.
    1.  Partition key (Id)
4.  Create lambda
    1.  Adding enviroment variables
        1.  bucketName
        2.  tableName
    2.  To test directly into Lambda
        1.  Change handler name

    ```
        {
        "resource": "/product",
        "httpMethod": "POST",
        "body": "{\n\t\"name\": \"Clean architecture\",\n\t\"rating\": 5,\n\t\"author\": \"Uncle BOB\",\n\t\"price\": 49.99,\n\t\"fileName\": \"Clean architecture.jpg\"\n}"
        }
    ```

5.  Create API Gateway
    1.  To upload image must be informed in `API Gateway` -> `Settings` -> `Binary Media Types` -> `multipart/form-data`

# Packages

- pip install python-multipart
- pip install boto3
