using SalesGoalsManager.RegraDeNegocio.Comuns;
using SalesGoalsManager.RegraDeNegocio.Dto;
using SalesGoalsManager.RegraDeNegocio.Extensoes;
using System.ComponentModel.DataAnnotations;

namespace SalesGoalsManager.RegraDeNegocio.Validacoes
{
    public class CadastroVendedorValidacao
    {       
        public void Validar(VendedorDto vendedor, List<VendedorDto> vendedoresExistentes)
        {
            var erros = new List<string>();

            if (vendedor.NomeVendedor.IsNullOrEmpty())
                erros.Add(Constantes.MsgVendedorNaoPreenchido);

            bool nomeDuplicado = vendedoresExistentes.Any(v =>
                v.Id != vendedor.Id &&
                v.NomeVendedor.Trim().Equals(vendedor.NomeVendedor?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (nomeDuplicado)
                erros.Add(Constantes.MsgVendedorMesmoNome);

            if (erros.Any())
                throw new ValidationException(string.Join(Environment.NewLine, erros));
        }
    }
}
