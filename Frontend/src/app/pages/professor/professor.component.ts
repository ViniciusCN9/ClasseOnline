import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { faPen, faTrash } from '@fortawesome/free-solid-svg-icons';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Classe } from 'src/app/models/classeModel';
import { HttpService } from 'src/app/service/http.service';

@Component({
  selector: 'app-professor',
  templateUrl: './professor.component.html'
})
export class ProfessorComponent implements OnInit {
  public iconeUpdate = faPen
  public iconeDelete = faTrash
  public formClasse: FormGroup
  public carregando = false;
  public classes: Classe[] = []

  constructor(private modalService: NgbModal, private httpService: HttpService, private formBuilder: FormBuilder,) {
    this.formClasse = this.formBuilder.group({
      nome: [""]
    })
  }

  ngOnInit(): void {
    this.carregando = true
    this.httpService.getClasse()
      .subscribe(
        (response) => {
          this.carregando = false
          this.classes = response;
        }
      )
  }

  abrirModal(evento: any) {
    this.modalService.open(evento).result.then(
      () => {
        console.log(this.formClasse.get("nome")?.value)
        this.formClasse.reset()
      },
      () => {
        this.formClasse.reset()
        console.log(this.formClasse.get("nome")?.value)
      }
    )
  }
}
