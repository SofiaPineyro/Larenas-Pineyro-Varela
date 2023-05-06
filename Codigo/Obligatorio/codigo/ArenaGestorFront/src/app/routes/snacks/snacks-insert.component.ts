import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SnackInsertDto } from 'src/app/models/Snacks/SnackInsertDto';
import { SnacksService } from 'src/app/services/snacks.service';

@Component({
  selector: 'app-snacks',
  templateUrl: './snacks-form.component.html',
  styleUrls: ['./snacks.component.scss'],
})
export class SnacksInsertComponent implements OnInit {
  model: SnackInsertDto = new SnackInsertDto();

  name: String = '';
  description: String = '';
  price: Number = 0;

  constructor(
    private service: SnacksService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  Confirmar() {
    this.model.name = this.name;
    this.model.description = this.description;
    this.model.price = this.price;
    console.log(this.price);
    this.service.Insert(this.model).subscribe(
      (res) => {
        this.toastr.success('Snack agregado correctamente', 'Éxito');
        this.router.navigate(['/administracion/snacks']);
      },
      (err) => {
        this.toastr.error(err.error, 'Error');
      }
    );
  }
}
