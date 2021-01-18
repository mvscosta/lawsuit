using System;

namespace Mc2Tech.PersonsApi.ViewModel.Create
{
    public class CreatePersonModel
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public byte[] Photo { get; set; }
        
    }
}
