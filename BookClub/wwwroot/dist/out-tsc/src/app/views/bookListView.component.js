import { __decorate } from "tslib";
import { Component } from "@angular/core";
let BookListView = class BookListView {
    constructor(bookList) {
        this.bookList = bookList;
    }
    ngOnInit() {
        this.bookList.loadBooks()
            .subscribe(); // fires off the operation
    }
};
BookListView = __decorate([
    Component({
        selector: "book-list",
        templateUrl: "bookListView.component.html",
        styleUrls: ["bookListView.component.css"]
    })
], BookListView);
export default BookListView;
//# sourceMappingURL=bookListView.component.js.map