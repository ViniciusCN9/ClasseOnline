export class Security {
    public static setAutenticado(token: string, usuario: string, funcao: number) {
        localStorage.setItem('token', token);
        localStorage.setItem('usuario', btoa(usuario))
        localStorage.setItem('funcao', btoa(funcao.toString()))
    }

    public static autenticado(): boolean {
        const token: string | null = this.getToken();
        const usuario: string | null = this.getUsuario();
        const funcao: number | null = this.getFuncao();

        if (token != null && usuario != null && funcao != null) {
            return true;
        }
        return false;
    }

    public static getToken() {
        return localStorage.getItem('token');
    }

    public static getUsuario() {
        return atob(localStorage.getItem('usuario')!);
    }

    public static getFuncao() {
        return parseInt(atob(localStorage.getItem('funcao')!));
    }

    public static limpar() {
        return localStorage.clear()
    }
}