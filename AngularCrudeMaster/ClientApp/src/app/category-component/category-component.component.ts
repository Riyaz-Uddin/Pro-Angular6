import { Component, OnInit } from '@angular/core';
import {
  HttpClientModule,
  HttpClient,
  HttpParams,
  HttpHeaders
} from '@angular/common/http';
@Component({
  selector: 'app-category-component',
  templateUrl: './category-component.component.html',
  styleUrls: ['./category-component.component.css']
})

export class CategoryComponentComponent implements OnInit {


  ngOnInit(): void {
  }
  public files: any[];
  items: any;
  constructor(public http: HttpClient) {
    this.files = [];
    this.http.get('https://localhost:7253/BookStore/GetAllCategory')
      .subscribe(data => {
        this.items = data;
      });
  }
}

