package com.example.services;

import software.amazon.awssdk.services.s3.S3Client;
import software.amazon.awssdk.services.s3.model.Bucket;

import java.io.File;
import java.io.IOException;
import java.util.List;

public interface S3Service {
    List<Bucket> getBuckets();

    void uploadFile(String bucketName, String keyName, String filePath);

    void downloadFile(String bucketName, String keyName, String downloadPath);
    File downloadFileAsFile(String bucketName, String keyName) throws IOException;
}
