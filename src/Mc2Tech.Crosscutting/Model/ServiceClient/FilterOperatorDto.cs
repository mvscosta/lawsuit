namespace Mc2Tech.Crosscutting.Model.ServiceClient
{
    public enum FilterOperatorDto
    {
        None = 0,
        IsLike = 1,
        IsNotLike = 2,
        IsLessThan = 3,
        IsLessThanOrEqualTo = 4,
        IsEqualTo = 5,
        IsNotEqualTo = 6,
        IsGreaterThanOrEqualTo = 7,
        IsGreaterThan = 8,
        StartsWith = 9,
        EndsWith = 10,
        Contains = 11,
        NotContains = 12,
        IsNull = 13,
        IsNotNull = 14,
        IsContainedIn = 15,
        IsNotContainedIn = 16
    }
}