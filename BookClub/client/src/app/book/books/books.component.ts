import { Book } from './../../_interfaces/books/book.model';
import { RepositoryService } from '../../shared/services/repository.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {
  public books: Book[];

  constructor(private repository: RepositoryService) { }

  ngOnInit(): void {
    this.getCompanies();
  }

  public getCompanies = () => {
    const apiAddress: string = "api/book";
    this.repository.getData(apiAddress)
    .subscribe(res => {
      this.books = res as Book[];
    })
  }
}
