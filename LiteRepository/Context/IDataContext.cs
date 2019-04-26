using LiteDB;
using System;

namespace LiteRepository
{
    public interface IDataContext
    {
     LiteDatabase Database { get;  }   
    }
    
}
