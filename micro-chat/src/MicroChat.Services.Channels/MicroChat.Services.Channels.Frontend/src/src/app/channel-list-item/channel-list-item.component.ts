import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgIf } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'channel-list-item',
  standalone: true,
  imports: [
    CommonModule,
    NgIf,
    RouterModule
  ],
  templateUrl: './channel-list-item.component.html',
  styleUrl: './channel-list-item.component.css'
})
export class ChannelListItemComponent {

  @Input() label = "";
  @Input() image = "";
  @Input() activeUsers = 0;
  @Input() hasUnviewedMessages = false;
  @Input() active = false;
  @Input() id = 0;

  @Output() onActive = new EventEmitter<number>();

  public setActive(label: any): void {
    this.onActive.emit(label);
  }

}