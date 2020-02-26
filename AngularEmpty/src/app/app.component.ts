import { Component } from '@angular/core';
import{UserManager} from 'oidc-client'
import { getLocaleDateFormat } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngularEmpty';
  //user:UserManager;
  config = {
    authority: "http://localhost:5000",
    client_id: "client_js",
    redirect_uri: "http://localhost:5003/callback",
    response_type: "code",
    scope:"openid profile ApiOne",
    post_logout_redirect_uri : "http://localhost:5003/index.html",
};
user = new UserManager(this.config);
  
  login(){
   this.user.signinRedirect();
    
  }
  api(){
    this.user.getUser().then(function (user) {
      var url = "http://localhost:5001/identity";

      var xhr = new XMLHttpRequest();
      xhr.open("GET", url);
      xhr.onload = function () {
         console.log("onload")
      }
      xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
      xhr.send();
  });
  }
  logout(){
     this.user.signoutRedirect();
  }
  log() {
    console.log("Called log");
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        else{
          console.log("Called log");
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
  }
    GetData(){
  //   this.user.getUser().then(function (user) {
  //     if (user) {
  //         console.log( user.profile);
  //     }
  //     else {
  //         console.log("User not logged in");
  //     }
  // });
  console.log(this.user);
}

}
