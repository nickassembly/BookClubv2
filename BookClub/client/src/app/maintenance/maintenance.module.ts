import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookListComponent } from './book-list/book-list.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    BookListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: 'list', component: BookListComponent }
    ])
  ],
})

export class MaintenanceModule { }
