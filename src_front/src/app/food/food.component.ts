import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FoodDescription } from 'src/dataModel/foodDescription';

@Component({
  selector: 'app-food',
  templateUrl: './food.component.html',
  styleUrls: ['./food.component.css']
})
export class FoodComponent implements OnInit {

  public food: FoodDescription;
  public foodName: string;

  constructor(private route: ActivatedRoute, private http: HttpClient) {
  }

  async ngOnInit(): Promise<any> {
    this.route.queryParams.subscribe(params => {
      this.foodName = params['name'];
    });

    this.food = await this.sendRequest();
  }

  async sendRequest() : Promise<FoodDescription> {
    const desc = await this.http.get<FoodDescription>('https://localhost:5000/Food/getByName/' + this.foodName).toPromise();
    return desc;
  }

}
