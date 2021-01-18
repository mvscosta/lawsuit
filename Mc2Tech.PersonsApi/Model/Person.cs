using Mc2Tech.Crosscutting.Interfaces.LawSuits;
using Mc2Tech.Crosscutting.Model.Base;
using System;

namespace Mc2Tech.PersonsApi.Model
{
    public class Person : BaseObject
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }
    }
}
