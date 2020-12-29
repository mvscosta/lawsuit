namespace Mc2Tech.Crosscutting.ViewModel.Base
{
    public class Command : SimpleSoft.Mediator.Command
    {
        public new string CreatedBy
        {
            get => base.CreatedBy;
            set => base.CreatedBy = value;
        }
    }

    public class Command<TData, TResult> : SimpleSoft.Mediator.Command<TResult>
    {
        public TData Data { get; set; }

        public new string CreatedBy
        {
            get => base.CreatedBy;
            set => base.CreatedBy = value;
        }
    }
}
