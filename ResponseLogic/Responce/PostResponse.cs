namespace GenericController.Entity
{
    internal class PostResponse
    {
        public object items { get; set; }
        public long count { get; set; }
    }

    public enum ResponseList
    {
        Success,
        Error
    }
}