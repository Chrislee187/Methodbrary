using System;

// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedParameter.Local

namespace Emma.Core.Tests.Support
{
    public static class SampleExtensionsClass
    {
        public static string ToAString(this object obj)
        {
            return string.Empty;
        }
        public static void ToNowhere(this object obj)
        {

        }
        public static void ToNowhere(this object obj, string a, float b, Type c)
        {

        }
        private static void ToNowherePrivate(this object obj, string a, float b, Type c)
        {

        }
         
        public static void NotAnExtensionMethod()
        {

        }
    }

    public static class SampleExtensionsClass2
    {
        public static void ToNowhere(this object obj)
        {

        }
        public static void ToNowhere(this object obj, string a, float b, Type c)
        {

        }
        private static void ToNowherePrivate(this object obj, string a, float b, Type c)
        {

        }

        public static void NotAnExtensionMethod()
        {

        }
    }
}