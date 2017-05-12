using System.Runtime.Serialization;

namespace In.Identity.ViewModel
{
    [DataContract]
    public class RestorePasswordViewModel : ConfirmByTokenViewModel
    {
        [DataMember(Name = "pwd")]
        public string Pwd { get; set; }
    }
}
