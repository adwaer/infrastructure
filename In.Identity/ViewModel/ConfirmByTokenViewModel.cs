using System.Runtime.Serialization;

namespace In.Identity.ViewModel
{
    [DataContract]
    public class ConfirmByTokenViewModel 
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
