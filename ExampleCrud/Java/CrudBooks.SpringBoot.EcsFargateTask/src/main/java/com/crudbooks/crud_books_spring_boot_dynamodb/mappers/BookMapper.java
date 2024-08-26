package com.crudbooks.crud_books_spring_boot_dynamodb.mappers;

import com.crudbooks.crud_books_spring_boot_dynamodb.models.Book;
import com.crudbooks.crud_books_spring_boot_dynamodb.models.Publisher;
import com.crudbooks.crud_books_spring_boot_dynamodb.requests.BookRequest;
import com.crudbooks.crud_books_spring_boot_dynamodb.requests.PublisherRequest;
import org.mapstruct.Mapper;
import org.mapstruct.factory.Mappers;

@Mapper
public interface BookMapper {
    BookMapper INSTANCE = Mappers.getMapper(BookMapper.class);

    Book bookRequestToBook(BookRequest bookRequest);
}
