import { Component } from '@angular/core';
import { UrlServiceService } from '../services/url-service.service';
import { from } from 'rxjs';

@Component({
  selector: 'app-add-animal',
  templateUrl: './add-animal.component.html',
  styleUrl: './add-animal.component.css'
})
export class AddAnimalComponent {


  ngOnInit() { }
  constructor(private _ser : UrlServiceService) { }

  addAnimal(data :any) {
    var form = new FormData();

    for (let key in data) {
      form.append(key, data[key])
    }

    this._ser.addAnimal(form).subscribe((data) => {
      alert("animal is added successfully")
    })
  }

}
