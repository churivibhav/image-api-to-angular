import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'image-app';
  imageid: string;

  onInit() {
    this.imageid = localStorage.getItem("image");
  }

  onNotify(message: string) {
    alert(message);
    this.imageid = message;
    localStorage.setItem("image", this.imageid);
  }
}
