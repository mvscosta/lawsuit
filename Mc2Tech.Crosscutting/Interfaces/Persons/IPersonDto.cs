using System;

namespace Mc2Tech.Crosscutting.Interfaces.LawSuits
{
    public interface IPersonDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }
    }
}
