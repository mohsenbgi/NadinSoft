using AutoFixture;
using AutoFixture.AutoMoq;

namespace NadinSoft.Application.Test
{
    public class TestFixture
    {
        public IFixture AutoFixture;
        public TestFixture()
        {
            AutoFixture = ConfigurationAutoDataAttribute();
        }

        public static IFixture ConfigurationAutoDataAttribute()
        {

            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }

}
