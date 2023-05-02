using backend.Models;

namespace backend.Dto
{
    public class ReservationDto
    {
        public int NoReservation { get; set; }

        public DateTime DateHeureDebut { get; set; }

        public DateTime DateHeureFin { get; set; }

        public string? TraiterParAdministrateurCourriel { get; set; }

        public DateTime? TraiterLe { get; set; }

        public int IdEtatReservation { get; set; }

        public string CreerParMembreCourriel { get; set; }

        public int NoSalle { get; set; }

        public string NomActivite { get; set; }

        public virtual ICollection<Invitation> Invitations { get; } = new List<Invitation>();
    }
}
