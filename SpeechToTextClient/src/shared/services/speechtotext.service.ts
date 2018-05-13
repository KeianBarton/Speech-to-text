import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';


@Injectable({
  providedIn: 'root'
})
export class SpeechtotextService {

  _apiRoot = "http://localhost:5000/api/speechToText/"
  constructor(private _http : HttpClient) { }

  postWAVAzure(wavBlob : any): Observable<any> {

    const body = {base64String : wavBlob}
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const options = {headers: headers};

    return this._http.post<any>(this._apiRoot + "parseAzure", body, options)
      .do(data => {
      })
      .catch(this.handleError);
  }

  postWAVWatson(wavBlob : any): Observable<any> {

    const body = JSON.stringify({Base64String : wavBlob});
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const options = {headers: headers};

    return this._http.post<any>(this._apiRoot + "parsewatson", body, options)
      .do(data => {
      })
      .catch(this.handleError);
  }

  private handleError(err: HttpErrorResponse) {
    console.log(err.message);
    return Observable.throw(err.message);
  }

  
}
