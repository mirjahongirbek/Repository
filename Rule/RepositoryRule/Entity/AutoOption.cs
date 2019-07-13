namespace RepositoryRule.Entity
{
    public class AuthOption
    {
        private static string issuer;
        private static string audinece;
        private static string key;
        private static int lifeTime;

        public static string ISSUER
        {
            get => issuer ?? "GenericISSUREER";
            set => issuer = value;
        }

        public static string AUDINECE
        {
            get => audinece ?? "http://localhost:8080/";
            set => audinece = value;
        }

        public static string Key
        {
            get => key ?? "mysupersecret_secretkey!123";
            set => key = value;
        }

        public static int LifeTime
        {
            get => lifeTime == 0 ? 200 : lifeTime;
            set => lifeTime = value;
        }
    }
}