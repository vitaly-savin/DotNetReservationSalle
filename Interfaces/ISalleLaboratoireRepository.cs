using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Interfaces
{
    public interface ISalleLaboratoireRepository
    {
        ICollection<SalleLaboratoire> GetSalleLaboratoires();
        SalleLaboratoire GetSalleLaboratoire(int noSalle);
        ICollection<SalleLaboratoire> GetSalleLaboratoiresPourReservation();
        ICollection<SalleLaboratoire> GetSalleLaboratoiresPourReservation(DateTime DateHeureDebut);
        SalleLaboratoire GetSalleLaboratoirePourReservation(int noSalle);
        ICollection<TypeActivite> GetTypeActivitesBySalleLaboratoire(int noSalle);

        bool SalleLaboratoireExist(int noSalle);
        bool CreateSalleLaboratoire(String[] lstTypeActivite, SalleLaboratoire salleLaboratoire);
        bool UpdateSalleLaboratoire(String[] lstTypeActivite, SalleLaboratoire salleLaboratoire);

        bool UpdateSalleLaboratoireEtat(SalleLaboratoire salleLaboratoire);

        bool DeleteSalleLaboratoire(SalleLaboratoire salleLaboratoire);
        bool Save();
    }
}
