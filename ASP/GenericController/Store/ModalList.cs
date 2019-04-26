using RepositoryRule.Enums;
using System.Reflection;

namespace GenericController.Store
{
    internal class ModalList
    {
       public Actions[] Actions { get; set; }
       public string Name { get; set; }
        public TypeInfo ViewModal { get; set; }
    }
}
