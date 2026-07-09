import { AfterViewInit, Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserRegister } from '../../classes/UserRegister';
import { UserService } from '../../services/UserService/user-service';
import { Router, RouterLink } from '@angular/router';

declare const google: any;
@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register implements AfterViewInit{
  userService = inject(UserService);
  myform: FormGroup = new FormGroup({})
  arr: Array<string> = []
  @ViewChild('addressInput') addressInput!: ElementRef;
  router = inject(Router);

  ngOnInit(): void {

    this.myform = new FormGroup({
      "name": new FormControl(null, [Validators.required, Validators.maxLength(20), Validators.minLength(2)]),
      "email": new FormControl(null, [Validators.required, Validators.email]),
      "phone": new FormControl(null, [Validators.required, Validators.pattern(/^\d{10}$/)]),
      "password": new FormControl(null, [Validators.required, Validators.minLength(6)]),
      "lat": new FormControl(null, [Validators.required]),
      "lng": new FormControl(null, [Validators.required])
    })
    this.arr = [...Object.keys(this.myform.controls).filter(f => f !== 'lat' && f !== 'lng')]
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

  register() {
    const formValues = this.myform.value;
    const newUser: UserRegister = {
      name: formValues.name,
      email: formValues.email,
      phone: formValues.phone,
      password: formValues.password,
      lat: formValues.lat,
      lng: formValues.lng
    }
    this.userService.register(newUser).subscribe({
      next: (response) => {
        console.log('משתמש נרשם בהצלחה:', response);
        alert('הרישום הצליח!');
        this.userService.saveAuthToStorage(response)
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error('שגיאה ברישום המשתמש:', err);
        if(err.status === 409) {
          alert('משתמש עם כתובת אימייל זו כבר רשום.');
          this.router.navigate(['/login']);
        }
      }
    });
  }


translations: { [key: string]: string } = {
  name: 'שם מלא',
  email: 'כתובת אימייל',
  phone: 'מספר טלפון',
  password: 'סיסמה מאובטחת'
};

setCurrentLocation() {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(
      position => {
        const lat = position.coords.latitude;
        const lng = position.coords.longitude;

        this.myform.patchValue({ lat, lng });

        const geocoder = new google.maps.Geocoder();
        geocoder.geocode({ location: { lat, lng } }, (results: any, status: any) => {
          if (status === 'OK' && results[0]) {

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
      { enableHighAccuracy: true } 
    );
  }
}
}