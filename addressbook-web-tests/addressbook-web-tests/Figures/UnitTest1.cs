﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace addressbook_web_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodSquare()
        {
            Square s1 = new Square(5);
            Square s2 = new Square(10);
            Square s3 = s1;

            Assert.AreEqual(s1.Size, 5);
            Assert.AreEqual(s2.Size, 10);
            Assert.AreEqual(s3.Size, 5);

            s3.Size = 15;
            Assert.AreEqual(s3.Size, 15);

            s2.Colored = true;
        }

        [TestMethod]
        public void TestMethodCircle()
        {
            Circle s1 = new Circle(5);
            Circle s2 = new Circle(10);
            Circle s3 = s1;

            Assert.AreEqual(s1.Radius, 5);
            Assert.AreEqual(s2.Radius, 10);
            Assert.AreEqual(s3.Radius, 5);

            s3.Radius = 15;
            Assert.AreEqual(s3.Radius, 15);

            s2.Colored = true;
        }

        [TestMethod]
        public void IfTest()
        {
            double total = 900;
            bool isVIPClient = true;

            if (total > 1000 || isVIPClient)
            {
                total = total * 0.9;
                System.Console.Out.Write("Скидки 10%, общая сумма " + total);
            }
            else
            {
                System.Console.Out.Write("Скидки нет, общая сумма "+ total);
            }
        }

        [TestMethod]
        public void TestCycles()
        {
            string[] words = new string[] { "Let", "me", "tell", "you", "something", "really", "interesting" };

            foreach(string element in words)
            {
                System.Console.Out.Write(element + " ");
            }
        }
    }
}
