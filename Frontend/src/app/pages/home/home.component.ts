import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Security } from 'src/app/utils/security.util';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  public funcao = 0;

  constructor() { }

  ngOnInit(): void {
    this.funcao = Security.getFuncao()
  }


}
