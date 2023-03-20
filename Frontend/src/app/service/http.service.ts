import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoginResponse } from '../models/loginResponseModel';
import { Classe } from '../models/classeModel';
import { Security } from '../utils/security.util';
import { Postagem } from '../models/postagemModel';
import { PostagemRequest } from '../models/postagemRequestModel';
import { Anexo } from '../models/anexoModel';

@Injectable({ providedIn: 'root' })
export class HttpService {
    private API_URL: string = environment.apiUrl
    private AUTH_ROUTE: string = environment.authRoute
    private CLASSE_ROUTE: string = environment.classeRoute
    private POSTAGEM_ROUTE: string = environment.postagemRoute
    private ANEXO_ROUTE: string = environment.anexoRoute

    constructor(private httpClient: HttpClient) { }

    postLogin(usuario: string, senha: string) {
        return this.httpClient.post<LoginResponse>(`${this.API_URL}/${this.AUTH_ROUTE}/login`, { usuario: usuario, senha: senha })
    }

    getClasses() {
        return this.httpClient.get<Classe[]>(`${this.API_URL}/${this.CLASSE_ROUTE}`, this.getHeaders())
    }

    getClasse(codigo: string) {
        return this.httpClient.get<Classe>(`${this.API_URL}/${this.CLASSE_ROUTE}/${codigo}`, this.getHeaders())
    }

    postClasse(nome: string) {
        return this.httpClient.post<Classe>(`${this.API_URL}/${this.CLASSE_ROUTE}/${nome}`, {}, this.getHeaders())
    }

    registrarAluno(codigo: string) {
        return this.httpClient.post<boolean>(`${this.API_URL}/${this.CLASSE_ROUTE}/registrar/${codigo}`, {}, this.getHeaders())
    }

    updateClasse(codigo: string, nome: string) {
        return this.httpClient.put(`${this.API_URL}/${this.CLASSE_ROUTE}/${codigo}/${nome}`, {}, this.getHeaders())
    }

    deleteClasse(codigo: string) {
        return this.httpClient.delete(`${this.API_URL}/${this.CLASSE_ROUTE}/${codigo}`, this.getHeaders())
    }

    getPostagens(codigo: string) {
        return this.httpClient.get<Postagem[]>(`${this.API_URL}/${this.POSTAGEM_ROUTE}/${codigo}`, this.getHeaders())
    }

    postPostagem(codigo: string, postagem: PostagemRequest) {
        return this.httpClient.post(`${this.API_URL}/${this.POSTAGEM_ROUTE}/${codigo}`, postagem, this.getHeaders())
    }

    deletePostagem(codigo: string, id: string) {
        return this.httpClient.delete(`${this.API_URL}/${this.POSTAGEM_ROUTE}/${codigo}/${id}`, this.getHeaders())
    }

    getAnexos(idPostagem: string) {
        return this.httpClient.get<Anexo[]>(`${this.API_URL}/${this.ANEXO_ROUTE}/${idPostagem}`, this.getHeaders());
    }

    postAnexo(arquivos: FormData) {
        return this.httpClient.post<Anexo[]>(`${this.API_URL}/${this.ANEXO_ROUTE}`, arquivos, this.getHeaders())
    }

    deleteAnexo(id: string) {
        return this.httpClient.delete(`${this.API_URL}/${this.ANEXO_ROUTE}/${id}`, this.getHeaders())
    }

    downloadAnexo(id: string) {
        return this.httpClient.get(`${this.API_URL}/${this.ANEXO_ROUTE}/download/${id}`, this.getHeadersToDownload())
    }

    private getHeaders() {
        return {
            headers: {
                "Authorization": `Bearer ${Security.getToken()}`
            }
        }
    }

    private getHeadersToDownload() {
        return {
            headers: {
                "Authorization": `Bearer ${Security.getToken()}`
            },
            responseType: 'blob' as 'json'
        }
    }
}