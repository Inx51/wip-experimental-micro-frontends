import { Component, inject } from '@angular/core';
import { ChannelListItemComponent } from '../channel-list-item/channel-list-item.component';
import { ChannelListItem } from '../channel-list-item/channelListItem';
import { CommonModule } from '@angular/common';
import { ChannelsserviceService } from '../channelsservice.service';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Settings } from '../../settings';

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

  channels: ChannelListItem[] | undefined;
  hubConnection: HubConnection;
  channelsService = inject(ChannelsserviceService);

  constructor() {
    this.channelsService.getChannels().then(channels => {
      this.channels = channels
    });

    this.hubConnection = new HubConnectionBuilder().withUrl(`${Settings.ChannelsBaseEndpoint}/hubs/channel`).build();
    
    this.hubConnection.on('ChannelCreated', (message) => {
      let c:ChannelListItem = {
        id:message.id,
        name:message.name,
        image:message.image,
        active:false,
        activeUsers:0,
        hasUnviewedMessages:false
      };
      this.channels?.push(c)
    });
    this.hubConnection.on('ChannelDeleted', (message) => {
      this.channels = this.channels?.filter(c => c.id != message);
    });

    this.hubConnection.start().catch((err) => document.write(err));
  }

  updateActiveItem(id: number): void {
    if (this.channels === undefined) {
      console.debug("Failed with channels!");
    }
    else {
      this.channels.forEach(channel => {
        if (channel.id == id) {
          channel.active = true;
        } else {
          channel.active = false;
        }
      });
    }
  }

}
