import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';

@Injectable({
  providedIn: 'root'
})
export class MemberDetailsResolver implements Resolve<Member> {

  constructor(private meberService : MembersService){}

  resolve(route: ActivatedRouteSnapshot): Observable<Member> {
    return this.meberService.getMember(route.paramMap.get('username'));
  }
}
