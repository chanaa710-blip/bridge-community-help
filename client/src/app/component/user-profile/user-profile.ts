import { Component, inject } from '@angular/core';
import { UserService } from '../../services/UserService/user-service';
import { User } from '../../classes/User';
import { Request } from '../../classes/Request';
import { FormsModule } from '@angular/forms';
import { RequestService } from '../../services/RequestService/request-service';
import { RequestCard } from '../request-card/request-card';
import { RequestStatus } from '../../classes/RequestStatus';
import { ViewChild, ElementRef, AfterViewInit } from '@angular/core'
import { Router } from '@angular/router';
import { signal } from '@angular/core';

declare var google: any;
@Component({
  selector: 'app-user-profile',
  imports: [FormsModule, RequestCard],
  templateUrl: './user-profile.html',
  styleUrl: './user-profile.css',
})
export class UserProfile {
  userService = inject(UserService);
  editUser: User = {};
  requestService = inject(RequestService);
  myRequests = signal<Array<Request>>([]);
  activeRequests = signal<Array<Request>>([]);
  closedRequests = signal<Array<Request>>([]);
  currentTab: 'active' | 'closed' = 'active';

  @ViewChild('addressInput') addressInput!: ElementRef

  @ViewChild('mapContainer') mapContainer!: ElementRef;
  map!: google.maps.Map;
  marker!: google.maps.Marker
  router = inject(Router);

  ngOnInit(): void {
    if (!this.userService.currentUser) {
      alert('לא נרשמת, מעביר אותך להרשמה...');
      this.router.navigate(['/register']);
      return;
    }
    if (this.userService.currentUser) {
      this.editUser = { ...this.userService.currentUser };
      this.loadRequests();
    }
  }

  loadRequests() {
    this.requestService.getRequestsByUserId(this.userService.currentUser.id).subscribe({
      next: (requests) => {
        console.log('Requests from server:', requests);
        this.myRequests.set(requests);
        this.filterRequests();
      },
      error: (err) => {
        console.error('Error fetching my requests:', err);
      }
    });
  }

  filterRequests() {
    const requests = this.myRequests();
  
    this.activeRequests.set(
      requests.filter(r =>
        r.status === RequestStatus.Open ||
        r.status === RequestStatus.InProgress
      )
    );
  
    this.closedRequests.set(
      requests.filter(r =>
        r.status === RequestStatus.Closed ||
        r.status === RequestStatus.Canceled
      )
    );
  
    console.log('Active:', this.activeRequests());
    console.log('Closed:', this.closedRequests());
  }

saveProfile() {
  this.userService.updateProfile(this.editUser).subscribe({
    next: (updatedUser) => {
      this.userService.saveUserToStorage(updatedUser);
      alert('פרופיל עודכן בהצלחה!');
    },
    error: (err) => {
      console.error('Error updating profile:', err);
      alert('אירעה שגיאה בעדכון הפרופיל. אנא נסה שוב.');
    }
  })
}

handleStatusChange(requestId: string, newStatus: RequestStatus) {
  this.requestService.updateStatus(requestId, newStatus).subscribe({
    next: (success) => {
      alert('הסטטוס עודכן בהצלחה!');
      this.loadRequests();
    },
    error: (err) => {
      console.error('Error updating request status:', err);
      alert('אירעה שגיאה בעדכון הבקשה. אנא נסה שוב.');
    }
  });
}

handleDelete(requestId: string) {
  this.requestService.deleteRequest(requestId).subscribe({
    next: () => {
      alert('הבקשה נמחקה!');
      this.loadRequests();
    },
    error: (err) => console.error('Error deleting:', err)
  });
}

setCurrentLocation() {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(
      position => {
        const { latitude, longitude } = position.coords;

        this.editUser.lat = latitude;
        this.editUser.lng = longitude;

        // 2. עדכון ויזואלי של תיבת הטקסט באמצעות Geocoder של גוגל
        const geocoder = new google.maps.Geocoder();
        geocoder.geocode({ location: { lat: this.editUser.lat, lng: this.editUser.lng } }, (results: any, status: any) => {
          if (status === 'OK' && results[0]) {
            // הזרקת הכתובת האמיתית לתוך ה-Input שעל המסך
            this.addressInput.nativeElement.value = results[0].formatted_address;
          } else {
            this.addressInput.nativeElement.value = '📍 מיקום נוכחי נקלט בהצלחה';
          }
        });
      },
      error => {
        console.error('שגיאה בקבלת המיקום:', error);
        alert('לא ניתן לקבל את המיקום. אנא אפשר גישה למיקום בדפדפן.');
      },
      { enableHighAccuracy: true } // מאלץ את הדפדפן להביא מיקום מדויק ולא מהקאש
    );
  }
}

ngAfterViewInit(): void {
  // 1. אתחול ה-Autocomplete (כפי שהיה)
  if(typeof google !== 'undefined') {
  const autocomplete = new google.maps.places.Autocomplete(this.addressInput.nativeElement);
  autocomplete.addListener('place_changed', () => {
    const place = autocomplete.getPlace();
    if (place.geometry && place.geometry.location) {
      const lat = place.geometry.location.lat();
      const lng = place.geometry.location.lng();
      this.editUser.lat = lat;
      this.editUser.lng = lng;
      this.initMap(lat, lng);
    }
  });

  setTimeout(() => {
    if (this.editUser.lat && this.editUser.lng) {
      this.initMap(this.editUser.lat, this.editUser.lng);
    } else {

      this.initMap(31.7683, 35.2137);
    }
  }, 500);
}
  }

initMap(lat: number, lng: number) {
  const position = { lat, lng };

  if (!this.map) {
    this.map = new google.maps.Map(this.mapContainer.nativeElement, {
      zoom: 16,
      center: position,
    });
    this.marker = new google.maps.Marker({ position, map: this.map });
  } else {
    this.map.setCenter(position);
    this.marker.setPosition(position);
  }
}
}
