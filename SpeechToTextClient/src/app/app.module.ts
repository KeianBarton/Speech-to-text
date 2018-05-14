import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { SpeechtotextService } from '../shared/services/speechtotext.service';
import { HttpClientModule } from '@angular/common/http';
import { RecordComponent } from '../shared/components/record/record.component';
import { HomeComponent } from './home/home.component';
import { WatsonComponent } from './home/results/watson/watson.component';
import { AzureComponent } from './home/results/azure/azure.component';
import { AwsComponent } from './home/results/aws/aws.component';

@NgModule({
  imports:      [ BrowserModule, FormsModule, HttpClientModule ],
  declarations: [ AppComponent, RecordComponent, HomeComponent, WatsonComponent, AzureComponent, AwsComponent ],
  bootstrap:    [ AppComponent ],
  providers:    [ SpeechtotextService ]
})
export class AppModule { }
