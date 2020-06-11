import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) { }
// Sending image to server
  public uploadImage(image: File) {
    const formData = new FormData();

    formData.append('image', image);

    return this.http.post('http://localhost:52907/api/WebSiteUser', formData);
  }
}
