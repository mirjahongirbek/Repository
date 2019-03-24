namespace GenericController.Entity
{
    public class Request
    {
        public string name { get; set; }
        public string data { get; set; }
    }
    public enum Responses
    {
        ServiceNotFound,
        Success,
        ComeNullModal
    }
    
}
