import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Component, Inject} from '@angular/core';
import {DataService} from '../../../Services/data.service';
import {FormControl, Validators} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-edit.dialog',
  templateUrl: './edit.dialog.component.html',
  styleUrl: './edit.dialog.component.css'
})
export class EditDialogComponent {
  constructor(public dialogRef: MatDialogRef<EditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, public dataService: DataService) {
      console.log('edit compo');
      console.log(data);
     }

formControl = new FormControl('', [
Validators.required
// Validators.email,
]);
// dob  :string='2021-08-21';

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

stopEdit(): void {
  this.dataService.updateCustomer(this.data);
}
}
