import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  validationErrors: any;

  constructor(private http: HttpClient) {}

  // tslint:disable-next-line: typedef
  ngOnInit() {}

  // tslint:disable-next-line: typedef
  get404Error() {
    this.http.get(this.baseUrl + 'products/42').subscribe(
      (response) => {
        console.log(response);
      },
      // tslint:disable-next-line: no-shadowed-variable
      (error) => {
        console.log(error);
      }
    );
  }

  // tslint:disable-next-line: typedef
  get500Error() {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe(
      (response) => {
        console.log(response);
      },
      // tslint:disable-next-line: no-shadowed-variable
      (error) => {
        console.log(error);
      }
    );
  }

  // tslint:disable-next-line: typedef
  get400Error() {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe(
      (response) => {
        console.log(response);
      },
      // tslint:disable-next-line: no-shadowed-variable
      (error) => {
        console.log(error);
      }
    );
  }

  // tslint:disable-next-line: typedef
  get400ValidationError() {
    this.http.get(this.baseUrl + 'products/fortytwo').subscribe(
      (response) => {
        console.log(response);
      },
      // tslint:disable-next-line: no-shadowed-variable
      (error) => {
        console.log(error);
        this.validationErrors = error.errors;
      }
    );
  }
}
