using System;
using System.Text;
using System.Linq;

namespace MedicalNumberGenerator
{
  class MaskedTextRandomValueGenerator
  {
    private readonly char[] alphanumericCharacters;
    private readonly char[] alphabeticCharacters = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    private readonly char[] numericCharacters = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

    public MaskedTextRandomValueGenerator()
    {
      alphanumericCharacters = alphabeticCharacters.Union(numericCharacters).ToArray();
    }

    public void Execute()
    {
      // To Do: run a validation to ensure that the mask format only contains characters that
      // the generator knows how to deal with.

      var textBuilder = new StringBuilder("", MaskFormat.Length);

      Text = "";

      Random rnd = new Random((int)DateTime.Now.Ticks);

      foreach (char maskCharacter in MaskFormat)
      {
        switch (maskCharacter)
        {
          case 'A':
            textBuilder.Append(alphanumericCharacters[rnd.Next(alphanumericCharacters.Length)]);
            break;
          case 'L':
            textBuilder.Append(alphabeticCharacters[rnd.Next(alphabeticCharacters.Length)]);
            break;
          case '9':
            textBuilder.Append(numericCharacters[rnd.Next(numericCharacters.Length)]);
            break;
          default:
            textBuilder.Append(maskCharacter);
            break;
        }
      }

      Text = textBuilder.ToString();
    }

    public string MaskFormat { get; set; }

    public string Text { get; private set; }
  }
}
