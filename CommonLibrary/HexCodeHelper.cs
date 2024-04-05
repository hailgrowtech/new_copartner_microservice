namespace CommonLibrary;

public static class HexCodeHelper
{
    public static string GenAlphaNumericHexCode(int length)
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
        var Charsarr = new char[length];
        var random = new Random();

        for (int i = 0; i < Charsarr.Length; i++)
        {
            Charsarr[i] = characters[random.Next(characters.Length)];
        }
        return new String(Charsarr);
    }
}