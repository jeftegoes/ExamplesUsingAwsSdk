from datetime import datetime, timezone

import boto3

BOOK_1 = "Capitaes da areia"
BOOK_2 = "Dona Flor e Seus Dois Maridos"
BOOK_3 = "Gabriela, Cravo e Canela"
METRIC_NAMESPACE = "MyBooksSold"

cloudwatch = boto3.client("cloudwatch")

response = cloudwatch.put_metric_data(
    MetricData=[
        {
            "MetricName": BOOK_1,
            "Dimensions": [
                {
                    "Name": "Author",
                    "Value": "Jorge Amado"
                },
            ],
            "Timestamp": datetime.now(timezone.utc),
            "Unit": "Count",
            "Value": 1
        },
        {
            "MetricName": BOOK_3,
            "Dimensions": [
                {
                    "Name": "Author",
                    "Value": "Jorge Amado"
                },
            ],
            "Timestamp": datetime.now(timezone.utc),
            "Unit": "Count",
            "Value": 1
        },
    ],
    Namespace=METRIC_NAMESPACE
)
