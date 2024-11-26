import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { Book } from "../model/boook";

@Injectable({
  providedIn: "root",
})
export class BookService {
  private readonly endpoint = "https://yzib8zbeyf.execute-api.sa-east-1.amazonaws.com/dev/";

  constructor(private http: HttpClient) {}

  getBooks() {
    return this.http.get<Book[]>(this.endpoint ).pipe(
      map((response: any) => {
        return response as Book[];
      })
    );
  }

  getBook(id: number) {
    return this.http.get<Book>(this.endpoint + id).pipe(
      map((responseApi: any) => {
        return responseApi as Book;
      })
    );
  }

  create(book: Book) {
    return this.http.post(this.endpoint, book);
  }

  update(book: Book) {
    return this.http.put(this.endpoint + book.id, book);
  }

  delete(book: Book) {
    return this.http.delete(this.endpoint + book.id);
  }
}
