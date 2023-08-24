import cgi
import io
import json
import logging
import os
import uuid

import boto3

logger = logging.getLogger()
logger.setLevel(logging.INFO)
region: str = os.environ.get("AWS_REGION")
bucket_name: str = os.environ.get("bucketName")


def handler(event, context):
    try:
        content_type_header = event['headers']['content-type']

        body = event['body']

        form = cgi.FieldStorage(
            fp=io.BytesIO(body.encode('utf-8')),
            headers=event['headers'],
            environ={'REQUEST_METHOD': 'POST',
                     'CONTENT_TYPE': content_type_header}
        )

        s3_client = boto3.client('s3')
        dynamodb_client = boto3.resource("dynamodb", region_name=region)
        table = dynamodb_client.Table("Products")

        uploaded_image = form["filename"]

        product = {
            "Id": str(uuid.uuid4()),
            "Name": form["name"].value,
            "rating": form["rating"].value,
            "Author": form["author"].value,
            "Price": form["price"].value,
            "FileName": uploaded_image.filename
        }

        table.put_item(Item=product)

        image_content = uploaded_image.file.read()
        s3_client.put_object(
            Body=image_content, Bucket=bucket_name, Key="test12.png")

        response = {
            'statusCode': 200,
            'body': json.dumps(f"Product inserted successfully {product}!")
        }

        return response

    except Exception as e:
        logger.error(f"Error: {e}")
        response = {
            'statusCode': 500,
            'body': json.dumps(f'An error occurred: {str(e)}')
        }

        return response
