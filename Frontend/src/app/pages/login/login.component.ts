import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { HttpService } from 'src/app/service/http.service';
import { Security } from 'src/app/utils/security.util';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public formLogin: FormGroup;
  public carregando = false;

  constructor(private notifierService: NotifierService, private httpService: HttpService, private formBuilder: FormBuilder, private router: Router) {
    this.formLogin = this.formBuilder.group({
      usuario: [""],
      senha: [""]
    })
  }

  ngOnInit(): void {
    if (Security.autenticado()) {
      this.router.navigate(["/home"])
    }
  }

  login() {
    this.carregando = true
    this.httpService.postLogin(this.formLogin.get("usuario")?.value, this.formLogin.get("senha")?.value)
      .subscribe(
        (response) => {
          this.carregando = false

          if (!response.sucesso) {
            this.notifierService.notify("error", "Usuário ou senha inválidos!")
            return
          }

          Security.setAutenticado(response.token, response.usuario, response.funcao)
          this.router.navigate(["/home"])
        },
        error => {
          this.carregando = false
          this.notifierService.notify("error", "Houve um erro ao fazer login!")
        });
  }

}
