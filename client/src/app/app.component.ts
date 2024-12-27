
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet ,CommonModule, NavComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent  implements OnInit {
  
 
  //here we are injecting the account service in the app component and this is 
  private accountService = inject(AccountService);

  title = 'Dating app';
  

  ngOnInit(): void {
    this.setCurrentUser();
  }

  //this is a method to set the current user globally so that when we are logged in and we refresh the page we dont have to login again and again
  setCurrentUser(){
    //first we are fetching the user from the local storage and storing it in a const variable named userString
    const userString = localStorage.getItem('user');
    //if the userString is null we are returning from the function
    if(!userString) return;
    //if the userString is not null we are parsing the userString to a JSON object and storing it in a const variable named user
    const user = JSON.parse(userString);
    //then we are setting the current user signal to the user object
    this.accountService.currentUser.set(user);
  }

  
}
