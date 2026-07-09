import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { AnswerList } from '../answer-list/answer-list';
import { AddAnswer } from '../add-answer/add-answer';
import { Request } from '../../classes/Request';
import { RequestService } from '../../services/RequestService/request-service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { StatusPipe } from '../../pipes/status.pipe';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-request-details',
  imports: [AnswerList, AddAnswer, CommonModule, StatusPipe],
  templateUrl: './request-details.html',
  styleUrl: './request-details.css',
})
export class RequestDetails implements OnInit{
  request: Request | null = null;
  route = inject(ActivatedRoute);
  requestService = inject(RequestService);
  cdr = inject(ChangeDetectorRef);
  @ViewChild('ansList') ansList!: AnswerList;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.requestService.getRequestById(id).subscribe((request) => {
        this.request = request;
        this.cdr.detectChanges();
      });
    }
  }

  onAnswerAdded() {
    this.ansList.loadAnswers();
  }
}
