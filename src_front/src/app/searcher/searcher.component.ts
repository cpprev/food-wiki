import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { FoodDescription } from 'src/dataModel/foodDescription';
import { Router } from '@angular/router';

@Component({
  selector: 'app-searcher',
  templateUrl: './searcher.component.html',
  styleUrls: ['./searcher.component.scss']
})
export class SearcherComponent implements OnInit {
  nameList: string[];
  subscription: Subscription;
  currentPattern: string;

  constructor(private router: Router, private http: HttpClient) {
  }

  ngOnInit() {
  }

  async sendRequest(pattern: string) : Promise<string[]> {
    const t = await this.http.get<string[]>('https://localhost:5000/Food/getWithPattern' + '?pattern=' + pattern).toPromise();
    return t;
  }

  async searchQueryOnDataSource($event: any): Promise<any> {
    this.currentPattern = $event.target.value;

    if ($event.target.value.length >= 1) {
      this.nameList = [];
      this.nameList = await this.sendRequest($event.target.value);
    } else {
      this.nameList = [];
    }
  }

  selectedResult(name: string): void {
    if (name.length > 0) {
      this.router.navigate(['/food', name]);
      this.nameList = [];
    }
  }
}
