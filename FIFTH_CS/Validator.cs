using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFTH_CS
{
    class Validator<T> : ICloneable
    {

        private List<Predicate<T>> _allRules;

        private Validator()
        {
            _allRules = new List<Predicate<T>>();
        }
        private Validator(List<Predicate<T>> allRules)
        {
            
            _allRules = new List<Predicate<T>>();
            foreach (var rule in allRules)
            {
                _allRules.Add(rule);
            }
        }

        static public ValidatorBuilder CreateBuilder()
        {
            return new ValidatorBuilder();
        }

        

        public class ValidatorBuilder
        {
            private Validator<T> _validator;

            public ValidatorBuilder AddRule(Predicate<T> rule)
            {
                this._validator._allRules.Add(rule);
                return this;
            }
            
            public ValidatorBuilder()
            {
                this._validator = new Validator<T>();
            }

            public Validator<T> GetValidator()
            {
                if (_validator._allRules.Count == 0)
                {
                    throw new EmptyValidationRuleListException();
                }
                return (Validator<T>)_validator.Clone();
            }
        }

        public void Validate(T obj)
        {
            Console.WriteLine(_allRules.Count);

            int index = 0;
            foreach (var rule in _allRules)
            {
                Console.WriteLine(obj.ToString());
                if (rule(obj) == false)
                {
                    throw new ObjectHasNotPassedValidationException(obj, index, rule);
                }
                index++;
            }
        }

        public object Clone()
        {
            return new Validator<T>(this._allRules);
        }

        class EmptyValidationRuleListException : Exception
        {
            public EmptyValidationRuleListException()
                : base("The validation rule list is empty")
            { }
        }

        class ObjectHasNotPassedValidationException : Exception
        {
            public ObjectHasNotPassedValidationException(T obj, int index, Predicate<T> rule)
                : base($"Object failed validation - object:{obj.GetType()} rule number {index}: {rule.ToString()}")
            { }
        }

    }
}
