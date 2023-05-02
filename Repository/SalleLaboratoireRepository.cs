using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.ObjectModel;

namespace backend.Repository
{
    public class SalleLaboratoireRepository : ISalleLaboratoireRepository
    {
        private readonly BdreservationSalleContext _context;
        public SalleLaboratoireRepository(BdreservationSalleContext context)
        {
            _context = context;
        }

        public bool CreateSalleLaboratoire(String[] lstTypeActivite, SalleLaboratoire salleLaboratoire)
        {
            for (int i = 0; i < lstTypeActivite.Length; i++)
            {
                var typeActivite = _context.TypeActivites.Where(t => t.NomActivite == lstTypeActivite[i]).FirstOrDefault();
                salleLaboratoire.NomActivites.Add(typeActivite);
            }
            _context.Add(salleLaboratoire);

            return Save();
        }

        public bool DeleteSalleLaboratoire(SalleLaboratoire salleLaboratoire)
        {
            _context.Remove(salleLaboratoire);
            return Save();
        }

        public SalleLaboratoire GetSalleLaboratoire(int noSalle)
        {
            
            return _context.SalleLaboratoires
                .Include(s => s.NomActivites)
                .Where(s => s.NoSalle == noSalle)
                    .FirstOrDefault();
        }
        public SalleLaboratoire GetSalleLaboratoirePourReservation(int noSalle)
        {

            return _context.SalleLaboratoires
                .Include(s => s.NomActivites.Where(t => t.EstActif == true))
                .Where(s => s.NoSalle == noSalle && s.EstActif == true)
                    .FirstOrDefault();
        }
        public ICollection<SalleLaboratoire> GetSalleLaboratoires()
        {
           
            return _context.SalleLaboratoires
                    .OrderBy(a => a.NoSalle)
                    .Include(s => s.NomActivites)
                    .ToList();   
        }

        public ICollection<SalleLaboratoire> GetSalleLaboratoiresPourReservation()
        {
            return _context.SalleLaboratoires
                .Include(s => s.NomActivites.Where(t => t.EstActif == true))
                .Where(s => s.EstActif == true)
                .OrderBy(a => a.NoSalle)
                .ToList();
        }

        public ICollection<SalleLaboratoire> GetSalleLaboratoiresPourReservation(DateTime DateHeureDebut)
        {
            /*
            Select SalleLaboratoire.*
            FROM[dbo].[SalleLaboratoire]
            WHERE noSalle not in (  SELECT noSalle
                                    FROM[dbo].[Reservation]
                                    WHERE '2023-04-22 12:00:00' >= dateHeureDebut and
                                          '2023-04-22 12:00:00' < dateHeureFin
                                        AND(Reservation.idEtatReservation = 1 OR Reservation.idEtatReservation = 2))
		    AND SalleLaboratoire.estActif = 1
        */
            var sallesND = from r in _context.Reservations
                           where((DateHeureDebut >= r.DateHeureDebut)
                                    && (DateHeureDebut < r.DateHeureFin)
                                    && (r.IdEtatReservation == 1 || r.IdEtatReservation == 2))
                           select r.NoSalle;

            return _context.SalleLaboratoires
                    .Include(s => s.NomActivites.Where(t => t.EstActif == true))
                    .Where(s => s.EstActif == true 
                           && !sallesND.Contains(s.NoSalle))
                    .OrderBy(a => a.NoSalle)
                    .ToList();
        }

        public ICollection<TypeActivite> GetTypeActivitesBySalleLaboratoire(int noSalle)
        {
          //return _context.SalleLaboratoireTypeActivites.Where(sa => sa.NoSalle == noSalle).Select(t => t.TypeActivite).ToList();
            SalleLaboratoire salleLaboratoire = new SalleLaboratoire();
            salleLaboratoire = _context.SalleLaboratoires
                    .Where(s => s.NoSalle == noSalle)
                    .Include(s => s.NomActivites)
                    .FirstOrDefault();
            ICollection<TypeActivite> lstTypeActivite = salleLaboratoire.NomActivites;
            
            return lstTypeActivite;
        }

        public bool SalleLaboratoireExist(int noSalle)
        {
            return _context.SalleLaboratoires.Any(s => s.NoSalle == noSalle);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateSalleLaboratoire(String[] lstTypeActivite, SalleLaboratoire salleLaboratoireNew)
        {
            
            SalleLaboratoire salleLaboratoire = _context.SalleLaboratoires
                                .Where(s => s.NoSalle == salleLaboratoireNew.NoSalle)
                                .Include(s => s.NomActivites)
                                .FirstOrDefault();
            
            //Effacer l'ancienne liste d'activité
            //ICollection<TypeActivite> lstTypeActiviteOld = GetTypeActivitesBySalleLaboratoire(salleLaboratoire.NoSalle);
            var lstTypeActiviteOld = salleLaboratoire.NomActivites.ToArray();
           
           // var lstTypeActiviteOld = GetTypeActivitesBySalleLaboratoire(salleLaboratoire.NoSalle).ToArray();
            for (int i = 0; i < lstTypeActiviteOld.Length; i++)
            {
                salleLaboratoire.NomActivites.Remove(lstTypeActiviteOld[i]);
            }
            _context.Update(salleLaboratoire);
            Save();
            
            salleLaboratoire.NoSalle = salleLaboratoireNew.NoSalle;
            salleLaboratoire.Capacite = salleLaboratoireNew.Capacite;
            salleLaboratoire.Description = salleLaboratoireNew.Description;
            salleLaboratoire.EstActif = salleLaboratoireNew.EstActif;
            salleLaboratoire.CreerParAdministrateurCourriel = salleLaboratoireNew.CreerParAdministrateurCourriel;

            //Ajouter la nouvelle liste d'activité
            for (int i = 0; i < lstTypeActivite.Length; i++)
            {
                var typeActivite = _context.TypeActivites.Where(t => t.NomActivite == lstTypeActivite[i]).FirstOrDefault();
                salleLaboratoire.NomActivites.Add(typeActivite);
            }
            _context.Update(salleLaboratoire);
            return Save();
        }

        public bool UpdateSalleLaboratoireEtat(SalleLaboratoire salleLaboratoire)
        {
            _context.Update(salleLaboratoire);
            return Save();
        }
    }
}
