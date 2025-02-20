namespace GamingHub.IdentityServer.API.Configuration
{
    /// <summary>
    /// The class provides information about options for Redis cache.
    /// </summary>
    public class RedisCacheOptions
    {
        public const string NAME_KEY = "RedisCache";

        /// <summary>
        /// Gets or sets name of cache.
        /// </summary>
        public string Name { get; set; } = "redis1";

        /// <summary>
        /// Gets or sets an endpoint to connect.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the sync timeout.
        /// </summary>
        public int SyncTimeout { get; set; } = 3000;
    }
}