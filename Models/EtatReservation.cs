using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class EtatReservation
{
    public int IdEtatReservation { get; set; }

    public string NomEtatReservation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}
