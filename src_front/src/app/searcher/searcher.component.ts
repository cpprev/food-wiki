import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { FoodDescription } from 'src/dataModel/foodDescription';

@Component({
  selector: 'app-searcher',
  templateUrl: './searcher.component.html',
  styleUrls: ['./searcher.component.scss']
})
export class SearcherComponent implements OnInit {
  resultDataList: FoodDescription[];
  subscription: Subscription;

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
  }

  async sendRequest(pattern: string) : Promise<FoodDescription[]> {
    const t = await this.http.get<FoodDescription[]>('https://localhost:5000/Food/getWithPattern' + '?pattern=' + pattern).toPromise();
    return t;
  }

  async searchQueryOnDataSource($event: any): Promise<any> {
    if ($event.target.value.length >= 1) {
      this.resultDataList = [];
      this.resultDataList = await this.sendRequest($event.target.value);
      //alert("li len : " + this.resultDataList)
    } else {
      this.resultDataList = [];
    }
  }

  selectedResult(result: any): void {
    /*this.userInput.nativeElement.value = result.originalText;
    this.userSelectedResult.emit({
      target: (result || null)
    });*/

    this.resultDataList = [];
  }
}
