FROM openjdk:22-jdk
ARG JAR_FILE=target/*.jar
COPY ./target/crud-books-spring-boot-dynamodb-0.0.1-SNAPSHOT.jar app.jar
ENTRYPOINT ["java", "-jar", "/app.jar"]
EXPOSE 8080