import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { initFlowbite } from 'flowbite';
import { ChannelListItemComponent } from './channel-list-item/channel-list-item.component';
import { ChannelListComponent } from './channel-list/channel-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    ChannelListItemComponent,
    ChannelListComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'microchat.services.channels.frontend.angular';

  ngOnInit(): void {
    initFlowbite();

    // On page load or when changing themes, best to add inline in `head` to avoid FOUC
    if (localStorage["theme"] === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
      document.documentElement.classList.add('dark')
    } else {
      document.documentElement.classList.remove('dark')
    }

    // Whenever the user explicitly chooses light mode
    localStorage["theme"] = 'light'

    // Whenever the user explicitly chooses dark mode
    localStorage["theme"] = 'dark'

    // Whenever the user explicitly chooses to respect the OS preference
    localStorage.removeItem('theme')
  }
}
