import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-professor',
  templateUrl: './professor.component.html'
})
export class ProfessorComponent implements OnInit {
  public formClasse: FormGroup;

  constructor(private modalService: NgbModal, private formBuilder: FormBuilder) {
    this.formClasse = this.formBuilder.group({
      nome: [""]
    })
  }

  ngOnInit(): void {
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
