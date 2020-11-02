import { throwError, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Component, OnInit, Output, EventEmitter, Injectable } from '@angular/core';
import { Contact, ContactList } from './contact.model';
import { GridValue } from './grid-value.model';
import { HttpEventType, HttpClient, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  formData: Contact
  public progress: number;
  public success: boolean;
  gridValue: GridValue;
  public listContact: ContactList;
  @Output() public onUploadFinished = new EventEmitter();


  constructor(private http: HttpClient) {
    this.listContact = new ContactList();
    this.gridValue = new GridValue();
  }
  restGridValue() {
    this.gridValue.Page = 1;
    this.gridValue.PageSize = 7;
    this.gridValue.TotalPage = 0;
    this.gridValue.Column = "Name";
    this.gridValue.SearchStr = "";
    this.gridValue.Sort = "asc";
  }
  postContact(formData: Contact) {
    return this.http.post('http://localhost:59860/api/ContactInfo', this.formData)
  }
  deleteContact(id: number) {
    let httpParams = new HttpParams().set('id', id.toString());
    let options = { params: httpParams };
    return this.http.delete('http://localhost:59860/api/ContactInfo/'+id.toString());
  }
  getContacts() {
    let data = {
      page: this.gridValue.Page.toString(),
      pageSize: this.gridValue.PageSize.toString(),
      searchStr: this.gridValue.SearchStr,
      column: this.gridValue.Column,
      sort: this.gridValue.Sort
    };

    return this.http.get('http://localhost:59860/api/ContactInfo', { params: data }).subscribe
      (
        res => {
          this.listContact = res as ContactList
          this.gridValue.TotalItem=this.listContact.Total;
          this.gridValue.TotalPage=Math.ceil(this.listContact.Total/this.gridValue.PageSize)
        },
        err => {

        }
      )
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post('http://localhost:59860/api/upload', formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.success = true;
          this.onUploadFinished.emit(event.body);
          console.log(this.onUploadFinished.emit(event.body));
          this.formData.Image = event.body["dbPath"];
          this.formData.ImageShow = event.body["shPath"];
        }
      });
  }
}

