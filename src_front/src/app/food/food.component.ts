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

  constructor(private router: Router, private route: ActivatedRoute, private http: HttpClient) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.foodName = params['name'];
      this.sendRequest().then((value) => {
        this.food = value;
      },
      (error) => {
        // TODO Later, redirect to a "notFound" component
        this.router.navigate(['/home']);
      });
     });
  }

  async sendRequest() : Promise<FoodDescription> {
    const desc = await this.http.get<FoodDescription>('https://localhost:5000/Food/getByName/' + this.foodName).toPromise();
    return desc;
  }

}
