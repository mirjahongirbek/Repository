using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartenRepository.Context
{
  public interface IMartenContext
    {
        IDocumentStore Store { get; set; }
    }
}
