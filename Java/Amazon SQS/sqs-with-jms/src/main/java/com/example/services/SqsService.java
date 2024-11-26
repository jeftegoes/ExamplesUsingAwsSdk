package com.example.services;

import jakarta.annotation.PostConstruct;
import jakarta.jms.JMSException;
import jakarta.jms.TextMessage;
import org.springframework.jms.annotation.JmsListener;
import org.springframework.stereotype.Service;

@Service
public class SqsService {
    @PostConstruct
    public void init() {
        System.out.println("SQS Listener Service started...");
    }

    @JmsListener(destination = "test-queue")
    public void pollMessages(TextMessage textMessage) throws JMSException {
        System.out.println(textMessage.getText());
    }
}