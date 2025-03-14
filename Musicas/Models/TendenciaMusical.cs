namespace Musicas.Models
{
    public class TendenciaMusical
    {
        public string Genero { get; protected set; }
        public int Ano { get; protected set; }
        public int QuantidadeMusicas { get; protected set; }

        public TendenciaMusical(string genero, int ano, int quantidadeMusicas)
        {
            Genero = genero;
            Ano = ano;
            QuantidadeMusicas = quantidadeMusicas;
        }

        public void SetGenero(string genero)
        {
            if (string.IsNullOrEmpty(genero))
                throw new Exception("O gênero não pode ser vazio.");

            Genero = genero;
        }

        public void SetAno(int ano)
        {
            if (ano > DateTime.Now.Year)
                throw new Exception("O ano não pode ser maior que o ano atual.");

            Ano = ano;
        }

        public void SetQuantidadeMusicas(int quantidadeMusicas)
        {
            if (quantidadeMusicas < 0)
                throw new Exception("A quantidade de músicas não pode ser negativa.");

            QuantidadeMusicas = quantidadeMusicas;
        }

        // 2. Análise de Tendências Musicais (1,0)
        // Você está desenvolvendo uma ferramenta para analisar tendências 
        // musicais ao longo do tempo. Crie uma classe TendenciaMusical que 
        // contenha o gênero musical, o ano e o número de músicas lançadas nesse 
        // ano para o gênero.
        // Implemente um método na classe TendenciaMusical que receba como 
        // parâmetro uma lista de músicas e retorne uma lista de objetos 
        // TendenciaMusical que represente a evolução do número de músicas 
        // lançadas por gênero ao longo dos anos. O resultado deve ser ordenado 
        // pelo ano de forma ascendente
        public static List<TendenciaMusical> RepresentarTendencia(List<Musica> musicas)
        {
            var tendencias = musicas
                .GroupBy(g => new { g.Genero, g.DataDeLancamento.Year })
                .Select(a => new
                {
                    Genero = a.Key.Genero,
                    Ano = a.Key.Year,
                    QuantidadeMusicas = a.Count()
                })
                .OrderBy(a => a.Ano)
                .ToList();

            List<TendenciaMusical> tendenciasMusicais = new List<TendenciaMusical>();

            foreach (var tendencia in tendencias)
            {
                tendenciasMusicais.Add(new TendenciaMusical(tendencia.Genero, tendencia.Ano, tendencia.QuantidadeMusicas));
            }

            return tendenciasMusicais;
        }
    }
}