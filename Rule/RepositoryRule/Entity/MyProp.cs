namespace RepositoryRule.Entity
{
    public class MyProp<T>
    {
        public T Value { get; set; }

        public static implicit operator T(MyProp<T> value)
        {
            return value.Value;
        }

        public static implicit operator MyProp<T>(T value)
        {
            return new MyProp<T> {Value = value};
        }
    }
}