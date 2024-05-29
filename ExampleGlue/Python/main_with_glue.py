# Default import statements.
import sys

from awsglue.context import GlueContext
from awsglue.job import Job
from awsglue.transforms import *
from awsglue.utils import getResolvedOptions
from pyspark.context import SparkContext
from pyspark.sql.functions import col, date_format, to_date

# Initialize a Spark context and Glue context
sc = SparkContext().getOrCreate()
glue_context = GlueContext(sc)
spark = glue_context.spark_session
job = Job(glue_context)

dynamodb_table_name = "Books"

print(f" ***************** Read data from DynamoDB using Glue ***************** ")
dyf = glue_context.create_dynamic_frame.from_options(
    connection_type="dynamodb",
    connection_options={
        "dynamodb.input.tableName": dynamodb_table_name,
        "dynamodb.region": "sa-east-1",
        "dynamodb.throughput.read.percent": "1.0",
        "dynamodb.splits": "1"
    }
)

print(f" ***************** Dataframe schema ***************** ")
dyf.printSchema()
print(f" ***************** Convert the Glue DynamicFrame to a Spark Dataframe and show data ***************** ")
dataframe = dyf.toDF()
dataframe.show()
print(f"Dataframe count items: {dyf.count()}")

start_date = "10/10/2010"
end_date = "10/10/2010"


print(f" ***************** Dataframe filtered ***************** ")
filtered_dataframe = dataframe.filter(
    (col('createdAt') >= start_date) & (col('createdAt') <= end_date))
filtered_dataframe = filtered_dataframe.withColumn(
    'createdAtFormated', date_format(to_date(col('createdAt'), "dd/MM/yyyy"), 'yyyyMMdd'))
filtered_dataframe.show()

print(f" ***************** Write the DataFrame to S3 in CSV format ***************** ")
s3_output_path = "s3://books-artifactory-939645320583/output-folder/"
filtered_dataframe.write.format("parquet").mode(
    "overwrite").save(s3_output_path)

job.commit()
