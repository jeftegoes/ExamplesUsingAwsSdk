using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using Producer.Models;

static async Task<SendMessageResponse> SendMessage(string message, string queueUrl, IAmazonSQS sqsClient)
{
    var sendMessageRequest = new SendMessageRequest();
    sendMessageRequest.QueueUrl = queueUrl;
    sendMessageRequest.MessageBody = message;
    var sendMessageResponse = await sqsClient.SendMessageAsync(sendMessageRequest);
    return sendMessageResponse;
}

static List<CustomMessage> CreateCustomMessages()
{
    var customMessages = new List<CustomMessage>();

    var length = 100;

    for (int i = 0; i < length - 1; i++)
    {
        customMessages.Add(new CustomMessage(i, $"I am message # : {i}"));
    }

    return customMessages;
}

static async Task<string> CreateQueue(IAmazonSQS sqsClient, string queueName, string visabilityTimeout)
{
    var createQueueRequest = new CreateQueueRequest();

    createQueueRequest.QueueName = queueName;

    var attrs = new Dictionary<string, string>();

    attrs.Add(QueueAttributeName.VisibilityTimeout, visabilityTimeout);
    createQueueRequest.Attributes = attrs;

    var createQueueResponse = await sqsClient.CreateQueueAsync(createQueueRequest);

    return createQueueResponse.QueueUrl;
}

Console.WriteLine("SQS Demo!");

var sqsClient = new AmazonSQSClient(Amazon.RegionEndpoint.SAEast1);
// var queueUrl = await CreateQueue(sqsClient, "DemoQueue", "10");

// Console.WriteLine("Queue created: {0}", queueUrl);
Console.WriteLine("Sending message to SQS...");

var messages = CreateCustomMessages();

foreach (var message in messages)
{
    var json = JsonConvert.SerializeObject(message);
    var sendMessageResponse = await SendMessage(json, Configuration.Constants._queueUrl, sqsClient);
    Console.WriteLine("Message {0} sent to queue. HTTP response code: {1}", message.Id, sendMessageResponse.HttpStatusCode.ToString());
}

Console.WriteLine("Finished sending messages to SQS.");