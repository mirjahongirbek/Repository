using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Exceptions
{
    public class ValidExeption : Exception
    {
       public int HttpStatusCode { get; }
        public ValidExeption(int statusCode)
        {
            HttpStatusCode = statusCode;
        }
        public List<IValidator> Validators { get; }
        public IValidator Validator { get;  }
        public object Error { get; }
        public ValidExeption(List<IValidator> list, int statusCode = 400) : this(statusCode)
        {
            Validators = list;
        }
        public ValidExeption(IValidator validator, int statuCode=400):this(statuCode)
        {
            Validator = validator;
        }
        public ValidExeption(object error, int staCode = 400) : this(staCode)
        {
            Error = error;
        }
    }
}
