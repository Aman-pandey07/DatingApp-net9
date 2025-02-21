import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  //this step below is to inject the HttpClient into the service
  private http = inject(HttpClient);

  //this step below is to set the base URL for the API for now we have hardcoded it
  baseUrl = environment.apiUrl;

  //this step below is to create a signal to store the current user signal is a reactive variable that can be subscribed to.beforw this we created a user interface which just define what the user is in the model folder in the user.ts file
  currentUser = signal<User | null>(null)

  //this is a  step below is to create a method to register a user where model:any is the user object
  login(model: any){
    //this step below is to make a post request to the API to register a user and then we are using the pipe operator to map the responce to the user object and then we are storing the user object in the local storage and then we are setting the current user signal to the user object becsuse if we dont do this the user will be lost when the page is refreshed
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user =>{
        if(user){
          //if we store the information in the local storage it will be stored in the browser and will be available even if the page is refreshed and we dont have to login again and again
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }


  register(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user =>{
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    )
  }


  //we have done exactly opposite of the login method here
  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
