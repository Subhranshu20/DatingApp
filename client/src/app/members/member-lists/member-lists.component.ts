import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-lists',
  templateUrl: './member-lists.component.html',
  styleUrls: ['./member-lists.component.css']
})
export class MemberListsComponent implements OnInit {
   members: Member[];
  //members:Observable<Member[]>;
  pagination: Pagination;
  userParams: UserParams
  user: User;
  genderList= [{value:'male',display:'Males'},{value:'female',display:'Females'}];
  // pageNumber=1;
  // pageSize=5;
  constructor(private memberService: MembersService) { 
    this.userParams=this.memberService.getUserParams();
    // this.accountService.currentUser$.pipe(take(1)).subscribe(user=>{
    //   this.user=user;
    //   this.userParams=new UserParams(user);
    // })
  }

  ngOnInit(): void {
    this.loadMembers();
  }
  loadMembers(){
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe(response => {
      this.members=response.result;
      this.pagination=response.pagination;

    })
    // this.memberService.getMembers().subscribe(members=> {

    //   this.members$=this.memberService.getMembers();
    // });
  }
  resetFilters(){
    this.userParams=this.memberService.resetUserParams();
    // this.userParams=new UserParams(this.user);
    this.loadMembers();
  }
  pageChanged(event: any) {
    // this.userParams.pageNumber = event.page;
    // this.memberService.setUserParams(this.userParams);
    this.userParams.pageNumber=event.page;
    this.memberService.setUserParams(this.userParams);
    this.loadMembers();
  }

}
