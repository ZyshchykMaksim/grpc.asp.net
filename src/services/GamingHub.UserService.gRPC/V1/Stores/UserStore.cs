namespace GamingHub.UserService.gRPC.V1.Stores;

/// <summary>
/// Represents a store of user information.
/// Provides methods to retrieve test user data.
/// </summary>
public class UserStore
{
    /// <summary>
    /// Retrieves a list of test users with predefined properties.
    /// </summary>
    /// <returns>A list of <see cref="UserStoreItem"/> containing test user information.</returns>
    public async Task<IList<UserStoreItem>> GetUsersSync()
    {
        return await Task.FromResult<List<UserStoreItem>>(
        [
            new UserStoreItem
            {
                UserId = new Guid("E03074E0-84CA-41DF-9569-03147FF74C25"),
                LastName = "Johnson",
                FirstName = "Alice",
                UserName = "alice_j",
                PhoneNumber = "+15005550000",
                Email = "alice.johnson@example.com",
                Address = "123 Maple St, Springfield",
                PostCode = "12345",
                Birthday = new DateTime(1999, 5, 14),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("119AD8DD-3034-463E-B9FC-C4450539E3BB"),
                LastName = "Smith",
                FirstName = "Bob",
                UserName = "bobsmith",
                PhoneNumber = "+1234567891",
                Email = "bob.smith@example.com",
                Address = "456 Oak St, Springfield",
                PostCode = "12346",
                Birthday = new DateTime(1994, 9, 23),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("A5723EEC-D680-4172-9186-D330A7313F0B"),
                LastName = "Brown",
                FirstName = "Charlie",
                UserName = "charlieb",
                PhoneNumber = "+1234567892",
                Email = "charlie.brown@example.com",
                Address = "789 Pine St, Springfield",
                PostCode = "12347",
                Birthday = new DateTime(2001, 3, 11),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("1201D4CA-9FB0-44D6-BCB7-1CD887B2213C"),
                LastName = "Clark",
                FirstName = "David",
                UserName = "davidc",
                PhoneNumber = "+1234567893",
                Email = "david.clark@example.com",
                Address = "159 Birch St, Springfield",
                PostCode = "12348",
                Birthday = new DateTime(1989, 7, 19),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("6BDBBB53-63ED-43D6-8FF3-1A351E1FA459"),
                LastName = "Adams",
                FirstName = "Eve",
                UserName = "eve_a",
                PhoneNumber = "+1234567894",
                Email = "eve.adams@example.com",
                Address = "753 Cedar St, Springfield",
                PostCode = "12349",
                Birthday = new DateTime(1992, 11, 25),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("DA7E6865-A159-4214-B6FD-16388E6C65D2"),
                LastName = "Wilson",
                FirstName = "Frank",
                UserName = "frankw",
                PhoneNumber = "+1234567895",
                Email = "frank.wilson@example.com",
                Address = "852 Elm St, Springfield",
                PostCode = "12350",
                Birthday = new DateTime(1987, 4, 7),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("7C833ACD-9DB1-48C5-9B62-91F5AEECD7F6"),
                LastName = "Lee",
                FirstName = "Grace",
                UserName = "gracelee",
                PhoneNumber = "+1234567896",
                Email = "grace.lee@example.com",
                Address = "369 Willow St, Springfield",
                PostCode = "12351",
                Birthday = new DateTime(1998, 8, 14),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("15B214DD-E7EA-4C32-AAFE-445018FB25E4"),
                LastName = "Scott",
                FirstName = "Henry",
                UserName = "henrys",
                PhoneNumber = "+1234567897",
                Email = "henry.scott@example.com",
                Address = "951 Aspen St, Springfield",
                PostCode = "12352",
                Birthday = new DateTime(1995, 6, 20),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("872543B1-2498-4B89-ACDB-081931C6F575"),
                LastName = "Walker",
                FirstName = "Ivy",
                UserName = "ivywalker",
                PhoneNumber = "+1234567898",
                Email = "ivy.walker@example.com",
                Address = "357 Redwood St, Springfield",
                PostCode = "12353",
                Birthday = new DateTime(2000, 1, 17),
                Status = UserStatus.Active
            },

            new UserStoreItem
            {
                UserId = new Guid("0B02C294-53DF-44CA-923B-4FABF7006996"),
                LastName = "Moore",
                FirstName = "Jack",
                UserName = "jackmo",
                PhoneNumber = "+1234567899",
                Email = "jack.moore@example.com",
                Address = "753 Chestnut St, Springfield",
                PostCode = "12354",
                Birthday = new DateTime(1991, 10, 9),
                Status = UserStatus.Active
            }
        ]);
    }

    /// <summary>
    /// Represents the detailed information of a user in the store.
    /// </summary>
    public class UserStoreItem
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the username for account identification.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the residential address of the user.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the postal code associated with the user's address.
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateTime Birthday { get; set; }
        
        /// <summary>
        /// Gets or sets the current status of the user.
        /// </summary>
        public UserStatus Status { get; set; }
    }
    
    /// <summary>
    /// Defines possible statuses for users in the store.
    /// </summary>
    public enum UserStatus
    {
        Disabled = 0,
        Active = 1,
        Expired = 2,
        Deleted = 3
    }
}