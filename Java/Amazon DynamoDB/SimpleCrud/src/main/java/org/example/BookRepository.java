package org.example;

import com.amazonaws.services.dynamodbv2.AmazonDynamoDB;
import com.amazonaws.services.dynamodbv2.AmazonDynamoDBClientBuilder;
import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBMapper;
import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBScanExpression;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.util.List;

public class BookRepository {
    private static final ObjectMapper objectMapper = new ObjectMapper();
    private static final AmazonDynamoDB client = AmazonDynamoDBClientBuilder.standard().build();
    private static final DynamoDBMapper dynamoDBMapper = new DynamoDBMapper(client);

    public void create(Book book) {
        dynamoDBMapper.save(book);
    }

    public Book getById(String id) {
        Book book = dynamoDBMapper.load(Book.class, id);
        return book;
    }

    public List<Book> get() {
        List<Book> books = dynamoDBMapper.scan(Book.class, new DynamoDBScanExpression());
        return books;
    }

    public void remove(Book book) {
        dynamoDBMapper.delete(book);
    }

    public void update(String id, Book book) {
        Book bookInDb = dynamoDBMapper.load(Book.class, id);

        bookInDb.setName(book.getName());
        bookInDb.setRating(book.getRating());
        bookInDb.setAuthor(book.getAuthor());
        bookInDb.setPrice(book.getPrice());

        dynamoDBMapper.save(bookInDb);
    }
}
