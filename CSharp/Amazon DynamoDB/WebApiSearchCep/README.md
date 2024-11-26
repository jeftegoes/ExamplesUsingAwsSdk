# Commands to test

- Synchronous Call

  - `aws lambda invoke --function-name ExampleAWSWebApiSearchCep --cli-binary-format raw-in-base64-out --payload '{ "path": "api/address/44077200", "httpMethod": "GET" }' response.json`

- Asynchronous Call

  - `aws lambda invoke --function-name ExampleAWSWebApiSearchCep --cli-binary-format raw-in-base64-out --payload '{ "path": "api/address/44077200", "httpMethod": "GET" }' --invocation-type Event --region sa-east-1 response.json`

- Request Payload for Lambda Function (AWS Console)

```
{
  "path": "api/address/44077200",
  "httpMethod": "GET"
}
```

# Points of attention

- In file LambdaEntryPoint.cs
- This class extends from APIGatewayProxyFunction which contains the method FunctionHandlerAsync which is the actual Lambda function entry point. The Lambda handler field should be set to `ExampleAWSWebApiSearchCep::ExampleAWSWebApiSearchCep.LambdaEntryPoint::FunctionHandlerAsync`

- `<PublishReadyToRun>false</PublishReadyToRun>`
  - To compile this property with true, use this specifc command:
    - dotnet publish --output `<output_path>` --configuration "Release" --framework "net6.0" /p:GenerateRuntimeConfigurationFiles=true --runtime linux-x64 --self-contained false 

```

```
