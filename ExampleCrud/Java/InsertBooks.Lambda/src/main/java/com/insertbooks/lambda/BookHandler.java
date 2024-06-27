package com.insertbooks.lambda;

import com.amazonaws.services.dynamodbv2.AmazonDynamoDB;
import com.amazonaws.services.dynamodbv2.AmazonDynamoDBClientBuilder;
import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBMapper;
import com.amazonaws.services.lambda.runtime.Context;
import com.amazonaws.services.lambda.runtime.events.APIGatewayProxyRequestEvent;
import com.amazonaws.services.lambda.runtime.events.APIGatewayProxyResponseEvent;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.JsonObject;

public class BookHandler {

    private static final ObjectMapper objectMapper = new ObjectMapper();
    private static final AmazonDynamoDB client = AmazonDynamoDBClientBuilder.standard().build();
    private static final DynamoDBMapper dynamoDBMapper = new DynamoDBMapper(client);

    public APIGatewayProxyResponseEvent saveBook(APIGatewayProxyRequestEvent request, Context context) {
        Book book = new Book();

        try {
            book = objectMapper.readValue(request.getBody(), Book.class);
        } catch (Exception e) {
            e.printStackTrace();
        }

        dynamoDBMapper.save(book);

        JsonObject returnValue = new JsonObject();
        returnValue.addProperty("Message", "Book saved successfully!");

        APIGatewayProxyResponseEvent response = new APIGatewayProxyResponseEvent();
        response.withStatusCode(200).withBody(returnValue.toString());

        return response;
    }
}
