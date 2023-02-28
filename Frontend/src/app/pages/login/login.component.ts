import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
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
  public usuarioInvalido = false;

  constructor(private httpService: HttpService, private formBuilder: FormBuilder, private router: Router) {
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
        response => {
          this.carregando = false
          let mockResponse = {
            sucesso: true,
            usuario: this.formLogin.get("usuario")?.value,
            funcao: 1,
            token: "token"
          }

          Security.setAutenticado(mockResponse.token, mockResponse.usuario, mockResponse.funcao)
          this.router.navigate(["/home"])
        },
        error => {
          this.carregando = false
          let mockResponse = {
            sucesso: true,
            usuario: "usuario",
            funcao: 1,
            token: "token"
          }
          
          Security.setAutenticado(mockResponse.token, mockResponse.usuario, mockResponse.funcao)
          this.router.navigate(["/home"])

          // this.usuarioInvalido = true
        });
  }

}
