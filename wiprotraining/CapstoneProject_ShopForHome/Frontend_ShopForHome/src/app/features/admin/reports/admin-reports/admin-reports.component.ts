import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { OnInit } from "@angular/core";
import { ReportsService, LowStockProduct, SalesReportResponse } from "../../../../core/services/reports.service";
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-admin-reports',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-reports.component.html',
  styleUrl: './admin-reports.component.scss',
  providers: [DatePipe]
})
export class AdminReportsComponent implements OnInit {
  lowStockProducts: LowStockProduct[] = [];
  salesReport: SalesReportResponse | null = null;
  from = '';
  to = '';
  loading = false;
  errorMessage = '';
  
  constructor(
    private rep: ReportsService,
    private datePipe: DatePipe
  ) {}
  
  ngOnInit() { 
    this.loadLowStock();
  }
  
  loadLowStock() {
    this.loading = true;
    this.rep.lowStock().subscribe({
      next: (products) => {
        this.lowStockProducts = products;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load low stock report';
        this.loading = false;
        console.error(err);
      }
    }); 
  }
  
  loadSales() { 
    if (!this.from || !this.to) {
      this.errorMessage = 'Please select both from and to dates';
      return;
    }
    
    this.loading = true;
    this.errorMessage = '';
    
    this.rep.sales(this.from, this.to).subscribe({
      next: (report) => {
        this.salesReport = report;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load sales report';
        this.loading = false;
        console.error(err);
      }
    }); 
  }
  
  formatDate(date: string): string {
    return this.datePipe.transform(date, 'mediumDate') || date;
  }
}