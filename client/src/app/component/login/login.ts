import { AfterViewInit, Component, ElementRef, inject, ViewChild } from '@angular/core';
import { UserService } from '../../services/UserService/user-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})

export class Login implements AfterViewInit {
  userService = inject(UserService);
  myform: FormGroup = new FormGroup({})
  router = inject(Router);
  @ViewChild('addressInput') addressInput!: ElementRef;

  ngOnInit(): void {

    this.myform = new FormGroup({
      "email": new FormControl(null, [Validators.required, Validators.email]),
      "password": new FormControl(null, [Validators.required, Validators.minLength(6)]),
      "lat": new FormControl(null),
      "lng": new FormControl(null)
    })
  }

  ngAfterViewInit(): void {
    this.initAutocomplete();
  }

  initAutocomplete() {
    const autocomplete = new google.maps.places.Autocomplete(this.addressInput.nativeElement, {
      fields: ['geometry', 'formatted_address']
    });

    autocomplete.addListener('place_changed', () => {
      const place = autocomplete.getPlace();

      if (place.geometry && place.geometry.location) {
        this.myform.patchValue({
          lat: place.geometry.location.lat(),
          lng: place.geometry.location.lng()
        });

        console.log('מיקום נבחר:', place.geometry.location.lat(), place.geometry.location.lng());
      }
    });
  }

  login() {
    if (this.myform.valid) {
      this.userService.login(this.myform.controls['email'].value, this.myform.controls['password'].value).subscribe({
        next: (response) => {
          console.log('התחברות הצליחה:', response);

          this.userService.saveAuthToStorage(response);

          const lat = this.myform.value.lat;
          const lng = this.myform.value.lng;

          if (lat && lng) {
            this.userService.updateLocation(response.user.id!, lat, lng).subscribe({
              next: () => {
                console.log('המיקום עודכן בשרת בהצלחה');

                const updatedUser = { ...response.user, lat, lng };

                this.userService.saveUserToStorage(updatedUser);

                alert('התחברת ועדכנת מיקום בהצלחה!');
                this.router.navigate(['/']);
              },
              error: (err) => {
                console.error('שגיאה בעדכון המיקום בשרת:', err);
                this.router.navigate(['/']);
              }
            });
          } else {
            alert('התחברת בהצלחה!');
            this.router.navigate(['/']);
          }
        },
        error: (err) => {
          console.error('שגיאה בהתחברות:', err);
          if (err.status === 401) {
            alert('אימייל או סיסמה שגויים. אנא נסה שוב.');
          } else {
            alert('אירעה שגיאה בהתחברות. אנא נסה שוב מאוחר יותר.');
          }
        }
      });
    }
  }

  setCurrentLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        position => {
          const lat = position.coords.latitude;
          const lng = position.coords.longitude;

          this.myform.patchValue({ lat, lng });

          this.myform.get('lat')?.markAsDirty();
          this.myform.get('lng')?.markAsDirty();
          this.myform.get('lat')?.markAsTouched();
          this.myform.get('lng')?.markAsTouched();

          this.myform.updateValueAndValidity();

          const geocoder = new google.maps.Geocoder();
          geocoder.geocode({ location: { lat, lng } }, (results: any, status: any) => {
            if (status === 'OK' && results && results[0]) {
              this.addressInput.nativeElement.value = results[0].formatted_address;
            } else {
              this.addressInput.nativeElement.value = 'מיקום נוכחי זוהה בהצלחה';
            }
          });
        },
        error => {
          console.error('שגיאה בקבלת המיקום:', error);
          alert('לא ניתן לקבל את המיקום. אנא ודאי שנתת הרשאת מיקום בדפדפן.');
        },
        { enableHighAccuracy: true } 
      );
    }
  }
}