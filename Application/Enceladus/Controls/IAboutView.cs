using Enceladus.UIToolbox;

namespace Enceladus
{
    interface IAboutView
    {
        GradientButton MainMenuButton { get; }
        BrandInfoBox RightBrandBox { get; }
        InfoBox RightsBox { get; }
        InfoBox AuthorBox { get; }
    }
}
