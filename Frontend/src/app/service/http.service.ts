import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoginResponse } from '../models/loginResponseModel';
import { Classe } from '../models/classeModel';
import { Security } from '../utils/security.util';

@Injectable({ providedIn: 'root' })
export class HttpService {
    private API_URL: string = environment.apiUrl
    private AUTH_ROUTE: string = environment.authRoute
    private CLASSE_ROUTE: string = environment.classeRoute

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

    updateClasse(codigo: string, nome: string) {
        return this.httpClient.put(`${this.API_URL}/${this.CLASSE_ROUTE}/${codigo}/${nome}`, {}, this.getHeaders())
    }

    deleteClasse(codigo: string) {
        return this.httpClient.delete(`${this.API_URL}/${this.CLASSE_ROUTE}/${codigo}`, this.getHeaders())
    }

    private getHeaders() {
        return {
            headers: {
                "Authorization": `Bearer ${Security.getToken()}`
            }
        }
    }
}