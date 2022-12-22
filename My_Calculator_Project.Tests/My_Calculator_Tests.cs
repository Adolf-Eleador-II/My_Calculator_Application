using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace My_Calculator_Project.Tests
{
    [TestClass]
    public class My_Calculator_Tests
    {
        [TestMethod]
        public void Sum_Tests()
        {
            Operation res = new();

            Assert.AreEqual(10 + 20, res.Sum(10, 20));
            Assert.AreEqual(-2 + 1, res.Sum(-2, 1));
            Assert.AreEqual(2 - 1, res.Sum(2, -1));
            Assert.AreEqual(15243 - 5243, res.Sum(15243, -5243));

            Assert.AreEqual(1 + 2 / 3, res.Sum(1, 2 / 3));
            Assert.AreEqual(3 + 2.9, res.Sum(3, 2.9));
            Assert.AreEqual(1 - 0.9, res.Sum(1, -0.9));
            Assert.AreEqual(-5 + 3.9, res.Sum(-5, 3.9));

        }

        [TestMethod]
        public void Def_Tests()
        {
            Operation res = new();

            Assert.AreEqual(10 - 5, res.Def(10, 5));
            Assert.AreEqual(-1 - 5, res.Def(-1, 5));
            Assert.AreEqual(10 + 5, res.Def(10, -5));

            Assert.AreEqual(1 - 2 / 3, res.Def(1, 2 / 3));
            Assert.AreEqual(1.0-2.0/3.0, res.Def(1.0, 2.0/3.0));
            Assert.AreEqual(3-2.9, res.Def(3, 2.9));
            Assert.AreEqual(1+0.9, res.Def(1, -0.9));
            Assert.AreEqual(-5-3.9, res.Def(-5, 3.9));
        }

        [TestMethod]
        public void Mul_Tests()
        {
            Operation res = new();

            Assert.AreEqual(55, res.Mul(11, 5));
            Assert.AreEqual(-5, res.Mul(-1, 5));
            Assert.AreEqual(-50, res.Mul(10, -5));
            Assert.AreEqual(0, res.Mul(0, -5));

            Assert.AreEqual(2 / 3, res.Mul(1, 2 / 3));
            Assert.AreEqual(10, res.Mul(2.5, 4));
            Assert.AreEqual(8.7, res.Mul(3, 2.9));
            Assert.AreEqual(-0.9, res.Mul(1, -0.9));
            Assert.AreEqual(-19.5, res.Mul(-5, 3.9));
        }

        [TestMethod]
        public void Div_Tests()
        {
            Operation res = new();

            Assert.AreEqual(11.0 / 5.0, res.Div(11, 5));
            Assert.AreEqual(-1.0 / 5, res.Div(-1, 5));
            Assert.AreEqual(-10.0 / 5, res.Div(10, -5));
            Assert.AreEqual(-0.0 / 5, res.Div(0, -5));

            Assert.AreEqual(1.0 / 2.0 * 3.0, res.Div(1.0, 2.0 / 3.0));
            Assert.AreEqual(-0.9 / 1, res.Div(-0.9, 1));
            Assert.AreEqual(1.0 / 0.5, res.Div(1, 0.5));
        }
    }
}