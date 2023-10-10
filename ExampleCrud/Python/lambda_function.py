import base64
import json
import logging
import os
import uuid

import boto3

logger = logging.getLogger()
logger.setLevel(logging.INFO)
region: str = os.environ.get("AWS_REGION")
bucket_name: str = os.environ.get("bucketName")
table_name: str = os.environ.get("tableName")


def handler(event, context):
    body = json.loads(event['body'])

    s3_client = boto3.client('s3')
    dynamodb_client = boto3.resource("dynamodb", region_name=region)
    table = dynamodb_client.Table(table_name)
    file_name = body["fileName"]
    product = {
        "Id": str(uuid.uuid4()),
        "Name": body["name"],
        "Rating": str(body["rating"]),
        "Author": body["author"],
        "Price": str(body["price"]),
        "FileName": file_name
    }

    table.put_item(Item=product)

    base64_image = body.get('filebase64')
    if (base64_image):
        image_data = base64.b64decode(base64_image)
        s3_client.put_object(
            Body=image_data, Bucket=bucket_name, Key=file_name)

    response = {
        'statusCode': 200,
        'body': json.dumps(f"Product inserted successfully {product}!")
    }

    return response
