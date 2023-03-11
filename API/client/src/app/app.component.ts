import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'EasyStore';
  products: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('https://localhost:5002/api/products?pageSize=50').subscribe({
      next: (response: any) => this.products = response.data, //What to do next
      error: error => console.log(error), //What to do if there is an error
      complete: () => {
        console.log('Request completed');
        console.log('Extra statement');
      },
    })
  }
}
