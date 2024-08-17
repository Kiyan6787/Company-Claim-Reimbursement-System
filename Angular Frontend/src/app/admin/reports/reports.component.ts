// reports.component.ts
import { Component, OnInit } from '@angular/core';
import { Chart, LinearScale, BarElement, Title, Tooltip, Legend, 
        BarController, LineController, CategoryScale, LineElement,
        PointElement, PieController, ArcElement } 
from 'chart.js';
import { AdminService } from '../services/admin.service';
import { NavbarComponent } from "../navbar/navbar.component";

Chart.register(LinearScale, BarElement, LineElement, Title, Tooltip, Legend, 
              BarController, LineController, CategoryScale,PointElement,
              PieController, ArcElement);

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [NavbarComponent],
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  types: { [key: number]: string } = {};
  chart: any;

  constructor(private service: AdminService) {}

  //Initialize the graph when page loads.
  ngOnInit(): void {
    
    //Bar Graph for total type-wise reimbursements.
    // Fetch types and amounts
    this.service.getTypes().subscribe(types => {
      this.types = types.reduce((map, type) => {
        map[type.reimbursementTypeId] = type.type;
        return map;
      }, {} as { [key: number]: string });

      //Mapping the types to the type ID from the previous fetch.
      this.service.getTypesAndAmount().subscribe(data => {
        const labels = data.map(item => this.types[item.reimbursementTypeId] || 'Unknown');
        const totals = data.map(item => item.totalAmount);
        const colors = this.barColors(data.length);

        const chart = new Chart('chartId', {
          type: 'bar',
          data: {
            labels: labels,
            datasets: [{
              label: 'Total Reimbursements',
              data: totals,
              backgroundColor: colors,
              borderColor: 'rgba(75, 192, 192, 1)'
            }]
          },
          options: {
            scales: {
              x: {
                beginAtZero: true
              },
              y: {
                beginAtZero: true
              }
            }
          }
        });
      });
    });

    //Line graph for month-wise reimbursements
    //Chart for month-wise reimbursements
    this.service.getMonthsAndAmount().subscribe(data => {
        const labels = data.map(item => item.monthYear);
        const totals = data.map(item => item.totalAmount);

        const chart = new Chart('monthlyChartId', {
          type: 'line',
          data: {
            labels: labels,
            datasets: [{
              label: 'Total Reimbursements',
              data: totals,
              backgroundColor: 'rgba(75, 192, 192, 0.2)',
              borderColor: 'rgba(75, 192, 192, 1)',
              borderWidth: 1,
              pointRadius: 5,
              pointBackgroundColor: 'rgba(255, 99, 132, 1)',
              pointBorderColor: 'rgba(255, 99, 132, 1)', 
              pointHoverRadius: 7, 
              pointHoverBackgroundColor: 'rgba(255, 159, 64, 1)', 
              pointHoverBorderColor: 'rgba(255, 159, 64, 1)',  
              pointStyle: 'rectRounded'
            }]
          },
          options: {
            scales: {
              x: {
                beginAtZero: true
              },
              y: {
                beginAtZero: true
              }
            }
          }
        });
    })

    this.service.getClaims().subscribe(claims => {
      this.createCurrencyPieChart(claims);
      console.log(claims)
    });

    this.createApprovedVDeclinedPieChart();
  }

  //Pie chart for currency pie chart
  createCurrencyPieChart(claims: any[]): void {
    const currencyCounts = claims.reduce((acc, claim) => {
      acc[claim.currency.code] = (acc[claim.currency.code] || 0) + 1;
      return acc;
    }, {} as { [key: string]: number });

    const labels = Object.keys(currencyCounts);
    const data = Object.values(currencyCounts);
    console.log("Lables: ", labels);
    console.log("Data: ", data);

    this.chart = new Chart('pieChartId', {
      type: 'pie',
      data: {
        labels: labels,
        datasets: [{
          data: data,
          backgroundColor: labels.map(() => `rgba(${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, 0.5)`),
          borderColor: labels.map(() => 'rgba(0, 0, 0, 0.1)'),
          borderWidth: 1
        }]
      }
    });
  }

  // Pie chart for approved vs declined claims
  createApprovedVDeclinedPieChart() {
    let approvedClaimsCount = 0;
    let declinedClaimsCount = 0;

    this.service.getApprovedClaims().subscribe(claims => {
      approvedClaimsCount = claims.length;
      
      this.service.getDeclinedClaims().subscribe(claims => {
        declinedClaimsCount = claims.length;

        const data = [approvedClaimsCount, declinedClaimsCount];
        const labels = ['Approved', 'Declined'];
        const backgroundColor = ['#007bff', '#dc3545'];

        new Chart('approvedVDeclinedPie', {
          type: 'pie',
          data: {
            labels: labels,
            datasets: [{
              data: data,
              backgroundColor: backgroundColor,
            }]
          }
        });
      });
    });
  }

  barColors(count: number): string[] {
    const palette = [
      'rgba(255, 99, 132, 0.6)',
      'rgba(54, 162, 235, 0.6)',
      'rgba(255, 206, 86, 0.6)',
      'rgba(75, 192, 192, 0.6)',
      'rgba(153, 102, 255, 0.6)',
      'rgba(255, 159, 64, 0.6)'
    ];
    return Array(count).fill(0).map((_, i) => palette[i % palette.length]);
  }
}
