import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { SpeechtotextService } from '../shared/services/speechtotext.service';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  imports:      [ BrowserModule, FormsModule, HttpClientModule ],
  declarations: [ AppComponent],
  bootstrap:    [ AppComponent ],
  providers:    [ SpeechtotextService ]
})
export class AppModule { }
