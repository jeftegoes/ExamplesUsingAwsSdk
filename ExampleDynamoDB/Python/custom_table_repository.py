import boto3
from boto3.dynamodb.conditions import Key

from custom_table import CustomTable


class CustomTableRepository:
    def __init__(self, table_name, region_name = "sa-east-1") -> None:
        self.dynamodb = boto3.resource("dynamodb", region_name)
        self.table_name: str = "CustomTable"
        self.table = self.dynamodb.Table(table_name)

    def save_item(self, custom_table: CustomTable) -> None:
        response = self.table.put_item(Item = custom_table.to_dict())
        return response

    def get_item(self, primary_key: str, secondary_key: str) -> CustomTable:
        response = self.table.get_item(Key={"Id": primary_key, "Description": secondary_key})
        item = response.get("Item")
        if item:
            return CustomTable(item['Id'], item['Description'])
        else:
            return None

    def update_item(self, primary_key: str, secondary_key: str, update_expression, expression_values):
        response = self.table.update_item(
            Key={"Id": primary_key, "Description": secondary_key},
            UpdateExpression=update_expression,
            ExpressionAttributeValues=expression_values,
            ReturnValues="ALL_NEW"
        )
        updated_item = response.get('Attributes', {})
        return CustomTable(updated_item['Id'], updated_item['Description'])


    def delete_item(self, primary_key: str, secondary_key: str) -> None:
        response = self.table.delete_item(Key={"Id": primary_key, "Description": secondary_key})
        return response
    
    def get_itens_by_pk(self, primary_key: str) -> list[CustomTable]:
        response = self.table.query(KeyConditionExpression = Key('Id').eq(primary_key))
        items = response.get('Items', [])
        return [CustomTable(item['Id'], item['Description']) for item in items]
