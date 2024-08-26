package com.crudbooks.crud_books_spring_boot_dynamodb.requests;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.List;

public class BookRequest {
    @JsonProperty("Id")
    private String id;

    @JsonProperty("Name")
    private String name;

    @JsonProperty("Rating")
    private int rating;

    @JsonProperty("Author")
    private String author;

    @JsonProperty("Price")
    private float price;

    @JsonProperty("Publisher")
    private List<PublisherRequest> publisher;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getRating() {
        return rating;
    }

    public void setRating(int rating) {
        this.rating = rating;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public float getPrice() {
        return price;
    }

    public void setPrice(float price) {
        this.price = price;
    }

    public List<PublisherRequest> getPublisher() {
        return publisher;
    }

    public void setPublisher(List<PublisherRequest> publisher) {
        this.publisher = publisher;
    }
}
