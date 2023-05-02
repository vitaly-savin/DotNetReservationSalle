using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models;

public partial class TypeActivite
{

    public string NomActivite { get; set; }

    public string Description { get; set; }

    public bool? EstActif { get; set; }

    public string CreerParAdministrateurCourriel { get; set; }

    public virtual Administrateur? CreerParAdministrateurCourrielNavigation { get; set; }
    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
    [JsonIgnore]
    public virtual ICollection<SalleLaboratoire> NoSalles { get; } = new List<SalleLaboratoire>();
  //  public virtual ICollection<SalleLaboratoire_TypeActivite> SalleLaboratoire_TypeActivites { get; set; }
}
