
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GenericController.Modals
{
    public class FileViewModal<TKey>
    {
        public string ServiceName { get; set; }
        public List<IFormFile> Files { get; set; }
        public TKey Id { get; set; }
        public string Field { get; set; }
        public string Path { get; set; }
    }
}
