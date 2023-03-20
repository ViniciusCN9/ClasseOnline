import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { faFile, faWindowClose } from '@fortawesome/free-solid-svg-icons';
import { NgbCollapse, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotifierService } from 'angular-notifier';
import { Anexo } from 'src/app/models/anexoModel';
import { Classe } from 'src/app/models/classeModel';
import { Postagem } from 'src/app/models/postagemModel';
import { PostagemRequest } from 'src/app/models/postagemRequestModel';
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
  public esconderAnexos = false
  public arquivos: File[] = [];

  constructor(private notifierService: NotifierService, private modalService: NgbModal, private httpService: HttpService, private formBuilder: FormBuilder) {
    this.formAdicionarConteudo = this.formBuilder.group({
      titulo: [""],
      texto: [""]
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
    try {
      this.modalService.open(adicionarModal).result.then(
        () => {
          const formData = new FormData()
          this.arquivos.forEach(arquivo => formData.append('files', arquivo))
          this.httpService.postAnexo(formData).subscribe(
            result => {
              let idAnexos: string[] = [];
              result.forEach(anexo => { idAnexos.push(anexo.id); console.log(anexo) })

              let postagemRequest = new PostagemRequest(this.formAdicionarConteudo.get('titulo')?.value,
                this.formAdicionarConteudo.get('texto')?.value, idAnexos)
              this.httpService.postPostagem(this.classe.codigo, postagemRequest).subscribe(
                () => { this.notifierService.notify("success", "Conteúdo adicionado com sucesso!"); location.reload() },
                () => this.notifierService.notify("error", "Erro ao adicionar conteúdo")
              )
            },
            () => this.notifierService.notify("error", "Erro ao adicionar anexos")
          )
        }
      )
    }
    finally {
      this.arquivos = []
      this.formAdicionarConteudo.reset();
    }
  }

  removerConteudo(removerModal: any, postagem: Postagem) {
    try {
      this.modalService.open(removerModal).result.then(
        () => {
          if (this.formRemoverConteudo.get("confirmacao")?.value !== this.textoExclusao) {
            this.notifierService.notify("error", "Texto de confirmação inválido!")
          } else {
            if (postagem.anexos.length > 0) {
              postagem.anexos.forEach(idAnexo => {
                this.httpService.deleteAnexo(idAnexo).subscribe()
              })
            }
            this.httpService.deletePostagem(this.classe.codigo, postagem.id).subscribe(
              () => {
                this.carregarPostagens()
                this.notifierService.notify("success", "Postagem removida com sucesso!")
              },
              () => this.notifierService.notify("error", "Erro ao remover postagem!")
            )
          }
        }
      )
    }
    finally {
      this.formRemoverConteudo.reset()
    }
  }

  alteracaoArquivos(evento: any) {
    for (var i = 0; i < evento.target.files.length; i++) {
      this.arquivos.push(evento.target.files[i]);
    }
    console.log(this.arquivos)
  }

  carregarAnexos(idPostagem: string) {
    this.httpService.getAnexos(idPostagem).subscribe(
      (result) => result.forEach(
        anexo => {
          this.postagens.forEach(postagem => {
            if (postagem.id === idPostagem) {
              postagem.arquivos.push(anexo)
            }
          })
        }
      )
    )
  }

  limparAnexos() {
    this.postagens.forEach(postagem => {
      postagem.arquivos = []
    })
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
