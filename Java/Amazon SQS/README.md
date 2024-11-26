# 1. Points of consideration

- SQS without JMS, the control of the message, as well as the deletion, is of the application.
- But with JMS the control of the message is of JMS.

# 2. Dependencies

- To use annontations `@EnableJms` and `@JmsListener`
  ```xml
    <dependency>
        <groupId>org.springframework</groupId>
        <artifactId>spring-jms</artifactId>
        <version>6.2.0</version>
    </dependency>
  ```
- To use `TextMessage` class
  ```xml
      <dependency>
          <groupId>org.springframework</groupId>
          <artifactId>spring-jms</artifactId>
          <version>6.2.0</version>
      </dependency>
  ```
