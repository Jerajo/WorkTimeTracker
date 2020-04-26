using Xamarin.Forms.PowerControls.Contracts;

namespace Xamarin.Forms.PowerControls.Buttons
{
    public class NavigationButtonModel : BindableBase
    {
        public NavigationButtonModel(string name,
            string path,
            string fontFamily,
            string glyph)
        {
            Name = name;
            Path = path;
            FontFamily = fontFamily;
            Glyph = glyph;
        }

        public string Name { get; }
        public string Path { get; }
        public string FontFamily { get; }
        public string Glyph { get; }
    }
}
