using NUnit.Framework;

namespace NUglify.Tests.Css
{
    [TestFixture]
    public class MathFunctions
    {
        [Test]
        public void PreservesNestedMathAndBinaryOperatorWhitespace()
        {
            const string css = ".frame { font-size: min(var(--max), max(var(--fit), var(--min) - clamp(0px, (var(--fit) - var(--min)) * 1000, 1px) * 1000)); }";
            var result = Uglify.Css(css);
            Assert.That(result.HasErrors, Is.False);
            Assert.That(result.Code, Does.Contain("clamp(0px,"));
            Assert.That(result.Code, Does.Contain("var(--min) - clamp("));
            Assert.That(result.Code, Does.Contain("var(--fit) - var(--min)"));
        }

        [TestCase("width:min(0px,1px)", "min(0px,1px)")]
        [TestCase("width:calc(min(1px,2px) + 0px)", " + 0px")]
        [TestCase("width:calc(calc(1px + 1px) + 0px)", " + 0px")]
        [TestCase("width:round(nearest, 0%, 1%)", "0%")]
        [TestCase("width:mod(0px, 1px)", "0px")]
        [TestCase("width:rem(0px, 1px)", "0px")]
        [TestCase("width:abs(0px)", "0px")]
        [TestCase("opacity:sign(0px)", "0px")]
        [TestCase("opacity:sin(0deg)", "0deg")]
        [TestCase("animation-delay:max(0s, 1s)", "0s")]
        [TestCase("width:round(1px + 0px, 1px)", "1px + 0px")]
        [TestCase("--length:0px; width:calc(var(--length) + 1px)", "--length:0px")]
        [TestCase("--length:/*!keep*/0px; width:calc(var(--length) + 1px)", "0px")]
        public void PreservesTypedMathValues(string declaration, string expected)
        {
            var result = Uglify.Css(".x{" + declaration + "}");
            Assert.That(result.HasErrors, Is.False);
            Assert.That(result.Code, Does.Contain(expected));
        }
    }
}
