import {Routes, RouterModule} from "@angular/router";
import {ModuleWithProviders} from "@angular/core";
import {LoginComponent} from "./components/login/login.component";
import {RegisterComponent} from "./components/register/register.component";
import {HomeComponent} from "./components/time-line/home/home.component";
import {AuthGuard} from "./guards/auth.guard";
import {IsLoggedGuard} from "./guards/is-logged.guard";

const routes: Routes = [
  {path: 'login', component: LoginComponent, canActivate: [IsLoggedGuard]},
  {path: 'register', component: RegisterComponent, canActivate: [IsLoggedGuard]},
  {path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  // otherwise redirect to login
  {path: '**', redirectTo: 'login'}
];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, {useHash: true});
