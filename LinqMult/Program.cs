using System.Globalization;
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
                Console.WriteLine("7 - Exercício 7");
                Console.WriteLine("8 - Exercício 8");
                Console.WriteLine("9 - Exercício 9");
                Console.WriteLine("10 - Exercício 10");
                Console.WriteLine("11 - Exercício 11");
                Console.WriteLine("12 - Exercício 12");
                Console.WriteLine("13 - Exercício 13");
                Console.WriteLine("14 - Exercício 14");
                Console.WriteLine("15 - Exercício 15");
                Console.WriteLine("16 - Exercício 16");
                Console.WriteLine("17 - Exercício 17");
                Console.WriteLine("0 - Sair");

                // Captura a opção do usuário
                option = int.Parse(Console.ReadLine());

                if (option != 0)
                {
                    switch (option)
                    {
                        case 1:
                            ExercicioUm();
                            break;

                        case 2:
                            ExercicioDois();
                            break;

                        case 3:
                            ExercicioTres();
                            break;

                        case 4:
                            ExercicioQuatro();
                            break;

                        case 5:
                            ExercicioCinco();
                            break;

                        case 6:
                            ExercicioSeis();
                            break;

                        case 7:
                            ExercicioSete();
                            break;

                        case 8:
                            ExercicioOito();
                            break;

                        case 9:
                            ExercicioNove();
                            break;

                        case 10:
                            ExercicioDez();
                            break;

                        case 11:
                            ExercicioOnze();
                            break;

                        case 12:
                            ExercicioDoze();
                            break;

                        case 13:
                            ExercicioTreze();
                            break;

                        case 14:
                            ExercicioQuatorze();
                            break;

                        case 15:
                            ExercicioQuinze();
                            break;

                        case 16:
                            ExercicioDezesseis();
                            break;

                        case 17:
                            ExercicioDezessete();
                            break;

                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }

                    // Pergunta se o usuário deseja ver outro exercício
                    Console.WriteLine("Deseja ver outro exercício? (s/n)");
                    string resposta = Console.ReadLine().ToLower();

                    if (resposta != "s")
                    {
                        break; // Sai do loop se a resposta não for "s"
                    }

                }
            } while (option != 0); // Continua até o usuário escolher a opção 0 para sair

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
            var tresMaisAntigos = funcionarios.OrderBy(d => d.DataAdmissao).Select(n => new
            {
                nome = n.Nome,
                data = n.DataAdmissao
            }).Take(3);

            foreach (var func in tresMaisAntigos)
            {
                Console.WriteLine($"{func.nome} - {func.data}");
            }
        }

        // 10. Qual é o tempo médio de empresa dos funcionários (em anos)?
        private static void ExercicioDez()
        {
            var tempoMedio = funcionarios.Select(d => DateTime.Now.Year - d.DataAdmissao.Year).Average();

            Console.WriteLine($"Tempo médio dos funcionários em anos: {Math.Floor(tempoMedio)}");
        }

        // 11. Quantos funcionários existem em cada estado?
        private static void ExercicioOnze()
        {
            var funcPorEstado = funcionarios.GroupBy(e => e.Estado).Select(f => new
            {
                estado = f.Key,
                quantidade = f.Select(n => n.Nome).Count()
            });

            foreach (var func in funcPorEstado)
            {
                Console.WriteLine($"{func.estado}: {func.quantidade} funcionários");
            }
        }

        // 12. Liste os 3 cargos mais comuns na empresa.
        private static void ExercicioDoze()
        {
            var comuns = funcionarios
            .GroupBy(c => c.Cargo)
            .Select(c => new
            {
                cargo = c.Key,
                quantidade = c.Count()
            })
            .OrderByDescending(c => c.quantidade)
            .Take(3)
            .ToList();

            foreach (var cargo in comuns)
            {
                Console.WriteLine($"{cargo.cargo} - {cargo.quantidade}");
            }
        }

        // 13. Qual funcionário recebe o maior salário dentro de cada departamento?
        private static void ExercicioTreze()
        {
            var maiorSalario = funcionarios
                .GroupBy(d => d.Departamento)
                .Select(s => new
                {
                    departamento = s.Key,
                    salario = s.Max(x => x.Salario),
                    nome = s.OrderByDescending(x => x.Salario).FirstOrDefault().Nome
                })
                .ToList();

            foreach (var func in maiorSalario)
            {
                Console.WriteLine($"{func.departamento} - {func.nome} - {func.salario}");
            }
        }

        // 14. Qual é a média salarial de cada estado?
        private static void ExercicioQuatorze()
        {
            var mediaSalarialPorEstado = funcionarios
                .GroupBy(e => e.Estado)
                .Select(s => new
                {
                    estado = s.Key,
                    media = s.Average(x => x.Salario)
                })
                .ToList();

            foreach (var media in mediaSalarialPorEstado)
            {
                Console.WriteLine($"{media.estado} - R$ {media.media.ToString("F2", CultureInfo.InvariantCulture)}");
            }
        }

        // 15. Liste os funcionários admitidos antes de 2015.
        private static void ExercicioQuinze()
        {
            var admitidosAntes = funcionarios
                .Select(d => d.DataAdmissao)
                .Where(d => d.Year < 2015)
                .Count();

            Console.WriteLine($"Funcionários admitidos antes de 2015: {admitidosAntes}");
        }

        // 16. Quem é o funcionário mais recente da empresa?
        private static void ExercicioDezesseis()
        {
            var maisRecente = funcionarios
                .OrderByDescending(d => d.DataAdmissao)
                .Select(n => n.Nome)
                .FirstOrDefault();

            Console.WriteLine($"Funcionário mais recente: {maisRecente}");
        }

        // 17. Qual cargo tem a maior variação salarial (máximo - mínimo)?
        private static void ExercicioDezessete()
        {
            var maiorVariacao = funcionarios
                .GroupBy(c => c.Cargo)
                .Select(m => new
                {
                    cargo = m.Key,
                    variacao = m.Max(s => s.Salario) - m.Min(s => s.Salario)
                })
                .OrderByDescending(v => v.variacao)
                .FirstOrDefault();

            Console.WriteLine($"Cargo com maior variação salarial: {maiorVariacao.cargo} - Variação: {maiorVariacao.variacao:C}");
        }
    }
}