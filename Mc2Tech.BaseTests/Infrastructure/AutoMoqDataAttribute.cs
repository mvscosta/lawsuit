using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Xunit;

namespace Mc2Tech.BaseTests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqComposite()).Customize(new IgnoreVirtualMembersCustomization()))
        {
        }
    }

    public class AutoMoqComposite : CompositeCustomization
    {
        public AutoMoqComposite() : base(new AutoMoqCustomization(),
                                            new OmitOnRecursionBehaviorCustomization())
        {
        }
    }

    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values), new AutoMoqDataAttribute())
        {
        }
    }
}