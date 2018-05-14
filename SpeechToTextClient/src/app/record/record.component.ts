import { Component, ViewChild } from '@angular/core';
import { SpeechtotextService } from '../../shared/services/speechtotext.service';
import { RecordService } from '../../shared/services/record.service';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.css']
})
export class RecordComponent {

  @ViewChild('audioElement')
  private audioElement: any;

  private stream: MediaStream;
  private WAVFile: Blob = null;

  constructor(
    private _speechToTextService: SpeechtotextService,
    private _recordService: RecordService
  ) {}

  play() {
    this.WAVFile = null;
    this._recordService.startRecording({
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
    this.WAVFile = this._recordService.WAVProcessing();
    this.stream = null;
  }

  submitWav64String(base64String: string, calbackType: any) {
    this._speechToTextService.postWAVWatson(base64String).subscribe(
      response => { console.log('Success'); }
    );
  }
}
