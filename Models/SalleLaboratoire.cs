using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class SalleLaboratoire
{
    public int NoSalle { get; set; }

    public int? Capacite { get; set; }

    public string? Description { get; set; }

    public bool? EstActif { get; set; }

    public string CreerParAdministrateurCourriel { get; set; }

    public virtual Administrateur? CreerParAdministrateurCourrielNavigation { get; set; }

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual ICollection<TypeActivite> NomActivites { get; set; } = new List<TypeActivite>();
   // public virtual ICollection<SalleLaboratoire_TypeActivite> SalleLaboratoire_TypeActivites { get; set; }



}
