import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public carregando = false;
  public usuarioInvalido = false;

  constructor() { }

  ngOnInit(): void {
  }

}