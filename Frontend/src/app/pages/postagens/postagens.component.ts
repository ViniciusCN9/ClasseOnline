import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { faFile, faWindowClose } from '@fortawesome/free-solid-svg-icons';
import { NgbCollapse, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotifierService } from 'angular-notifier';
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
  public iconRemoverArquivo = faWindowClose
  public formAdicionarConteudo: FormGroup
  public formRemoverConteudo: FormGroup
  public textoExclusao = "excluir permanentemente"
  public postagens: Postagem[] = []
  public anexos: Anexo[] = []
  public exibirAnexos = false
  public arquivos: File[] = [];

  constructor(private notifierService: NotifierService, private modalService: NgbModal, private httpService: HttpService, private formBuilder: FormBuilder) {
    this.formAdicionarConteudo = this.formBuilder.group({
      titulo: [""],
      texto: [""],
      anexos: [""]
    })
    this.formRemoverConteudo = this.formBuilder.group({
      confirmacao: [""]
    })
  }

  ngOnInit(): void {
    this.carregarPostagens()
  }

  carregarPostagens() {
    this.httpService.getPostagens(this.classe.codigo).subscribe((result) => this.postagens = result.sort(e => e.timestamp))
  }

  adicionarConteudo(adicionarModal: any) {
    this.modalService.open(adicionarModal)
  }

  removerConteudo(removerModal:any, id: string) {
    this.modalService.open(removerModal).result.then(
      () => {
        if (this.formRemoverConteudo.get("confirmacao")?.value !== this.textoExclusao) {
          this.notifierService.notify("error", "Texto de confirmação inválido!")
        } else {
          this.httpService.deletePostagem(this.classe.codigo, id).subscribe(
            () => {
              this.carregarPostagens()
              this.notifierService.notify("success", "Postagem removida com sucesso!")
            },
            () => this.notifierService.notify("error", "Erro ao remover postagem!")
          )
        }
      }
    )
    console.log(`${id} - ${this.classe.codigo}`)
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

  removerAnexo(index: number) {
    let arquivosAtualizados: File[] = []
    this.arquivos.forEach((arquivo, arquivoIndex) => {
      if (arquivoIndex !== index) {
        arquivosAtualizados.push(arquivo)
      }
    });
    this.arquivos = arquivosAtualizados
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
