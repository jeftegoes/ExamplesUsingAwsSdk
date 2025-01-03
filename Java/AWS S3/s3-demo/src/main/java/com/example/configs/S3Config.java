package com.example.configs;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import software.amazon.awssdk.auth.credentials.ProfileCredentialsProvider;
import software.amazon.awssdk.regions.Region;
import software.amazon.awssdk.services.s3.S3AsyncClient;
import software.amazon.awssdk.services.s3.S3Client;

@Configuration
public class S3Config {
    @Bean
    public S3Client getClient() {
        S3Client s3Client = S3Client.builder()
                .region(Region.SA_EAST_1)
                .credentialsProvider(ProfileCredentialsProvider.create())
                .build();

        return s3Client;
    }

    @Bean
    public S3AsyncClient getS3AsyncClient() {
        S3AsyncClient s3AsyncClient = S3AsyncClient.builder()
                .region(Region.SA_EAST_1)
                .credentialsProvider(ProfileCredentialsProvider.create())
                .build();

        return s3AsyncClient;
    }
}
