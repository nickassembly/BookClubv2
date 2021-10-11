import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { BookList } from './services/booklist.service';
import BookListView from './views/bookListView.component';

@NgModule({
  declarations: [
    AppComponent,
    BookListView
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
  providers: [
    BookList
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
