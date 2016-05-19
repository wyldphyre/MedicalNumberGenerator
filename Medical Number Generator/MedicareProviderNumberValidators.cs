using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalNumberGenerator
{
  class MedicareProviderNumberValidator
  {
    private List<string> issueList = new List<string>();
    private string value = "";

    private char[] practiceLocationCharacters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 'X', 'Y', };
    private Dictionary<int, char> practiceLocationCheckDigitValueDictionary = new Dictionary<int, Char>();

    public MedicareProviderNumberValidator()
    {      
      practiceLocationCheckDigitValueDictionary.Add(1, 'Y');
      practiceLocationCheckDigitValueDictionary.Add(2, 'X');
      practiceLocationCheckDigitValueDictionary.Add(3, 'W');
      practiceLocationCheckDigitValueDictionary.Add(4, 'T');
      practiceLocationCheckDigitValueDictionary.Add(5, 'L');
      practiceLocationCheckDigitValueDictionary.Add(6, 'K');
      practiceLocationCheckDigitValueDictionary.Add(7, 'J');
      practiceLocationCheckDigitValueDictionary.Add(8, 'H');
      practiceLocationCheckDigitValueDictionary.Add(9, 'F');
      practiceLocationCheckDigitValueDictionary.Add(10, 'B');
      practiceLocationCheckDigitValueDictionary.Add(11, 'A');
    }

    public void Execute()
    {
      System.Diagnostics.Debug.Assert(value != "", "You must provide a Value to validate");

      issueList.Clear();

      var paddedProviderNumber = "";

      if (value.Length < 7)
        issueList.Add("Provider number must be at least 7 characters");
      else if (value.Length > 8)
        issueList.Add("Provider number can be no more than 8 characters");
      else if (value.Length == 7)
        paddedProviderNumber = '0' + value;
      else
        paddedProviderNumber = value;
      
      if (paddedProviderNumber.Length == 8)
      {
        // Check that the 7th character is an allowed practice location character
        char seventhCharacter = paddedProviderNumber[6];

        if (!practiceLocationCharacters.Contains(seventhCharacter))
          issueList.Add(String.Format(@"Practice location character [{0}]", seventhCharacter));
      
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

        char validationCheckDigit;

        practiceLocationCheckDigitValueDictionary.TryGetValue(validationCheckDigitValue + 1, out validationCheckDigit);

        if (validationCheckDigit != paddedProviderNumber[7])
          issueList.Add("Check digit character is not valid.");
      }
    }

    public bool HasIssues()
    {
      return issueList.Count > 0;
    }

    public string Value
    {
      get { return value; }
      set { this.value = value; }
    }
  }
}
