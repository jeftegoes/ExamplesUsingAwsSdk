# 1. Baby steps for lambda

1.  Create role `arn:aws:iam::XXXXXXXXXXX:role/CrudBookLambdaRole`
    1. **Attention to Trust Relationship LAMBDA**
    2. `AmazonDynamoDBFullAccess`
    3. `AmazonS3FullAccess`
    4. `AWSLambdaExecute`
    5. `AWSLambdaBasicExecutionRole`
    6. `AWSXrayWriteOnlyAccess`
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
                        "AWS": "arn:aws:iam::XXXXXXXXXXX:role/CrudBookLambdaConsoleManualExecutionRole"
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
5.  Choose the type of project
6.  Create API Gateway
    1.  To upload image must be informed in `API Gateway` -> `Settings` -> `Binary Media Types` -> `multipart/form-data`

# 2. Baby steps for ecs/fargate

## 2.1. Pre-requisites

1. AWS CLI
2. Docket

## 2.2. Steps

1. **To do this we, need an AWS CLI and Docker configured!**
2. Create role `arn:aws:iam::XXXXXXXXXXX:role/CrudBookEcsRole`
   1. **Attention to Trust Relationship -> Elastic Conteiner Service (ecs-tasks.amazonaws.com)**
   2. `AmazonDynamoDBFullAccess`
   3. `AmazonAPIGatewayPushToCloudWatchLogs`
   4. `AmazonS3FullAccess`
   5. `AmazonEC2ContainerRegistryFullAccess`
   7. `AWSXrayWriteOnlyAccess`
   8. `AmazonSNSFullAccess` (optional)
3. Amazon ECR > Private registry > Repositories > Create repository
4. View push commands > Execute all!
5. Create Cluster ECS FARGATE!
6. Create Task Definition
   1. C# port 8080
   2. `CMD-SHELL,curl -f http://localhost/health?param1=1&param2=2 || exit 1`
   3. Check Public IP
7. Configure Security Group
   1. `Custom TCP	TCP	8080	0.0.0.0/0`
8. Create Cluster type ECS/FARGATE
9. Create target group such `IP addresses`
10. Create Applicaiton Load Balancer
11. Create service
12. Configure API Gateway method
