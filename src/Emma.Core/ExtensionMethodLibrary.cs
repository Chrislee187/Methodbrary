using System.Collections.Generic;
using System.Linq;
using Emma.Core.MethodSources;

namespace Emma.Core
{
    public class ExtensionMethodLibrary
    {
        private IEnumerable<ExtensionMethodsSource> _sources;

        public ExtensionMethodLibrary(params ExtensionMethodsSource[] sources)
        {
            _sources = new List<ExtensionMethodsSource>(sources);
        }

        public IEnumerable<ExtensionMethod> Methods => 
            _sources.SelectMany(s => s.Methods)
                .OrderBy(s => s.ToString());

        public IEnumerable<ExtensionMethod> FindByName(string name, StringMatchMode matchMode = StringMatchMode.Contains) => 
            Find(new ExtensionMethodQuery { NameMatchMode = matchMode, Name = name });
        public IEnumerable<ExtensionMethod> FindByExtendingType(string type, StringMatchMode matchMode = StringMatchMode.Equals) =>
            Find(new ExtensionMethodQuery { ExtendingTypeMatchMode = matchMode, ExtendingType = type });
        public IEnumerable<ExtensionMethod> FindByReturnType(string type, StringMatchMode matchMode = StringMatchMode.Equals) =>
            Find(new ExtensionMethodQuery { ReturnTypeMatchMode = matchMode, ReturnType = type });
        public IEnumerable<ExtensionMethod> FindByParamTypes(string[] types, StringMatchMode matchMode = StringMatchMode.Equals) =>
            Find(new ExtensionMethodQuery { ParamTypesMatchMode = matchMode, ParamTypes = types });

        public IEnumerable<ExtensionMethod> Find(ExtensionMethodQuery query) => 
            Methods.Where(query.Match);
    }

    public enum StringMatchMode
    {
        Equals, StartsWith, EndsWith, Contains
    }
}