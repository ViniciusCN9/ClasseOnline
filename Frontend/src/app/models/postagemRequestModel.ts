export class PostagemRequest {
    constructor(
        public titulo: string,
        public texto: string,
        public anexos: string[]
    ) { }
}