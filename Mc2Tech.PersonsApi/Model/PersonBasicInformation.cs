using System;

namespace Mc2Tech.PersonsApi.Model
{
    public class PersonBasicInformation
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }
    }
}
