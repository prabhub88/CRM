import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import {DataService} from '../../../Services/data.service';
import {FormControl, Validators} from '@angular/forms';
import {Customers} from '../../../Models/customers.model'
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-add.dialog',
  templateUrl: './add.dialog.component.html',
  styleUrl: './add.dialog.component.css'
})
export class AddDialogComponent {
  constructor(public dialogRef: MatDialogRef<AddDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Customers,
    public dataService: DataService) { }

formControl = new FormControl('', [
Validators.required
// Validators.email,
]);

getErrorMessage() {
return this.formControl.hasError('required') ? 'Required field' :
this.formControl.hasError('email') ? 'Not a valid email' :
'';
}

submit() {
// emppty stuff
}
onNoClick(): void {
  this.dialogRef.close();
}

public confirmAdd(): void {
  this.dataService.addCustomer(this.data);
}
}
