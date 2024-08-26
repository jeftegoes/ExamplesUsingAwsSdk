package com.crudbooks.crud_books_spring_boot_dynamodb.requests;

import com.fasterxml.jackson.annotation.JsonProperty;

public class PublisherRequest {

    @JsonProperty("Name")
    private String name;

    @JsonProperty("ContactInfo")
    private String contactInfo;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getContactInfo() {
        return contactInfo;
    }

    public void setContactInfo(String contactInfo) {
        this.contactInfo = contactInfo;
    }
}
