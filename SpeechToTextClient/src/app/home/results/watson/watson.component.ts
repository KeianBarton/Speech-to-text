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

  responseModel: any = null;
  error = false;

  ngOnInit() {
    this._speechToTextService.postWAVWatson(this.wavBase64String).subscribe(
      response => {
        this.responseModel = response;
      },
      err => {
        this.error = true;
      }
    );
  }

}
