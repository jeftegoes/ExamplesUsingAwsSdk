import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { Book } from "src/app/model/boook";
import { BookService } from "src/app/services/book.service";

@Component({
  selector: "app-book-form",
  templateUrl: "./book-form.component.html",
  styleUrls: ["./book-form.component.scss"],
})
export class BookFormComponent implements OnInit {
  bookForm: FormGroup;
  book: Book;

  constructor(private fb: FormBuilder, private bookService: BookService) {
    this.createForm();
    this.book = new Book();
  }

  ngOnInit() {}

  createForm() {
    this.bookForm = this.fb.group({
      name: ['Clean Code'],
      rating: ['5'],
      author: ['Uncle Bob'],
      price: ['10.11'],
      fileName: ['Clean Code.jpg'],
      filebase64: [''],
    });
  }


  onSubmit() {
    const controls = this.bookForm.controls;

    this.book.name = this.bookForm.get('name').value;
    this.book.rating = Number(this.bookForm.get('rating').value);
    this.book.author = this.bookForm.get('author').value;
    this.book.price = Number(this.bookForm.get('price').value);
    this.book.fileName = this.bookForm.get('fileName').value;
    this.book.filebase64 = this.bookForm.get('filebase64').value;

    var result$ = this.bookService.create(this.book);

    result$.subscribe(
      (data) => {
        alert('The book created with success!');
      },
      (error) => {
        alert('Error creating book: ' + error);
      }
    );
  }
}
