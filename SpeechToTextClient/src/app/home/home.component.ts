import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  private title = 'Speech to Text Comparator';
  private WavBase64String = '';

  handleAudioChange(WavBase64String: string) {
    this.WavBase64String = null;
    this.WavBase64String = WavBase64String;
  }
}
