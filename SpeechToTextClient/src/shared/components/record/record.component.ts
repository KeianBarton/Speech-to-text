import { Component, ViewChild, Output, EventEmitter } from '@angular/core';
import { AudioService } from '../../services/audio.service';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.css']
})
export class RecordComponent {

  @ViewChild('audioElement')
  private audioElement: any;

  private wavBase64String = '';
  @Output() audioEmitter = new EventEmitter<string>();

  constructor(
    private _audioService: AudioService
  ) {}

  startRecording() {
    this.wavBase64String = '';
    this._audioService.startRecording({
      video: false,
      audio: true,
      maxLength: 10,
      debug: true
    });
  }

  stopRecording() {
    this._audioService.stopRecording();
    this.wavBase64String = this._audioService.getAudioString();
  }
}
