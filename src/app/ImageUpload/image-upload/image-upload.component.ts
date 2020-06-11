import { HttpClient } from '@angular/common/http';
import { ImageService } from './image.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {
  @Output()
  public imageSource: EventEmitter<any> = new EventEmitter<any>();
  private isUploadBtn = true;
constructor(private http: HttpClient) {
}  
  ngOnInit(): void {
  }
  // Proccesing image to send to server
  fileChange(event) {
  const fileList: FileList = event.target.files;
  if (fileList.length > 0) {
    const file: File = fileList[0];
    const formData: FormData = new FormData(); 
    formData.append('uploadFile', file, file.name);
    this.imageSource.emit(formData);
    console.log('file: ' + file);
  }
  } 
  }