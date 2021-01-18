using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Mc2Tech.BaseTests
{
    public class AutoConfiguredMoqDataAttribute : AutoDataAttribute
    {
        public AutoConfiguredMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqConfiguredComposite()))
        {
        }
    }

    public class AutoMoqConfiguredComposite : CompositeCustomization
    {
        public AutoMoqConfiguredComposite() : base(new AutoMoqCustomization { ConfigureMembers = true },
                                                        new OmitOnRecursionBehaviorCustomization())
        {
        }
    }
}