import { Book } from './../../_interfaces/books/book.model';
import { RepositoryService } from '../../shared/services/repository.service';
import { Component, OnInit } from '@angular/core';
import { UserBook } from 'src/app/_interfaces/books/userbook.model';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {
  public userBooks: UserBook[];

  constructor(private repository: RepositoryService) { }

  ngOnInit(): void {
    this.getUserBooks();
  }

  public getUserBooks = () => {
    const apiAddress: string = "api/book/user";
    this.repository.getData(apiAddress)
    .subscribe(res => {
      this.userBooks = res as UserBook[];
    })
  }
}
