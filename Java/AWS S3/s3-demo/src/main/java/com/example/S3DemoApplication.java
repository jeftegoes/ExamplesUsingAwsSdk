package com.example;

import com.example.services.S3Service;
import com.example.services.S3ServiceAsyncImpl;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import software.amazon.awssdk.services.s3.model.Bucket;

import java.io.File;
import java.io.FileWriter;
import java.util.List;

@SpringBootApplication
public class S3DemoApplication {
    private final S3Service s3Service;

    public S3DemoApplication(S3ServiceAsyncImpl s3Service) {
        this.s3Service = s3Service;
    }

    public static void main(String[] args) {
        SpringApplication.run(S3DemoApplication.class, args);
    }

    @Bean
    public CommandLineRunner commandLineRunner() {
        return runner -> {
            String bucketName = "books-artifactory-939645320583";
            String customFileName = "my-custom-name-file.txt";

            List<Bucket> buckets = this.s3Service.getBuckets();
            System.out.println("List of buckets...");
            buckets.forEach(bucket -> {
                String name = bucket.name();
                System.out.println(name);
            });

            System.out.println("Upload file to bucket...");
            this.s3Service.uploadFile(bucketName, customFileName, "my-file.txt");

            File custtomFile = new File(customFileName);
            custtomFile.delete();

            System.out.println("Download file to bucket...");
            this.s3Service.downloadFile(bucketName, customFileName, customFileName);
        };
    }
}
