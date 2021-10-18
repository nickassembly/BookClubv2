import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styles: [``]
})
export class CustomersComponent implements OnInit  {
  customers: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get("http://localhost:8888/api/Book", {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      this.customers = response;
    }, err => {
      console.log(err)
    });
  }
}
