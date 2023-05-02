using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly BdreservationSalleContext _context;
        public ReservationRepository(BdreservationSalleContext context)
        {
            _context = context;
        }

        public bool AccepterReservation(string accepterPar, Reservation reservation)
        {
            reservation.IdEtatReservation = 2;
            reservation.TraiterParAdministrateurCourriel = accepterPar;
            reservation.TraiterLe = DateTime.Now;
            _context.Update(reservation);
            return Save();
        }
        public bool RefuserReservation(string refuserPar, Reservation reservation)
        {
            reservation.IdEtatReservation = 3;
            reservation.TraiterParAdministrateurCourriel = refuserPar;
            reservation.TraiterLe = DateTime.Now;
            _context.Update(reservation);
            return Save();
        }
        public bool AnnulerReservation(Reservation reservation)
        {
            reservation.IdEtatReservation = 4;
            _context.Update(reservation);
            return Save();
        }

        public bool CreateReservation(String[] lstInvites, Reservation reservation)
        {
            for (int i = 0; i < lstInvites.Length; i++)
            {
                
                Invitation invitation = new Invitation();
                invitation.MembreCourriel = lstInvites[i];
                invitation.IdEtatInvitation = 1;
                reservation.Invitations.Add(invitation);
            }
            //Ajouter une invitation pour le créateur de la réservation
            Invitation invitationCreateur = new Invitation();
            invitationCreateur.MembreCourriel = reservation.CreerParMembreCourriel;
            invitationCreateur.IdEtatInvitation = 2;
            reservation.Invitations.Add(invitationCreateur);

            _context.Add(reservation);
            return Save();
        }

        public bool DeleteReservation(Reservation reservation)
        {
            _context.Remove(reservation);
            return Save();
        }

        public Reservation GetReservation(int noReservation)
        {
            return _context.Reservations
                .Include(s => s.Invitations)
                .Where(r => r.NoReservation == noReservation)
                .FirstOrDefault();
        }

        public ICollection<Reservation> GetReservations()
        {
            return _context.Reservations
                .OrderBy(R => R.NoReservation)
                .Include(s => s.Invitations)
                .ToList();
        }

        public ICollection<Reservation> GetReservationsByMembre(string membreCourriel)
        {
            return _context.Reservations
                .Include(s => s.Invitations)
                .Where(r => r.CreerParMembreCourriel == membreCourriel)
                .OrderBy(R => R.NoReservation)
                .ToList();
        }

        public ICollection<Reservation> GetReservationsBySalle(int noSalle)
        {
            return _context.Reservations
                .Include(s => s.Invitations)
                .Where(r => r.NoSalle == noSalle)
                .OrderBy(R => R.NoReservation)
                .ToList();
        }

        public ICollection<Reservation> GetReservationsNonTraiter()
        {
            return _context.Reservations
                .Include(s => s.Invitations)
                .Where(r => r.IdEtatReservation == 1)
                .OrderBy(R => R.NoReservation)
                .ToList();
        }

        public bool ReservationExist(int noReservation)
        {
            return _context.Reservations.Any(r => r.NoReservation == noReservation);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReservation(Reservation reservation)
        {
            _context.Update(reservation);
            return Save();
        }

        public ICollection<Reservation> GetReservationsAVenirByMembre(string membreCourriel)
        {
            return _context.Reservations
                .Include(s => s.Invitations)
                .Where(r => r.CreerParMembreCourriel == membreCourriel && 
                            r.DateHeureDebut >= DateTime.Now &&
                            (r.IdEtatReservation == 1 || r.IdEtatReservation == 2))
                .OrderBy(R => R.NoReservation)
                .ToList();
        }

        public ICollection<Reservation> GetReservationsByMembreInvite(string membreCourriel)
        {
            return _context.Reservations
                .Include(s => s.Invitations.Where(i => i.MembreCourriel == membreCourriel))
                .Where(r => (r.CreerParMembreCourriel == membreCourriel || r.Invitations.Count > 0)
                      && r.IdEtatReservation != 4)
                .OrderBy(R => R.NoReservation)
                .ToList();
        }
    }
}
