# Default import statements.
import sys
from datetime import datetime

from awsglue.context import GlueContext
from awsglue.dynamicframe import DynamicFrame
from awsglue.job import Job
from awsglue.transforms import *
from awsglue.utils import getResolvedOptions
from pyspark.context import SparkContext
from pyspark.sql.functions import col, date_format, to_date

# Initialize a Spark context and Glue context
sc = SparkContext().getOrCreate()
glue_context = GlueContext(sc)
spark = glue_context.spark_session
spark.conf.set("spark.sql.sources.partitionOverwriteMode", "dynamic")

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

print(f" ***************** DataFrame schema ***************** ")
dyf.printSchema()
print(f" ***************** Convert the Glue DynamicDataFrame to a Spark Dataframe and show data ***************** ")
dataframe = dyf.toDF()
dataframe.show()
print(f"Dataframe count items: {dyf.count()}")

created_at = "10/10/2010"

date_obj = datetime.strptime(created_at, "%d/%m/%Y")
created_at_partition = date_obj.strftime("%Y%m%d")

print(f" ***************** Dataframe filtered ***************** ")
filtered_dataframe = dataframe.filter(
    (col('createdAt') == created_at))
filtered_dataframe = filtered_dataframe.withColumn(
    'createdAtFormated', date_format(to_date(col('createdAt'), "dd/MM/yyyy"), 'yyyyMMdd'))
filtered_dataframe.show()

print(f" ***************** Write the DataFrame to S3 in CSV format ***************** ")
s3_output_path = f"s3://books-artifactory-939645320583/output-folder/createdatformated={created_at_partition}/"
filtered_dataframe.write.format("parquet").mode(
    "overwrite").save(s3_output_path)

print(f" ***************** Generate datasource ***************** ")

partitioned_data = glue_context.create_dynamic_frame.from_options(
    connection_type="s3",
    connection_options={
        "paths": [s3_output_path],
        "recurse": True
    },
    format="parquet"
)

partitioned_data.show()

print(f" ***************** Write partition ***************** ")

data_sink = glue_context.getSink(
    path=s3_output_path, connection_type="s3", partitionsKeys=["createdatformated"],
    enableUpdateCatalog=True)
data_sink.setFormat("parquet", useGlueParquetWriter=True)
data_sink.setCatalogInfo(catalogDatabase="bookslake", catalogTableName="books")

# glue_context.write_dynamic_frame.from_catalog(
#     frame=partitioned_data,
#     database="bookslake",
#     table_name="books",
#     additional_options={
#         "partitionKeys": ["createdatformated"]
#     }
# )

print(f" ***************** Convert do Dynamic DataFrame ***************** ")
# filtered_dynamic_dataframe = DynamicFrame.fromDF(
#     filtered_dataframe, glue_context, "Books")
# filtered_dynamic_dataframe.show()


data_sink.writeFrame(partitioned_data)

job.commit()
