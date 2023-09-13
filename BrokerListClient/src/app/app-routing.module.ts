import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageBrokerlistComponent } from './features/page-brokerlist/page-brokerlist.component';

const routes: Routes = [
  {
    path: '',
    component: PageBrokerlistComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
