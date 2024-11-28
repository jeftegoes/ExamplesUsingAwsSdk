package com.example.models;

import com.fasterxml.jackson.annotation.JsonProperty;

public class SecretData {
    @JsonProperty("MyFirstSecretKey")
    private String myFirstSecretKey;

    @JsonProperty("MySecondSecretKey")
    private String mySecondSecretKey;

    public SecretData() {
    }

    public SecretData(String myFirstSecretKey, String mySecondSecretKey) {
        this.myFirstSecretKey = myFirstSecretKey;
        this.mySecondSecretKey = mySecondSecretKey;
    }

    public String getMyFirstSecretKey() {
        return myFirstSecretKey;
    }

    public void setMyFirstSecretKey(String myFirstSecretKey) {
        this.myFirstSecretKey = myFirstSecretKey;
    }

    public String getMySecondSecretKey() {
        return mySecondSecretKey;
    }

    public void setMySecondSecretKey(String mySecondSecretKey) {
        this.mySecondSecretKey = mySecondSecretKey;
    }

    @Override
    public String toString() {
        return "SecretData{" +
                "myFirstSecretKey='" + myFirstSecretKey + '\'' +
                ", mySecondSecretKey='" + mySecondSecretKey + '\'' +
                '}';
    }
}
