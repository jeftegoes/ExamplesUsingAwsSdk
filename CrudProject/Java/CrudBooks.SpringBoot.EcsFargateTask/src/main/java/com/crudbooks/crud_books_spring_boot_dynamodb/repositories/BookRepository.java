package com.crudbooks.crud_books_spring_boot_dynamodb.repositories;

import com.crudbooks.crud_books_spring_boot_dynamodb.models.Book;
import org.springframework.stereotype.Repository;
import software.amazon.awssdk.enhanced.dynamodb.DynamoDbEnhancedClient;
import software.amazon.awssdk.enhanced.dynamodb.DynamoDbTable;
import software.amazon.awssdk.enhanced.dynamodb.TableSchema;

import java.util.List;

@Repository
public class BookRepository {
    private final DynamoDbTable<Book> bookTable;

    public BookRepository(DynamoDbEnhancedClient dynamoDbEnhancedClient) {

        this.bookTable = dynamoDbEnhancedClient.table("Books", TableSchema.fromBean(Book.class));
    }

    public List<Book> getAll() {
        return this.bookTable.scan().items().stream().toList();
    }

    public Book getById(String id) {
        return this.bookTable.getItem(r -> r.key(k -> k.partitionValue(id)));
    }

    public void save(Book book) {
        bookTable.putItem(book);
    }

    public void delete(String id) {
        this.bookTable.deleteItem(r -> r.key(k -> k.partitionValue(id)));
    }

    public void update(String id, Book book) {
        Book bookInDb = this.bookTable.getItem(r -> r.key(k -> k.partitionValue(id)));

        bookInDb.setName(book.getName());
        bookInDb.setRating(book.getRating());
        bookInDb.setAuthor(book.getAuthor());
        bookInDb.setPrice(book.getPrice());

        bookTable.putItem(bookInDb);
    }
}
