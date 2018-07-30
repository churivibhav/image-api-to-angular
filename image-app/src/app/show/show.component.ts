import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "app-show",
  templateUrl: "./show.component.html",
  styleUrls: ["./show.component.css"]
})
export class ShowComponent implements OnInit {
  public imageData: string;
  public loaded = false;
  private placeholderImage = "/assets/placeholder.png";
  constructor(private http: HttpClient) {
    this.imageData = this.placeholderImage;
  }

  ngOnInit() {
    this.http
      .get("https://localhost:5001/api/images/test.png", { responseType: "text" })
      .subscribe(data => {
        console.log(data.toString());
        this.imageData = data.toString();
        this.loaded = true;
      });
  }
}
