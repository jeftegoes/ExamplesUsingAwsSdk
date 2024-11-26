using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

const string METRIC_NAMESPACE = "MyBooksSold";

const string BOOK_1 = "Capitaes da areia";
const string BOOK_2 = "Dona Flor e Seus Dois Maridos";
const string BOOK_3 = "Gabriela, Cravo e Canela";

var cloudWatchClient = new AmazonCloudWatchClient();

var utcNowMinus15 = DateTime.UtcNow.AddMinutes(-15);

var putMetricDataRequest = new PutMetricDataRequest()
{
    Namespace = METRIC_NAMESPACE,
    MetricData = new List<MetricDatum>()
    {
        new MetricDatum()
        {
            MetricName = BOOK_1,
            Value = 2,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(1),
        },
        new MetricDatum()
        {
            MetricName = BOOK_3,
            Value = 1,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(2)
        },
        new MetricDatum()
        {
            MetricName = BOOK_1,
            Value = 3,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(3)
        },
        new MetricDatum()
        {
            MetricName = BOOK_2,
            Value = 4,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(4)
        },
        new MetricDatum()
        {
            MetricName = BOOK_1,
            Value = 6,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(5)
        },
        new MetricDatum()
        {
            MetricName = BOOK_1,
            Value = 3,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(6)
        },
        new MetricDatum()
        {
            MetricName = BOOK_1,
            Value = 5,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(7)
        },
        new MetricDatum()
        {
            MetricName = BOOK_3,
            Value = 3,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(8)
        },
        new MetricDatum()
        {
            MetricName = BOOK_2,
            Value = 2,
            Unit = StandardUnit.Count,
            TimestampUtc = utcNowMinus15.AddMinutes(9)
        },
    }
};

await cloudWatchClient.PutMetricDataAsync(putMetricDataRequest);

Console.WriteLine("Metrics inserted with successfully.");