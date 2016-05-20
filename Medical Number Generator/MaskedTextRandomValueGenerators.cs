using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalNumberGenerator
{
  class MaskedTextRandomValueGenerator
  {
    private List<char> alphanumericCharacterList = new List<char>();
    private List<char> alphabeticCharacterList = new List<char>();
    private List<char> numericCharacterList = new List<char>();

    public MaskedTextRandomValueGenerator()
    {
      alphabeticCharacterList.Add('A');
      alphabeticCharacterList.Add('B');
      alphabeticCharacterList.Add('C');
      alphabeticCharacterList.Add('D');
      alphabeticCharacterList.Add('E');
      alphabeticCharacterList.Add('F');
      alphabeticCharacterList.Add('G');
      alphabeticCharacterList.Add('H');
      alphabeticCharacterList.Add('I');
      alphabeticCharacterList.Add('J');
      alphabeticCharacterList.Add('K');
      alphabeticCharacterList.Add('L');
      alphabeticCharacterList.Add('M');
      alphabeticCharacterList.Add('N');
      alphabeticCharacterList.Add('O');
      alphabeticCharacterList.Add('P');
      alphabeticCharacterList.Add('Q');
      alphabeticCharacterList.Add('R');
      alphabeticCharacterList.Add('S');
      alphabeticCharacterList.Add('T');
      alphabeticCharacterList.Add('U');
      alphabeticCharacterList.Add('V');
      alphabeticCharacterList.Add('W');
      alphabeticCharacterList.Add('X');
      alphabeticCharacterList.Add('Y');
      alphabeticCharacterList.Add('Z');

      numericCharacterList.Add('1');
      numericCharacterList.Add('2');
      numericCharacterList.Add('3');
      numericCharacterList.Add('4');
      numericCharacterList.Add('5');
      numericCharacterList.Add('6');
      numericCharacterList.Add('7');
      numericCharacterList.Add('8');
      numericCharacterList.Add('9');
      numericCharacterList.Add('0');

      foreach (char character in alphabeticCharacterList)
        alphanumericCharacterList.Add(character);

      foreach (char character in numericCharacterList)
        alphanumericCharacterList.Add(character);
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
            textBuilder.Append(alphanumericCharacterList[rnd.Next(alphanumericCharacterList.Count)]);
            break;
          case 'L':
            textBuilder.Append(alphabeticCharacterList[rnd.Next(alphabeticCharacterList.Count)]);
            break;
          case '9':
            textBuilder.Append(numericCharacterList[rnd.Next(numericCharacterList.Count)]);
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
