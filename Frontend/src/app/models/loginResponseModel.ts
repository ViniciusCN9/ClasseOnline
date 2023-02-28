export class LoginResponse {
    constructor(
        public sucesso: boolean,
        public usuario: string,
        public funcao: number,
        public token: string
    ) { }
}