using System;
using System.Linq;

namespace Emma.Core
{

    public class ExtensionMethodQuery
    {
        public string Name { get; set; }
        public StringMatchMode NameMatchMode { get; set; }

        public string ExtendingType { get; set; }
        public StringMatchMode ExtendingTypeMatchMode { get; set; }

        public string ReturnType { get; set; }
        public StringMatchMode ReturnTypeMatchMode { get; set; }

        public string[] ParamTypes { get; set; }
        public StringMatchMode ParamTypesMatchMode { get; set; }

        public bool Insensitive { get; set; }

        public bool Match(ExtensionMethod em)
        {
            var matched = true;

            matched &= NamesMatch(em.Name);
            matched &= ExtendingTypesMatch(em.ExtendingType);
            matched &= ReturnTypesMatch(em.ReturnType);
            matched &= ParamTypesMatch(em.ParamTypes);

            return matched;
        }

        private bool NamesMatch(string compare) => 
            string.IsNullOrEmpty(Name) 
            || QueryMatchers.String(Name, compare, Insensitive, NameMatchMode);

        private bool ExtendingTypesMatch(string compare) => 
            string.IsNullOrEmpty(ExtendingType) 
            || QueryMatchers.String(ExtendingType, compare, Insensitive, ExtendingTypeMatchMode);

        private bool ReturnTypesMatch(string compare) => 
            string.IsNullOrEmpty(ReturnType) 
            || QueryMatchers.String(ReturnType, compare, Insensitive, ReturnTypeMatchMode);

        private bool ParamTypesMatch(string[] compare) => 
            ParamTypes == null || !ParamTypes.Any() || ParamTypes.All(compare.Contains);
    }
}