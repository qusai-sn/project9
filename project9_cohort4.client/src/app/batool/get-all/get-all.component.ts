import { Component } from '@angular/core';
import { UrlServiceService } from '../services/url-service.service';

@Component({
  selector: 'app-get-all',
  templateUrl: './get-all.component.html',
  styleUrl: './get-all.component.css'
})
export class GetAllComponent {

  ngOnInit() {
    this.getAll();
  }

  constructor(private _ser : UrlServiceService) {

  }
  dataArray: any
  getAll() {

    this._ser.getAllAniamls().subscribe( (data)=>{
      this.dataArray = data
    })
  }


}
