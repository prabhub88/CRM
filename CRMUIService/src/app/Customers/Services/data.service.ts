import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {Customers} from '../Models/customers.model'
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {environment} from '../../Environments/environment';
@Injectable({
  providedIn: 'root'
})
export class DataService {

    private readonly API_URL_get =environment.config.apiBaseUrl+environment.config.getAllCustomers ;
    private readonly API_URL_CreteUpdate =environment.config.apiBaseUrl+environment.config.createOrUpdateCustomer ;
    private readonly API_URL_Del =environment.config.apiBaseUrl+environment.config.deleteCustomer ;

    dataChange: BehaviorSubject<Customers[]> = new BehaviorSubject<Customers[]>([]);
    // Temporarily stores data from dialogs
    dialogData: any;
      
    constructor(private httpClient: HttpClient) {}
    // custArr :Customers[]=[
    //     {customerNo:111,customerName:'test1',dob: '1988-07-15',gender:'Male'},
    //     {customerNo:112,customerName:'test2',dob: '2001-08-21',gender:'Female'}
        
    //       ];

    get data(): Observable<Customers[]> {
      return this.dataChange;
    }
  
    getDialogData() {
      return this.dialogData;
    }
  
    /** CRUD METHODS */
    getAllICustomers(): void {
      this.httpClient.get<Customers[]>(this.API_URL_get).subscribe(data => {
        console.log(data);
          this.dataChange.next(data);
        },
        (error: HttpErrorResponse) => {
        console.log (error.name + ' ' + error.message);
        });
    }
  
    callAPI(url:string,cust:Customers){
      this.httpClient.post<any>(url,cust).subscribe(data => {
        console.log(data);
         // this.dataChange.next(new Customers());
        },
        (error: HttpErrorResponse) => {
        console.log (error.name + ' ' + error.message);
        });
        location.reload();
    }

    // DEMO ONLY, you can find working methods below
    addCustomer(cust: Customers): void {
      var dobdate=new Date(cust.dob);
      cust.dob=dobdate.getFullYear().toString()+"-"+("0" +(dobdate.getMonth()+1).toString()).slice(-2)
      +"-"+("0" +dobdate.getDate().toString()).slice(-2);
      this.dialogData = cust;
      this.callAPI(this.API_URL_CreteUpdate,cust);
    }
  
    updateCustomer(cust: Customers): void {
        var dobdate=new Date(cust.dob);
        cust.dob=dobdate.getFullYear().toString()+"-"+("0" +(dobdate.getMonth()+1).toString()).slice(-2)+"-"+("0" +dobdate.getDate().toString()).slice(-2);
      this.dialogData = cust;
      this.callAPI(this.API_URL_CreteUpdate,cust);
    }
  
    deleteCustomer(id: number): void {
      console.log(id);
      this.httpClient.delete<any>(this.API_URL_Del+id.toString()).subscribe(data => {
        console.log(data);
           
        },
        (error: HttpErrorResponse) => {
        console.log (error.name + ' ' + error.message);
        });
        location.reload();
    }
}
