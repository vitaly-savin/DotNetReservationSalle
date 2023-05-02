using backend.Models;

namespace backend.Interfaces
{
    public interface ISalleLaboratoireTypeActiviteRepository
    {
        ICollection<SalleLaboratoireTypeActivite> GetSalleLaboratoireTypeActivites();
        ICollection<SalleLaboratoireTypeActivite> GetSalleLaboratoireTypeActivites(int noSalle);
        SalleLaboratoireTypeActivite GetSalleLaboratoireTypeActivite(int noSalle, string nomActivite);
        bool SalleLaboratoireExist(int noSalle);

        bool SalleLaboratoireTypeActiviteExist(int noSalle, string nomActivite);
        bool CreateSalleLaboratoireTypeActivite(SalleLaboratoireTypeActivite SalleLaboratoireTypeActivite);
        bool DeleteSalleLaboratoireTypeActivite(SalleLaboratoireTypeActivite salleLaboratoireTypeActivite);
        bool Save();
    }
}
