import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AccountService } from './account.service';
import { Member } from '../_models/members';


@Injectable({
  providedIn: 'root'
})
export class MembersService {

  private http = inject(HttpClient);
  private acccountService = inject(AccountService);
  baseUrl = environment.apiUrl;
  constructor() { }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users', this.getHttpOptions());
  }
  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + username, this.getHttpOptions());
  }
  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.acccountService.currentUser()?.token}`
      })
    }
  }

}
