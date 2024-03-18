import { Component } from '@angular/core';
import { ChannelListItemComponent } from '../channel-list-item/channel-list-item.component';
import { ChannelListItem } from '../channel-list-item/channelListItem';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'channel-list',
  standalone: true,
  imports: [
    ChannelListItemComponent,
    CommonModule
  ],
  templateUrl: './channel-list.component.html',
  styleUrl: './channel-list.component.css'
})
export class ChannelListComponent {

  channels: ChannelListItem[] = [
    {
      name:"Food",
      active:false,
      activeUsers:13,
      hasUnviewedMessages:false,
      image:"/assets/placeholders/images/icon-food-1.jpg",
      label:"Food"
    },
    {
      name:"Animals",
      active:false,
      activeUsers:0,
      hasUnviewedMessages:false,
      image:"/assets/placeholders/images/icon-animals-1.jpg",
      label:"Animals"
    },
    {
      name:"Gaming",
      active:false,
      activeUsers:3,
      hasUnviewedMessages:true,
      image:"/assets/placeholders/images/icon-gaming-1.jpg",
      label:"Gaming"
    },
    {
      name:"Cars",
      active:false,
      activeUsers:2,
      hasUnviewedMessages:false,
      image:"/assets/placeholders/images/icon-cars-1.png",
      label:"Cars"
    },
  ]

  updateActiveItem (label: string):void {
    this.channels.forEach(channel => {
      console.log(channel.label);
      if(channel.label == label) {
        channel.active = true;
      } else {
        channel.active = false;
      }
    });
  }

}
