import { Component, inject, signal, ViewChild } from '@angular/core';
import { UserService } from '../../services/UserService/user-service';
import { RequestService } from '../../services/RequestService/request-service';
import { GoogleMap, GoogleMapsModule, MapCircle } from '@angular/google-maps';
import { FormsModule } from '@angular/forms';
import { RequestStatus } from '../../classes/RequestStatus';
import { Router } from '@angular/router';
import { MapInfoWindow } from '@angular/google-maps';
import { TruncatePipe } from '../../pipes/truncate.pipe';
import { CategoryService } from '../../services/CategoryService/category-service';
import { Category } from '../../classes/Category';

@Component({
  selector: 'app-map',
  imports: [GoogleMapsModule, FormsModule, MapInfoWindow, TruncatePipe, MapCircle],
  templateUrl: './map.html',
  styleUrl: './map.css',
})
export class Map {
  userService = inject(UserService);
  requestService = inject(RequestService);
  categoryService = inject(CategoryService);
  router = inject(Router);
  selectedItem: any = null;

  center: google.maps.LatLngLiteral = { lat: 31.7683, lng: 35.2137 };
  zoom = 13;
  radiusInKm = 5;
  viewType: 'requests' | 'users' = 'requests';

  categories = signal<Array<Category>>([]);
  selectedCategoryId: string | null = null;

  @ViewChild(GoogleMap) map!: GoogleMap;

  mapOptions: google.maps.MapOptions = {
    zoom: 13
  };

  ngOnInit(): void {
    this.loadCategories();
  }

  ngAfterViewInit(): void {
    this.refreshCenter();
    this.updateZoomForRadius();
    this.loadData();
  }

  loadCategories() {
    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories.set(data);
      },
      error: (err) => console.error('Error loading categories for map:', err)
    });
  }

  
  getCategoryIcon(req: any): string {
    if (req?.category?.icon) {
      return req.category.icon;
    }
    const found = this.categories().find(c => c.id === req?.categoryId);
    return found?.icon ?? '📍';
  }

  openInfoWindow(marker: any, req: any, infoWindow: MapInfoWindow) {
    this.selectedItem = req;
    infoWindow.open(marker);
  }

  refreshCenter() {
    const user = this.userService.currentUser;
    if (user && user.lat && user.lng) {
      this.center = {
        lat: Number(user.lat),
        lng: Number(user.lng)
      };
    }
  }


  private updateZoomForRadius() {
    const containerWidth = this.map?.googleMap?.getDiv()?.offsetWidth || window.innerWidth;

    const metersPerPixelAtZoom0 = 156543.03392 * Math.cos((this.center.lat * Math.PI) / 180);

    const targetMeters = this.radiusInKm * 1000 * 2 * 1.5;

    let zoom = Math.log2((metersPerPixelAtZoom0 * containerWidth) / targetMeters);
    zoom = Math.min(Math.max(Math.round(zoom), 3), 17); 

    this.zoom = zoom;
  }

  loadData() {
    this.updateZoomForRadius();

    const radiusMeters = this.radiusInKm * 1000;

    if (this.viewType === 'requests') {
      this.requestService.getRequestsByLocationAndCategory(this.center.lat, this.center.lng, radiusMeters, this.selectedCategoryId).subscribe({
        next: (data: Array<any>) => {
          this.requestService.arr_requests = data.filter((r: any) =>
            r.status !== RequestStatus.Closed &&
            r.status !== RequestStatus.Canceled &&
            r.userId !== this.userService.currentUser?.id
          );
        },
        error: (err: any) => {
          console.error('Error fetching requests:', err);
        }
      });
    } else {
      this.userService.getUsersInRadius(this.center.lat, this.center.lng, radiusMeters).subscribe({
        next: (data: Array<any>) => {
          this.userService.arr_users = data.filter((u: any) => u.id !== this.userService.currentUser?.id);
        },
        error: (err: any) => {
          console.error('Error fetching users:', err);
        }
      });
    }
  }
}