using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly BdreservationSalleContext _context;
        public InvitationRepository(BdreservationSalleContext context)
        {
            _context = context;
        }
        public bool CreateInvitation(Invitation invitation)
        {
            _context.Add(invitation);
            return Save();
        }

        public bool DeleteInvitation(Invitation invitation)
        {
            _context.Remove(invitation);
            return Save();
        }

        public Invitation GetInvitation(int noReservation, string membreCourriel)
        {
            return _context.Invitations
                 .Include(s => s.IdEtatInvitationNavigation)
                //.Include(s => s.NoReservationNavigation)
                //.Include(s => s.MembreCourrielNavigation)
                .Where(i => i.NoReservation == noReservation && 
                            i.MembreCourriel == membreCourriel)
                .FirstOrDefault();
        }

        public ICollection<Invitation> GetInvitations()
        {
            return _context.Invitations
                .Include(s => s.IdEtatInvitationNavigation)
                //.Include(s => s.NoReservationNavigation)
                // .Include(s => s.MembreCourrielNavigation)
                .OrderBy(i => i.NoReservation)
                .ToList();
        }

        public ICollection<Invitation> GetInvitationsByReservation(int noReservation)
        {
            return _context.Invitations
                    .Include(s => s.IdEtatInvitationNavigation)
                    //.Include(s => s.NoReservationNavigation)
                    //.Include(s => s.MembreCourrielNavigation)
                    .Where(i => i.NoReservation == noReservation)
                    .OrderBy(i => i.MembreCourriel)
                    .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool InvitationExist(int noReservation, string membreCourriel)
        {
            return _context.Invitations.Any(i => i.NoReservation == noReservation &&
                                                 i.MembreCourriel == membreCourriel);
        }

        public bool UpdateInvitation(Invitation invitation)
        {
            _context.Update(invitation);
            return Save();
        }

        public bool AccepterInvitation(Invitation invitation)
        {
            invitation.IdEtatInvitation = 2;
            invitation.DateReponse = DateTime.Now;
            _context.Update(invitation);
            return Save();
        }
        public bool RefuserInvitation(Invitation invitation)
        {
            invitation.IdEtatInvitation = 3;
            invitation.DateReponse = DateTime.Now;
            _context.Update(invitation);
            return Save();
        }

        public ICollection<Invitation> GetInvitationsByMembre(string membreCourriel)
        {
            return _context.Invitations
                 .Include(s => s.IdEtatInvitationNavigation)
                //.Include(s => s.NoReservationNavigation)
                //.Include(s => s.MembreCourrielNavigation)
                .Where(i => i.MembreCourriel == membreCourriel)
                .OrderBy(i => i.NoReservation)
                .ToList();
        }

        public ICollection<Invitation> GetInvitationsAccepteByReservation(int noReservation)
        {
            return _context.Invitations
                 .Include(s => s.IdEtatInvitationNavigation)
                //.Include(s => s.NoReservationNavigation)
                //.Include(s => s.MembreCourrielNavigation)
                .Where(i => i.NoReservation == noReservation &&
                            i.IdEtatInvitation == 2)
                .OrderBy(i => i.MembreCourriel)
                .ToList();
        }
    }
}
