
export class Contact {
    Name:string;
    LastName:string;
    Email:string;
    Phone:string;
    Image:string;
    ImageShow:string;
    WebSite:string;
    Note:string;
    Gender:boolean;
    AddressId:number;
    ContactId:number;
    Street1:string;
    Street2:string;
    City:string;
    State:string;
    Country:string;
    PostalCode:string;
}
export class ContactList
{
    List:Array<Contact>;
    Total:number;
}