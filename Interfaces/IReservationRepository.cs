using backend.Models;

namespace backend.Interfaces
{
    public interface IReservationRepository
    {
        ICollection<Reservation> GetReservations();
        Reservation GetReservation(int noReservation);
        ICollection<Reservation> GetReservationsByMembre(string membreCourriel);
        ICollection<Reservation> GetReservationsByMembreInvite(string membreCourriel);
        ICollection<Reservation> GetReservationsAVenirByMembre(string membreCourriel);
        ICollection<Reservation> GetReservationsBySalle(int noSalle);
        ICollection<Reservation> GetReservationsNonTraiter();
        bool ReservationExist(int noReservation);
        bool CreateReservation(String[] lstInvites, Reservation reservation);

        bool UpdateReservation(Reservation reservation);

        bool AnnulerReservation(Reservation reservation);
        bool AccepterReservation(string accepterPar, Reservation reservation);
        bool RefuserReservation(string refuserPar, Reservation reservation);
        bool DeleteReservation(Reservation reservation);

        bool Save();
    }
}
