import logging
import os

import boto3
from flask import Flask

logger = logging.getLogger()
logger.setLevel(logging.INFO)
region: str = os.environ.get("AWS_REGION")
bucket_name: str = os.environ.get("bucketName")
table_name: str = os.environ.get("tableName")
s3_client = boto3.client('s3')
dynamodb_client = boto3.resource("dynamodb", region_name=region)
table = dynamodb_client.Table(table_name)

app = Flask(__name__)
@app.get("/products")

def get_products():
    print(f"Bucket name: {bucket_name}")
    print(f"Table name: {table_name}")
    
    data = table.scan()
    response = data["Items"]
    return response

@app.route("/")
def version():
    return "v1.0.0.0"


@app.get("/health")
def health():
    return "OK", 200

if __name__ == '__main__':
      app.run(debug=True, host='0.0.0.0', port=80)