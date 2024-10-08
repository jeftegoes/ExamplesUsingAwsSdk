package com.example;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.ZoneId;
import java.util.UUID;

import com.example.entities.Book;

import software.amazon.awssdk.enhanced.dynamodb.DynamoDbEnhancedClient;
import software.amazon.awssdk.enhanced.dynamodb.DynamoDbTable;
import software.amazon.awssdk.enhanced.dynamodb.TableSchema;
import software.amazon.awssdk.regions.Region;
import software.amazon.awssdk.services.dynamodb.DynamoDbClient;

public class App {
    public static void main(String[] args) {
        DynamoDbClient dynamoDbClient = DynamoDbClient.builder().region(Region.SA_EAST_1).build();
        DynamoDbEnhancedClient dynamoDbEnhancedClient = DynamoDbEnhancedClient.builder().dynamoDbClient(dynamoDbClient).build();
        DynamoDbTable<Book> bookTable = dynamoDbEnhancedClient.table("Book", TableSchema.fromBean(Book.class));

        LocalTime fixedTime = LocalTime.of(20, 0, 0);

        LocalDate variableDay = LocalDate.now();

        LocalDateTime dateTime = LocalDateTime.of(variableDay, fixedTime);

        System.out.println("Date time: " + dateTime);

        ZoneId zoneId = ZoneId.systemDefault();
        long epochSecond = dateTime.atZone(zoneId).toEpochSecond();

        System.out.println("Epoch time: " + epochSecond);

        Book book = new Book(UUID.randomUUID().toString(), "Bible", 5, "God", 49.90f, epochSecond);

        bookTable.putItem(book);

        System.out.println("Book inserted!");
    }
}
