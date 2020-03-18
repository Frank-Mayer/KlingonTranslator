using Klingon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KlingonTranslator.Test
{
    [TestClass]
    public class NumberToStringTests
    {
        [TestMethod]
        public void One()
        {
            var result = Translate.TryNumberToKlingon(1, out var n);
            Assert.IsTrue(result);
            Assert.AreEqual(Translate.KlingonNumbers[1], n);
        }

        [TestMethod]
        public void Comma()
        {
            var result = Translate.TryNumberToKlingon(4.85, out var n);
            Assert.IsTrue(result);
            Assert.AreEqual((Translate.KlingonNumbers[4])+" "+ Translate.KlingonDecimalMark+ " " + Translate.KlingonNumbers[8]+ " " + Translate.KlingonNumbers[5], n);
        }

        [TestMethod]
        public void Negative()
        {
            var result = Translate.TryNumberToKlingon(-3, out var n);
            Assert.IsTrue(result);
            Assert.AreEqual(Translate.KlingonNegativeSign+" "+Translate.KlingonNumbers[3], n);
        }
    }
}
