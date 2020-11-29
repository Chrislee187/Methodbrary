using System;

namespace Emma.Core.MethodSources
{
    public class ExtensionMethodsSource
    {
        
        public DateTimeOffset LastUpdated { get; set; }
        public ExtensionMethod[] Methods { get; set; }
    }
}