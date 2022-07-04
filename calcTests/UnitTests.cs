namespace calcTests {
    [TestClass]
    public class UnitTests {
        [TestMethod]
        public void CheckDivideByZero() {
            calc.ICalc.Calc(1, 0, "/");
        }
    }
}