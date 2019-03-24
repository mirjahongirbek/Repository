using Newtonsoft.Json;

namespace GenericController.Entity
{
    internal class PostResponse
    {
        public object items { get; set; }
        public long count { get; set; }
    }

}
