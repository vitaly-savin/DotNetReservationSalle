using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class EtatInvitation
{
    public int IdEtatInvitation { get; set; }

    public string NomEtatInvitation { get; set; } = null!;

    public virtual ICollection<Invitation> Invitations { get; } = new List<Invitation>();
}
