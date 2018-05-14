import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-aws',
  templateUrl: './aws.component.html',
  styleUrls: ['./aws.component.css']
})
export class AwsComponent implements OnInit {
  @Input() private wavBase64String = '';

  constructor() { }

  ngOnInit() {
  }

}
