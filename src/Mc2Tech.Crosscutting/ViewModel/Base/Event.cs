namespace Mc2Tech.Crosscutting.ViewModel.Base
{
    public class Event : SimpleSoft.Mediator.Event
    {
        public new string CreatedBy
        {
            get => base.CreatedBy;
            set => base.CreatedBy = value;
        }
    }
}
