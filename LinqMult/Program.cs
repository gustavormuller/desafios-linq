using LinqMult.Models;
using NPOI.SS.UserModel;

namespace LinqMult
{
    internal class Program
    {
        private static string caminhoArquivo = Path.Combine(Environment.CurrentDirectory, "Funcionarios.xlsx");
        private static List<Funcionario> funcionarios = [];

        private static void Main(string[] args)
        {
            ImportarDadosPlanilha();
            ExercicioOito();
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

                    string id = linha.GetCell(0).StringCellValue;
                    string nome = linha.GetCell(1).StringCellValue;
                    string cargo = linha.GetCell(2).StringCellValue;
                    string departamento = linha.GetCell(3).StringCellValue;
                    DateTime dataAdmissao = linha.GetCell(4).DateCellValue ?? DateTime.Now;
                    decimal salario = (decimal)linha.GetCell(5).NumericCellValue;
                    string cidade = linha.GetCell(6).StringCellValue;
                    string estado = linha.GetCell(7).StringCellValue;

                    Funcionario funcionario = new Funcionario(id, nome, cargo, departamento, dataAdmissao, salario, cidade, estado);

                    funcionarios.Add(funcionario);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        // 1. Quantos funcionários com nomes distintos existem na empresa?
        private static void ExercicioUm()
        {
            var nomes = funcionarios.Select(x => x.Nome);
            var quantidade = nomes.Distinct().Count();

            var quantidade2 = funcionarios.DistinctBy(x => x.Nome).Count();

            Console.WriteLine($"A quantidade de funcionários com nomes distintos é {quantidade2}");
        }

        // 2. Liste os funcionários ordenados pelo nome.
        private static void ExercicioDois()
        {
            var funcionariosOrdernados = funcionarios.OrderBy(x => x.Nome);

            foreach (var funcionario in funcionariosOrdernados)
            {
                Console.WriteLine($"{funcionario.Nome} - {funcionario.Cargo}");
            }
        }

        // 3. Quantos funcionários existem em cada departamento?
        private static void ExercicioTres()
        {
            var funcionariosDepartamento = funcionarios.GroupBy(x => x.Departamento).Select(g => new
            {
                Departamento = g.Key,
                QuantidadeFuncionarios = g.Count()
            });

            foreach (var departamento in funcionariosDepartamento)
            {
                Console.WriteLine($"{departamento.Departamento}: {departamento.QuantidadeFuncionarios} funcionários");
            }
        }

        // 4. Qual é o salário médio da empresa?
        private static void ExercicioQuatro()
        {
            var salarioMedio = funcionarios.Average(x => x.Salario);

            Console.WriteLine($"Salário médio: {salarioMedio:c}");
        }

        // 5. Faça um TOP 5 dos funcionários com os maiores salários.
        private static void ExercicioCinco()
        {
            var funcionariosComMaiorSalario = funcionarios.OrderByDescending(x => x.Salario).Take(5).ToList();

            for (int i = 0; i < funcionariosComMaiorSalario.Count(); i++)
            {
                Console.WriteLine($"{i + 1}º lugar com maior salário: {funcionariosComMaiorSalario[i].Nome} - {funcionariosComMaiorSalario[i].Salario:c}");
            }
        }

        // 6. Quais são os 3 departamentos com os maiores salários médios?
        private static void ExercicioSeis()
        {
            var departamentosComMaiorSalario = funcionarios.GroupBy(x => x.Departamento).Select(g => new
            {
                Departamento = g.Key,
                mediaSalarial = g.Average(s => s.Salario)
            }).OrderByDescending(m => m.mediaSalarial).Take(3);

            foreach (var departamento in departamentosComMaiorSalario)
            {
                Console.WriteLine($"{departamento.Departamento}: {departamento.mediaSalarial:c}");
            }
        }

        // 7. Quantas admissões temos mensalmente? Faça uma lista ordenada ascendente dos meses e as respectivas quantidades.
        private static void ExercicioSete()
        {
            var admissoesMensais = funcionarios
                .GroupBy(f => new
                {
                    Ano = f.DataAdmissao.Year,
                    Mes = f.DataAdmissao.Month
                })
                .Select(g => new
                {
                    Ano = g.Key.Ano,
                    Mes = g.Key.Mes,
                    QuantidadeAdmissoes = g.Count()
                })
                .OrderBy(x => x.Ano)
                .ThenBy(y => y.Mes)
                .ToList();

            foreach (var admissao in admissoesMensais)
            {
                Console.WriteLine($"{admissao.Mes}/{admissao.Ano} - {admissao.QuantidadeAdmissoes} admissões");
            };
        }

        // 8. Quantos funcionários foram admitidos nos últimos 12 meses?
        private static void ExercicioOito()
        {
            var admissoesUltimosMeses = funcionarios
                .GroupBy(f => new
                {
                    Ano = f.DataAdmissao.Year,
                    Mes = f.DataAdmissao.Month
                })
                .Select(g => new
                {
                    Ano = g.Key.Ano,
                    Mes = g.Key.Mes,
                    Contagem = g.Count()
                })
                .OrderByDescending(x => x.Ano)
                .ThenByDescending(y => y.Mes)
                .Take(12);

            int total = 0;
            foreach (var mes in admissoesUltimosMeses)
            {
                total += mes.Contagem;
            }

            Console.WriteLine($"Total de admissões dos últimos 12 meses: {total}");
        }

        // 9. Quem são os 3 funcionários mais antigos na empresa?
        private static void ExercicioNove()
        {
            var tresMaisAntigos = funcionarios
                .GroupBy(x => x.Nome);
        }
    }
}