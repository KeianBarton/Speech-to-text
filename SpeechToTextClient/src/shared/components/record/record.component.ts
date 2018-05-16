import { Component, ViewChild, Output, EventEmitter, OnInit, NgZone } from '@angular/core';
import { AudioService } from '../../services/audio.service';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.css']
})
export class RecordComponent implements OnInit {

  @ViewChild('audioElement')
  private audioElement: any;
  private wavBase64String = '';
  private recording = false;
  private completed = false;
  private visulisationValues: number[] = null;
  private barWidth = 1;

  @Output() audioEmitter = new EventEmitter<string>();

  constructor(
    private _audioService: AudioService,
    private _ngZone: NgZone
  ) { }

  ngOnInit() {
    this._audioService.visulisationCallback = this.visualisationCallback.bind(this);
  }

  visualisationCallback(this, res) {
    const i = 0;
    this._ngZone.run(() => {
      const divider = 16;
      const widthDivision = res.length / divider;
      this.barWidth = Math.round((100 / widthDivision) - 2);
      this.visualisationValues = res.filter(function (value, index, Arr) {
        return index % divider === 0;
      });
    });
  }

  startRecording() {
    this.wavBase64String = '';
    this._audioService.startRecording({
      video: false,
      audio: true,
      maxLength: 10,
      debug: true
    });
    this.recording = true;
  }

  stopRecording() {
    this._audioService.stopRecording();
    this._audioService.callback = this.proccessAudioStringCallback.bind(this);
    this.recording = false;
    this.wavBase64String = this._audioService.processAudioString();
  }

  proccessAudioStringCallback(this, result) {
    this.wavBase64String = result;
    this.completed = true;
    this.audioEmitter.emit(this.wavBase64String);
  }


}
