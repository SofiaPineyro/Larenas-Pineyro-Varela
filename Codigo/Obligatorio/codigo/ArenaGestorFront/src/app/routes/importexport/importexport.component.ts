import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ImportResult } from 'src/app/models/ImportResult';
import { ImportexportService } from 'src/app/services/importexport.service';

@Component({
  templateUrl: './importexport.component.html'
})
export class ImportexportComponent implements OnInit {

  path: string = "";
  method: string = "";

  methods: Array<string> = new Array()
  result?: ImportResult = undefined

  constructor(private service: ImportexportService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.GetMethods().subscribe(res => {
      this.methods = res;
    })
  }

  Importar() {
    this.service.Import(this.path, this.method).subscribe(res => {
      this.result = res
    }, err => {
      console.log(err)
    })
  }

  Exportar() {
    this.service.Export(this.path, this.method).subscribe((res: string) => {
      this.toastr.success(res)
    }, err => {
      console.log(err)
    })
  }

}
