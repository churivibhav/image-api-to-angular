import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppComponent } from "./app.component";
import { UploadComponent } from "./upload/upload.component";
import { HttpClientModule } from "@angular/common/http";
import { ShowComponent } from './show/show.component';

@NgModule({
  declarations: [AppComponent, UploadComponent, ShowComponent],
  imports: [BrowserModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
