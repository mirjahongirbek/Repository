using System;

namespace RepositoryRule.Entity
{
    public class AuthResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTime { get; set; }
    }
}