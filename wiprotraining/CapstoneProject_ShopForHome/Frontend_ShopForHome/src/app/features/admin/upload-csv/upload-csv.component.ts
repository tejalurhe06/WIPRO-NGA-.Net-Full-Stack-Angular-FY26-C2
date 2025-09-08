import { Component } from "@angular/core";
import { AdminProductsService } from "../../../core/services/admin-products.service";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-upload-csv',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './upload-csv.component.html',
  styleUrl: './upload-csv.component.scss'
})
export class UploadCsvComponent {
  file?: File; 
  message = ''; 
  success = false;
  
  constructor(private ap: AdminProductsService){}
  
  onFile(e: any){ 
    this.file = e.target.files?.[0]; 
  }
  
  upload(){ 
    if(!this.file) return;
    
    this.ap.bulkUpload(this.file).subscribe({
      next: _ => {
        this.success = true;
        this.message = 'Uploaded successfully';
      }, 
      error: err => {
        this.success = false;
        this.message = err.error?.message || 'Upload failed';
      }
    }); 
  }
}