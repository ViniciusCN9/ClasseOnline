import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-atividades',
  templateUrl: './atividades.component.html',
  styleUrls: ['./atividades.component.css']
})
export class AtividadesComponent implements OnInit {
  @Input() funcao = 0
  @Input() codigo = ""

  constructor() { }

  ngOnInit(): void {
  }

}
