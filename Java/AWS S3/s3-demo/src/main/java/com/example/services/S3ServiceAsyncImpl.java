package com.example.services;

import org.springframework.stereotype.Service;
import software.amazon.awssdk.core.async.AsyncRequestBody;
import software.amazon.awssdk.core.async.AsyncResponseTransformer;
import software.amazon.awssdk.core.sync.ResponseTransformer;
import software.amazon.awssdk.services.s3.S3AsyncClient;
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
public class S3ServiceAsyncImpl implements S3Service {
    private final S3AsyncClient s3AsyncClient;

    public S3ServiceAsyncImpl(S3AsyncClient s3AsyncClient) {
        this.s3AsyncClient = s3AsyncClient;
    }

    public List<Bucket> getBuckets() {
        ListBucketsResponse response = s3AsyncClient.listBuckets().join();

        return response.buckets();
    }

    public void uploadFile(String bucketName, String keyName, String filePath) {
        PutObjectRequest putObjectRequest = PutObjectRequest.builder()
                .bucket(bucketName)
                .key(keyName)
                .build();

        AsyncRequestBody requestBody = AsyncRequestBody.fromFile(Paths.get(filePath));

        this.s3AsyncClient.putObject(putObjectRequest, requestBody).join();
    }

    public void downloadFile(String bucketName, String keyName, String downloadPath) {
        GetObjectRequest getObjectRequest = GetObjectRequest.builder()
                .bucket(bucketName)
                .key(keyName)
                .build();

        AsyncResponseTransformer responseTransformer = AsyncResponseTransformer.toFile(Paths.get(downloadPath));

        this.s3AsyncClient.getObject(getObjectRequest, responseTransformer).join();
    }

    public File downloadFileAsFile(String bucketName, String keyName) throws IOException {
        File tempFile = Files.createTempFile("s3-download-", "-" + keyName).toFile();

        GetObjectRequest getObjectRequest = GetObjectRequest.builder()
                .bucket(bucketName)
                .key(keyName)
                .build();

        this.s3AsyncClient.getObject(getObjectRequest, AsyncResponseTransformer.toFile(tempFile.toPath())).join();

        return tempFile;
    }

    @Override
    public File downloadFileAsFileAndBytes(String bucketName, String keyName) throws IOException {
        return null;
    }
}
