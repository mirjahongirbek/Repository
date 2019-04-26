using GenericController.Attributes;
using System;
using System.Collections.Generic;

namespace Examples.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    [ViewModel("product", RepositoryRule.Enums.Actions.Create, RepositoryRule.Enums.Actions.Read)]
    public class ProductViewModal
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }

    }
}