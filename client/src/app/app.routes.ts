import { Routes } from '@angular/router';
import { RequestDashboard } from './component/request-dashboard/request-dashboard';
import { Map } from './component/map/map';
import { Login } from './component/login/login';
import { Register } from './component/register/register';
import { AddRequest } from './component/add-request/add-request';
import { RequestDetails } from './component/request-details/request-details';
import { UserProfile } from './component/user-profile/user-profile';

export const routes: Routes = [
    {path: '', component:RequestDashboard},
    {path: 'dashboard', component:RequestDashboard},
    {path: 'request-details/:id', component:RequestDetails},
    {path: 'map', component:Map},
    {path: 'login', component:Login},
    {path: 'register', component:Register},
    {path: 'user-profile', component:UserProfile},
    {path: 'add-request', component:AddRequest},
];
