using System.Drawing;

namespace TvDatabase.VisualComponents
{
    /// <summary>
    /// A class containing the centralized strings used in various places of the application.
    /// </summary>
    public static class MenuButtonCaptions
    {
        /// <summary>The text used on the menu buttons of the main form</summary>
        public static readonly string[] MainForm_MainMenu = { "Update database", "More actions", "Hide", "EXIT" };
        /// <summary>The text used on the menu buttons of the more actions option of the main form</summary>
        public static readonly string[] MainForm_MainMenu_MoreActions = { "About the app", "Help", "Edit shows", "Edit settings", "Special functions" };
        /// <summary>The text used on the menu buttons of the special functions option of the more actions option of the main form</summary>
        public static readonly string[] MainForm_MainMenu_MoreActions_SpecialFunctions = { "Open workspace", "Export database", "Import database", "Import from WTvSH3", "Clear database" };
        /// <summary>The text used on the menu buttons of the updater form</summary>
        public static readonly string[] UpdateForm_MainMenu = { "Cancel", "Close" };
        /// <summary>The text used on the menu buttons of the episode view options from the epViews form</summary>
        public static readonly string[] SeriesForm_EpisodeViewOptions = { "View detailed info", "Set as NOT watched", "Set as watched", "Set as watched up to here" };
    }

    /// <summary>
    /// A class containing the centralized sizes and dimensions used in various places of the application.
    /// </summary>
    public static class Sizes
    {
        /// <summary>The standard size of a theTvDb.com acting role image</summary>
        public static readonly Size DefaultActingRoleSize = new Size(300, 450);
        /// <summary>The standard size of a theTvDb.com poster</summary>
        public static readonly Size DefaultPosterSize = new Size(680, 1000);
        /// <summary>The standard size of a theTvDb.com banner</summary>
        public static readonly Size DefaultBannerSize = new Size(758, 140);
        /// <summary>The standard padding between form controls</summary>
        public const int ControlPadding = 12;
        /// <summary>The standard size of a main menu button</summary>
        public static readonly Size MainMenuButton = new Size(300, 60);
        /// <summary>The standard size of a menu button</summary>
        public static readonly Size MenuButton = new Size(200, 50);
        /// <summary>The standard size of a tab view FancyCheckBox</summary>
        public static readonly Size TabViewCheckBox = new Size(180, 46);
    }

    /// <summary>
    /// A class containing the centralized colors used in various places of the application.
    /// </summary>
    public static class Colors
    {
        /// <summary>The standard color to be used as background (focused or not) by forms and by visual component classes across the application</summary>
        public static readonly BinaryVariants<Color> Backgrounds = new BinaryVariants<Color>(ColorTranslator.FromHtml("#101010"), ColorTranslator.FromHtml("#161616"));
        /// <summary>The standard color to be used for accent visual features (focused or not) by visual component classes across the application</summary>
        public static readonly BinaryVariants<Color> Accents = new BinaryVariants<Color>(ColorTranslator.FromHtml("#0160b7"), ColorTranslator.FromHtml("#74b701"));
        /// <summary>The standard color to be used for text foreground or graphics (focused or not) by visual component classes across the application</summary>
        public static readonly BinaryVariants<Color> TextOrLines = new BinaryVariants<Color>(ColorTranslator.FromHtml("#cce6ff"), ColorTranslator.FromHtml("#f0ffd7"));
    }

    /// <summary
    /// Represents a pair of items, of whichever type is specified, that can be accessed via a boolean value 
    /// (whian represent, for example, whether the mouse button is down or not).
    /// </summary>
    /// <typeparam name="ITEM_TYPE">the data type of the item pair</typeparam>
    public class BinaryVariants<ITEM_TYPE>
    {
        /// <summary>Stores the items in a fixed-length (2) array</summary>
        private ITEM_TYPE[] items = new ITEM_TYPE[2];

        /// <summary>Constructs a BinaryVariants object from the given item variants</summary>
        /// <param name="itemFalse">the item of the pair for when the boolean value is False</param>
        /// <param name="itemTrue">the item of the pair for when the boolean value is True</param>
        public BinaryVariants(ITEM_TYPE itemFalse, ITEM_TYPE itemTrue)
        {
            this[false] = itemFalse;
            this[true] = itemTrue;
        }

        /// <summary>Gets or sets the item variant for the specified boolean value</summary>
        /// <param name="True">the boolean value (which can represent any specific thing in a given program)</param>
        /// <returns>the item variant of the pair that corresponds to the specified boolean value</returns>
        public ITEM_TYPE this[bool True]
        {
            get { return this.items[True ? 1 : 0]; }
            set { this.items[True ? 1 : 0] = value; }
        }
    }
}
