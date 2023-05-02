using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Administrateur
{
    public string Courriel { get; set; } = null!;

    public string Matricule { get; set; } = null!;

 
    public virtual Personne? CourrielNavigation { get; set; }

    public virtual ICollection<Membre> Membres { get; } = new List<Membre>();

    public virtual ICollection<Plainte> Plaintes { get; } = new List<Plainte>();

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual ICollection<SalleLaboratoire> SalleLaboratoires { get; } = new List<SalleLaboratoire>();

    public virtual ICollection<TypeActivite> TypeActivites { get; } = new List<TypeActivite>();
}
