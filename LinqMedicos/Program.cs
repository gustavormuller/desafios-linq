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
            Ex1();
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
    }
}