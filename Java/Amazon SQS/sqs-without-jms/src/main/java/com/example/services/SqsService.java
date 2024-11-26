package com.example.services;

import jakarta.annotation.PostConstruct;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;
import software.amazon.awssdk.services.sqs.SqsClient;
import software.amazon.awssdk.services.sqs.model.*;

import java.util.List;

@Service
public class SqsService {
    private final SqsClient sqsClient;
    private final String queueUrl = "https://sqs.sa-east-1.amazonaws.com/939645320583/test-queue";

    public SqsService(SqsClient sqsClient) {
        this.sqsClient = sqsClient;
    }

    @PostConstruct
    public void init() {
        System.out.println("SQS Listener Service started...");
    }

    @Scheduled(fixedRate = 5000)
    public void pollMessages() {
        try {
            ReceiveMessageRequest receiveMessageRequest = ReceiveMessageRequest.builder()
                    .queueUrl(queueUrl)
                    .maxNumberOfMessages(5)
                    .waitTimeSeconds(10)
                    .build();

            List<Message> messages = sqsClient.receiveMessage(receiveMessageRequest).messages();

            for (Message message : messages) {
                System.out.println("Received message: " + message.body());

                DeleteMessageRequest deleteRequest = DeleteMessageRequest.builder()
                        .queueUrl(queueUrl)
                        .receiptHandle(message.receiptHandle())
                        .build();

                sqsClient.deleteMessage(deleteRequest);
            }

        } catch (SqsException e) {
            System.err.println("Error polling messages: " + e.getMessage());
        }
    }
}