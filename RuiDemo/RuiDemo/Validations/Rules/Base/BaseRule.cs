using System;
using System.Collections.Generic;
using System.Text;

namespace RuiDemo.Validations.Rules.Base
{
    public class BaseRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public BaseRule(string message)
        {
            ValidationMessage = message;
        }

        public virtual bool Check(T value)
        {
            throw new NotImplementedException();
        }
    }
}
