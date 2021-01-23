using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.PersonsApi.ViewModel.Get
{
    public class GetPersonPhotoByIdQuery : Query<byte[]>
    {
        public Guid PersonId { get; set; }
    }
}
