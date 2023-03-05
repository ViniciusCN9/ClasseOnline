using System;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class GeradorCodigoClasseUtil
    {
        public static string GerarCodigoClasse()
        {
            var parteA = GerarTextoAleatorio(4);
            var parteB = GerarTextoAleatorio(4);
            var parteC = GerarTextoAleatorio(4);
            var parteD = GerarTextoAleatorio(4);

            return $"{parteA}-{parteB}-{parteC}-{parteD}";
        }

        private static string GerarTextoAleatorio(int tamanho)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                        .Select(e => e[random.Next(e.Length)])
                        .ToArray());
            return result;
        }
    }
}