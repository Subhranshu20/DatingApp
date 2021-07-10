import { Component, Input, OnInit,EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/user';


@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css']
})
export class RolesModalComponent implements OnInit {
  @Input() updateSeletedRoles = new EventEmitter();
  user: User;
  roles: any[];
  // title: string;
  // list : any[] =[];
  // closeBtnName : string;
  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }
  updateRoles(){
    this.updateSeletedRoles.emit(this.roles);
    console.log(this.roles);
    this.bsModalRef.hide();
  }

}
