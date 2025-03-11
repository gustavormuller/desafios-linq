namespace LinqMedicos.Models
{
    class Consulta
    {
        public DateTime DataConsulta { get; protected set; }
        public string HoraDaConsulta { get; protected set; }
        public string NomePaciente { get; protected set; }
        public string NumeroTelefone { get; protected set; }
        public long Cpf { get; protected set; }
        public string Rua { get; protected set; }
        public string Cidade { get; protected set; }
        public string Estado { get; protected set; }
        public string Especialidade { get; protected set; }
        public string NomeMedico { get; protected set; }
        public bool Particular { get; protected set; }
        public long NumeroDaCarteirinha { get; protected set; }
        public double ValorConsulta { get; protected set; }

        public Consulta(DateTime dataConsulta, string horaDaConsulta, string nomePaciente, string numeroTelefone, long cpf, string rua, string cidade, string estado, string especialidade, string nomeMedico, bool particular, long numeroDaCarteirinha, double valorConsulta)
        {
            SetDataConsulta(dataConsulta);
            SetHoraDaConsulta(horaDaConsulta);
            SetNomePaciente(nomePaciente);
            SetNumeroTelefone(numeroTelefone);
            SetCpf(cpf);
            SetRua(rua);
            SetCidade(cidade);
            SetEstado(estado);
            SetEspecialidade(especialidade);
            SetNomeMedico(nomeMedico);
            SetParticular(particular);
            SetNumeroDaCarteirinha(numeroDaCarteirinha);
            SetValorConsulta(valorConsulta);
        }

        public void SetDataConsulta(DateTime dataConsulta)
        {
            if (dataConsulta < DateTime.MinValue)
                throw new Exception("Data da consulta não pode ser antes de hoje.");

            DataConsulta = dataConsulta;
        }

        public void SetHoraDaConsulta(string horaDaConsulta)
        {
            if (string.IsNullOrWhiteSpace(horaDaConsulta))
                throw new Exception("Hora da consulta não pode estar vazia.");

            HoraDaConsulta = horaDaConsulta;
        }

        public void SetNomePaciente(string nomePaciente)
        {
            if (string.IsNullOrWhiteSpace(nomePaciente))
                throw new Exception("Nome do paciente não pode estar vazio.");
            NomePaciente = nomePaciente;
        }

        public void SetNumeroTelefone(string numeroTelefone)
        {
            NumeroTelefone = numeroTelefone;
        }

        public void SetCpf(long cpf)
        {
            if (cpf <= 0)
                throw new Exception("CPF deve ter 11 dígitos.");
            
            Cpf = cpf;
        }

        public void SetRua(string rua)
        {
            if (string.IsNullOrWhiteSpace(rua))
                throw new Exception("O campo rua não pode estar vazio.");

            Rua = rua;
        }

        public void SetCidade(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new Exception("Cidade não pode estar vazio.");

            Cidade = cidade;
        }

        public void SetEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new Exception("Estado não pode estar vazio.");

            Estado = estado;
        }

        public void SetEspecialidade(string especialidade)
        {
            if (string.IsNullOrWhiteSpace(especialidade))
                throw new Exception("Especialidade não pode estar vazio.");

            Especialidade = especialidade;
        }

        public void SetNomeMedico(string nomeMedico)
        {
            if (string.IsNullOrWhiteSpace(nomeMedico))
                throw new Exception("Nome do médico não pode estar vazio.");

            NomeMedico = nomeMedico;
        }

        public void SetParticular(bool particular)
        {
            Particular = particular;
        }

        public void SetNumeroDaCarteirinha(long numeroDaCarteirinha)
        {
            if (numeroDaCarteirinha <= 0)
                throw new Exception("Número da carteirinha inválido.");
            NumeroDaCarteirinha = numeroDaCarteirinha;
        }

        public void SetValorConsulta(double valorConsulta)
        {
            ValorConsulta = valorConsulta;
        }

    }
}