import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SnackResultDto } from 'src/app/models/Snacks/SnackResultDto';
import { SnacksService } from 'src/app/services/snacks.service';

@Component({
  selector: 'app-snacks',
  templateUrl: './snacks.component.html',
  styleUrls: ['./snacks.component.scss'],
})
export class SnacksComponent implements OnInit {
  snackList: Array<SnackResultDto> = new Array();
  snackToDelete: Number = 0;

  constructor(private toastr: ToastrService, private service: SnacksService) {}

  ngOnInit(): void {
    this.GetData();
  }

  GetData() {
    this.service.Get().subscribe((res) => {
      this.snackList = res;
    });
  }

  SetSnackToDelete(id: Number) {
    this.snackToDelete = id;
  }

  Delete() {
    this.service.Delete(this.snackToDelete).subscribe(
      (res) => {
        this.toastr.success('Usuario eliminado correctamente', 'Éxito');
        this.GetData();
      },
      (err) => {
        this.toastr.error(err.error, 'Error');
      }
    );
  }
}
