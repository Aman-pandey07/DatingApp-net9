import { Component, EventEmitter, inject, Inject, input, Input, output, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  private toastr = inject(ToastrService);
  
  constructor(private accountService: AccountService) {}
   // usersFromHomeComponent = input.required<any>()
  @Output() cancleRegister = new EventEmitter<boolean>();
  model : any = {};

  register(){ 
    this.accountService.register(this.model).subscribe({
      next: (response: any) =>{
        console.log(response);
        this.cancle();
      },
      error: (error: any) => this.toastr.error(error.error)
    })
  }

  cancle(){
    this.cancleRegister.emit(false)
  }

}