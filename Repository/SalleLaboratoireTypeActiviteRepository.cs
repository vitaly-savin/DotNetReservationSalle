using backend.Interfaces;
using backend.Models;
namespace backend.Repository
{
    public class SalleLaboratoireTypeActiviteRepository : ISalleLaboratoireTypeActiviteRepository
    {
        private readonly BdreservationSalleContext _context;
        public SalleLaboratoireTypeActiviteRepository(BdreservationSalleContext context)
        {
            _context = context;
        }

        public bool CreateSalleLaboratoireTypeActivite(SalleLaboratoireTypeActivite salleLaboratoireTypeActivite)
        {
            _context.Add(salleLaboratoireTypeActivite);
            return Save();
        }

        public bool DeleteSalleLaboratoireTypeActivite(SalleLaboratoireTypeActivite salleLaboratoireTypeActivite)
        {
            _context.Remove(salleLaboratoireTypeActivite);
            return Save();
        }

        public SalleLaboratoireTypeActivite GetSalleLaboratoireTypeActivite(int noSalle, string nomActivite)
        {
            //return _context.SalleLaboratoireTypeActivites.Where(t => t.NoSalle == noSalle && t.NomActivite == nomActivite).FirstOrDefault();
            return null;
        }

        public ICollection<SalleLaboratoireTypeActivite> GetSalleLaboratoireTypeActivites()
        {
            //return _context.SalleLaboratoireTypeActivites.OrderBy(sa => sa.NoSalle).ToList();
            return null;
        }

        public ICollection<SalleLaboratoireTypeActivite> GetSalleLaboratoireTypeActivites(int noSalle)
        {
            //todo
            //return _context.SalleLaboratoire_TypeActivites.Where(s => s.NoSalle == noSalle).OrderBy(t => t.NomActivite).ToList();
            return null;
        }

        public bool SalleLaboratoireExist(int noSalle)
        {
            return _context.SalleLaboratoires.Any(sa => sa.NoSalle == noSalle);
        }

        public bool SalleLaboratoireTypeActiviteExist(int noSalle, string nomActivite)
        {
            //todo
            //return _context.SalleLaboratoire_TypeActivites.Any(sa => sa.NoSalle == noSalle && sa.NomActivite == nomActivite);
            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
