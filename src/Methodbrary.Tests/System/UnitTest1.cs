using System;
using System.Linq;
using NUnit.Framework;

namespace Methodbrary.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var value = "()".Aggregate(0, (a, c) => a < 0 ? 0 : c == '(' ? a+1 : c == ')' ? a-1 : 0);

            // var value = ")(()))".Select(c => c == '(' ? 1 : c == ')' ? -1 : 0).Sum() == 0;
            Console.WriteLine(value);
        }
    }
}