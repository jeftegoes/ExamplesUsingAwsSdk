import json
from datetime import datetime

import boto3
from botocore.exceptions import NoCredentialsError

dynamodb = boto3.client('dynamodb')
s3 = boto3.client('s3')

dynamodb_table_name = 'Candidates'

s3_bucket_name = 'candidates-bucket-manual'

def set_format_file() -> str:
    timestamp = datetime.now().strftime('%Y-%m-%d_%H-%M-%S')
    return f'data_{timestamp}.json'

def get_dynamodb_itens():
    response = dynamodb.scan(TableName=dynamodb_table_name)
    return response.get('Items', [])

def format_dynamodb_itens(items):
    formatted_items = [
        {
            "user": item['userId']['S'],
            "id": item['Id']['S'],
            "rating": int(item['Rating']['N'])
        }
        for item in items
    ]

    return formatted_items

def put_s3_object(formatted_items):
    s3.put_object(
        Bucket=s3_bucket_name,
        Key=set_format_file(),
        Body=json.dumps(formatted_items, indent=2)
    )

try:

    put_s3_object(format_dynamodb_itens(get_dynamodb_itens()))

    print("Data transfer completed successfully!")

except NoCredentialsError:
    print("AWS credentials not found. Make sure you have configured your credentials.")

except Exception as e:
    print(f"An error occurred: {str(e)}")
