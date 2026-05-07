using ProjetoCadastros.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCadastros.RegraDeNegocio
{
    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public List<ProdutoDto> Produtos { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
    }
}
