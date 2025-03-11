using System.Globalization;
using System.Text.RegularExpressions;
using LinqMedicos.Models;
using NPOI.SS.UserModel;

namespace LinqMedicos
{
    class Program
    {
        private static List<Consulta> consultas = [];

        static void Main(string[] args)
        {
            string caminhoArquivo = Path.Combine(Environment.CurrentDirectory, "DesafioMedicos.xlsx");

            ImportarDadosPlanilha(caminhoArquivo);

            Console.WriteLine("Digite o número relacionado ao exercício que deseja ver:");
            Console.WriteLine("1 - Exercício 1");
            Console.WriteLine("2 - Exercício 2");
            Console.WriteLine("3 - Exercício 3");
            Console.WriteLine("4 - Exercício 4");
            Console.WriteLine("5 - Exercício 5");
            Console.WriteLine("0 - Sair");
            int option = int.Parse(Console.ReadLine());

            while (option != 0)
            {
                switch (option)
                {
                    case 1:
                        Ex1();
                        break;

                    case 2:
                        Ex2();
                        break;

                    case 3:
                        Ex3();
                        break;

                    case 4:
                        Ex4();
                        break;

                    case 5:
                        Ex5();
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine("Digite o número relacionado ao exercício que deseja ver:");
                Console.WriteLine("1 - Exercício 1");
                Console.WriteLine("2 - Exercício 2");
                Console.WriteLine("3 - Exercício 3");
                Console.WriteLine("4 - Exercício 4");
                Console.WriteLine("5 - Exercício 5");
                Console.WriteLine("0 - Sair");
                option = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Saindo...");
        }

        static void ImportarDadosPlanilha(string caminhoArquivo)
        {
            try
            {
                IWorkbook pasta = WorkbookFactory.Create(caminhoArquivo);

                ISheet planilha = pasta.GetSheetAt(0);

                for (int i = 1; i < planilha.PhysicalNumberOfRows; i++)
                {
                    IRow linha = planilha.GetRow(i);

                    DateTime dataConsulta = DateTime.Parse(linha.GetCell(0).StringCellValue);
                    string horaDaConsulta = linha.GetCell(1).ToString();
                    string nomePaciente = linha.GetCell(2).ToString();
                    string numeroTelefone = linha.GetCell(3)?.ToString() ?? "";
                    string cpfString = Regex.Replace(linha.GetCell(4).ToString(), @"\D", "");
                    long cpf = long.Parse(cpfString);
                    string rua = linha.GetCell(5).ToString();
                    string cidade = linha.GetCell(6).ToString();
                    string estado = linha.GetCell(7).ToString();
                    string especialidade = linha.GetCell(8).ToString();
                    string nomeMedico = linha.GetCell(9).ToString();
                    bool particular = linha.GetCell(10).ToString().Trim().Equals("Sim", StringComparison.OrdinalIgnoreCase);
                    long numeroDaCarteirinha = long.Parse(linha.GetCell(11).ToString());
                    double valorConsulta = double.Parse(linha.GetCell(12).ToString(), CultureInfo.InvariantCulture);

                    consultas.Add(new Consulta(dataConsulta, horaDaConsulta, nomePaciente, numeroTelefone, cpf, rua, cidade, estado, especialidade, nomeMedico, particular, numeroDaCarteirinha, valorConsulta));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        // 1 – Liste ao total quantos pacientes temos para atender do dia 27/03 até dia 31/03. Sem repetições.
        static void Ex1()
        {
            DateTime inicio = new DateTime(2023, 3, 27);
            DateTime fim = new DateTime(2023, 3, 31);

            var totalPacientes = consultas.Where(d => d.DataConsulta >= inicio && d.DataConsulta <= fim).Select(p => p.NomePaciente).Distinct().Count();

            Console.WriteLine($"Total: {totalPacientes} pacientes");
        }

        // 2 – Liste ao total quantos médicos temos trabalhando em nosso consultório. Conte a quantidade de médicos sem repetições. 
        static void Ex2()
        {
            var totalMedicos = consultas.Select(n => n.NomeMedico).Distinct().Count();

            Console.WriteLine($"Total: {totalMedicos} médicos");
        }

        // 3 – Liste o nome dos médicos e suas especialidades.
        static void Ex3()
        {
            var especialidades = consultas
                .GroupBy(x => x.NomeMedico)
                .Select(n => new
                {
                    nome = n.Key,
                    especialidade = n.Select(c => c.Especialidade).Distinct()
                });

            foreach (var medico in especialidades)
            {
                Console.WriteLine($"Médico: {medico.nome}, Especialidade: {string.Join(", ", medico.especialidade)}");
            }
        }

        // 4 – Liste o total em valor de consulta que receberemos. Some o valor de todas as consultas.
        static void Ex4()
        {
            var totalEmConsultas = consultas.Select(v => v.ValorConsulta).Sum();
            var totalPorEspecialidade = consultas.GroupBy(e => e.Especialidade).Select(g => new
            {
                especialidade = g.Key,
                total = g.Select(v => v.ValorConsulta).Sum()
            });

            Console.WriteLine($"Total em dinheiro das consultas: R$ {totalEmConsultas.ToString("F2", CultureInfo.InvariantCulture)}");

            foreach (var especialidade in totalPorEspecialidade)
            {
                Console.WriteLine($"{especialidade.especialidade} - R$ {especialidade.total.ToString("F2", CultureInfo.InvariantCulture)}");
            }
        }

        // 5 – Para o dia 30/03. Quantas consultas vão ser realizadas? Quantas são Particular? Liste para esse dia os horários de consulta de cada médico e suas especialidades.
        static void Ex5()
        {
            DateTime dia = new DateTime(2023, 03, 30);

            var totalConsultas = consultas.Where(d => d.DataConsulta == dia).Count();
            var particular = consultas.Where(d => d.DataConsulta == dia).Where(p => p.Particular).Count();

            Console.WriteLine($"Para o dia 30/03 - Total de {totalConsultas} consultas. {particular} particular e {totalConsultas - particular} convênios.");

            var medicos = consultas.Where(d => d.DataConsulta == dia).GroupBy(m => m.NomeMedico).Select(n => new
            {
                medico = n.Key,
                especialidade = n.Select(e => e.Especialidade).Distinct(),
                horario = n.Select(h => h.HoraDaConsulta)
            }).ToList();

            foreach (var medico in medicos)
            {
                Console.WriteLine($"{medico.medico} - {string.Join(',', medico.especialidade)} : {string.Join(',', medico.horario)}");
            }
        }
    }
}