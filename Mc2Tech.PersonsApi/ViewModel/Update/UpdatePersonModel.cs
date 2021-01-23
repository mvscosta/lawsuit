using System;

namespace Mc2Tech.PersonsApi.ViewModel.Update
{
    public class UpdatePersonModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] Photo { get; set; }
    }
}
