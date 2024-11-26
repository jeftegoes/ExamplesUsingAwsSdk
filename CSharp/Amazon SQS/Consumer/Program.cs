
using System.Net;
using Amazon.SQS;
using Amazon.SQS.Model;
using Configuration;
using Consumer.Models;
using Newtonsoft.Json;

static async Task DeleteMessage(string queueUrl, IAmazonSQS sqsClient, Message message)
{
    await sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
}

static async Task ReceiveMessage(string queueUrl, IAmazonSQS sqsClient)
{
    var receiveMessageRequest = new ReceiveMessageRequest()
    {
        MaxNumberOfMessages = 10,
        WaitTimeSeconds = 20,
        QueueUrl = queueUrl
    };

    var numberOfMessages = await NumberOfMessageInQueue(queueUrl, sqsClient);

    for (int i = 0; i < numberOfMessages; i++)
    {
        var receiveMessageResponse = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);

        if (receiveMessageResponse.HttpStatusCode == HttpStatusCode.OK)
        {
            var message = receiveMessageResponse.Messages[0];

            var msg = JsonConvert.DeserializeObject<CustomMessage>(message.Body);

            Console.WriteLine("***************************");
            Console.WriteLine("SQS message id: {0}", message.MessageId);
            Console.WriteLine("Message id: {0}", msg.Id);
            Console.WriteLine("Message description: {0}", msg.Description);
            Console.WriteLine("Message created on: {0}", msg.CreatedOn);
            Console.WriteLine("***************************");
            Console.WriteLine("");

            await DeleteMessage(queueUrl, sqsClient, message);
        }
    }
}

static async Task<int> NumberOfMessageInQueue(string queueUrl, IAmazonSQS sqsClient)
{
    var numberOfMessages = 0;

    var attReq = new GetQueueAttributesRequest();
    attReq.QueueUrl = queueUrl;
    attReq.AttributeNames.Add("ApproximateNumberOfMessages");

    var response = await sqsClient.GetQueueAttributesAsync(attReq);

    numberOfMessages = response.ApproximateNumberOfMessages;

    return numberOfMessages;
}

var sqsClient = new AmazonSQSClient(Amazon.RegionEndpoint.SAEast1);

await ReceiveMessage(Constants._queueUrl, sqsClient);