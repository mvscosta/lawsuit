﻿using Mc2Tech.Crosscutting.ViewModel.Base;
using System;
using Mc2Tech.PersonsApi.Model;

namespace Mc2Tech.PersonsApi.ViewModel.Get
{
    public class GetPersonBasicInformationByIdQuery : Query<PersonBasicInformation>
    {
        public Guid PersonId { get; set; }
    }
}
