using System;

namespace Mc2Tech.Crosscutting.Interfaces.Persons
{
    public interface IPersonDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }
    }
}
