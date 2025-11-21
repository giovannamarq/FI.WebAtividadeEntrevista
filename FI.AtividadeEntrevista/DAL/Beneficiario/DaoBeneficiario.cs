using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FI.AtividadeEntrevista.DML;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal DaoBeneficiario()
        {
        }

        /// <summary>
        /// Inclui novo beneficiário.
        /// Procedure: FI_SP_IncBeneficiario
        /// </summary>
        internal long Incluir(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new SqlParameter("NOME", beneficiario.Nome));
            parametros.Add(new SqlParameter("IDCLIENTE", beneficiario.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);

            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);

            return ret;
        }

        /// <summary>
        /// Altera um beneficiario
        /// Procedure: FI_SP_AltBenef
        /// </summary>
        internal void Alterar(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("ID", beneficiario.Id));
            parametros.Add(new SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new SqlParameter("NOME", beneficiario.Nome));

            base.Executar("FI_SP_AltBenef", parametros);
        }

        /// <summary>
        /// Exclui um beneficiario 
        /// Procedure: FI_SP_DelBenef
        /// </summary>
        internal void Excluir(long id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("ID", id));

            base.Executar("FI_SP_DelBenef", parametros);
        }

        /// <summary>
        /// Consulta a lista de beneficiários de um cliente
        /// Procedure: FI_SP_ConsBenef
        /// </summary>
        internal List<Beneficiario> ConsultarPorCliente(long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("IDCLIENTE", idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBenef", parametros);
            return Converter(ds);
        }

        /// <summary>
        /// Verifica se o CPF de um beneficiário já existe para um cliente
        /// Procedure: FI_SP_VerifBenefExistencia
        /// </summary>
        internal bool VerificarExistencia(string cpf, long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("CPF", cpf));
            parametros.Add(new SqlParameter("IDCLIENTE", idCliente));

            DataSet ds = base.Consultar("FI_SP_VerifBenefExistencia", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> lista = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario benef = new Beneficiario();
                    benef.Id = row.Field<long>("ID");
                    benef.CPF = row.Field<string>("CPF");
                    benef.Nome = row.Field<string>("NOME");
                    benef.IdCliente = row.Field<long>("IDCLIENTE");
                    lista.Add(benef);
                }
            }
            return lista;
        }
    }
}
