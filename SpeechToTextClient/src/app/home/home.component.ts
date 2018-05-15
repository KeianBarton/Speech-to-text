import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  private title = 'Speech to Text Comparator';
  private wavBase64String :string = null;

  handleAudioChange(WavBase64String: string) {
    this.wavBase64String = null;
    this.wavBase64String = WavBase64String;
  }
}
