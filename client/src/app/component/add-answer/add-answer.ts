import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { UserService } from '../../services/UserService/user-service';
import { AnswerService } from '../../services/AnswerService/answer-service';
import { Answer } from '../../classes/Answer';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-answer',
  imports: [FormsModule],
  templateUrl: './add-answer.html',
  styleUrl: './add-answer.css',
})
export class AddAnswer {
  @Input() requestId: string = ''
  @Output() answerAdded = new EventEmitter<void>();
  answerService = inject(AnswerService);
  userService = inject(UserService);

  content: string = ''

  addAnswer() {
    const user = this.userService.currentUser;
    if (!user) {
      console.error('No user logged in');
      return;
    }
    const newAnswer:Answer={
      requestId: this.requestId,
      userId: user.id,
      userName: user.name,
      content: this.content
    }
    console.log('Payload to send:', newAnswer)
    this.answerService.addAnswer(newAnswer).subscribe({
      next: () => {
        this.content = '';
        this.answerAdded.emit();
      },
      error: (err) => {
        console.error('שגיאה בהוספת תשובה:', err);
      }
    });
  }
}

