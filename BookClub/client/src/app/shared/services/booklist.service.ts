import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Book } from "../Book";

@Injectable()

export class BookList {

    constructor(private http: HttpClient) {

    }
    public books: Book[] = [];

    loadBooks(): Observable<void> {
        return this.http.get<[]>("/api/Book")
            .pipe(map(data => {
                this.books = data;
                return;
            }));
    }
}
