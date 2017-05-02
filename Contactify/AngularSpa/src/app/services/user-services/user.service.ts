import {Injectable} from "@angular/core";
import {BaseService} from "../base.service";
import {Modal} from "angular2-modal/plugins/bootstrap";
import {HttpClient} from "../system-services/http-client.service";
import {Observable} from "rxjs/Observable";
import {UserViewModel} from "../../models/view-models/user.view-model";
import {Response} from "@angular/http";

@Injectable()

export class UserService extends BaseService {
  constructor(private httpClient: HttpClient, modal: Modal) {
    super(modal)
  }

  public getTopFiveFriendsByUserId(userId: number): Observable<UserViewModel[]> {
    return this.httpClient.get('user/get-top-five/' + userId, true)
      .map((response: Response) => response.json())
      .catch(this.handleError)
  }
}
