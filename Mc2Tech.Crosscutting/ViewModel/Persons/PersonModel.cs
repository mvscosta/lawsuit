using Mc2Tech.Crosscutting.Interfaces.LawSuits;
using System;

namespace Mc2Tech.Crosscutting.ViewModel.Persons
{
    public class PersonModel : IPersonDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public Byte[] Photo { get; set; }
    }
}