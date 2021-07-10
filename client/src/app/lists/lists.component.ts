import { Component, OnInit } from '@angular/core';
import { LikesParams } from '../_models/likesParam';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  members: Partial<Member[]>;
  // predicate = 'liked';
  pagination: Pagination;
  likesParams:LikesParams;

  constructor(private memberService: MembersService) { 
    this.likesParams=this.memberService.getLikesParams();
  }

  ngOnInit(): void {
    this.loadLikes();
  }
  loadLikes(){
    this.memberService.setLikesParams(this.likesParams);
    this.memberService.getLikes(this.likesParams).subscribe(response => {
      this.members=response.result;
      // this.likesParams=response.
      this.pagination=response.pagination;
    })
  }
  pageChanged(event: any) {
    // this.userParams.pageNumber = event.page;
    // this.memberService.setUserParams(this.userParams);
    this.likesParams.pageNumber=event.page;
    this.memberService.setLikesParams(this.likesParams);
    this.loadLikes();
  }

}
