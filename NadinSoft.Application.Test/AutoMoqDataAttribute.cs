using AutoFixture.Xunit2;

namespace NadinSoft.Application.Test
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AutoMoqDataAttribute : AutoDataAttribute
    {

        public AutoMoqDataAttribute() : base(TestFixture.ConfigurationAutoDataAttribute)
        {
        }
    }

    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects) { }
    }
}
