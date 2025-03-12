namespace Musicas.Models
{
    class Musica
    {
        public int Codigo { get; protected set; }
        public DateTime DataDeLancamento { get; protected set; }
        public string Nome { get; protected set; }
        public string Artista { get; protected set; }
        public string Album { get; protected set; }
        public string Genero { get; protected set; }
        public double Duracao { get; protected set; }
        public string Gravadora { get; protected set; }
        public string Pais { get; protected set; }
        public string Idioma { get; protected set; }

        public Musica(int codigo, DateTime dataDeLancamento, string nome, string artista, string album, string genero, double duracao, string gravadora, string pais, string idioma)
        {
            SetCodigo(codigo);
            SetDataDeLancamento(dataDeLancamento);
            SetNome(nome);
            SetArtista(artista);
            SetAlbum(album);
            SetGenero(genero);
            SetDuracao(duracao);
            SetGravadora(gravadora);
            SetPais(pais);
            SetIdioma(idioma);
        }

        public void SetCodigo(int codigo)
        {
            if (codigo < 0)
                throw new Exception("O código não pode ser menor que 0");

            Codigo = codigo;
        }

        public void SetDataDeLancamento(DateTime dataDeLancamento)
        {
            if (dataDeLancamento < DateTime.MinValue)
                throw new Exception("A data de lançamento não pode ser anterior à data mínima");

            DataDeLancamento = dataDeLancamento;
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O nome não pode estar vazio");

            Nome = nome;
        }

        public void SetArtista(string artista)
        {
            if (string.IsNullOrWhiteSpace(artista))
                throw new Exception("O artista não pode estar vazio");

            Artista = artista;
        }

        public void SetAlbum(string album)
        {
            if (string.IsNullOrWhiteSpace(album))
                throw new Exception("O álbum não pode estar vazio");

            Album = album;
        }

        public void SetGenero(string genero)
        {
            if (string.IsNullOrWhiteSpace(genero))
                throw new Exception("O gênero não pode estar vazio");

            Genero = genero;
        }

        public void SetDuracao(double duracao)
        {
            if (duracao <= 0)
                throw new Exception("A duração da música deve ser maior que zero.");

            Duracao = duracao;
        }

        public void SetGravadora(string gravadora)
        {
            if (string.IsNullOrWhiteSpace(gravadora))
                throw new Exception("A gravadora não pode estar vazio");

            Gravadora = gravadora;
        }

        public void SetPais(string pais)
        {
            if (string.IsNullOrWhiteSpace(pais))
                throw new Exception("O país não pode estar vazio");

            Pais = pais;
        }

        public void SetIdioma(string idioma)
        {
            if (string.IsNullOrWhiteSpace(idioma))
                throw new Exception("O idioma não pode estar vazio");

            Idioma = idioma;
        }
    }
}