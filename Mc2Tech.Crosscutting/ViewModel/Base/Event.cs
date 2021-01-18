namespace Mc2Tech.Crosscutting.ViewModel.Base
{
    public class Event : SimpleSoft.Mediator.Event
    {
        public string AccessToken { get; set; }

        public new string CreatedBy
        {
            get => base.CreatedBy;
            set => base.CreatedBy = value;
        }
    }
}
