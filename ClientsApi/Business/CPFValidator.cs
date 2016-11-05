using System;

namespace ClientsApi.Business
{
    public static class CPFValidator
    {
        public static bool IsValid(long cpf)
        {
            string sCpf = cpf.ToString();

            if (sCpf.Length > 11)
                return false;

            while(sCpf.Length < 11)
                sCpf = "0" + sCpf;

            char primeiroDigito = CalcularDigito(sCpf, 1);
            if (primeiroDigito != sCpf[9])
                return false;

            char segundoDigito = CalcularDigito(sCpf, 2);
            if (segundoDigito != sCpf[10])
                return false;

            return true;
        }

        private static char CalcularDigito(string cpf, int digito)
        {
            int size = 8 + digito;

            int[] mult = new int[size];
            int fator = size + 1;
            for (int i = 0; i < size; i++)
            {
                mult[i] = Convert.ToInt16(cpf[i].ToString()) * fator;
                fator--;
            }

            int soma = 0;
            foreach (var item in mult)
                soma += item;

            int res;
            int resto = soma % 11;
            if (resto < 2)
                res = 0;
            else
                res = 11 - resto;

            return res.ToString()[0];
        }
        
    }
}