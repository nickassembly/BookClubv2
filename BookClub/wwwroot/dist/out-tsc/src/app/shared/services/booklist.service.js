import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
let BookList = class BookList {
    constructor(http) {
        this.http = http;
        this.books = [];
    }
    loadBooks() {
        return this.http.get("/api/Book")
            .pipe(map(data => {
            this.books = data;
            return;
        }));
    }
};
BookList = __decorate([
    Injectable()
], BookList);
export { BookList };
//# sourceMappingURL=booklist.service.js.map