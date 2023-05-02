
using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Invitation
{
    public int NoReservation { get; set; }

    public string MembreCourriel { get; set; } = null!;

    public int IdEtatInvitation { get; set; }

    public DateTime? DateReponse { get; set; }

    public virtual EtatInvitation? IdEtatInvitationNavigation { get; set; }

    public virtual Membre? MembreCourrielNavigation { get; set; } 

    public virtual Reservation? NoReservationNavigation { get; set; }
}
