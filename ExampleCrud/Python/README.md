# Baby steps

1.  Create role arn:aws:iam::939645320583:role/CrudProductLambdaConsoleManualExecutionRole
    1.  AmazonDynamoDBFullAccess
    2.  AmazonS3FullAccess
    3.  AWSLambdaExecute
    4.  AWSLambdaBasicExecutionRole
    5.  AWSXrayWriteOnlyAccess
2.  Create `products-images-939645320583` and `front-end` buckets.
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
                "Resource": "arn:aws:s3:::products-images-939645320583/*"
            },
            {
                "Sid": "AllowLambdaAccess",
                "Effect": "Allow",
                "Principal": {
                    "AWS": "arn:aws:iam::939645320583:role/CrudProductLambdaConsoleManualExecutionRole"
                },
                "Action": [
                    "s3:PutObject",
                    "s3:PutObjectAcl",
                    "s3:GetObject",
                    "s3:GetObjectVersion",
                    "s3:DeleteObject",
                    "s3:DeleteObjectVersion"
                ],
                "Resource": "arn:aws:s3:::products-images-939645320583/*"
            }
        ]
        }
       ```
3.  Create product table into DynamoDb.
4.  Create lambda
    1.  Adding enviroment variable
5.  Create API Gateway
    1.  To upload image must be informed in `API Gateway` -> `Settings` -> `Binary Media Types` -> `multipart/form-data`

# Packages

- pip install python-multipart
- pip install boto3
