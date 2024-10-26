import { Component } from '@angular/core';
import { UrlServiceService } from '../services/url-service.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-update-info',
  templateUrl: './update-info.component.html',
  styleUrl: './update-info.component.css'
})
export class UpdateInfoComponent {
  param :any
  ngOnInit() {

    this.param = this._active.snapshot.paramMap.get('id')


  }
  constructor(private _ser : UrlServiceService , private _active : ActivatedRoute) {



  }
  updateAnimal(data: any) {

    var form = new FormData()
    for (let key in data) {

      form.append(key, data[key])
    }
    this._ser.updateAnimal(this.param , form ).subscribe(() => {

      alert("updated Sucssfule")
    })

  }
}
