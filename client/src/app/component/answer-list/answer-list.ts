import { Component, inject, Input } from '@angular/core';
import { AnswerService } from '../../services/AnswerService/answer-service';
import { Answer } from '../../classes/Answer';
import { AnswerItem } from '../answer-item/answer-item';
import { signal } from '@angular/core';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-answer-list',
  imports: [AnswerItem, DatePipe],
  templateUrl: './answer-list.html',
  styleUrl: './answer-list.css',
})
export class AnswerList {
  @Input() requestId: string = '';
  @Input() requestOwnerId: string = '';
  answerService = inject(AnswerService);
  answers=signal<Array<Answer>>([]);

  ngOnInit(): void {
    this.loadAnswers(); 
  }

  loadAnswers(): void {
    if (this.requestId) {
      this.answerService.getAnswersByRequestId(this.requestId).subscribe({
        next: (data) => {
          console.log('Received answers data:', data);
          const sortedData = [...data].sort((a, b) => 
          new Date(a.createdAt ?? new Date()).getTime() - new Date(b.createdAt ?? new Date()).getTime()
        );
        
        this.answers.set(sortedData);
          console.log('Fetched answers:', this.answers());
        },
        error: (err) => {
          console.error('Error fetching answers:', err);
        }
      });
    }
  }

 isNewDay(currentDate: any, previousDate: any): boolean {
  if (!currentDate) return false;
  if (!previousDate) return true;

  // הפיכה ל-Date בוודאות, גם אם זה string או Date אובייקט
  const d1 = new Date(currentDate);
  const d2 = new Date(previousDate);

  // בדיקה אם התאריכים תקינים
  if (isNaN(d1.getTime()) || isNaN(d2.getTime())) return false;

  return d1.toDateString() !== d2.toDateString();
}

isToday(date: any): boolean {
  if (!date) return false;
  const today = new Date();
  const d = new Date(date);
  
  return d.toDateString() === today.toDateString();
}

handleDelete(answerId: string): void {
  this.answerService.deleteAnswer(answerId).subscribe({
    next: () => {
      this.loadAnswers();
    },
    error: (err) => {
      console.error('שגיאה במחיקת תשובה:', err);
    }
  });
}
}

  