import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BooksComponent } from './books/books.component';
import { RouterModule } from '@angular/router';
let BookModule = class BookModule {
};
BookModule = __decorate([
    NgModule({
        declarations: [BooksComponent],
        imports: [
            CommonModule,
            RouterModule.forChild([
                { path: 'books', component: BooksComponent }
            ])
        ]
    })
], BookModule);
export { BookModule };
//# sourceMappingURL=book.module.js.map