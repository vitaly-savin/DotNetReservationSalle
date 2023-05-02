using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public partial class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NoReservation { get; set; }

    public DateTime DateHeureDebut { get; set; }

    public DateTime DateHeureFin { get; set; }

    public string? TraiterParAdministrateurCourriel { get; set; }

    public DateTime? TraiterLe { get; set; }

    public int IdEtatReservation { get; set; }

    public string CreerParMembreCourriel { get; set; } = null!;

    public int NoSalle { get; set; }

    public string NomActivite { get; set; } = null!;

    public virtual Membre? CreerParMembreCourrielNavigation { get; set; }

    public virtual EtatReservation? IdEtatReservationNavigation { get; set; }

    public virtual ICollection<Invitation> Invitations { get; } = new List<Invitation>();

    public virtual SalleLaboratoire? NoSalleNavigation { get; set; }

    public virtual TypeActivite? NomActiviteNavigation { get; set; }

    public virtual ICollection<Plainte> Plaintes { get; } = new List<Plainte>();

    public virtual Administrateur? TraiterParAdministrateurCourrielNavigation { get; set; }
}
