using System;

namespace Emma.Core.Tests
{
    public static class SampleExtensionsClass
    {
        public static string ToString(this object obj)
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