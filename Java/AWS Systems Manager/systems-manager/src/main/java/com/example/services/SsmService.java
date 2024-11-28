package com.example.services;

import org.springframework.stereotype.Service;
import software.amazon.awssdk.services.ssm.SsmClient;
import software.amazon.awssdk.services.ssm.model.GetParameterRequest;
import software.amazon.awssdk.services.ssm.model.GetParameterResponse;

@Service
public class SsmService {
    private final SsmClient ssmClient;

    public SsmService(SsmClient ssmClient) {
        this.ssmClient = ssmClient;
    }

    public String getParameter(String parameterName) {
        GetParameterRequest request = GetParameterRequest.builder()
                .name(parameterName)
                .withDecryption(true)
                .build();

        GetParameterResponse response = ssmClient.getParameter(request);

        return response.parameter().value();
    }
}
