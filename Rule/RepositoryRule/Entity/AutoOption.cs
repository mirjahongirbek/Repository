using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public  class AuthOption
    {
        private static string issuer;
        private static string audinece;
        private static string key;
        private static int lifeTime;
        public static string ISSUER { get { return issuer??"GenericISSUREER"; } set { issuer = value; } }
        public static string AUDINECE { get { return audinece ?? "http://localhost:8080/"; } set { audinece = value; } }
        public static string Key { get { return key ?? "mysupersecret_secretkey!123"; } set { key = value; }  }
        public static int LifeTime { get { return lifeTime == 0 ? 200 : lifeTime; } set { lifeTime = value; } }
        
    }
}
