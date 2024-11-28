package com.example;

import com.example.models.SecretData;
import com.example.services.SecretsManagerService;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

import java.util.Map;

@SpringBootApplication
public class SecretsManagerApplication {
    private final SecretsManagerService secretsManagerService;
    private final ObjectMapper objectMapper;

    public static void main(String[] args) {
        SpringApplication.run(SecretsManagerApplication.class, args);
    }

    @Autowired
    public SecretsManagerApplication(SecretsManagerService secretsManagerService, ObjectMapper objectMapper) {
        this.secretsManagerService = secretsManagerService;
        this.objectMapper = objectMapper;
    }

    @Bean
    public CommandLineRunner commandLineRunner() {
        return runner -> {
            String myCustomSecret = this.secretsManagerService.getSecret("MyCustomSecret");

            SecretData secretData = objectMapper.readValue(myCustomSecret, SecretData.class);

            System.out.println(secretData);
        };
    }
}
