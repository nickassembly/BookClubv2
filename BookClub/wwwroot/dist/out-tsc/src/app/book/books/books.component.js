import { __decorate } from "tslib";
import { Component } from '@angular/core';
let BooksComponent = class BooksComponent {
    constructor(repository) {
        this.repository = repository;
        this.getCompanies = () => {
            const apiAddress = "api/book";
            this.repository.getData(apiAddress)
                .subscribe(res => {
                this.books = res;
            });
        };
    }
    ngOnInit() {
        this.getCompanies();
    }
};
BooksComponent = __decorate([
    Component({
        selector: 'app-books',
        templateUrl: './books.component.html',
        styleUrls: ['./books.component.css']
    })
], BooksComponent);
export { BooksComponent };
//# sourceMappingURL=books.component.js.map