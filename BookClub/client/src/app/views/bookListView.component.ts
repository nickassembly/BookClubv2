import { Component, OnInit } from "@angular/core";
import { BookList } from "../services/booklist.service";

@Component({
    selector: "book-list",
    templateUrl: "bookListView.component.html",
    styleUrls: ["bookListView.component.css"]
})
export default class BookListView implements OnInit {
    constructor(public bookList: BookList) {
    }

    ngOnInit(): void {
        this.bookList.loadBooks()
            .subscribe(); // fires off the operation
    }
}