namespace Musicas.Models
{
    public class Ouvinte
    {
        public string NomeOuvinte { get; protected set; }
        public List<string> GenerosFavoritos { get; protected set; } = [];
        public List<string> MusicasOuvidas { get; protected set; } = [];

        public Ouvinte(string nomeOuvinte, List<string> generosFavoritos, List<string> musicasOuvidas)
        {
            SetNomeOuvinte(nomeOuvinte);
            SetGenerosFavoritos(generosFavoritos);
            SetMusicasOuvidas(musicasOuvidas);
        }

        public void SetNomeOuvinte(string nomeOuvinte)
        {
            if (string.IsNullOrWhiteSpace(nomeOuvinte))
                throw new Exception("Nome do ouvinte não pode estar vazio");

            NomeOuvinte = nomeOuvinte;
        }

        public void SetGenerosFavoritos(List<string> generosFavoritos)
        {
            GenerosFavoritos = generosFavoritos;
        }

        public void SetMusicasOuvidas(List<string> musicasOuvidas)
        {
            MusicasOuvidas = musicasOuvidas;
        }

        public List<Musica> RecomendarMusicas(List<Musica> musicas)
        {
            var musicasRecomendadas = musicas
                .Where(m => GenerosFavoritos.Contains(m.Genero) && !MusicasOuvidas.Contains(m.Nome))
                .OrderByDescending(m => m.DataDeLancamento)
                .ToList();

            return musicasRecomendadas;
        }
    }
}