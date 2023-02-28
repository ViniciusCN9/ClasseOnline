import { Component, OnInit } from '@angular/core';
import { Security } from 'src/app/utils/security.util';
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons'
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public iconeSair = faSignOutAlt
  public usuario: string = "";
  public funcao: number = 0;

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.usuario = Security.getUsuario()
    this.funcao = Security.getFuncao()
  }

  logout() {
    Security.limpar()
    this.router.navigate([""])
  }

}
