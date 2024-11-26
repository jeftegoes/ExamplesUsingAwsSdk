# 1. What is this project

This is a small / simple example that shows how using AWS SDK to create Custom Metrics.

| DateHour            | Book                          | Qty |
| ------------------- | ----------------------------- | --- |
| 16/11/2023 10:00:10 | Capitaes da areia             | 2   |
| 16/11/2023 10:00:20 | Gabriela, Cravo e Canela      | 1   |
| 16/11/2023 10:00:30 | Capitaes da areia             | 3   |
| 16/11/2023 10:00:40 | Dona Flor e Seus Dois Maridos | 4   |
| 16/11/2023 10:00:50 | Capitaes da areia             | 6   |
| 16/11/2023 10:01:00 | Capitaes da areia             | 3   |
| 16/11/2023 10:01:10 | Capitaes da areia             | 5   |
| 16/11/2023 10:01:20 | Gabriela, Cravo e Canela      | 3   |
| 16/11/2023 10:01:30 | Dona Flor e Seus Dois Maridos | 2   |

# 2. Project Type Console

.NET 8.0

## 2.1. Result of this project

![Amazon CloudWatch Custom Metrics](Images/Metrics.png)

# 3. Documentations

- [Amazon ECS CloudWatch metrics](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/cloudwatch-metrics.html)
- [Handle errors in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0)

# 4. Use Cases

- Scenario 1: Different metric names in the same namespace:
  ![Amazon CloudWatch](Images/Scenario1DifferentMetricNames.png)
  ![Grafana](Images/Scenario1GrafanaDifferentMetricNames.png)
- Scenario 2: Same metric names in the same namespace:
  ![Amazon CloudWatch](Images/Scenario2SameMetricNames.png)
  ![Grafana](Images/Scenario2GrafanaSameMetricNames.png)
