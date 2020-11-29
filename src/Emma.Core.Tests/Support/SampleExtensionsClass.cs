using System;
using System.Collections;
using System.Collections.Generic;

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
        public static void ToNowhere(this object obj, string a, float b, IEnumerable<int> c)
        {

        }

        private static void ToNowherePrivate(this object obj, string a, float b, IEnumerable<int> c)
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
        public static void ToNowhere(this object obj, string a, int b, Type c)
        {

        }
        public static void ToNowhere(this object obj, string a, long b, Type c)
        {

        }
        public static void ToNowhere(this object obj, string a, byte b, Type c)
        {

        }
        public static void ToNowhere(this object obj, string a, char b, Type c)
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