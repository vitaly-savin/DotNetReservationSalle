using MessagePack;
using Microsoft.AspNetCore.Mvc;

namespace backend.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalleLaboratoireTypeActivite
    {
        
        public int NoSalle { get; set; }
        public string NomActivite { get; set; }
        //public SalleLaboratoire SalleLaboratoire { get; set; }
        //public TypeActivite TypeActivite { get; set; }

    }
}
