import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { RequestCard } from '../request-card/request-card';
import { RequestService } from '../../services/RequestService/request-service';
import { GoogleMapsModule } from '@angular/google-maps';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/UserService/user-service';
import { Request } from '../../classes/Request';
import { signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../../services/CategoryService/category-service';
import { Category } from '../../classes/Category';

@Component({
  selector: 'app-request-dashboard',
  standalone: true,
  imports: [CommonModule, RequestCard, FormsModule, RouterLink],
  templateUrl: './request-dashboard.html',
  styleUrl: './request-dashboard.css',
})
export class RequestDashboard {
  @ViewChild('searchInput') searchInput!: ElementRef;
  requestsService = inject(RequestService);
  userService = inject(UserService);
  categoryService = inject(CategoryService);
  
  error: string = '';
  radiusInKm: number = 5;
  arr_requests = signal<Array<Request>>([]);
  categories = signal<Array<Category>>([]);
  selectedCategoryId: string | null = null; 

  ngOnInit(): void {
    this.loadCategories(); 
    this.filterByLocation();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories.set(data); // עדכון ה-signal של הקטגוריות
        console.log('Categories loaded into dashboard:', data);
      },
      error: (err) => {
        console.error('Failed to load categories', err);
      }
    });
  }

  filterByLocation(): void {
    this.error = '';

    let lat = this.userService.currentUser?.lat;
    let lng = this.userService.currentUser?.lng;

    if (lat && lng) {
      this.fetchRequests(lat, lng);
    } 
    else if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          this.fetchRequests(position.coords.latitude, position.coords.longitude);
        },
        (geoError) => {
          console.warn('GPS failed, using default location');
          this.fetchRequests(32.0853, 34.7818); 
          this.error = 'לא ניתן לגשת למיקום הנוכחי, מציג תוצאות ממרכז הארץ.';
        }
      );
    } else {
      this.fetchRequests(32.0853, 34.7818);
    }
  }


  private fetchRequests(lat: number, lng: number): void {
    this.requestsService.getRequestsByLocationAndCategory(lat, lng, this.radiusInKm * 1000, this.selectedCategoryId).subscribe({
      next: (mydata) => {
        this.arr_requests.set(mydata);
        console.log('Requests fetched successfully:', mydata);
        console.log(this.arr_requests().length);
      },
      error: (err) => {
        this.error = 'אירעה שגיאה בטעינת הבקשות.';
        console.error(err);
      }
    });
  }
}