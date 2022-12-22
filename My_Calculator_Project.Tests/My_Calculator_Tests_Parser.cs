using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace My_Calculator_Project.Tests
{
    [TestClass]
    public class My_Calculator_Parser
    {
        [TestMethod]
        public void Parser_Test_Sum()
        {
            Parser par = new();
            Assert.AreEqual(1, par.ParserExperession("+++1"));//0+0+0+1
            Assert.AreEqual(1 + 1, par.ParserExperession("1+1"));
            Assert.AreEqual(1 + -1, par.ParserExperession("1+-1"));//1+0-1
            Assert.AreEqual(-1 + 1, par.ParserExperession("-1+1"));
            Assert.AreEqual(0.5 + 5, par.ParserExperession("0.5+5"));
            Assert.AreEqual(0.5 - 5, par.ParserExperession("0.5+-5"));

            Assert.AreEqual(1 + 1 + 1, par.ParserExperession("1+1+1"));
            Assert.AreEqual(-5.3 + 5.2, par.ParserExperession("-5.3+5.2"));
        }
        [TestMethod]
        public void Parser_Test_Dif()
        {
            Parser par = new();
            Assert.AreEqual(-10, par.ParserExperession("-10"));//0-10
            Assert.AreEqual(12, par.ParserExperession("--12"));//+12
            Assert.AreEqual(-5, par.ParserExperession("---5"));//+-5 
            Assert.AreEqual(30, par.ParserExperession("----30"));//++30
            Assert.AreEqual(31, par.ParserExperession("1----30"));
        }
        [TestMethod]
        public void Parser_Test_Mul()
        {
            Parser par = new();
            Assert.AreEqual(1, par.ParserExperession("0.5*2"));
            Assert.AreEqual(-0.25, par.ParserExperession("0.25*-1"));
            Assert.AreEqual(0.25, par.ParserExperession("0.25*--1"));
        }
        [TestMethod]
        public void Parser_Test_Div()
        {
            Parser par = new();
            Assert.AreEqual(3.0 / 2, par.ParserExperession("3/2"));
            Assert.AreEqual(9.0 / 9 / 8, par.ParserExperession("9/9/8"));
            Assert.AreEqual(0.1 / 0.05, par.ParserExperession("0.1/0.05"));
            Assert.AreEqual(10 / 0.005, par.ParserExperession("10/0.005"));
            Assert.AreEqual(-10 / 0.005, par.ParserExperession("10/-0.005"));
            Assert.AreEqual(10 / 0.005, par.ParserExperession("-10/-0.005"));
        }





        [TestMethod]
        public void Parser_Test_Global()
        {
            Parser par = new();
            Assert.AreEqual(16, par.ParserExperession("2*-(1-5)*2"));
            Assert.AreEqual(-4, par.ParserExperession("2*-(6-5)*2"));
            Assert.AreEqual(-6, par.ParserExperession("2*(1-5)*2+5*(5-3)"));
            Assert.AreEqual(25, par.ParserExperession("5*0-5*-5+0+0"));

        }

        [TestMethod]
        public void Parser_Test_V()
        {
            Parser par = new();
            Assert.AreEqual(2.50 + 6.50, par.ParserExperession("2.50+6.50"));
            Assert.AreEqual(2 + 2 * 2 / 2, par.ParserExperession("2+2*2/2"));
            Assert.AreEqual((1.0 + (1.0 + 3.0 / (1.0 + 1.0))) * (2.0 / 2.0) + 8.0, par.ParserExperession("(1.0+(1.0+3.0/(1.0+1.0)))*(2.0/2.0)+8.0"));
            Assert.AreEqual((2 + (7 + 90 / (1 + 1))) * (2 / 2) + 76, par.ParserExperession("(2+(7+90/(1+1)))*(2/2)+76"));
        }
    }
}


