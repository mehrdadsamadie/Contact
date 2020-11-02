import { Component, OnInit, Input, EventEmitter, OnDestroy, HostListener } from '@angular/core';
import { ContactService } from 'src/app/shared/contact.service';
import { Contact, ContactList } from 'src/app/shared/contact.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styles: []
})
export class ContactListComponent implements OnInit {
  arrayFilter: String;
  config: any;
  constructor(private service: ContactService) {
  }

  changedExtraHandler(event) {

    this.service.gridValue.SearchStr = event;
    this.refreshGrid();
  }
  ngOnInit() {
    this.service.restGridValue();
    this.refreshGrid();
  }

  refreshGrid() {
    this.service.getContacts();
  }
  update(contact: Contact) {
    this.service.formData = Object.assign({}, contact)
  }
  setPage(page: number) {
    this.service.gridValue.Page = page;
    this.refreshGrid();
  }
  setSort(column: string) {
    if (column == this.service.gridValue.Column) {
      this.service.gridValue.Sort = this.service.gridValue.Sort =="asc"?"desc":"asc";
    }
    else
    {
      this.service.gridValue.Column=column;
      this.service.gridValue.Sort ="asc";
    }
    this.refreshGrid();
  }
  delete(id: number) {
    if(window.confirm('Are sure you want to delete this item ?')){
    this.service.deleteContact(id).subscribe
      (
        res => {
          this.refreshGrid();
        },
        err => {
        }
      )
    }
  }



}
