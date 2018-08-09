import { Component, OnInit, OnChanges } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Input } from "@angular/core";
@Component({
  selector: "app-show",
  templateUrl: "./show.component.html",
  styleUrls: ["./show.component.css"]
})
export class ShowComponent implements OnInit {
  @Input() id: string;
  public imageData: string;
  public loaded = false;
  private placeholderImage = "/assets/placeholder.png";
  constructor(private http: HttpClient) {
    this.imageData = this.placeholderImage;
  }

  ngOnInit() {
      this.id = localStorage.getItem("image");
      this.refresh();
  }

  private refresh() {
    this.http
      .get(`https://localhost:5001/api/imageData/` + this.id, { responseType: "text" })
      .subscribe(data => {
        console.log(data.toString());
        this.imageData = data.toString();
        this.loaded = true;
      });
  }

  onChanges() {
    this.refresh();
  }
}
