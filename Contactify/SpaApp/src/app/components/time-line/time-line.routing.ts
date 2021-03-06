import { Routes } from "@angular/router"
import { NewsFeedComponent } from "./news-feed/news-feed.component"
import { AuthGuard } from "../../core/guards"
import { ProfileComponent } from "../profile/profile.component"

export const timeLineRouting: Routes = [
  {path: '', component: NewsFeedComponent, canActivate: [AuthGuard], pathMatch: "full"}
]
