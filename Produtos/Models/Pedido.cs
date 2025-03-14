namespace Produtos.Models
{
    public class Pedido
    {
        public int CodigoPedido { get; protected set; }
        public DateTime DataPedido { get; protected set; }
        public int QuantidadePedido { get; protected set; }
        public Produto Produto { get; set; }

        public Pedido(int codigoPedido, DateTime dataPedido, int quantidadePedido, Produto produto)
        {
            SetCodigoPedido(codigoPedido);
            SetDataPedido(dataPedido);
            SetQuantidadePedido(quantidadePedido);
            Produto = produto;
        }

        public void SetCodigoPedido(int codigoPedido)
        {
            if (codigoPedido <= 0)
                throw new Exception("O código do pedido deve ser maior do que zero.");

            CodigoPedido = codigoPedido;
        }

        public void SetDataPedido(DateTime dataPedido)
        {
            if (dataPedido < DateTime.MinValue)
                throw new Exception("A data do pedido não pode ser anterior à data mínima do sistema.");

            DataPedido = dataPedido;
        }

        public void SetQuantidadePedido(int quantidadePedido)
        {
            if (quantidadePedido <= 0)
                throw new Exception("A quantidade de itens no pedido deve ser maior do que zero.");

            QuantidadePedido = quantidadePedido;
        }

        // Calcula a média de vendas diárias de um produto, método necessário para realizar o desafio extra 01
        public static double CalcularMediaDeVendas(List<Pedido> pedidos)
        {
            var totalVendido = pedidos.Sum(q => q.QuantidadePedido);
            var diasCorridos = pedidos
                .Select(d => d.DataPedido.Day)
                .Distinct()
                .Count();

            double media = totalVendido / diasCorridos;

            if (diasCorridos < 0)
            {
                return 0;
            }
            else
            {
                return Math.Floor(media);
            }
        }
    }
}