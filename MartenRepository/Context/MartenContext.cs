using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartenRepository.Context
{
  public interface MartenContext
    {
        IDocumentStore Store { get; set; }
    }
}
