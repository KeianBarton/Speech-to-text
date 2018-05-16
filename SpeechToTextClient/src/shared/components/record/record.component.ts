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
  private wavBase64String: string = '';

  private recording: boolean = false;
  private visulisationValues: number[] = null;
  private barWidth: number = 1;

  private readyToReRecord: boolean = true;


  @Output() audioEmitter = new EventEmitter<string>();

  constructor(
    private _audioService: AudioService,
    private _ngZone: NgZone
  ) { }

  ngOnInit() {
    this._audioService.visulisationCallback = this.visualisationCallback.bind(this);
  }

  visualisationCallback(this, res) {
    var i = 0;
    this._ngZone.run(() => {
      var divider = 16;
      var widthDivision = res.length / divider;
      this.barWidth = Math.round((100 / widthDivision) - 2);
      this.visualisationValues = res.filter(function (value, index, Arr) {
        return index % divider == 0;
      });;
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
    this._audioService.callback = this.proccessAudioStringCallback.bind(this)
    this.recording = false;
    this.wavBase64String = this._audioService.processAudioString();
  }

  proccessAudioStringCallback(this, result) {
    this.wavBase64String = result;
    this.readyToReRecord = false;
    this.audioEmitter.emit(this.wavBase64String);
  }


}
