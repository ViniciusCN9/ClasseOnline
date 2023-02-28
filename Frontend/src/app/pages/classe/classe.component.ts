import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Security } from 'src/app/utils/security.util';

@Component({
  selector: 'app-classe',
  templateUrl: './classe.component.html',
  styleUrls: ['./classe.component.css']
})
export class ClasseComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    if (!Security.autenticado()) {
      this.router.navigate([""])
    }
  }

}
