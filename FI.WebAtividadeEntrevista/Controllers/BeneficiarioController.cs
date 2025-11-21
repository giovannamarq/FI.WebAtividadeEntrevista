using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly BoBeneficiario boBeneficiario = new BoBeneficiario();

        /// <summary>
        /// Salva ou altera um beneficiário.
        /// </summary>
        [HttpPost]
        public JsonResult Salvar(BeneficiarioModel model)
        {
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();
                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            try
            {
                string cpfLimpo = model.CPF.Replace(".", "").Replace("-", "");

                Beneficiario beneficiario = new Beneficiario()
                {
                    Id = model.Id,
                    IdCliente = model.IdCliente,
                    CPF = cpfLimpo,
                    Nome = model.Nome
                };
                if (model.Id == 0)
                {
                    beneficiario.Id = boBeneficiario.Incluir(beneficiario);
                    return Json(new { Resultado = true, Mensagem = "Beneficiário incluido com sucesso.", Id = beneficiario.Id });
                }
                else
                {
                    boBeneficiario.Alterar(beneficiario);
                    return Json(new { Resultado = true, Mensagem = "Beneficiário alterado com sucesso." });
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { Resultado = false, Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Lista os beneficiários de um cliente.
        /// </summary>
        [HttpGet]
        public JsonResult Listar(long idCliente)
        {
            try
            {
                var beneficiariosDML = boBeneficiario.ConsultarPorCliente(idCliente);

                var models = beneficiariosDML.Select(b => new BeneficiarioModel
                {
                    Id = b.Id,
                    IdCliente = b.IdCliente,
                    Nome = b.Nome,
                    CPF = (new Regex(@"^(\d{3})(\d{3})(\d{3})(\d{2})$")).Replace(b.CPF, "$1.$2.$3-$4")
                }).ToList();
                return Json(new { Resultado = true, Records = models }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { Resultado = false, Mensagem = "Erro ao listar beneficiários: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Exclui um beneficiário pelo ID.
        /// </summary>
        [HttpPost]
        public JsonResult Excluir(long id)
        {
            try
            {
                boBeneficiario.Excluir(id);
                return Json(new { Resultado = true, Mensagem = "Beneficiário excluido com sucesso." });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { Resultado = false, Mensagem = "Erro ao excluir beneficiário: " + ex.Message });
            }
        }
    }
}