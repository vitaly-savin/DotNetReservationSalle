using backend.Models;

namespace backend.Interfaces
{
    public interface IInvitationRepository
    {
        ICollection<Invitation> GetInvitations();
        Invitation GetInvitation(int noReservation, string membreCourriel);
        ICollection<Invitation> GetInvitationsByReservation(int noReservation);
        ICollection<Invitation> GetInvitationsAccepteByReservation(int noReservation);
        ICollection<Invitation> GetInvitationsByMembre(string membreCourriel);
        bool InvitationExist(int noReservation, string membreCourriel);

        bool CreateInvitation(Invitation invitation);

        bool UpdateInvitation(Invitation invitation);

        bool AccepterInvitation(Invitation invitation);
        bool RefuserInvitation(Invitation invitation);

        bool DeleteInvitation(Invitation invitation);

        bool Save();
    }
}
