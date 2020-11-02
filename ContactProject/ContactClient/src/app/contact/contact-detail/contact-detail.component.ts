import { Component, OnInit } from '@angular/core';
import { ContactService } from 'src/app/shared/contact.service';
import { NgForm } from '@angular/forms';
import { from } from 'rxjs';

@Component({
  selector: 'app-contact-detail',
  templateUrl: './contact-detail.component.html',
  styles: []
})
export class ContactDetailComponent implements OnInit {

  constructor(private service: ContactService) { }
  errorList: Array<string>;
  ngOnInit() {
    this.resterror();
    this.restForm();
  }
  changeGender(gender: boolean) {
    this.service.formData.Gender = gender;
  }
  resterror() {
    this.errorList = [];
  }
  restForm(form?: NgForm) {
    if (form != null)
      form.resetForm();
    this.service.formData =
      {
        AddressId: 0,
        City: '',
        ContactId: 0,
        Country: '',
        Email: '',
        Gender: true,
        Image: '',
        ImageShow:'',
        LastName: '',
        Name: '',
        Note: '',
        Phone: '',
        PostalCode: '',
        State: '',
        Street1: '',
        Street2: '',
        WebSite: ''
      }
  }
  onSubmit(form: NgForm) {
    this.resterror();
    this.service.postContact(form.value).subscribe(
      res => {
        this.restForm(form);
        this.service.getContacts();
      },
      err => {
        console.log(err.error.errors);
        this.parseErrors(err.error.errors)
      }
    )
  }
 parseErrors(response) {
    var errors = [];
    for (var key in response) {
        for (var i = 0; i < response[key].length; i++) {
            errors.push(response[key][i]);
        }
    }
    this.errorList =  errors;
  }
}

