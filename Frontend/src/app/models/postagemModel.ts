import { Anexo } from "./anexoModel";

export class Postagem {
    constructor(
        public id: string,
        public timestamp: number,
        public usuario: string,
        public titulo: string,
        public texto: string,
        public anexos: string[],
        public arquivos: Anexo[] // Atributo exclusivo da interface
    ) {
        this.arquivos = []
    }
}