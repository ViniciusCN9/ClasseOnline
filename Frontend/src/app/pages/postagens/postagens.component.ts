import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { faFile } from '@fortawesome/free-solid-svg-icons';
import { NgbCollapse, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Anexo } from 'src/app/models/anexoModel';
import { Classe } from 'src/app/models/classeModel';
import { Postagem } from 'src/app/models/postagemModel';
import { HttpService } from 'src/app/service/http.service';

@Component({
  selector: 'app-postagens',
  templateUrl: './postagens.component.html',
  styleUrls: ['./postagens.component.css']
})
export class PostagensComponent implements OnInit {
  @Input() funcao = 0
  @Input() classe: Classe = new Classe("", "", [], [])
  public iconeArquivo = faFile
  public formAdicionarConteudo: FormGroup
  public postagens: Postagem[] = []
  public anexos: Anexo[] = []
  public exibirAnexos = false
  public arquivos: File[] = [];

  constructor(private modalService: NgbModal, private httpService: HttpService, private formBuilder: FormBuilder) {
    this.formAdicionarConteudo = this.formBuilder.group({
      titulo: [""],
      texto: [""],
      anexos: [""]
    })
  }

  ngOnInit(): void {
    this.httpService.getPostagens(this.classe.codigo).subscribe((result) => this.postagens = result.sort(e => e.timestamp))
  }

  adicionarConteudo(adicionarModal: any) {
    this.modalService.open(adicionarModal)
  }

  alteracaoArquivos(evento: any) {
    for (var i = 0; i < evento.target.files.length; i++) {
      this.arquivos.push(evento.target.files[i]);
    }
    console.log(this.arquivos)
  }

  carregarAnexos(idPostagem: string) {
    this.httpService.getAnexos(idPostagem).subscribe((result) => this.anexos = result)
  }

  downloadArquivo(id: string, nome: string) {
    this.httpService.downloadAnexo(id).subscribe(
      (response: any) => {
        const downloadedFile = new Blob([response]);
        const a = document.createElement('a');
        a.setAttribute('style', 'display:none;');
        document.body.appendChild(a);
        a.download = nome;
        a.href = URL.createObjectURL(downloadedFile);
        a.target = '_blank';
        a.click();
        document.body.removeChild(a);
      })
  }

}
