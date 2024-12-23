
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent  implements OnInit {
  
  http = inject(HttpClient); //here we defined a http property and accessed the http client which we defined in step1 in appconfig.ts and this is step 2
  title = 'Dating app';
  users: any;

  ngOnInit(): void {
    this.http.get("http://localhost:5000/api/users").subscribe({
     // next: () => {} this is a empty call back function
     next: response => this.users = response, //here we are defining a function to get the responce and assign it to the user property step5

     error: error => console.log(error), //here we are defining a function to get the error if any and log it to the console step6

     complete : () => console.log('Request has completed') //here we are defining a function to get the completion of the responce step7

    }) //here we are accessing the http property in this ngOnInit constructor and calling the get method to consume the api and this is step 3
    //here the angular consider all the responce as an observable so we need to subscribe to it to get the responce and then defin our function of what to do with the responce step 4
  }
}