import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class HttpService {
    private API_URL: string = environment.apiUrl
    private LOGIN_ROUTE: string = environment.loginRoute

    constructor(private httpClient: HttpClient) { }

    postLogin(usuario: string, senha: string) {
        return this.httpClient.post(`${this.API_URL}/${this.LOGIN_ROUTE}`, {usuario: usuario, senha: senha})
    }
}