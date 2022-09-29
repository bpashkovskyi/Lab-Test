using System.Collections.Generic;

namespace LabTest
{
    public class Database
    {
        private const string FilePath = "D://test.txt";

        public void Initialize()
        {
            var users = new List<User>
            {
                new User { UserName = "User1", HashedPassword = PasswordHasher.ComputeSha512Hash("Password1") },
                new User { UserName = "User2", HashedPassword = PasswordHasher.ComputeSha512Hash("Password2") }
            };

            var records = new List<string>();

            foreach (var user in users)
            {
                var record = $"{user.UserName},{user.HashedPassword}";
                records.Add(record);
            }

            System.IO.File.WriteAllLines(FilePath, records);
        }

        public List<User> GetUsers()
        {
            var records = System.IO.File.ReadAllLines(FilePath);

            var users = new List<User>();

            foreach (var record in records)
            {
                var userData = record.Split(',');
                var user = new User
                {
                    UserName = userData[0],
                    HashedPassword = userData[1],
                };

                users.Add(user);
            }

            return users;
        }
    }
}
