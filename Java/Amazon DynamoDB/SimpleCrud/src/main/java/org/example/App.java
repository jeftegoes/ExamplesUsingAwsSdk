package org.example;

import java.util.List;

public class App {
    public static void main(String[] args) {
        BookRepository bookRepository = new BookRepository();

        System.out.println("** Method getById(9dcd68fe-1805-42ee-824a-13d8e1b60d85) ***");
        System.out.println(bookRepository.getById("9dcd68fe-1805-42ee-824a-13d8e1b60d85"));

        System.out.println("** Method get() ***");
        List<Book> books = bookRepository.get();
        books.forEach(book -> {
            System.out.println(book);
        });

        Book book = new Book();
        book.setName("Capitães da Areia");
        book.setAuthor("Jorge Amado");
        book.setPrice(49.48);
        book.setRating(5);

        bookRepository.create(book);
        System.out.println("Inserted!" + book.getId());

        book.setPrice(50.33);
        bookRepository.update(book.getId(), book);

        bookRepository.remove(book);
        System.out.println("Removed!" + book.getId());
    }
}
