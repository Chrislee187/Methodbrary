using System;
using System.Collections.Generic;

namespace Emma.Core.MethodSources
{
    public class ExtensionMethodsSource
    {
        
        public DateTimeOffset LastUpdated { get; set; }
        public IEnumerable<ExtensionMethod> Methods { get; set; }
    }
}