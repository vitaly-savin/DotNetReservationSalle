using backend.Interfaces;
using backend.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace backend.Repository
{
    public class TypeActiviteRepository : ITypeActiviteRepository
    {
        private readonly BdreservationSalleContext _context;
        public TypeActiviteRepository(BdreservationSalleContext context) 
        {
            _context = context;
        }

        public bool CreateTypeActivite(TypeActivite typeActivite)
        {
            _context.Add(typeActivite);
            return Save();
        }

        public bool DeleteTypeActivite(TypeActivite typeActivite)
        {
            _context.Remove(typeActivite);
            return Save();
        }

        public ICollection<SalleLaboratoire> GetSalleLaboratoiresByTypeActivite(string nomActivite)
            
        {
            //todo
            //return _context.TypeActivites.Where(sa => sa.NomActivite == nomActivite).Select(t => t.NoSalles).ToList();
            //return _context.SalleLaboratoire_TypeActivites.Where(sa => sa.NomActivite == nomActivite).Select(t => t.SalleLaboratoire).ToList();
            return null;
        }

        public TypeActivite GetTypeActivite(string nomActivite)
        {
            return _context.TypeActivites.Where(t => t.NomActivite == nomActivite).FirstOrDefault();
        }

        public ICollection<TypeActivite> GetTypeActivites() 
        {
            return _context.TypeActivites.OrderBy(a => a.NomActivite).ToList();
        }

        public ICollection<TypeActivite> GetTypeActivitesActifs()
        {
            return _context.TypeActivites
                .Where(t => t.EstActif == true)
                .OrderBy(a => a.NomActivite)
                .ToList();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TypeActiviteExist(string nomActivite)
        {
            return _context.TypeActivites.Any(t => t.NomActivite == nomActivite);
        }

        public bool UpdateTypeActivite(TypeActivite typeActivite)
        {
            _context.Update(typeActivite);
            return Save();
        }

        public bool UpdateTypeActiviteEtat(TypeActivite typeActivite)
        {
            _context.Update(typeActivite);
            return Save();
        }
    }
}
