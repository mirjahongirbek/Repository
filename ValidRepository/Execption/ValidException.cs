using System;
using System.Collections.Generic;
using RepositoryRule.Entity;

namespace ValidRepository.Execption
{
    public class ValidException : Exception
    {
        public ValidException(int statusCode)
        {
            HttpStatusCode = statusCode;
        }

        public ValidException(List<IValidator> list, int statusCode = 400) : this(statusCode)
        {
            Validators = list;
        }

        public ValidException(IValidator validator, int statuCode = 400) : this(statuCode)
        {
            Validator = validator;
        }

        public ValidException(object error, int staCode = 400) : this(staCode)
        {
            Error = error;
        }

        public int HttpStatusCode { get; }
        public List<IValidator> Validators { get; }
        public IValidator Validator { get; }
        public object Error { get; }
    }
}