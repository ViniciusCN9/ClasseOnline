import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgbCollapseModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { NotifierModule } from 'angular-notifier';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { ClasseComponent } from './pages/classe/classe.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { LoadComponent } from './shared/load/load.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { RecuperarSenhaComponent } from './pages/recuperar-senha/recuperar-senha.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProfessorComponent } from './pages/professor/professor.component';
import { AlunoComponent } from './pages/aluno/aluno.component';
import { PostagensComponent } from './pages/postagens/postagens.component';
import { AtividadesComponent } from './pages/atividades/atividades.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ClasseComponent,
    NotFoundComponent,
    LoadComponent,
    NavbarComponent,
    RecuperarSenhaComponent,
    ProfessorComponent,
    AlunoComponent,
    PostagensComponent,
    AtividadesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    FontAwesomeModule,
    NgbCollapseModule,
    NotifierModule.withConfig({ position: { horizontal: { position: 'right' } } })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
