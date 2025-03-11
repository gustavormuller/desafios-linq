namespace LinqMedicos.Models
{
    class Medico
    {
        public string NomeMedico { get; protected set; }
        public List<string> Especialidades { get; protected set; } = [];

        public Medico(string nomeMedico, List<string> especialidades)
        {
            SetNomeMedico(nomeMedico);
            SetEspecialidades(especialidades);
        }

        public void SetNomeMedico(string nomeMedico)
        {
            if (string.IsNullOrWhiteSpace(nomeMedico))
                throw new Exception("Nome do médico não pode estar vazio.");

            NomeMedico = nomeMedico;
        }

        public void SetEspecialidades(List<string> especialidades)
        {
            Especialidades = especialidades;
        }
    }
}