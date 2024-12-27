import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf, TitleCasePipe } from '@angular/common';
import { BsDropdownConfig, BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive, RouterLinkWithHref } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,NgIf,BsDropdownModule,RouterLink,RouterLinkActive,TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

  //this step below is to inject the AccountService into the component.ts here we have initiated a local accountService variable and assigned it to the AccountService
   accountService  = inject(AccountService);

   //in this step we have injected Router in the navComponent.ts file
   private router = inject(Router);

   //here we are injecting toaster service which we have installed in the the npm in our angular app and we also have defined its css in our angular.json
   private toastr = inject(ToastrService);

  // //this step below is to create a variable loggedIn and set it to false
  // loggedIn: boolean = false;
  model : any = {};


  login() {
    //this step below is to call the login method from the AccountService and subscribe to the response we have suscribed the response because it is an observable and observable does not do anmything we have to subscribe to it to get the response
    this.accountService.login(this.model).subscribe({
      next: _ =>{
        // console.log(response);
        this.router.navigateByUrl('/members') 
      },
      error : error => this.toastr.error(error.error)
      
    })
    console.log(this.model); 
  }

  //this is a logout function which just sets the login flag to false
  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }

}
