package com.example;

import com.example.services.SsmService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class SystemsManagerApplication {

    private final SsmService ssmService;

    public static void main(String[] args) {
        SpringApplication.run(SystemsManagerApplication.class, args);
    }

    public SystemsManagerApplication(SsmService ssmService) {
        this.ssmService = ssmService;
    }

    @Bean
    public CommandLineRunner commandLineRunner() {
        return runner -> {
            String parameterValue = this.ssmService.getParameter("MyCustomParameter");
            System.out.println(parameterValue);

        };
    }

}
