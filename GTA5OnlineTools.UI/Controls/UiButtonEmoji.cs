namespace GTA5OnlineTools.UI.Controls;

public class UiButtonEmoji : Button
{
    /// <summary>
    /// Emoji图标
    /// </summary>
    public string Emoji
    {
        get { return (string)GetValue(EmojiProperty); }
        set { SetValue(EmojiProperty, value); }
    }
    public static readonly DependencyProperty EmojiProperty =
        DependencyProperty.Register("Emoji", typeof(string), typeof(UiButtonEmoji), new PropertyMetadata(default));
}
