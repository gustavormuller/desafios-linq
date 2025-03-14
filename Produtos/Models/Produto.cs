namespace Produtos.Models
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Fabricante { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Empresa { get; set; }

        public Produto(int codigo, string nome, string categoria, string fabricante, double preco, int quantidade, DateTime dataEntrada, string empresa)
        {
            SetCodigo(codigo);
            SetNome(nome);
            SetCategoria(categoria);
            SetFabricante(fabricante);
            SetPreco(preco);
            SetQuantidade(quantidade);
            SetDataEntrada(dataEntrada);
            SetEmpresa(empresa);
        }

        private void SetCodigo(int codigo)
        {
            if (codigo < 0)
                throw new Exception("O código não pode ser menor que zero.");

            Codigo = codigo;
        }

        private void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O nome não pode estar vazio.");

            if (nome.Length > 255)
                throw new Exception("O nome pode conter no máximo 255 caracteres.");

            Nome = nome;
        }

        private void SetCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                throw new Exception("A categoria não pode estar vazia.");

            if (categoria.Length > 255)
                throw new Exception("A categoria pode conter no máximo 255 caracteres.");

            Categoria = categoria;
        }

        private void SetFabricante(string fabricante)
        {
            if (string.IsNullOrWhiteSpace(fabricante))
                throw new Exception("O fabricante não pode estar vazio.");

            if (fabricante.Length > 255)
                throw new Exception("O fabricante pode conter no máximo 255 caracteres.");

            Fabricante = fabricante;
        }

        private void SetPreco(double preco)
        {
            if (preco <= 0)
                throw new Exception("O preço deve ser maior que zero.");

            Preco = preco;
        }

        private void SetQuantidade(int quantidade)
        {
            if (quantidade < 0)
                throw new Exception("A quantidade não pode ser menor que zero.");

            Quantidade = quantidade;
        }

        private void SetDataEntrada(DateTime dataEntrada)
        {
            if (dataEntrada < DateTime.MinValue)
                throw new Exception("A data de entrada não pode ser menor que a data mínima.");

            DataEntrada = dataEntrada;
        }

        private void SetEmpresa(string empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa))
                throw new Exception("A empresa não pode estar vazia.");

            if (empresa.Length > 255)
                throw new Exception("A empresa pode conter no máximo 255 caracteres.");

            Empresa = empresa;
        }
    }
}