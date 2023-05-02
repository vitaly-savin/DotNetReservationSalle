using backend.Models;

namespace backend.Dto
{
    public class SalleLaboratoireDto
    {
        public int NoSalle { get; set; }

        public int? Capacite { get; set; }

        public string? Description { get; set; }

        public bool? EstActif { get; set; }

        public string CreerParAdministrateurCourriel { get; set; }
        public virtual ICollection<TypeActivite> NomActivites { get; set; } = new List<TypeActivite>();
    }
}
