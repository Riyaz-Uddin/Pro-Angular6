import { Component, OnInit } from '@angular/core';
import {
  HttpClientModule,
  HttpClient,
  HttpParams,
  HttpHeaders
} from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-book-items-angular-component',
  providers: [],
  templateUrl: './book-items-angular-component.component.html',
  styleUrls: ['./book-items-angular-component.component.css']
})
export class BookItemsAngularComponentComponent implements OnInit {

  ngOnInit() {

  }
  public files: any[];
  items: any;
  sl: number = 0;
  name2: string = "";
  BookId2: string = "";
  BookName2: string = '';
  CategoryId2: string = "";
  Description2: string = "";
  Active2: boolean = false;
  Date2: string = "";
  Price2: number = 0;
  Image2: string = "";
  CategoryName2: string = "";
  Author2: string = "";
  message: string = "";
  constructor(public http: HttpClient, private route: ActivatedRoute) {
    this.files = [];
    
    this.http.get('https://localhost:7253/BookStore/GetAllItems')
      .subscribe(data => {
        this.items = data;
        //this.CreateId();
        console.log(this.items);
      });
    this.sl = 0;
    
    this.route.queryParams.subscribe(params => {
      this.CategoryId2 = params['CategoryID'];
      this.deptchange();
    });


  }
  deptchange() {
    this.items = [];
    this.Author2 = "";
    this.http.get('https://localhost:7253/BookStore/GetCategory/' + this.CategoryId2)
      .subscribe(data => {
        if (data != "") {
          this.CategoryName2 = Object.values(data)[0].CategoryName;
          this.Author2 = Object.values(data)[0].Author;
          this.showItems();
        }
      });
  }
  CreateId() {
    this.http.get('https://localhost:7253/BookStore/GenerateCodeCategory')
      .subscribe(data => {
        this.CategoryId2 = data.toString();
      });
    
  }
 
  showItems() {
    this.http.get('https://localhost:7253/BookStore/GetItems/' + this.CategoryId2)
      .subscribe(data => {
        this.items = data;
        console.log(this.items);
      });
    this.sl = 0;
  }
  onFileChanged(event: any) {
    this.files = event.target.files;
    const formData = new FormData();
    formData.append('files', this.files[0]);
    this.http.post('https://localhost:7253/BookStore/Post/', formData).subscribe(data => {
      this.Image2 = this.files[0].name
    });
  }
  addItems(BookId: string, BookName: string, CategoryId: string, Description: string, Active: string, Date: string, Price: string, Image: string): void {
    this.items.push({
      BookId: BookId,
      BookName: BookName,
      Description: Description,
      Active: false,
      Date: Date,
      Price: Price,
      Image: this.files[0].name,

    });
    this.BookName2 = '';
    this.BookId2 = '';
    this.Description2 = "";
    this.Active2 = false;
    this.Date2 = "";
    this.Price2 = 0;
  }
  convertDate(inputFormat: Date) {
    function pad(s: number) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat)
    return [d.getFullYear(), pad(d.getMonth() + 1), pad(d.getDate())].join('-')
  }
  show(id: number, BookId1: string, BookName1: string, CategoryId1: string, Description1: string, Active1: boolean, Date1: string, Price1: number, Image1: string): void {
    this.sl = id;
    this.BookId2 = BookId1;
    this.BookName2 = BookName1;
    this.Description2 = Description1;
    this.Active2 = Active1;
    this.Price2 = Price1;
    this.Date2 = this.convertDate(new Date(Date1));
    this.Image2 = Image1;

  }
  updateItems(BookId: HTMLInputElement, BookName: HTMLInputElement, CategoryId: HTMLInputElement, Description: HTMLInputElement, Active: HTMLInputElement, Date: HTMLInputElement, Price: HTMLInputElement): void {
    this.items[this.sl].BookId = BookId.value;
    this.items[this.sl].BookName = BookName.value;
    this.items[this.sl].Description = Description.value;
    this.items[this.sl].Active = Active.value;
    this.items[this.sl].Date = Date.value;
    this.items[this.sl].Price = Price.value;
    if (Active.value=="true")
      this.items[this.sl].Active = true;
    else
      this.items[this.sl].Active = false;
    this.items[this.sl].Date = Date.value;
    this.BookName2 = '';
    this.BookId2 = '';
    this.Description2 = "";
    this.Active2 = false;
    this.Date2 = "";
    this.Price2 = 0;


  }
  deleteItems(): void {
    this.items.splice(this.sl, 1);
    this.BookName2 = '';
    this.BookId2 = '';
    this.Description2 = "";
    this.Active2 = false;
    this.Date2 = "";
    this.Price2 = 0;

  }

  deleteAll(): void {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    var data = {};
    this.http.post<any>('https://localhost:7253/BookStore/RemoveBCShopVM/' + this.CategoryId2, JSON.stringify(data), httpOptions).subscribe(data => {
      window.location.href = 'https://localhost:4200/';
    });
  }

  saveAll(): void {
    var i = 0;
    var detailsArr = [];
    var BookCategory = {
      CategoryId: this.CategoryId2,
      CategoryName: this.CategoryName2,
      Author: this.Author2
    };
    for (let value of this.items) {
      detailsArr.push({
        BookId: value.BookId,
        BookName: value.BookName,
        CategoryId: this.CategoryId2,
        Description: value.Description,
        Active: value.Active,
        Date: value.Date,
        Price: value.Price,
        Image: value.Image,
      });
    }
    var data = {
      "BookCategory": BookCategory,
      "BookItem": detailsArr
    };
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    console.log(JSON.stringify(data));
    this.http.post<any>('https://localhost:7253/BookStore/AddBCShopVM', JSON.stringify(data), httpOptions).subscribe(data => {
      window.location.href = 'https://localhost:4200/';
    });
  }
  myalert(data: string) {
  }
}

