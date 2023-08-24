# Baby steps

1.  Create role arn:aws:iam::939645320583:role/SaveCandidateLambdaConsoleManualExecutionRole
    1.  To acess cloudwatch, s3, dynamodb
2.  Create `product` and `front-end` buckets.
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
                   "Resource": "arn:aws:s3:::candidates-bucket-manual/*"
               },
               {
                   "Sid": "AllowLambdaAccess",
                   "Effect": "Allow",
                   "Principal": {
                       "AWS": "arn:aws:iam::939645320583:role/SaveCandidateLambdaConsoleManualExecutionRole"
                   },
                   "Action": [
                       "s3:PutObject",
                       "s3:PutObjectAcl",
                       "s3:GetObject",
                       "s3:GetObjectVersion",
                       "s3:DeleteObject",
                       "s3:DeleteObjectVersion"
                   ],
                   "Resource": "arn:aws:s3:::candidates-bucket-manual/*"
               }
           ]
       }
       ```
3.  Create product table into DynamoDb.
4.  Create lambda
    1.  Adding enviroment variable

# Packages

- pip install python-multipart
- pip install boto3
