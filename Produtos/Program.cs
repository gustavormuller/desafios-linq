using System.Globalization;
using System.Xml.Serialization;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using Produtos.Models;

namespace Produtos
{
    internal class Program
    {
        private static string caminhoArquivo = Path.Combine(Environment.CurrentDirectory, "Produtos.xlsx");
        private static List<Produto> produtos = [];
        private static List<Pedido> pedidos = [];

        private static void Main(string[] args)
        {
            ImportarDadosPlanilha();

            int option;
            do
            {
                Console.WriteLine("Digite o número relacionado ao exercício que deseja ver:");
                Console.WriteLine("1 - Exercício 1");
                Console.WriteLine("2 - Exercício 2");
                Console.WriteLine("3 - Exercício 3");
                Console.WriteLine("4 - Exercício 4");
                Console.WriteLine("5 - Exercício 5");
                Console.WriteLine("6 - Exercício 6");
                Console.WriteLine("7 - Desafio extra 01");
                Console.WriteLine("8 - Desafio extra 02");
                Console.WriteLine("0 - Sair");

                // Captura a opção do usuário
                option = int.Parse(Console.ReadLine());

                if (option != 0)
                {
                    switch (option)
                    {
                        case 1:
                            Exercicio01();
                            break;

                        case 2:
                            Exercicio02();
                            break;

                        case 3:
                            Exercicio03();
                            break;

                        case 4:
                            Exercicio04();
                            break;

                        case 5:
                            Exercicio05();
                            break;

                        case 6:
                            Exercicio06();
                            break;

                        case 7:
                            DesafioExtra01();
                            break;

                        case 8:
                            DesafioExtra02();
                            break;

                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Deseja ver outro exercício? (s/n)");
                    string resposta = Console.ReadLine().ToLower();

                    if (resposta.ToLower() != "s")
                    {
                        break;
                    }

                }
            } while (option != 0);

            Console.WriteLine("Saindo...");
        }

        private static void ImportarDadosPlanilha()
        {
            try
            {
                IWorkbook pastaTrabalho = WorkbookFactory.Create(caminhoArquivo);

                ISheet planilha = pastaTrabalho.GetSheetAt(0);

                for (int i = 1; i < planilha.PhysicalNumberOfRows; i++)
                {
                    IRow linha = planilha.GetRow(i);

                    int codigo = (int)linha.GetCell(0).NumericCellValue;
                    string nome = linha.GetCell(1).StringCellValue;
                    string categoria = linha.GetCell(2).StringCellValue;
                    string fabricante = linha.GetCell(3).StringCellValue;
                    double preco = linha.GetCell(4).NumericCellValue;
                    int quantidade = (int)linha.GetCell(5).NumericCellValue;
                    DateTime dataEntrada = linha.GetCell(6).DateCellValue ?? DateTime.Now;
                    string empresa = linha.GetCell(7).StringCellValue;

                    Produto produto = new Produto(codigo, nome, categoria, fabricante, preco, quantidade, dataEntrada, empresa);

                    produtos.Add(produto);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // 1. Qual é o produto mais caro do estoque? (0,5)
        // Exemplo de saída esperada: O produto mais caro é o Parabrisa Ford Fusion, custando R$ 1.200.
        private static void Exercicio01()
        {
            var produtoMaisCaro = produtos
                .Select(p => new
                {
                    Nome = p.Nome,
                    Preco = p.Preco
                })
                .OrderByDescending(p => p.Preco)
                .FirstOrDefault();

            Console.WriteLine();
            Console.WriteLine($"O produto mais caro do estoque é o {produtoMaisCaro.Nome}, custando {produtoMaisCaro.Preco:c}");
        }

        // 2. Quantos produtos com nomes diferentes há no estoque? (0,5)
        // Exemplo de saída esperada: Temos 50 produtos com nomes diferentes disponíveis em nosso catálogo.
        private static void Exercicio02()
        {
            var quantidadeProdutosDiferentes = produtos
                .Select(p => p.Nome)
                .Distinct()
                .Count();

            Console.WriteLine();
            Console.WriteLine($"Temos {quantidadeProdutosDiferentes} produtos com nomes diferentes disponíveis em nosso catálogo.");
        }

        // 3. Quantos produtos entraram no estoque por mês? Faça uma lista 
        // ordenada ascendente dos meses e as respectivas quantidades. (0,5)
        // Exemplo de saída esperada:
        // - janeiro/2025: 20 unidades
        // - fevereiro/2025: 45 unidades
        // - março/2025: 12 unidades
        private static void Exercicio03()
        {
            var produtosPorMes = produtos
                .GroupBy(d => new
                {
                    mes = d.DataEntrada.Month,
                    ano = d.DataEntrada.Year
                })
                .Select(p => new
                {
                    Mes = p.Key.mes,
                    Ano = p.Key.ano,
                    Quantidade = p.Count()
                })
                .OrderBy(a => a.Ano)
                .ThenBy(m => m.Mes)
                .ToList();

            Console.WriteLine();
            foreach (var p in produtosPorMes)
            {
                Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(p.Mes)}/{p.Ano}: {p.Quantidade} unidades");
            }
        }

        // 4. Crie um ranking
        private static void Exercicio04()
        {
            // a. das 5 categorias com mais produtos em estoque. (0,4)
            var categoriasComMaisProdutosEmEstoque = produtos
                .GroupBy(c => c.Categoria)
                .Select(p => new
                {
                    Categoria = p.Key,
                    Quantidade = p.Select(e => e.Quantidade).Sum()
                })
                .OrderByDescending(q => q.Quantidade)
                .Take(5)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Top 5 categorias com mais produtos em estoque:");
            int i = 1;
            foreach (var c in categoriasComMaisProdutosEmEstoque)
            {
                Console.WriteLine($"{i}º Lugar: {c.Categoria} - {c.Quantidade} unidades");
                i++;
            }


            // b. dos 3 centros de distribuição com mais estoque. (0,2)
            var centrosComMaisEstoque = produtos
                .GroupBy(g => g.Empresa)
                .Select(p => new
                {
                    Filial = p.Key,
                    Quantidade = p.Select(e => e.Quantidade).Sum()
                })
                .OrderByDescending(q => q.Quantidade)
                .Take(3)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Top 3 centros de distribuição com mais estoque:");
            int j = 1;
            foreach (var c in centrosComMaisEstoque)
            {
                Console.WriteLine($"{j}º Lugar: {c.Filial} - {c.Quantidade} unidades");
                j++;
            }


            // c. dos 5 produtos que mais possuem estoque. (0,4)
            var produtosComMaisEstoque = produtos
                .GroupBy(c => c.Nome)
                .Select(p => new
                {
                    Produto = p.Key,
                    Quantidade = p.Select(e => e.Quantidade).Sum()
                })
                .OrderByDescending(q => q.Quantidade)
                .Take(5)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Top 5 produtos com mais estoque:");
            int k = 1;
            foreach (var p in produtosComMaisEstoque)
            {
                Console.WriteLine($"{k}º Lugar: {p.Produto} - {p.Quantidade} unidades");
                k++;
            }
        }

        // 5. Em relação aos fabricantes, quantos temos em nossa base? Crie um ranking dos 5 fabricantes com mais itens em estoque. (0,5)
        // Exemplo de saída esperada:
        // Temos 10 fabricantes cadastrados em nossa base.
        // - 1° Lugar: AGC – 85 unidades
        // - 2° Lugar: Fanavid – 55 unidades
        // - 3° Lugar: Nakata – 20 unidades
        private static void Exercicio05()
        {
            var quantidadeFabricantes = produtos
                .Select(f => f.Fabricante)
                .Distinct()
                .Count();

            Console.WriteLine($"Temos {quantidadeFabricantes} fabricantes cadastrados em nossa base.");

            var fabricantesComMaisEstoque = produtos
                .GroupBy(f => f.Fabricante)
                .Select(p => new
                {
                    Fabricante = p.Key,
                    Quantidade = p.Select(e => e.Quantidade).Sum()
                })
                .OrderByDescending(q => q.Quantidade)
                .Take(5)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Top 5 fabricantes com mais itens em estoque:");
            int i = 1;
            foreach (var f in fabricantesComMaisEstoque)
            {
                Console.WriteLine($"{i}º Lugar: {f.Fabricante} - {f.Quantidade}");
                i++;
            }
        }

        // 6. Quais os produtos com maior valor total em estoque? (0,5)
        // Exemplo de saída esperada:
        // Para-brisa Ford Fusion – R$ 12.000 (10un x R$ 1.200)
        // Retrovisor Toyota Corolla – R$ 9.000 (30un x R$ 300)
        private static void Exercicio06()
        {
            var produtosComMaiorValorTotal = produtos
                .GroupBy(g => g.Nome)
                .Select(p => new
                {
                    Nome = p.Key,
                    Preco = p.Max(p => p.Preco),
                    Quantidade = p.Max(e => e.Quantidade),
                    ValorTotal = p.Max(x => x.Preco * x.Quantidade)
                })
                .OrderByDescending(v => v.ValorTotal)
                .Take(10)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Top 10 produtos com maior valor total em estoque de uma filial (em R$):");
            int i = 1;
            foreach (var p in produtosComMaiorValorTotal)
            {
                Console.WriteLine($"{i}º: {p.Nome} - {p.ValorTotal:c} ({p.Quantidade}un x {p.Preco:c})");
                i++;
            }
        }


        // Desafio Extra 01

        // Método necessário para resolução do desafio extra 01
        public static void CalcularRupturaDeEstoque(List<Pedido> pedidos)
        {
            var pedidosUltimos30Dias = pedidos
                .Where(d => d.DataPedido >= DateTime.Now.AddDays(-30))
                .GroupBy(g => g.Produto)
                .Select(p => new
                {
                    Produto = p.Key,
                    MediaDiariaVendas = Pedido.CalcularMediaDeVendas(p.ToList()),
                    DiasEstoque = p.Key.Quantidade / Pedido.CalcularMediaDeVendas(p.ToList()),
                    QuantidadeEmEstoque = p.Key.Quantidade
                })
                .OrderBy(x => x.DiasEstoque)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Lista de produtos:");
            Console.WriteLine();
            foreach (var item in pedidosUltimos30Dias)
            {
                Console.WriteLine($"Nome do produto: {item.Produto.Nome}");
                Console.WriteLine($"Quantidade em estoque: {item.QuantidadeEmEstoque} unidades");
                Console.WriteLine($"Média diária de vendas: {item.MediaDiariaVendas} unidades");
                Console.WriteLine($"Projeção de ruptura: {Math.Floor(item.DiasEstoque)} dias");
                Console.WriteLine();
            }
        }

        // Execução do desafio extra 01
        public static void DesafioExtra01()
        {
            // Utilizando Guid para pegar um produto aleatório da lista de produtos.
            Produto produto1 = produtos.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            Pedido pedido1 = new Pedido(1, DateTime.Now.AddDays(-12), 12, produto1);
            pedidos.Add(pedido1);

            Produto produto2 = produtos.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            Pedido pedido2 = new Pedido(2, DateTime.Now.AddDays(-17), 5, produto2);
            pedidos.Add(pedido2);

            Produto produto3 = produtos.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            Pedido pedido3 = new Pedido(3, DateTime.Now.AddDays(-9), 3, produto3);
            pedidos.Add(pedido3);

            CalcularRupturaDeEstoque(pedidos);
        }


        // Desafio Extra 02

        // Análise de produtos em encalhe, método do desafio extra 02
        public static void AnalisarProdutosEmEncalhe(List<Pedido> pedidos)
        {
            var produtosEmEncalhe = pedidos
                .Where(p => p.DataPedido < DateTime.Now.AddDays(-90))
                .GroupBy(p => p.Produto)
                .Select(g => new
                {
                    Produto = g.Key,
                    UltimaVenda = g.Max(p => p.DataPedido),
                    QuantidadeEstoque = g.Key.Quantidade
                })
                .OrderByDescending(x => x.QuantidadeEstoque)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Produtos em encalhe:");
            foreach (var item in produtosEmEncalhe)
            {
                Console.WriteLine($"Nome do produto: {item.Produto.Nome}");
                Console.WriteLine($"Última venda: {item.UltimaVenda.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Quantidade em estoque: {item.QuantidadeEstoque} unidades");
                Console.WriteLine();
            }
        }

        // Execução do desafio extra 02
        private static void DesafioExtra02()
        {
            // Utilizando Guid para pegar um produto aleatório da lista de produtos.
            Produto produto1 = produtos.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            Pedido pedido1 = new Pedido(1, DateTime.Now.AddDays(-121), 12, produto1);
            pedidos.Add(pedido1);

            Produto produto2 = produtos.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            Pedido pedido2 = new Pedido(2, DateTime.Now.AddDays(-95), 5, produto2);
            pedidos.Add(pedido2);

            Produto produto3 = produtos.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            Pedido pedido3 = new Pedido(3, DateTime.Now.AddDays(-215), 3, produto3);

            pedidos.Add(pedido3);
            Produto produto4 = produtos.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            Pedido pedido4 = new Pedido(4, DateTime.Now.AddDays(-20), 8, produto4);
            pedidos.Add(pedido4);

            AnalisarProdutosEmEncalhe(pedidos);
        }
    }
}