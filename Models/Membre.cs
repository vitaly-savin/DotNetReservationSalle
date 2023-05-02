using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Membre
{
    public string Courriel { get; set; } = null!;

    public string? Adresse { get; set; }

    public string? Province { get; set; }

    public string? CodePostal { get; set; }

    public string? Telephone { get; set; }

    public bool? EstActif { get; set; }

    public string? EtatModifierParAdministrateurCourriel { get; set; }

    public virtual Personne? CourrielNavigation { get; set; }

    public virtual Administrateur? EtatModifierParAdministrateurCourrielNavigation { get; set; }

    public virtual ICollection<Invitation> Invitations { get; } = new List<Invitation>();

    public virtual ICollection<Plainte> Plaintes { get; } = new List<Plainte>();

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}
