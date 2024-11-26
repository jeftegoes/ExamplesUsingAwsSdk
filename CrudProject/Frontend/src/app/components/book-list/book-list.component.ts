import { Component, OnInit } from "@angular/core";
import { Book } from "src/app/model/boook";
import { BookService } from "../../services/book.service";

@Component({
  selector: "app-book-list",
  templateUrl: "./book-list.component.html",
  styleUrls: ["./book-list.component.scss"],
})
export class BookListComponent implements OnInit {
  books: Book[];

  constructor(private bookService: BookService) {}

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    // this.bookService.getBooks().subscribe(
    //   (result) => {
    //     this.books = result;
    //     console.log(this.books);
    //   },
    //   (error) => {
    //     alert(error);
    //   }
    // );
  }
}
