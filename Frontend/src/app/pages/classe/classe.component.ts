import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotifierService } from 'angular-notifier';
import { Classe } from 'src/app/models/classeModel';
import { HttpService } from 'src/app/service/http.service';
import { Security } from 'src/app/utils/security.util';

@Component({
  selector: 'app-classe',
  templateUrl: './classe.component.html'
})
export class ClasseComponent implements OnInit {
  public funcao = 0
  public codigo: string = ""
  public classe: Classe = new Classe("", "", [], [])
  public exibicao = 0

  constructor(private router: Router, private activatedRoute: ActivatedRoute, private httpService: HttpService) { }

  ngOnInit(): void {
    if (!Security.autenticado()) {
      this.router.navigate([""])
    }
    this.funcao = Security.getFuncao()
    this.activatedRoute.paramMap.subscribe(params => this.codigo = params.get("codigo") ?? "")
    this.httpService.getClasse(this.codigo).subscribe((response) => this.classe = response)
  }

  exibirPostagens() {
    this.exibicao = 1
  }

  exibirAtividades() {
    this.exibicao = 2
  }
}
