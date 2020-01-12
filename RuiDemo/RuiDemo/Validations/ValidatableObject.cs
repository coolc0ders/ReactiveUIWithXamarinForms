using ReactiveUI;
using RuiDemo.Validations.Rules.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuiDemo.Validations
{
    public class ValidatableObject<T> : ReactiveObject, IValidity
    {
        private T _value;
        public T Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set { this.RaiseAndSetIfChanged(ref _isValid, value); }
        }

        private string _errorText;
        public string ErrorMessage
        {
            get { return _errorText; }
            set { this.RaiseAndSetIfChanged(ref _errorText, value); }
        }


        public List<string> Errors { get; set; }
        public List<IValidationRule<T>> Validations { get; set; }


        public ValidatableObject()
        {
            Validations = new List<IValidationRule<T>>();
            Errors = new List<string>();
            this.WhenAny(obj => obj.Value,
                value => value).Subscribe((value) =>
                {
                    Validate();
                });
        }

        /// <summary>
        /// Tells if the object is valid without 
        /// setting its state 
        /// </summary>
        /// <returns></returns>
        public bool TryValidate()
        {
            return TryValidate(Value);
        }

        public bool TryValidate(T val)
        {
            Errors.Clear();
            Errors = new List<string>(Validations.Where(v => !v.Check(val))
                .Select(v => v.ValidationMessage));

            return !Errors.Any();
        }

        public bool Validate()
        {
            IsValid = TryValidate();
            if (Errors.Any())
            {
                ErrorMessage = string.Join(Environment.NewLine, Errors);
            }
            else
            {
                ErrorMessage = string.Empty;
            }
            return IsValid;
        }
    }
}
