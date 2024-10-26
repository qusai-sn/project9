import { Component } from '@angular/core';
import { UrlServiceService } from '../services/url-service.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-get-by-id',
  templateUrl: './get-by-id.component.html',
  styleUrl: './get-by-id.component.css'
})
export class GetByIdComponent {

  param :any
  ngOnInit() {
    this.param = this._activate.snapshot.paramMap.get('id11')
    console.log(" this.param", this.param)

    this.getById()
  }

  constructor(private _ser : UrlServiceService, private _activate : ActivatedRoute) { }

  yousef : any
  getById() {
    this._ser.geytAnumalById(this.param).subscribe((data) => {
      this.yousef = data
    })
  }
  deleteAnimal(id :any) {
    this._ser.deleteAnimal(id).subscribe(() => {
      alert("delete done ")
    })
  }
}
