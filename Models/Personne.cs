using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using backend.Models;

namespace backend.Models;

public partial class Personne
{
    public string Courriel { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;
    [JsonIgnore]
    public virtual Administrateur? Administrateur { get; set; }
    [JsonIgnore]
    public virtual Membre? Membre { get; set; }
}