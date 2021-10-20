import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BooksComponent } from './books/books.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [BooksComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: 'books', component: BooksComponent }
    ])
  ]
})
export class BookModule { }
