package com.crudbooks.crud_books_spring_boot_dynamodb.models;

import software.amazon.awssdk.enhanced.dynamodb.mapper.annotations.DynamoDbAttribute;
import software.amazon.awssdk.enhanced.dynamodb.mapper.annotations.DynamoDbBean;
import software.amazon.awssdk.enhanced.dynamodb.mapper.annotations.DynamoDbPartitionKey;

import java.util.List;

@DynamoDbBean
public class Book {
    private String id;
    private String name;
    private int rating;
    private String author;
    private float price;
    private List<Publisher> publisher;

    @DynamoDbPartitionKey
    @DynamoDbAttribute("Id")
    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    @DynamoDbAttribute("Name")
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @DynamoDbAttribute("Rating")
    public int getRating() {
        return rating;
    }

    public void setRating(int rating) {
        this.rating = rating;
    }

    @DynamoDbAttribute("Author")
    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    @DynamoDbAttribute("Price")
    public float getPrice() {
        return price;
    }

    public void setPrice(float price) {
        this.price = price;
    }

    @DynamoDbAttribute("Publisher")
    public List<Publisher> getPublisher() {
        return publisher;
    }

    public void setPublisher(List<Publisher> publisher) {
        this.publisher = publisher;
    }
}
