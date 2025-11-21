using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiário, com validação de CPF e unicidade por cliente.
        /// </summary>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            beneficiario.CPF = Regex.Replace(beneficiario.CPF ?? string.Empty, "[^0-9]", "");

            if(!CPFValido(beneficiario.CPF))
            {
                throw new Exception("O CPF do beneficiário é inválido");
            }

            if(VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente))
            {
                throw new Exception("Este CPF já está cadastrado para um cliente.");
            }

            DaoBeneficiario benef = new DaoBeneficiario();
            return benef.Incluir(beneficiario);
        }

        /// <summary>
        /// Alterar um beneficiário, com validação de CPF. 
        /// </summary>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            beneficiario.CPF = Regex.Replace(beneficiario.CPF ?? string.Empty, "[^0-9]", "");

            if (!CPFValido(beneficiario.CPF))
            {
                throw new Exception("O CPF do beneficiário é inválido");
            }

            DaoBeneficiario benef = new DaoBeneficiario();
            benef.Alterar(beneficiario);
        }

        /// <summary>
        /// Exclui o beneficiário pelo ID.
        /// </summary>
        public void Excluir(long id)
        {
            DaoBeneficiario benef = new DaoBeneficiario();
            benef.Excluir(id);
        }

        /// <summary>
        /// Consulta a lista de beneficiários de um cliente.
        /// </summary>
        public List<DML.Beneficiario> ConsultarPorCliente(long idCliente)
        {
            DaoBeneficiario benef = new DaoBeneficiario();
            return benef.ConsultarPorCliente(idCliente);
        }

        /// <summary>
        /// Verifica a existência do CPF do beneficiário para um cliente específico.
        /// </summary>
        public bool VerificarExistencia(string CPF, long idCliente)
        {
            string CPFLimpo = Regex.Replace(CPF ?? string.Empty, "[^0-9]", "");

            DaoBeneficiario benef = new DaoBeneficiario();
            return benef.VerificarExistencia(CPFLimpo, idCliente);
        }

        /// <summary>
        /// Valida o CPF.
        /// </summary>
        private bool CPFValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
            {
                return false;
            }
            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
