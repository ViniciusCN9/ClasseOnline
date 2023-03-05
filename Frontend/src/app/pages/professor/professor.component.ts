import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { faPen, faTrash } from '@fortawesome/free-solid-svg-icons';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotifierService } from 'angular-notifier';
import { Classe } from 'src/app/models/classeModel';
import { HttpService } from 'src/app/service/http.service';

@Component({
  selector: 'app-professor',
  templateUrl: './professor.component.html'
})
export class ProfessorComponent implements OnInit {
  public iconeUpdate = faPen
  public iconeDelete = faTrash
  public formInserirClasse: FormGroup
  public formAtualizarClasse: FormGroup
  public formDeletarClasse: FormGroup
  public carregando = false;
  public classes: Classe[] = []
  public textoExclusao = "excluir permanentemente"

  constructor(private notifierService: NotifierService, private modalService: NgbModal, private httpService: HttpService, private formBuilder: FormBuilder,) {
    this.formInserirClasse = this.formBuilder.group({
      nome: [""]
    })
    this.formAtualizarClasse = this.formBuilder.group({
      novoNome: [""]
    })
    this.formDeletarClasse = this.formBuilder.group({
      confirmacao: [""]
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

  inserirClasseModal(inserirModal: any) {
    this.modalService.open(inserirModal).result.then(
      () => this.httpService.postClasse(this.formInserirClasse.get("nome")?.value).subscribe(
        () => location.reload(),
        () => this.notifierService.notify("error", "Houve um erro ao criar classe!")
      )
    )
    this.formInserirClasse.reset()
  }

  atualizarClasseModal(atualizarModal: any, classe: Classe) {
    this.formAtualizarClasse.patchValue({ novoNome: classe.nome })
    this.modalService.open(atualizarModal).result.then(
      () => {
        console.log(this.formAtualizarClasse.get("novoNome")?.value)
        this.httpService.updateClasse(classe.codigo, this.formAtualizarClasse.get("novoNome")?.value).subscribe(
          () => location.reload(),
          () => this.notifierService.notify("error", "Houve um erro ao atualizar classe!")
        )
      }
    )
  }

  deletarClasseModal(deletarModal: any, codigo: string) {
    this.modalService.open(deletarModal).result.then(
      () => {
        if (this.formDeletarClasse.get("confirmacao")?.value !== this.textoExclusao) {
          this.notifierService.notify("error", "Texto de confirmação inválido!")
        } else {
          this.httpService.deleteClasse(codigo).subscribe(
            () => location.reload(),
            () => this.notifierService.notify("error", "Houve um erro ao deletar classe!")
          )
        }
      }
    )
    this.formDeletarClasse.reset()
  }
}
