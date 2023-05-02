using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models;

public partial class Plainte
{
    public int NoReservation { get; set; }

    public string MembreCourriel { get; set; }

    public DateTime DatePlainte { get; set; }

    public string Description { get; set; }

    public string? AdministrateurCourriel { get; set; }

    public virtual Administrateur? AdministrateurCourrielNavigation { get; set; }

    public virtual Membre? MembreCourrielNavigation { get; set; }

    public virtual Reservation? NoReservationNavigation { get; set; }
}
