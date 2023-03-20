import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotifierService } from 'angular-notifier';
import { Classe } from 'src/app/models/classeModel';
import { HttpService } from 'src/app/service/http.service';

@Component({
  selector: 'app-aluno',
  templateUrl: './aluno.component.html'
})
export class AlunoComponent implements OnInit {
  public carregando = false;
  public formEntrarClasse: FormGroup
  public classes: Classe[] = []

  constructor(private notifierService: NotifierService, private modalService: NgbModal, private httpService: HttpService, private formBuilder: FormBuilder) {
    this.formEntrarClasse = this.formBuilder.group({
      codigo: [""]
    })
  }

  ngOnInit(): void {
    this.carregando = true
    this.httpService.getClasses()
      .subscribe(
        (response) => {
          this.carregando = false
          this.classes = response;
        }
      )
  }

  entrarClasseModal(entrarModal: any) {
    this.modalService.open(entrarModal).result.then(
      () => this.httpService.registrarAluno(this.formEntrarClasse.get("codigo")?.value).subscribe(
        (result) => result == true ? location.reload() : this.notifierService.notify("warning", "Aluno já registrado na classe!"),
        () => this.notifierService.notify("error", "Erro ao registrar aluno na classe!")
      )
    )
    this.formEntrarClasse.reset()
  }

}
