namespace calcTests {
    [TestClass]
    public class UnitTests {
        [TestMethod]
        public void CheckDivideByZero() {
            calc.FGL.Calc(1, 0, "/");
        }
    }
}