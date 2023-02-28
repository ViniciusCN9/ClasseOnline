import { Component, OnInit } from '@angular/core';
import { Security } from 'src/app/utils/security.util';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public usuario: string = "";
  public funcao: number = 0;

  constructor() { }

  ngOnInit(): void {
    this.usuario = Security.getUsuario()
    this.funcao = Security.getFuncao()
  }

}
