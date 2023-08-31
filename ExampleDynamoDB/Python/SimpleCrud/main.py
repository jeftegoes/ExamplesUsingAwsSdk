from custom_table import CustomTable
from custom_table_repository import CustomTableRepository

custom_table1 = CustomTable("1", "Test 1")
custom_table2 = CustomTable("1", "Test 2")

custom_table_repository = CustomTableRepository("CustomTable")
custom_table_repository.delete_item("1", "Test 1")
custom_table_repository.delete_item("1", "Test 2")

print(f"1: save_item response: {custom_table_repository.save_item(custom_table1)}")
print(f"2: save_item response: {custom_table_repository.save_item(custom_table2)}")

retrieved_item = custom_table_repository.get_item("1", "Test 1")
if retrieved_item == None:
    print("Not found.")
else:
    print("3: get_item response:", retrieved_item.__dict__)

queried_items = custom_table_repository.get_items_by_pk("1")
print("4: get_items_by_pk response:", [item.__dict__ for item in queried_items])

update_expression = "set NewColumn = :val"
expression_values = {':val': 'Test 3'}
updated_item = custom_table_repository.update_item("1", "Test 1", update_expression, expression_values)
print("Updated Item:", updated_item.__dict__)