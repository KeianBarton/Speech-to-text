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

  private stream: MediaStream;
  private wavBase64String = '';
  @Output() audioEmitter = new EventEmitter<string>();

  constructor(
    private _audioService: AudioService
  ) {}

  record() {
    this.wavBase64String = '';
    this._audioService.startRecording({
      video: false,
      audio: true,
      maxLength: 10,
      debug: true
    });
  }

  stop() {
    const stream = this.stream;
    stream.getAudioTracks().forEach(track => track.stop());
    stream.getVideoTracks().forEach(track => track.stop());
    this.wavBase64String = this._audioService.WAVProcessing();
    this.stream = null;
  }
}
