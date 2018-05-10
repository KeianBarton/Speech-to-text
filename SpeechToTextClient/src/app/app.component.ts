import { Component, ViewChild } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Access Microphone';
  stream: MediaStream;
  @ViewChild('audioElement') audioElement: any;

  leftchannel: any = new Array;
  rightchannel: any = new Array;
  recordingLength: number = 0;
  bufferSize: number = 2048;
  sampleRate: number = 0;

  WAVFile: Blob = null;

  play() {
    this.startRecording({ video: false, audio: true, maxLength: 10, debug: true });
  }

  startRecording(config) {
    var browser = <any>navigator;
    browser.getUserMedia = (browser.getUserMedia ||
      browser.webkitGetUserMedia ||
      browser.mozGetUserMedia ||
      browser.msGetUserMedia);

    if (browser.mediaDevices && browser.mediaDevices.getUserMedia) {

      this.stream = null;
      this.WAVFile = null;
      this.leftchannel = new Array;
      this.rightchannel = new Array;
      this.recordingLength = 0;

      browser.mediaDevices.getUserMedia(config)
        .then(stream => {
          this.stream = stream;
          this.handleSuccess();

        })
        .catch(error => console.log("An error has occured during recording"));
    } else {
      alert("Audio capture is not supported in this browser");
    }
  }

  stop() {
    let stream = this.stream;
    stream.getAudioTracks().forEach(track => track.stop());
    stream.getVideoTracks().forEach(track => track.stop());
    this.WAVFile = this.WAVProcessing();
    this.stream = null;
  }

  handleSuccess() {
    let stream = this.stream;
    let context = new AudioContext();
    let sampleRate = context.sampleRate;
    let volume = context.createGain();

    let source = context.createMediaStreamSource(stream);
    let bufferSize = this.bufferSize;
    this.sampleRate = sampleRate;

    source.connect(volume);

    let processor = context.createScriptProcessor(bufferSize, 2, 2);

    processor.onaudioprocess = this.generateSounds.bind(this, bufferSize);

    volume.connect(processor);
    processor.connect(context.destination);
  }

  generateSounds(this, bufferSize, e) {
    var left = e.inputBuffer.getChannelData(0);
    var right = e.inputBuffer.getChannelData(1);
    // we clone the samples

    this.leftchannel.push(new Float32Array(left));
    this.rightchannel.push(new Float32Array(right));
    this.recordingLength += bufferSize;
  }

  mergeBuffers(channelBuffer, recordingLength) {
    var result = new Float32Array(recordingLength);
    var offset = 0;
    var lng = channelBuffer.length;
    for (var i = 0; i < lng; i++) {
      var buffer = channelBuffer[i];
      result.set(buffer, offset);
      offset += buffer.length;
    }
    return result;
  }

  interleave(leftChannel, rightChannel) {
    var length = leftChannel.length + rightChannel.length;
    var result = new Float32Array(length);

    var inputIndex = 0;

    for (var index = 0; index < length;) {
      result[index++] = leftChannel[inputIndex];
      result[index++] = rightChannel[inputIndex];
      inputIndex++;
    }
    return result;
  }

  writeUTFBytes(view, offset, string) {
    var lng = string.length;
    for (var i = 0; i < lng; i++) {
      view.setUint8(offset + i, string.charCodeAt(i));
    }
  }

  WAVProcessing(): any {
    var leftBuffer = this.mergeBuffers(this.leftchannel, this.recordingLength);
    var rightBuffer = this.mergeBuffers(this.rightchannel, this.recordingLength);
    // we interleave both channels together
    var interleaved = this.interleave(leftBuffer, rightBuffer);

    // create the buffer and view to create the .WAV file
    var buffer = new ArrayBuffer(44 + interleaved.length * 2);
    var view = new DataView(buffer);

    // write the WAV container, check spec at: https://ccrma.stanford.edu/courses/422/projects/WaveFormat/
    // RIFF chunk descriptor
    this.writeUTFBytes(view, 0, 'RIFF');
    view.setUint32(4, 44 + interleaved.length * 2, true);
    this.writeUTFBytes(view, 8, 'WAVE');
    // FMT sub-chunk
    this.writeUTFBytes(view, 12, 'fmt ');
    view.setUint32(16, 16, true);
    view.setUint16(20, 1, true);
    // stereo (2 channels)
    view.setUint16(22, 2, true);
    view.setUint32(24, this.sampleRate, true);
    view.setUint32(28, this.sampleRate * 4, true);
    view.setUint16(32, 4, true);
    view.setUint16(34, 16, true);
    // data sub-chunk
    this.writeUTFBytes(view, 36, 'data');
    view.setUint32(40, interleaved.length * 2, true);

    // write the PCM samples
    var lng = interleaved.length;
    var index = 44;
    var volume = 1;
    for (var i = 0; i < lng; i++) {
      view.setInt16(index, interleaved[i] * (0x7FFF * volume), true);
      index += 2;
    }

    // our final binary blob that we can hand off
    var blob = new Blob([view], { type: 'audio/wav' });
    var url = window.URL.createObjectURL(blob);

    var a = document.createElement("a");
    document.body.appendChild(a);

    blob = blob;
    url = window.URL.createObjectURL(blob);
    a.href = url;
    a.download = "wavwob";
    a.click();
    window.URL.revokeObjectURL(url);
  };
}

  

