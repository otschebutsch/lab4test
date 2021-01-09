using System;
using Xunit;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;

namespace Lab4
{
    public class AuthDBTest
    {
        private const string Server = @"DESKTOP-6V7SVAP\OTCHEBUCH";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"gabartem";
        private const int ConnectionTime = 75;
        private AuthDatabaseUtils authDatabaseUtils = new AuthDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);

        [Fact]
        public void AddNormalCreds()
        {
            string login = "user";
            string password = PasswordHasher.GetHash("password");
            Assert.True(authDatabaseUtils.AddCredentials(login, password));
        }
        
        [Fact]
        public void AddVariousCreds()
        {
            Assert.True(authDatabaseUtils.AddCredentials("ґєїёэ", PasswordHasher.GetHash("ґєїёэ")));
            Assert.True(authDatabaseUtils.AddCredentials("123", PasswordHasher.GetHash("123")));
            Assert.True(authDatabaseUtils.AddCredentials("畝俱樂部迷", PasswordHasher.GetHash("畝俱樂部迷")));
            Assert.True(authDatabaseUtils.AddCredentials(".,<>?#~![]{}", PasswordHasher.GetHash(".,<>?#~![]{}")));
            Assert.True(authDatabaseUtils.AddCredentials("✔️✔️✔️", PasswordHasher.GetHash("✔️✔️✔️")));
            Assert.True(authDatabaseUtils.AddCredentials("usermeme", PasswordHasher.GetHash("P A S S W O R D")));
        }


        [Fact]
        public void AddSamePswdCreds()
        {
            string login = "user123";
            string password = PasswordHasher.GetHash("password");
            Assert.True(authDatabaseUtils.AddCredentials(login, password));
        }


        [Fact]
        public void AddSameLoginCreds()
        {
            Assert.False(authDatabaseUtils.AddCredentials("ґєїёэ", PasswordHasher.GetHash("ґєїёэ")));
            Assert.False(authDatabaseUtils.AddCredentials("123", PasswordHasher.GetHash("123")));
            Assert.False(authDatabaseUtils.AddCredentials("畝俱樂部迷", PasswordHasher.GetHash("畝俱樂部迷")));
            Assert.False(authDatabaseUtils.AddCredentials(".,<>?#~![]{}", PasswordHasher.GetHash(".,<>?#~![]{}")));
            Assert.False(authDatabaseUtils.AddCredentials("✔️✔️✔️", PasswordHasher.GetHash("✔️✔️✔️")));
            Assert.False(authDatabaseUtils.AddCredentials("usermeme", PasswordHasher.GetHash("P A S S W O R D")));
        }
  

        [Fact]
        public void AddEmptyLoginCreds()
        {
            string login = "";
            string password = PasswordHasher.GetHash("password");
            Assert.False(authDatabaseUtils.AddCredentials(login, password));
        }


        [Fact]
        public void AddNullLoginCreds()
        {
            string login = null;
            string password = PasswordHasher.GetHash("password");
            Assert.False(authDatabaseUtils.AddCredentials(login, password));
        }


        [Fact]
        public void AddEmptyPswdCreds()
        {
            string login = "user2";
            string password = PasswordHasher.GetHash("");
            Assert.True(authDatabaseUtils.AddCredentials(login, password));
        }


        [Fact]
        public void AddNullPswdCreds()
        {
            string login = "user3";
            Assert.Throws<ArgumentNullException>(() => authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(null)));
        }

        
        [Fact]
        public void UpdatePswdCreds()
        {
            authDatabaseUtils.AddCredentials("userup", PasswordHasher.GetHash("password"));
            Assert.True(authDatabaseUtils.UpdateCredentials("userup", PasswordHasher.GetHash("password"), "userup", PasswordHasher.GetHash("newpassword")));
        }


        [Fact]
        public void UpdateSameCreds()
        {
            authDatabaseUtils.AddCredentials("userzhb", PasswordHasher.GetHash("passworddd"));
            Assert.True(authDatabaseUtils.UpdateCredentials("userzhb", PasswordHasher.GetHash("passworddd"), "userzhb", PasswordHasher.GetHash("passworddd")));
        }


        [Fact]
        public void UpdateWrongCreds()
        {
            Assert.False(authDatabaseUtils.UpdateCredentials("notuser", PasswordHasher.GetHash("notpassword"), "user5", PasswordHasher.GetHash("password")));
        }


        [Fact]
        public void UpdateNotHashedPswdCreds()
        {
            authDatabaseUtils.AddCredentials("userhash", PasswordHasher.GetHash("password"));
            Assert.False(authDatabaseUtils.UpdateCredentials("userhash", PasswordHasher.GetHash("password"), "userhash", "password"));
        }
        
        
        [Fact]
        public void CheckCredsTest()
        {
            string login = "check";
            string password = PasswordHasher.GetHash("checkpswd");
            authDatabaseUtils.AddCredentials(login, password);
            Assert.True(authDatabaseUtils.CheckCredentials(login, password));
        }


        [Fact]
        public void CheckWrongCreds()
        {
            Assert.False(authDatabaseUtils.CheckCredentials("notuser", PasswordHasher.GetHash("notpassword")));
        }


        [Fact]
        public void CheckWrongPswdCreds()
        {
            Assert.False(authDatabaseUtils.CheckCredentials("user", PasswordHasher.GetHash("notpassword")));
        }

        [Fact]
        public void CheckWrongLoginCreds()
        {
            Assert.False(authDatabaseUtils.CheckCredentials("notuser", PasswordHasher.GetHash("password")));
        }


        [Fact]
        public void DeleteWrongPswdCreds()
        {
            Assert.False(authDatabaseUtils.DeleteCredentials("user", PasswordHasher.GetHash("notpassword")));
        }


        [Fact]
        public void DeleteCredsTest()
        {
            Assert.True(authDatabaseUtils.DeleteCredentials("user123", PasswordHasher.GetHash("password")));
            Assert.True(authDatabaseUtils.DeleteCredentials("user2", PasswordHasher.GetHash("")));
        }
        

        [Fact]
        public void DeleteWrongCreds()
        {
            Assert.False(authDatabaseUtils.DeleteCredentials("notuser", PasswordHasher.GetHash("notpassword")));
        }
    }
}
