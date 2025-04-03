using System.Drawing;

namespace Lab01_2nd_tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests
    {
        [Test]
        public void RectangleConstructor_CorrectInitialization()
        {
            int sideA = 3, sideB = 4;
            Rectangle rectangle = new Rectangle(sideA, sideB);
            Assert.AreEqual(sideA, rectangle.getSideA);
            Assert.AreEqual(sideB, rectangle.getSideB);
        }
        [Test]
        public void CalculateArea_Correct()
        {
            Rectangle test = new Rectangle(3, 3);
            Assert.AreEqual(9, test.Area);
        }
        [Test]
        public void CalculatePerimeter_Correct()
        {
            Rectangle test = new Rectangle(3, 3);
            Assert.AreEqual(12, test.Perimeter);
        }
       
    }
}
