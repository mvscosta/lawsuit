using Mc2Tech.Crosscutting.Model.Base;

namespace Mc2Tech.PersonsApi.Model
{
    public class PersonEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public byte[] Photo { get; set; }
    }
}
