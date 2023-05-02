using backend.Models;

namespace backend.Interfaces
{
    public interface ITypeActiviteRepository
    {
        
        ICollection<TypeActivite> GetTypeActivites();
        ICollection<TypeActivite> GetTypeActivitesActifs();
        TypeActivite GetTypeActivite(string nomActivite);
        ICollection<SalleLaboratoire> GetSalleLaboratoiresByTypeActivite(string nomActivite);
        bool TypeActiviteExist(string nomActivite);

        bool CreateTypeActivite(TypeActivite typeActivite);

        bool UpdateTypeActivite(TypeActivite typeActivite);

        bool UpdateTypeActiviteEtat(TypeActivite typeActivite);

        bool DeleteTypeActivite(TypeActivite typeActivite);

        bool Save();
    }
}
