using System;
using System.Collections.Generic;
using System.Text;

namespace RuiDemo.Validations.Rules.Base
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        /// <summary>
        /// Validates the object's value and set
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Check(T value);
    }
}
