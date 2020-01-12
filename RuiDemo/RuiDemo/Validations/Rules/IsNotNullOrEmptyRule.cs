using RuiDemo.Validations.Rules.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuiDemo.Validations.Rules
{
    public class IsNotNullOrEmptyRule : BaseRule<string>
    {
        public IsNotNullOrEmptyRule(string message) : base(message)
        {

        }

        public override bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
