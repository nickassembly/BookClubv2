import { __decorate } from "tslib";
import { Component } from '@angular/core';
let BookListComponent = class BookListComponent {
    constructor(repository) {
        this.repository = repository;
        this.books = [];
        this.getAllBooks = () => {
            let apiAddress = "api/Book/";
            this.repository.getData(apiAddress)
                .subscribe(res => {
                this.books = res;
            });
        };
    }
    ngOnInit() {
        this.getAllBooks();
    }
};
BookListComponent = __decorate([
    Component({
        selector: 'app-book-list',
        templateUrl: './book-list.component.html',
        styleUrls: ['./book-list.component.css']
    })
], BookListComponent);
export { BookListComponent };
//# sourceMappingURL=book-list.component.js.map