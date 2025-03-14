using System.Globalization;
using System.Runtime.Serialization;
using Atendimentos.Models;
using NPOI.SS.UserModel;

namespace Atendimentos
{
    internal class Program
    {
        private static string caminhoArquivo = Path.Combine(Environment.CurrentDirectory, "Atendimento.xls");
        private static List<Atendimento> atendimentos = [];

        private static void Main(string[] args)
        {
            ImportarDadosPlanilha();

            Ex01();
            Ex02();
            Ex03();
            Ex04();
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

                    int codigoAtendimento = (int)linha.GetCell(0).NumericCellValue;
                    DateTime dataAbertura = DateTime.ParseExact(linha.GetCell(1).StringCellValue, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    string seguradora = linha.GetCell(2).StringCellValue;
                    string itemDanificado = linha.GetCell(3).StringCellValue;
                    double valorFranquia = double.Parse(linha.GetCell(4).StringCellValue, CultureInfo.InvariantCulture);
                    string nomeSegurado = linha.GetCell(5).StringCellValue;
                    string nomeAtendente = linha.GetCell(6).StringCellValue;
                    string cidade = linha.GetCell(7).StringCellValue;
                    string estado = linha.GetCell(8).StringCellValue;
                    int numeroApolice = (int)linha.GetCell(9).NumericCellValue;
                    string nomeVeiculo = linha.GetCell(10).StringCellValue;

                    Atendimento atendimento = new Atendimento(codigoAtendimento, dataAbertura, seguradora, itemDanificado, valorFranquia, nomeSegurado, nomeAtendente, cidade, estado, numeroApolice, nomeVeiculo);

                    atendimentos.Add(atendimento);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        // 1 – Qual é o maior valor de franquia pago por um Segurado? (0,5)
        private static void Ex01()
        {
            var maiorValor = atendimentos.Max(v => v.ValorFranquia);

            Console.WriteLine($"O maior valor de franquia pago foi {maiorValor:c}");
        }

        // 2 – Quantos atendentes temos em nossa Central? Conte sem repetição. (0,5)
        private static void Ex02()
        {
            var quantidadeAtendentes = atendimentos.Select(a => a.NomeAtendente).Distinct().Count();

            Console.WriteLine($"A quantidade de atendentes na central é {quantidadeAtendentes}");
        }

        // 3 – Quantos atendimentos temos mensalmente? Faça uma lista ordenada ascendente dos meses e as respectivas quantidades. (1,00)
        // Exemplo:
        // Janeiro: 40 atendimentos
        // Fevereiro: 30 atendimentos
        // Março: 20 atendimentos
        private static void Ex03()
        {
            var atendimentosMensais = atendimentos
                .GroupBy(m => m.DataAbertura.Month)
                .Select(s => new
                {
                    Mes = s.Key,
                    Quantidade = s.Count()
                })
                .OrderBy(m => m.Mes)
                .ToList();

            foreach (var qtdAtendimentos in atendimentosMensais)
            {
                Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(qtdAtendimentos.Mes)} - {qtdAtendimentos.Quantidade} atendimentos");
            }
        }

        // 4 – Faça um TOP 5 de Seguradoras que mais tiveram atendimentos. Faça um TOP 5 dos veículos que mais apareceram na lista. Faça um TOP 3 de Itens danificados que mais apareceram na lista.  (1,00)
        // Exemplo:
        // Tokio – 30
        // Bradesco – 21
        private static void Ex04()
        {
            var seguradorasComMaisAtendimentos = atendimentos
                .GroupBy(s => s.Seguradora)
                .Select(g => new
                {
                    Seguradora = g.Key,
                    Quantidade = g.Count()
                })
                .OrderByDescending(q => q.Quantidade)
                .Take(5)
                .ToList();

            for (int i = 0; i < seguradorasComMaisAtendimentos.Count; i++)
            {
                Console.WriteLine($"{i + 1}º lugar: {seguradorasComMaisAtendimentos[i].Seguradora} - {seguradorasComMaisAtendimentos[i].Quantidade}");
            }
        }

    }
}