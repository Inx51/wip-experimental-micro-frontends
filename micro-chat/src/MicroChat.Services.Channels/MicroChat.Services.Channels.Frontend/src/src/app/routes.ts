import { Routes } from '@angular/router';
import { ChannelListComponent } from './channel-list/channel-list.component';
import { AppComponent } from './app.component';

const routeConfig: Routes = [
    {
      path: 'channel/:channel',
      component: ChannelListComponent,
      title: 'Channels'
    }
  ];
  
  export default routeConfig;