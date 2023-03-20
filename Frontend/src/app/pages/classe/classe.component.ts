import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faCopy } from '@fortawesome/free-solid-svg-icons';
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
  public iconeCopiar = faCopy
  public funcao = 0
  public codigo: string = ""
  public classe: Classe = new Classe("", "", [], [])
  public exibicao = 0

  constructor(private notifierService: NotifierService, private router: Router, private activatedRoute: ActivatedRoute, private httpService: HttpService) { }

  ngOnInit(): void {
    if (!Security.autenticado()) {
      this.router.navigate([""])
    }
    this.funcao = Security.getFuncao()
    this.activatedRoute.paramMap.subscribe(params => this.codigo = params.get("codigo") ?? "")
    this.httpService.getClasse(this.codigo).subscribe((response) => this.classe = response)
  }

  exibirPostagens() {
    if (this.exibicao === 1) {
      this.exibicao = 0
      return
    }

    if (this.exibicao === 0 || 2) {
      this.exibicao = 1
    }
  }

  exibirAtividades() {
    if (this.exibicao === 2) {
      this.exibicao = 0
      return
    }

    if (this.exibicao === 0 || 1) {
      this.exibicao = 2
    }
  }

  copiarCodigo() {
    try {
      const elemento = document.createElement('textarea');
      elemento.style.position = 'fixed';
      elemento.style.left = '0';
      elemento.style.top = '0';
      elemento.style.opacity = '0';
      elemento.value = this.codigo;
      document.body.appendChild(elemento);
      elemento.focus();
      elemento.select();
      document.execCommand('copy');
      document.body.removeChild(elemento);
    }
    finally {
      this.notifierService.notify("success", "Código copiado com sucesso!")
    }
  }
}
