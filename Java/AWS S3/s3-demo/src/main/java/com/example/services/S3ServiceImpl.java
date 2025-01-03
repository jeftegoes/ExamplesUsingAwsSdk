package com.example.services;

import org.springframework.stereotype.Service;
import software.amazon.awssdk.core.sync.RequestBody;
import software.amazon.awssdk.core.sync.ResponseTransformer;
import software.amazon.awssdk.services.s3.S3Client;
import software.amazon.awssdk.services.s3.model.Bucket;
import software.amazon.awssdk.services.s3.model.GetObjectRequest;
import software.amazon.awssdk.services.s3.model.ListBucketsResponse;
import software.amazon.awssdk.services.s3.model.PutObjectRequest;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.List;

@Service
public class S3ServiceImpl implements S3Service {
    private final S3Client s3Client;

    public S3ServiceImpl(S3Client s3Client) {
        this.s3Client = s3Client;
    }

    public List<Bucket> getBuckets() {
        ListBucketsResponse response = s3Client.listBuckets();

        return response.buckets();
    }

    public void uploadFile(String bucketName, String keyName, String filePath) {
        PutObjectRequest putObjectRequest = PutObjectRequest.builder()
                .bucket(bucketName)
                .key(keyName)
                .build();

        RequestBody requestBody = RequestBody.fromFile(Paths.get(filePath));

        this.s3Client.putObject(putObjectRequest, requestBody);
    }

    public void downloadFile(String bucketName, String keyName, String downloadPath) {
        GetObjectRequest getObjectRequest = GetObjectRequest.builder()
                .bucket(bucketName)
                .key(keyName)
                .build();

        ResponseTransformer responseTransformer = ResponseTransformer.toFile(Paths.get(downloadPath));

        this.s3Client.getObject(getObjectRequest, responseTransformer);
    }

    public File downloadFileAsFile(String bucketName, String keyName) throws IOException {
        File tempFile = Files.createTempFile("s3-download-", "-" + keyName).toFile();

        GetObjectRequest getObjectRequest = GetObjectRequest.builder()
                .bucket(bucketName)
                .key(keyName)
                .build();

        this.s3Client.getObject(getObjectRequest, ResponseTransformer.toFile(tempFile.toPath()));

        return tempFile;
    }
}
