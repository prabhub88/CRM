import {Component, ElementRef, OnInit, ViewChild,ChangeDetectorRef} from '@angular/core';
import {DataService} from '../../Services/data.service';
import {HttpClient} from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {Customers} from '../../Models/customers.model';
import {DataSource} from '@angular/cdk/collections';
import {AddDialogComponent} from '../../Dialogs/Add/add.dialog/add.dialog.component';
import {EditDialogComponent} from '../../Dialogs/Edit/edit.dialog/edit.dialog.component';
import {DeleteDialogComponent} from '../../Dialogs/Delete/delete.dialog/delete.dialog.component';
import {MatTable} from '@angular/material/table';
import {BehaviorSubject, fromEvent, merge, Observable} from 'rxjs';
import {map} from 'rxjs/operators';
@Component({
  selector: 'app-view-customer',
  templateUrl: './view-customer.component.html',
  styleUrl: './view-customer.component.css'
})
export class ViewCustomerComponent {
    
  displayedColumns = ['customerNumber', 'customerName', 'dob', 'gender', 'actions'];
  datasource: Customers[] ;
  index: number;
  CustNo: number;

  constructor(public httpClient: HttpClient,
    public dialogService: MatDialog,
    public dataService: DataService,
    private changeDetectorRefs: ChangeDetectorRef
  ) {}

@ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
@ViewChild(MatSort, {static: true}) sort: MatSort;
@ViewChild('filter',  {static: true}) filter: ElementRef;
@ViewChild(MatTable) table: MatTable<any>;

ngOnInit() {
this.loadData();
}

refresh() {
  this.table.renderRows();
}

openAddDialog() {
  const dialogRef = this.dialogService.open(AddDialogComponent, {
    data: {issue: {} },
    height:'auto',
    maxHeight:'80%'
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result === 1) {
      // After dialog is closed we're doing frontend updates
      // For add we're just pushing a new row inside DataService
      this.datasource?.push(this.dataService.getDialogData());
    }
  });
}

startEdit(i: number, custno: number, name: string, dob: string, gender: string) {
  this.CustNo = custno;
  console.log(custno);
  this.index = i;
  const dialogRef = this.dialogService.open(EditDialogComponent, {
     height:'auto',
    maxHeight:'80%',
    data: {customerNumber: custno, customerName: name, dob: dob, gender: gender}
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result === 1) {
      const foundIndex = this.datasource?.findIndex(x => x.customerNumber === this.CustNo);
      this.datasource[foundIndex] = this.dataService.getDialogData();
      // And lastly refresh table
      console.log('afteredit');
      console.log(this.datasource);
      console.log(foundIndex);
    }
  });
}

deleteItem(i: number, customerNo: number, customerName: string, dob: string, gender: string) {
  this.index = i;
  this.CustNo = customerNo;
  const dialogRef = this.dialogService.open(DeleteDialogComponent, {
    data: {customerNumber: customerNo, name: customerName, dob: dob, gender: gender}
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result === 1) {
      const foundIndex = this.datasource?.findIndex(x => x.customerNumber === this.CustNo);
      // for delete we use splice in order to remove single object from DataService
      this.datasource?.splice(foundIndex || 0, 1);
       
    }
  });
}

public loadData() {
   this.dataService.getAllICustomers();
   this.dataService.data.subscribe(
    x=> this.datasource=x
   );
  console.log(this.datasource);
}

}