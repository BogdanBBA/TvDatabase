using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// Defines a structure that holds modifiable basic font attributes, as well as color.
    /// </summary>
    public class FontWithColor
    {
        /// <summary>Gets or sets the name of the font.</summary>
        public string FontName { get; set; }
        /// <summary>Gets or sets the size of the font.</summary>
        public int FontSize { get; set; }
        /// <summary>Gets or sets the bold attribute of the font.</summary>
        public bool Bold { get; set; }
        /// <summary>Gets or sets the italic attribute of the font.</summary>
        public bool Italic { get; set; }
        /// <summary>Gets or sets the underlined attribute of the font.</summary>
        public bool Underlined { get; set; }
        /// <summary>Gets or sets the color of the font.</summary>
        public Color FontColor { get; set; }

        /// <summary>Constructs a FontWithColor object with the default attributes.</summary>
        public FontWithColor()
            : this("Segoe UI", 12, Color.DarkGray)
        {
        }

        /// <summary>Constructs a FontWithColor object with the specified (and other default) attributes.</summary>
        /// <param name="fontName">the name of the font</param>
        /// <param name="fontSize">the size of the font</param>
        /// <param name="fontColor">the color of the font</param>
        public FontWithColor(string fontName, int fontSize, Color fontColor)
            : this(fontName, fontSize, false, false, false, fontColor)
        {
        }
        
        /// <summary>Constructs a FontWithColor object with the specified (and other default) attributes.</summary>
        /// <param name="fontName">the name of the font</param>
        /// <param name="fontSize">the size of the font</param>
        /// <param name="bold">the bold attribute of the font</param>
        /// <param name="italic"the italic attribute of the font></param>
        /// <param name="underlined">the underlined attribute of the font</param>
        /// <param name="fontColor">the color of the font</param>
        public FontWithColor(string fontName, int fontSize, bool bold, bool italic, bool underlined, Color fontColor)
        {
            this.FontName = fontName;
            this.FontSize = fontSize;
            this.Bold = bold;
            this.Italic = italic;
            this.Underlined = underlined;
            this.FontColor = fontColor;
        }

        /// <summary>Constructs a new FontWithColor object with the same attributes as the object specified.</summary>
        /// <param name="baseFont">the FontWithColor object to copy attributes from</param>
        public FontWithColor(FontWithColor baseFont)
            : this(baseFont.FontName, baseFont.FontSize, baseFont.Bold, baseFont.Italic, baseFont.Underlined, baseFont.FontColor)
        {
        }

        /// <summary>Generates a Font object from the current attributes.</summary>
        /// <returns>a System.Drawing.Font object</returns>
        public Font GetFont()
        {
            FontStyle fontStyle = FontStyle.Regular;
            if (this.Bold)
                fontStyle = fontStyle ^ FontStyle.Bold;
            if (this.Italic)
                fontStyle = fontStyle ^ FontStyle.Italic;
            if (this.Underlined)
                fontStyle = fontStyle ^ FontStyle.Underline;
            return new Font(this.FontName, this.FontSize, fontStyle);
        }

        /// <summary>Generates a Brush object from the current object's FontColor attribute.</summary>
        /// <returns>a SolidBrush object</returns>
        public Brush GetBrush()
        {
            return new SolidBrush(this.FontColor);
        }
    }
}
