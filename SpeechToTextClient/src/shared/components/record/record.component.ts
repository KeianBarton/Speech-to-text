import { Component, ViewChild, Output, EventEmitter, OnInit } from '@angular/core';
import { AudioService } from '../../services/audio.service';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.css']
})
export class RecordComponent implements OnInit{

  @ViewChild('audioElement')
  private audioElement: any;
  private wavBase64String = '';
  private recording: boolean = false;
  private volumeChannel1 : number = 0;
  private volumeChannel2 : number = 0;

  @Output() audioEmitter = new EventEmitter<string>();

  constructor(
    private _audioService: AudioService
  ) {}

  ngOnInit(){
    this._audioService.visulisationCallback = function(channel1, channel2){
      this.volumeChannel1 = channel1;
      this.volumeChannel2 = channel2;
      console.log("Channel 1: " + channel1 + "   Channel 2: " + channel2)
    }
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

  proccessAudioStringCallback(this, result){
    this.wavBase64String = result;
    this.audioEmitter.emit(this.wavBase64String);
  }


}
