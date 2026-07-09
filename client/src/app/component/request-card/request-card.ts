import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Request } from '../../classes/Request';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TruncatePipe } from '../../pipes/truncate.pipe';
import { RequestService } from '../../services/RequestService/request-service';
import { RequestStatus } from '../../classes/RequestStatus';

@Component({
  selector: 'app-request-card',
  imports: [CommonModule, RouterModule, TruncatePipe],
  templateUrl: './request-card.html',
  styleUrl: './request-card.css',
})
export class RequestCard {
  requestService = inject(RequestService);
  @Input() request: Request = null!
  @Input() showActions: boolean = false;
  @Input() showDeleted: boolean = false;
  isExpanded: boolean = false;
  @Output() statusChanged = new EventEmitter<RequestStatus>();
  @Output() delete = new EventEmitter<string>();
  RequestStatus = RequestStatus;
  
  updateStatus(status: RequestStatus) {
    this.statusChanged.emit(status);
  }
  toggleExpand() {
    this.isExpanded = !this.isExpanded;
  }


  deleteRequest() {
    if (confirm('האם את בטוחה שאת רוצה למחוק את הבקשה?')) {
      this.delete.emit(this.request.id);
    }
  }

  

}
