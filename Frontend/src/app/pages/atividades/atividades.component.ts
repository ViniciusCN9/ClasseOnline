import { Component, Input, OnInit } from '@angular/core';
import { Classe } from 'src/app/models/classeModel';

@Component({
  selector: 'app-atividades',
  templateUrl: './atividades.component.html',
  styleUrls: ['./atividades.component.css']
})
export class AtividadesComponent implements OnInit {
  @Input() funcao = 0
  @Input() classe: Classe = new Classe("", "", [], [])

  constructor() { }

  ngOnInit(): void {
  }

}
