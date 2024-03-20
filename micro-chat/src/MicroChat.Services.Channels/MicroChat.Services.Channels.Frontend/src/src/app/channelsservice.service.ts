import { Injectable } from '@angular/core';
import { Settings } from '../settings'
import { ChannelListItem } from './channel-list-item/channelListItem';

@Injectable({
  providedIn: 'root'
})
export class ChannelsserviceService {

  constructor() { }

  async getChannels(): Promise<ChannelListItem[]> {
    const data = await fetch(`${Settings.ChannelsBaseEndpoint}/channels`);
    return await data.json() ?? [];
  }
}
