import { Component, OnInit } from '@angular/core';
import { RepositoryService } from './../../shared/services/repository.service';
import { Book } from '../../_interfaces/books/book.model';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})

export class BookListComponent implements OnInit {
  public books: Book[] = [];
  constructor(private repository: RepositoryService) { }
  ngOnInit(): void {
    this.getAllBooks();
  }
  public getAllBooks = () => {
    let apiAddress: string = "api/Book/";
    this.repository.getData(apiAddress)
    .subscribe(res => {
      this.books = res as Book[];
    })
  }
}
