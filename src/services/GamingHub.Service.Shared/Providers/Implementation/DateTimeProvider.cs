namespace GamingHub.Service.Shared.Providers.Implementation
{
    /// <summary>
    /// The class provides information about datetime.
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        #region Implementation of IDateTimeProvider

        /// <inheritdoc />
        public DateTime Now => DateTime.Now;

        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;

        #endregion
    }
}
