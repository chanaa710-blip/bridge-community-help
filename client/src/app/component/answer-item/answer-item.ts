import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Answer } from '../../classes/Answer';
import { TruncatePipe } from '../../pipes/truncate.pipe';
import { DatePipe } from '@angular/common';
import { UserService } from '../../services/UserService/user-service';

@Component({
  selector: 'app-answer-item',
  imports: [TruncatePipe, DatePipe],
  templateUrl: './answer-item.html',
  styleUrl: './answer-item.css',
})
export class AnswerItem {
  @Input() ans!: Answer;
  @Output() delete = new EventEmitter<string>(); 
  userService = inject(UserService);
  isExpanded: boolean = false;
  showMenu: boolean = false;

  get isMyMessage(): boolean {
    return this.ans.userId === this.userService.currentUser?.id;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
    this.showMenu = false;
  }

  onDelete(event: Event) {
    this.showMenu = false;
    if (confirm('האם למחוק את התגובה?')) {
      this.delete.emit(this.ans.id);
    }
  }

}
