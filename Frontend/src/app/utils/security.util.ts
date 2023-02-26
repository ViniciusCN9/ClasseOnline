export class Security {
    public static setAuthenticate(token: string, username: string, role: number) {
        localStorage.setItem('token', token);
        localStorage.setItem('username', btoa(username))
        localStorage.setItem('role', btoa(role.toString()))
    }

    public static isAuthenticated(): boolean {
        const token: string | null = this.getToken();
        const username: string | null = this.getUsername();
        const role: number | null = this.getRole();

        if (token != null && username != null && role != null) {
            return true;
        }
        return false;
    }

    public static getToken() {
        return localStorage.getItem('token');
    }

    public static getUsername() {
        return atob(localStorage.getItem('username')!);
    }

    public static getRole() {
        return parseInt(atob(localStorage.getItem('role')!));
    }

    public static clear() {
        return localStorage.clear()
    }
}