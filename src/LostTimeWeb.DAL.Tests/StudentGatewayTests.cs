using System;
using NUnit.Framework;

namespace LostTimeWeb.DAL.Tests
{
    [TestFixture]
    public class StudentGatewayTests
    {
        readonly Random _random;

        public StudentGatewayTests()
        {
            _random = new Random();
        }
    }
}
