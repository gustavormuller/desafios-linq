namespace Atendimentos.Models
{
    public class Atendimento
    {
        public int CodigoAtendimento { get; protected set; }
        public DateTime DataAbertura { get; protected set; }
        public string Seguradora { get; protected set; }
        public string ItemDanificado { get; protected set; }
        public double ValorFranquia { get; protected set; }
        public string NomeSegurado { get; protected set; }
        public string NomeAtendente { get; protected set; }
        public string Cidade { get; protected set; }
        public string Estado { get; protected set; }
        public int NumeroApolice { get; protected set; }
        public string NomeVeiculo { get; protected set; }

        public Atendimento(int codigoAtendimento, DateTime dataAbertura, string seguradora, string itemDanificado, double valorFranquia, string nomeSegurado, string nomeAtendente, string cidade, string estado, int numeroApolice, string nomeVeiculo)
        {
            SetCodigoAtendimento(codigoAtendimento);
            SetDataAbertura(dataAbertura);
            SetSeguradora(seguradora);
            SetItemDanificado(itemDanificado);
            SetValorFranquia(valorFranquia);
            SetNomeSegurado(nomeSegurado);
            SetNomeAtendente(nomeAtendente);
            SetCidade(cidade);
            SetEstado(estado);
            SetNumeroApolice(numeroApolice);
            SetNomeVeiculo(nomeVeiculo);
        }

        private void SetCodigoAtendimento(int codigoAtendimento)
        {
            if (codigoAtendimento < 0)
                throw new Exception("Código de atendimento não pode ser menor que zero.");

            CodigoAtendimento = codigoAtendimento;
        }

        private void SetDataAbertura(DateTime dataAbertura)
        {
            if (DataAbertura < DateTime.MinValue)
                throw new Exception("Data de abertura não pode ser menor que a data mínima");

            DataAbertura = dataAbertura;
        }

        private void SetSeguradora(string seguradora)
        {
            if (string.IsNullOrWhiteSpace(seguradora))
                throw new Exception("A seguradora não pode estar vazia");

            Seguradora = seguradora;
        }

        private void SetItemDanificado(string itemDanificado)
        {
            if (string.IsNullOrWhiteSpace(itemDanificado))
                throw new Exception("O item danificado não pode estar vazio");

            ItemDanificado = itemDanificado;
        }

        private void SetValorFranquia(double valorFranquia)
        {
            ValorFranquia = valorFranquia;
        }

        private void SetNomeSegurado(string nomeSegurado)
        {
            if (string.IsNullOrWhiteSpace(nomeSegurado))
                throw new Exception("O nome do segurado não pode estar vazio");

            NomeSegurado = nomeSegurado;
        }

        private void SetNomeAtendente(string nomeAtendente)
        {
            if (string.IsNullOrWhiteSpace(nomeAtendente))
                throw new Exception("O nome do atendente não pode estar vazio");

            NomeAtendente = nomeAtendente;
        }

        private void SetCidade(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new Exception("A cidade não pode estar vazia");

            Cidade = cidade;
        }

        private void SetEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new Exception("O estado não pode estar vazio");

            Estado = estado;
        }

        private void SetNumeroApolice(int numeroApolice)
        {
            if (numeroApolice < 0)
                throw new Exception("O número da apólice não pode estar vazio");
        }

        private void SetNomeVeiculo(string nomeVeiculo)
        {
            if (string.IsNullOrWhiteSpace(nomeVeiculo))
                throw new Exception("O nome do veículo não pode estar vazio");

            NomeVeiculo = nomeVeiculo;
        }
    }
}