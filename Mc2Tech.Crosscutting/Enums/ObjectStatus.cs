namespace Mc2Tech.Crosscutting.Enums
{
    /// <summary>
    /// Object entity status
    /// </summary>
    public enum ObjectStatus
    {
        /// <summary>
        /// Inactivated
        /// </summary>
        Inactive = 0,
        /// <summary>
        /// Active
        /// </summary>
        Active = 1,
        /// <summary>
        /// Blocked
        /// </summary>
        Blocked = 2,
        /// <summary>
        /// Pending to delete
        /// </summary>
        PendingDelete = 8,
        /// <summary>
        /// Logical deleted item
        /// </summary>
        LogicalDeleted = 9
    }
}