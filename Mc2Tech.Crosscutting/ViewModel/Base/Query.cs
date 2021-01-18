namespace Mc2Tech.Crosscutting.ViewModel.Base
{
    public class Query<TResult> : SimpleSoft.Mediator.Query<TResult>
    {
        public string AccessToken { get; set; }

        public new string CreatedBy
        {
            get => base.CreatedBy;
            set => base.CreatedBy = value;
        }
    }
}
