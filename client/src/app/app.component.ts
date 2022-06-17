import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Skinet';
  products : IProduct[] = [];

  constructor(private http : HttpClient){}

  ngOnInit(): void {
    this.http.get('https://localhost:7230/api/Products').subscribe({
      next : (response : IProduct[]) => {
        console.log(response);
        this.products = response;

      },
      error : (error) => {
        console.log(error);

      }
    });
  }
}
