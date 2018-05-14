import { Component, OnInit, Input } from '@angular/core';
import { SpeechtotextService } from '../../../../shared/services/speechtotext.service';

@Component({
  selector: 'app-watson',
  templateUrl: './watson.component.html',
  styleUrls: ['./watson.component.css']
})
export class WatsonComponent implements OnInit {
  @Input() private wavBase64String = '';

  constructor(private _speechToTextService: SpeechtotextService) { }

  ngOnInit() {
  }

  submitWav64String(base64String: string, calbackType: any) {
    this._speechToTextService.postWAVWatson(base64String).subscribe(
      response => { console.log('Success'); }
    );
  }
}
