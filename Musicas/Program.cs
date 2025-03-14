
using System.Globalization;
using Musicas.Models;
using NPOI.SS.UserModel;

namespace Musicas
{
    internal class Program
    {
        private static string caminhoArquivo = Path.Combine(Environment.CurrentDirectory, "musicas.xlsx");
        private static List<Musica> musicas = [];

        private static void Main(string[] args)
        {
            ImportarDadosPlanilha(caminhoArquivo);

            // Ex01();
            // Ex02();
            // Ex03();
            // Ex04();
            // Ex05();
            // Ex06();
            Extra01();
            Extra02();
        }

        private static void ImportarDadosPlanilha(string caminhoArquivo)
        {
            try
            {


                IWorkbook pastaTrabalho = WorkbookFactory.Create(caminhoArquivo);

                ISheet planilha = pastaTrabalho.GetSheetAt(0);

                for (int i = 1; i < planilha.PhysicalNumberOfRows; i++)
                {
                    IRow linha = planilha.GetRow(i);

                    int codigo = (int)linha.GetCell(0).NumericCellValue;
                    DateTime dataDeLancamento = linha.GetCell(1).DateCellValue ?? DateTime.Now;
                    string nome = linha.GetCell(2).StringCellValue;
                    string artista = linha.GetCell(3).StringCellValue;
                    string album = linha.GetCell(4).StringCellValue;
                    string genero = linha.GetCell(5).StringCellValue;
                    double duracao = linha.GetCell(6).NumericCellValue;
                    string gravadora = linha.GetCell(7).StringCellValue;
                    string pais = linha.GetCell(8).StringCellValue;
                    string idioma = linha.GetCell(9).StringCellValue;

                    Musica musica = new Musica(codigo, dataDeLancamento, nome, artista, album, genero, duracao, gravadora, pais, idioma);

                    musicas.Add(musica);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }

        // 1. Qual é a maior duração de uma música? (0,5)
        // Exemplo de saída esperada: A música de maior duração possui 12min.
        private static void Ex01()
        {
            var maiorDuracao = musicas.Max(d => d.Duracao);

            Console.WriteLine($"A música de maior duração tem {maiorDuracao.ToString("F2", CultureInfo.InvariantCulture)} min");
        }

        // 2. Quantos artistas únicos temos na nossa base de dados? (0,5)
        // Exemplo de saída esperada: Temos 80 artistas diferentes cadastrados em nossa base.
        private static void Ex02()
        {
            var artistasUnicos = musicas
                .Select(a => a.Artista)
                .Distinct()
                .Count();

            Console.WriteLine($"Temos {artistasUnicos} artistas diferentes cadastrados em nossa base");
        }

        // 3. Quantos álbuns foram lançados mensalmente? Faça uma lista ordenada ascendente dos meses e as respectivas quantidades. (0,5)
        // Exemplo de saída esperada:
        // - janeiro/2024: 50 álbuns
        // - fevereiro/2024: 20 álbuns
        // - março/2024: 60 álbuns
        private static void Ex03()
        {
            var albunsMensais = musicas
                .GroupBy(d => new
                {
                    d.DataDeLancamento.Month,
                    d.DataDeLancamento.Year
                })
                .Select(a => new
                {
                    mes = a.Key.Month,
                    ano = a.Key.Year,
                    quantidade = a.Select(x => x.Album).Distinct().Count()
                })
                .OrderBy(x => x.ano)
                .ThenBy(x => x.mes)
                .ToList();

            foreach (var quantidade in albunsMensais)
            {
                Console.WriteLine($"{quantidade.mes:D2}/{quantidade.ano}: {quantidade.quantidade} álbum");
            }
        }

        // 4. Crie um ranking
        // a. dos 5 gêneros musicais com mais músicas. (0,4)
        //  Exemplo de saída esperada:
        //  - 1° Lugar: Pop – 30 músicas
        //  - 2° Lugar: Rock – 20 músicas
        // b. dos 3 álbuns com mais músicas. (0,2)
        //  Exemplo de saída esperada:
        //  - 1° Lugar: Álbum B – 30 músicas
        //  - 2° Lugar: Álbum C – 20 músicas
        // c. dos 5 países que mais lançaram músicas. (0,4)
        //  Exemplo de saída esperada:
        //  - 1° Lugar: Brasil – 25 músicas
        //  - 2° Lugar: Alemanha – 10 músicas
        private static void Ex04()
        {
            var generosComMaisMusicas = musicas
                .GroupBy(g => g.Genero)
                .Select(m => new
                {
                    genero = m.Key,
                    quantidade = m.Count()
                })
                .OrderByDescending(m => m.quantidade)
                .Take(5)
                .ToList();

            Console.WriteLine("Top 5 gêneros com mais músicas:");
            for (int i = 0; i < generosComMaisMusicas.Count; i++)
            {
                Console.WriteLine($"{i + 1}º Lugar: {generosComMaisMusicas[i].genero} - {generosComMaisMusicas[i].quantidade} músicas");
            }

            var albunsComMaisMusicas = musicas
                .GroupBy(g => g.Album)
                .Select(m => new
                {
                    album = m.Key,
                    quantidade = m.Count()
                })
                .OrderByDescending(m => m.quantidade)
                .Take(3)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Top 3 álbuns com mais músicas:");
            for (int i = 0; i < albunsComMaisMusicas.Count; i++)
            {
                Console.WriteLine($"{i + 1}º Lugar: {albunsComMaisMusicas[i].album} - {albunsComMaisMusicas[i].quantidade} músicas");
            }

            var paisesComMaisMusicas = musicas
                .GroupBy(g => g.Pais)
                .Select(m => new
                {
                    pais = m.Key,
                    quantidade = m.Count()
                })
                .OrderByDescending(m => m.quantidade)
                .Take(5)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Top 5 países com mais músicas:");
            for (int i = 0; i < paisesComMaisMusicas.Count; i++)
            {
                Console.WriteLine($"{i + 1}º Lugar: {paisesComMaisMusicas[i].pais} - {paisesComMaisMusicas[i].quantidade} músicas");
            }
        }

        // 5. Em relação às gravadoras, quantas temos em nossa base? Crie um ranking das 5 gravadoras com mais músicas lançadas. (0,5)
        //     Exemplo de saída esperada:
        //     Temos 10 gravadoras cadastradas em nossa base.
        //     - 1° Lugar: Gravadora A – 50 músicas
        //     - 2° Lugar: Gravadora B – 25 músicas
        //     - 3° Lugar: Gravadora C – 12 músicas
        private static void Ex05()
        {
            var rankingGravadoras = musicas
                .GroupBy(g => g.Gravadora)
                .Select(x => new
                {
                    gravadora = x.Key,
                    quantidade = x.Count()
                })
                .OrderByDescending(q => q.quantidade)
                .Take(5)
                .ToList();

            Console.WriteLine("Top 5 gravadoras com mais músicas:");
            for (int i = 0; i < rankingGravadoras.Count; i++)
            {
                Console.WriteLine($"{i + 1}º Lugar: {rankingGravadoras[i].gravadora} - {rankingGravadoras[i].quantidade} músicas");
            }
        }

        // 6. Qual o idioma com maior número de músicas lançadas por mês? (0,5)
        // Exemplo de saída esperada:
        // - janeiro/2024 - Inglês – 50 músicas
        // - fevereiro/2024 - Espanhol – 25 músicas
        // - março/2024 - Português – 12 músicas
        private static void Ex06()
        {
            var idiomaMaiorMusicas = musicas
                .GroupBy(d => new { d.DataDeLancamento.Month, d.DataDeLancamento.Year })
                .Select(x => new
                {
                    mes = x.Key.Month,
                    ano = x.Key.Year,
                    idioma = x.GroupBy(i => i.Idioma).Select(n => new
                    {
                        idioma = n.Key,
                        quantidade = n.Count()
                    })
                    .OrderByDescending(x => x.quantidade)
                    .FirstOrDefault()
                })
                .OrderBy(d => d.ano)
                .ThenBy(d => d.mes)
                .ToList();

            foreach (var idioma in idiomaMaiorMusicas)
            {
                Console.WriteLine($"{idioma.mes}/{idioma.ano} - {idioma.idioma.idioma} - {idioma.idioma.quantidade} músicas");
            }
        }

        // Desafio extra 1
        private static void Extra01()
        {
            List<string> generos = ["Country", "Blues", "Classical"];
            List<string> ouvidas = ["Remain", "Thousand", "Off", "Central", "Fear"];

            Ouvinte ouvinte = new Ouvinte("Gustavo", generos, ouvidas);

            List<Musica> recomendadas = ouvinte.RecomendarMusicas(musicas);

            Console.WriteLine($"Recomendações para o ouvinte {ouvinte.NomeOuvinte}:");
            if (recomendadas.Count > 0)
            {
                foreach (var musica in recomendadas)
                {
                    Console.WriteLine($"{musica.Nome} - {musica.Album} - {musica.Genero} - {musica.DataDeLancamento.ToString("dd/MM/yyyy")}");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma música recomendada.");
            }
        }

        private static void Extra02()
        {
            List<TendenciaMusical> tendencias = TendenciaMusical.RepresentarTendencia(musicas);

            Console.WriteLine("Tendências musicais:");
            foreach (var tendencia in tendencias)
            {
                Console.WriteLine($"{tendencia.Ano} - {tendencia.Genero} - {tendencia.QuantidadeMusicas} músicas");
            }
        }
    }
}