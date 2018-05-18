import { Component, OnInit, Input } from '@angular/core';
import { SpeechtotextService } from '../../../../shared/services/speechtotext.service';

@Component({
  selector: 'app-aws',
  templateUrl: './aws.component.html',
  styleUrls: ['./aws.component.css']
})
export class AwsComponent implements OnInit {
  @Input() private wavBase64String = '';

  constructor(private _speechToTextService: SpeechtotextService) { }

  responseModel: any = null;
  error = false;

  ngOnInit() {
    this._speechToTextService.postWAVAzure(this.wavBase64String).subscribe(
      response => {
        this.responseModel = response;
      },
      err => {
        this.error = true;
      }
    );
  }

}
