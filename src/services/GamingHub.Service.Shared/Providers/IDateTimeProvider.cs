namespace GamingHub.Service.Shared.Providers
{
    /// <summary>
    /// The interface provides information about datetime.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets a datetime object that is set to the current date and time on this computer, expressed as the local time.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Gets a datetime object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        DateTime UtcNow { get; }
    }
}
