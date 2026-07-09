import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { PickerComponent } from '@ctrl/ngx-emoji-mart';

import { RequestService } from '../../services/RequestService/request-service';
import { UserService } from '../../services/UserService/user-service';
import { CategoryService } from '../../services/CategoryService/category-service';

import { Request } from '../../classes/Request';
import { Category } from '../../classes/Category';
import { RequestStatus } from '../../classes/RequestStatus';

@Component({
  selector: 'app-add-request',
  standalone: true,
  imports: [ReactiveFormsModule, PickerComponent],
  templateUrl: './add-request.html',
  styleUrl: './add-request.css'
})
export class AddRequest {

  requestsService = inject(RequestService);
  userService = inject(UserService);
  categoryService = inject(CategoryService);
  router = inject(Router);

  categories: Category[] = [];

  isAddingNewCategory = false;
  showEmojiPicker = false;

  myform: FormGroup = new FormGroup({});

  ngOnInit(): void {

    if (!this.userService.currentUser) {
      alert("לא נרשמת למערכת");
      this.router.navigate(['/register']);
      return;
    }

    this.loadCategories();

    this.myform = new FormGroup({
      userId: new FormControl(this.userService.currentUser.id),
      userName: new FormControl(this.userService.currentUser.name),
      title: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50)
      ]),

      content: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(500)
      ]),

      lat: new FormControl(this.userService.currentUser.lat),
      lng: new FormControl(this.userService.currentUser.lng),
      categoryId: new FormControl(null, Validators.required),
      newCategoryName: new FormControl(''),
      newCategoryDescription: new FormControl(''),
      newCategoryIcon: new FormControl('')
    });

  }

  loadCategories() {
    this.categoryService.getAllCategories().subscribe({  
      next: data =>{
          console.log('Categories fetched successfully:', data); this.categories = data; }
    });
  }

  onCategoryChange(event: Event) {
    const value = (event.target as HTMLSelectElement).value;
    this.isAddingNewCategory = value === 'new';

    const nameControl = this.myform.get('newCategoryName');
    const iconControl = this.myform.get('newCategoryIcon');

    if (this.isAddingNewCategory) {
      nameControl?.setValidators([Validators.required, Validators.minLength(2)]);
      iconControl?.setValidators([Validators.required]);
    } else {
      nameControl?.clearValidators();
      iconControl?.clearValidators();
      this.showEmojiPicker = false;
    }

    nameControl?.updateValueAndValidity();
    iconControl?.updateValueAndValidity();
  }

  toggleEmojiPicker() {
    this.showEmojiPicker = !this.showEmojiPicker;
  }

  selectEmoji(event: any) {

    this.myform.patchValue({
      newCategoryIcon: event.emoji.native
    });

    this.showEmojiPicker = false;

  }

async addRequest() {
    if (this.myform.invalid)
      return;

    try {
      // משתנה שיחזיק את ה-ID הסופי שיישלח לשרת
      let finalCategoryId = this.myform.value.categoryId;

      if (this.isAddingNewCategory) {
        const category: Category = {
          name: this.myform.value.newCategoryName,
          icon: this.myform.value.newCategoryIcon
        };

        // קריאה לשרת ליצירת קטגוריה וקבלת ה-Guid שלה
        const serverResponse = await firstValueFrom(this.categoryService.add(category));
        
        console.log('תגובת השרת לאחר יצירת קטגוריה:', serverResponse);

        // הגנה מפני מצב שבו השרת מחזיר אובייקט { id: "..." } או מחרוזת ישירה
        if (serverResponse && typeof serverResponse === 'object' && 'id' in serverResponse) {
          finalCategoryId = (serverResponse as any).id;
        } else {
          finalCategoryId = serverResponse; 
        }
      }

      // וידוא אחרון שה-ID שחזר לא ריק או משובש לפני שבונים את הבקשה
      if (!finalCategoryId || finalCategoryId === 'new') {
        alert("חלה שגיאה בקבלת מזהה הקטגוריה החדשה מהשרת.");
        return;
      }

      const request: Request = {
        userId: this.myform.value.userId,
        userName: this.myform.value.userName,
        title: this.myform.value.title,
        content: this.myform.value.content,
        lat: this.myform.value.lat,
        lng: this.myform.value.lng,
        categoryId: finalCategoryId, // שימוש ב-ID המאומת והנקי
        status: RequestStatus.Open
      };

      console.log('שולח בקשה סופית לשרת עם האובייקט:', request);

      this.requestsService.addRequest(request).subscribe({
        next: () => {
          alert("הבקשה נוספה בהצלחה");
          this.router.navigate(['/dashboard']);
        },
        error: err => {
          console.error('שגיאה בשמירת הבקשה בשרת:', err);
          alert("אירעה שגיאה בשמירת הבקשה, בדקי את ה-Network לשגיאה המלאה");
        }
      });
    }
    catch (err) {
      console.error('שגיאה כללית בתהליך:', err);
      alert("אירעה שגיאה ביצירת הקטגוריה");
    }
  }
}