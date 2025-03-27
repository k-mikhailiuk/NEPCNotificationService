namespace Common;

public static class PanMask
{
    public static string MaskPan(string pan)
    {
        if (string.IsNullOrEmpty(pan))
            return string.Empty;

        if (pan.Length <= 4)
            return pan;

        var last4 = pan[^4..];

        return '*' + last4;
    }

}