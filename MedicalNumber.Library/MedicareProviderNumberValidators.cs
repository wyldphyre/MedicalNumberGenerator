using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalNumber.Library
{
  public static class MedicareProviderNumberHelper
  {
    private static readonly char[] practiceLocationCharacters;
    private static readonly Dictionary<int, char> practiceLocationCheckDigitValueDictionary;

    static MedicareProviderNumberHelper()
    {
      practiceLocationCharacters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 'X', 'Y', };

      practiceLocationCheckDigitValueDictionary = new Dictionary<int, char>
      {
        {1, 'Y'},
        {2, 'X'},
        {3, 'W'},
        {4, 'T'},
        {5, 'L'},
        {6, 'K'},
        {7, 'J'},
        {8, 'H'},
        {9, 'F'},
        {10, 'B'},
        {11, 'A'}
      };
    }

    public static bool Validate(string number, out List<string> issues)
    {
      System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(number), "You must provide a number to validate");

      issues = new List<string>();
      
      var paddedProviderNumber = string.Empty;

      if (number.Length < 7)
        issues.Add("Provider number must be at least 7 characters");
      else if (number.Length > 8)
        issues.Add("Provider number can be no more than 8 characters");
      else if (number.Length == 7)
        paddedProviderNumber = '0' + number;
      else
        paddedProviderNumber = number;
      
      if (paddedProviderNumber.Length == 8)
      {
        // Check that the 7th character is an allowed practice location character
        var seventhCharacter = paddedProviderNumber[6];

        if (!practiceLocationCharacters.Contains(seventhCharacter))
          issues.Add($@"Practice location character [{seventhCharacter}]");
      
        var characterValue1 = int.Parse(paddedProviderNumber[0].ToString());
        var characterValue2 = int.Parse(paddedProviderNumber[1].ToString());
        var characterValue3 = int.Parse(paddedProviderNumber[2].ToString());
        var characterValue4 = int.Parse(paddedProviderNumber[3].ToString());
        var characterValue5 = int.Parse(paddedProviderNumber[4].ToString());
        var characterValue6 = int.Parse(paddedProviderNumber[5].ToString());

        var validationCheckDigitValue =
            (characterValue1 * 3 +
             characterValue2 * 5 +
             characterValue3 * 8 +
             characterValue4 * 4 +
             characterValue5 * 2 +
             characterValue6 +
             Array.IndexOf(practiceLocationCharacters, seventhCharacter) * 6) % 11;

        practiceLocationCheckDigitValueDictionary.TryGetValue(validationCheckDigitValue + 1, out var validationCheckDigit);

        if (validationCheckDigit != paddedProviderNumber[7])
          issues.Add("Check digit character is not valid.");
      }

      return issues.Count == 0;
    }
  }
}
