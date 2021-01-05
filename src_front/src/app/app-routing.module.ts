import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { FoodComponent } from './food/food.component';
import { SearcherComponent } from './searcher/searcher.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full'},
  { path: 'home', component: SearcherComponent },
  { path: 'about', component: AboutComponent },
  { path: 'food/:name', component: FoodComponent },
  // TODO Make a page not found component
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
