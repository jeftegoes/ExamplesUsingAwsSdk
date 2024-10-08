package com.insertbooks.lambda;

import com.amazonaws.services.dynamodbv2.AmazonDynamoDB;
import com.amazonaws.services.dynamodbv2.AmazonDynamoDBClientBuilder;
import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBMapper;
import com.amazonaws.services.lambda.runtime.Context;
import com.amazonaws.services.lambda.runtime.events.APIGatewayProxyRequestEvent;
import com.amazonaws.services.lambda.runtime.events.APIGatewayProxyResponseEvent;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.util.Map;

public class BookHandler {
    private static final AmazonDynamoDB client = AmazonDynamoDBClientBuilder.standard().build();
    private static final DynamoDBMapper dynamoDBMapper = new DynamoDBMapper(client);
    private static Logger logger = LogManager.getLogger(BookHandler.class);

    public APIGatewayProxyResponseEvent saveBook(APIGatewayProxyRequestEvent request, Context context) {
        Gson gson = new Gson();
        Book book = gson.fromJson(request.getBody(), Book.class);

        dynamoDBMapper.save(book);

        JsonObject returnValue = new JsonObject();
        returnValue.addProperty("Message", "Book saved successfully!");

        APIGatewayProxyResponseEvent response = new APIGatewayProxyResponseEvent();
        response.withStatusCode(200).withBody(returnValue.toString());

        String json = gson.toJson(book);
        logger.info(json);

        return response;
    }

    public APIGatewayProxyResponseEvent getBookById(APIGatewayProxyRequestEvent request, Context context) {
        Map<String, String> pathParameters = request.getPathParameters();
        String bookId = pathParameters.get("bookId");

        Book book = dynamoDBMapper.load(Book.class, bookId);

        APIGatewayProxyResponseEvent response = new APIGatewayProxyResponseEvent();

        if (book == null)
            return response.withStatusCode(500).withBody("Book not found!");

        Gson gson = new Gson();
        String json = gson.toJson(book);

        logger.info(json);

        return response.withStatusCode(200).withBody(json);
    }
}
