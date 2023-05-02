using backend.Models;

namespace backend.Dto
{
    public class InvitationDto
    {
        public int NoReservation { get; set; }

        public string MembreCourriel { get; set; }

        public int IdEtatInvitation { get; set; }

        public DateTime? DateReponse { get; set; }

        public virtual EtatInvitation? IdEtatInvitationNavigation { get; set; }
        public virtual ReservationDto? NoReservationNavigation { get; set; }
        public virtual Membre? MembreCourrielNavigation { get; set; }
    }
}
