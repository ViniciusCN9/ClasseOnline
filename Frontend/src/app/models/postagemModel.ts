export class Postagem {
    constructor(
        public id: string,
        public timestamp: number,
        public usuario: string,
        public titulo: string,
        public texto: string,
        public anexos: string[]
    ) { }
}