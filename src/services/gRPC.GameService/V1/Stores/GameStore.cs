namespace gRPC.GameService.V1.Stores;

/// <summary>
/// Represents a game store that manages a collection of games.
/// </summary>
public class GameStore
{
    private readonly List<GameStoreItem> _games =
    [
        new()
        {
            GameId = new Guid("661525C4-7C1F-4997-A950-92EB3FF54E3D"),
            Name = "Cyberpunk 2077",
            Description = "Futuristic open-world RPG",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("32A2D1CA-D0C6-442E-A22E-2E95A488E6B7"),
            Name = "The Witcher 3",
            Description = "Fantasy RPG with deep story",
            CreatedAt = DateTime.UtcNow, Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("903BC70E-88B3-4965-B269-21BF9B07BC79"),
            Name = "Red Dead Redemption 2",
            Description = "Open-world Western adventure",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("874AB4D5-D642-4F78-AE79-F65152FBBF78"),
            Name = "Halo Infinite",
            Description = "Sci-fi FPS with open world",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("157EB716-5246-4BD5-8182-805B36CBEDAD"),
            Name = "Elden Ring",
            Description = "Action RPG with open world",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("E7DBEEBB-6FF6-4C23-BCA1-C7938B5DB756"),
            Name = "God of War",
            Description = "Norse mythology action game",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("573976CA-1BE2-48B8-B61C-5BE016594EC4"),
            Name = "Horizon Zero Dawn",
            Description = "Post-apocalyptic adventure",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("49F2BABC-04D1-4B23-86FE-04300A5AAE5E"),
            Name = "Doom Eternal",
            Description = "Fast-paced FPS shooter",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("EB836B38-C7D4-4FFF-B877-F1DCFC0AAAC0"),
            Name = "The Last of Us Part II",
            Description = "Emotional survival adventure",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("08BAE9D6-2038-42EF-B876-0AC666D33472"),
            Name = "Ghost of Tsushima",
            Description = "Samurai open-world game",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("75FC9325-08BD-4E00-87BB-104ADBE12A58"),
            Name = "Assassin's Creed Valhalla",
            Description = "Viking open-world RPG",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("6FF6E538-0602-4620-AE2B-287262F4DA76"),
            Name = "Far Cry 6",
            Description = "Open-world FPS",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("8458A63E-03BA-4FDF-BF6D-A7F7FE81E94D"),
            Name = "GTA V",
            Description = "Open-world crime action",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("36F68D90-8654-4A36-9E37-106369F3CA05"),
            Name = "Spider-Man: Miles Morales",
            Description = "Superhero adventure",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("0593357F-082A-4043-B968-3067D270B76D"),
            Name = "Forza Horizon 5",
            Description = "Open-world racing game",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("D3847858-A204-400D-B9E2-ECCAEE5918AC"),
            Name = "Final Fantasy VII Remake",
            Description = "Classic RPG remake",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("2C24B518-A114-4837-A667-5A7B2B12E0BB"),
            Name = "Resident Evil Village",
            Description = "Survival horror game",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("6702C0FD-AE56-4083-B41C-9EBC1E5C5E42"),
            Name = "Death Stranding",
            Description = "Open-world courier adventure",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("F7C2861C-B32D-4438-A047-EC14D5D98BA5"),
            Name = "Sekiro: Shadows Die Twice",
            Description = "Samurai action game",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("1FE00F48-2B5B-4A98-B9B5-97279C099736"),
            Name = "Dark Souls III",
            Description = "Challenging RPG adventure",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("2F38FD23-8D33-4754-9111-B050E31DE05D"),
            Name = "Bloodborne",
            Description = "Lovecraftian horror RPG",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("ACA9B8B3-CF46-40F9-A4B3-3089B6B757EF"),
            Name = "Battlefield 2042",
            Description = "Large-scale FPS battles",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("201691F1-B6A1-46C1-A58B-71BC1178F5E1"),
            Name = "Call of Duty: Modern Warfare",
            Description = "Realistic military FPS",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("A7A69A41-3DEC-4127-8343-6ED9FC85F95D"),
            Name = "Overwatch 2",
            Description = "Team-based shooter",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        },

        new()
        {
            GameId = new Guid("5AF01A2B-BF0A-4F23-9B6B-9B31371CD069"),
            Name = "Starfield",
            Description = "Space exploration RPG",
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active
        }
    ];

    /// <summary>
    /// Gets a list of games available in the store.
    /// </summary>
    /// <returns>A task containing a list of <see cref="GameStoreItem"/> objects.</returns>
    public async Task<IList<GameStoreItem>> GetGamesSync()
    {
        return await Task.FromResult(_games);
    }

    public async Task<bool> AddGameAsync(GameStoreItem game)
    {
        if (game == null)
        {
            return await Task.FromResult(false);
        }
        
        _games.Add(game);
        
        return await Task.FromResult(true);
    }

    /// <summary>
    /// Represents an individual game in the store.
    /// </summary>
    public class GameStoreItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the game.
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the game was added to the store (UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the current status of the game.
        /// </summary>
        public GameStatus Status { get; set; }
    }

    /// <summary>
    /// Defines possible statuses for games in the store.
    /// </summary>
    public enum GameStatus
    {
        Disabled = 0,
        Active = 1,
        Expired = 2,
        Deleted = 3
    }
}