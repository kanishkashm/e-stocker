import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { StockComponent } from './stock/stock.component';
import { SigninRedirectCallbackComponent } from './common/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from './common/signout-redirect-callback.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  { path: 'signin-callback', component: SigninRedirectCallbackComponent },
  { path: 'signout-callback', component: SignoutRedirectCallbackComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'stock', component: StockComponent },
  { path: '',   redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
