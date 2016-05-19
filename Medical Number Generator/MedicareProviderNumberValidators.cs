using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalNumberGenerator
{
    class MedicareProviderNumberValidator
    {
        private List<string> issueList = new List<string>();
        private string value = "";

        private List<char> practiceLocationCharacterList = new List<char>();
        private Dictionary<int, char> practiceLocationCheckDigitValueDictionary = new Dictionary<int, Char>();

        public MedicareProviderNumberValidator()
        {
            practiceLocationCharacterList.Add('0');
            practiceLocationCharacterList.Add('1');
            practiceLocationCharacterList.Add('2');
            practiceLocationCharacterList.Add('3');
            practiceLocationCharacterList.Add('4');
            practiceLocationCharacterList.Add('5');
            practiceLocationCharacterList.Add('6');
            practiceLocationCharacterList.Add('7');
            practiceLocationCharacterList.Add('8');
            practiceLocationCharacterList.Add('9');
            practiceLocationCharacterList.Add('A');
            practiceLocationCharacterList.Add('B');
            practiceLocationCharacterList.Add('C');
            practiceLocationCharacterList.Add('D');
            practiceLocationCharacterList.Add('E');
            practiceLocationCharacterList.Add('F');
            practiceLocationCharacterList.Add('G');
            practiceLocationCharacterList.Add('H');
            practiceLocationCharacterList.Add('J');
            practiceLocationCharacterList.Add('K');
            practiceLocationCharacterList.Add('L');
            practiceLocationCharacterList.Add('M');
            practiceLocationCharacterList.Add('N');
            practiceLocationCharacterList.Add('P');
            practiceLocationCharacterList.Add('Q');
            practiceLocationCharacterList.Add('R');
            practiceLocationCharacterList.Add('T');
            practiceLocationCharacterList.Add('U');
            practiceLocationCharacterList.Add('V');
            practiceLocationCharacterList.Add('W');
            practiceLocationCharacterList.Add('X');
            practiceLocationCharacterList.Add('Y');

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

            string paddedProviderNumber = "";

            if (value.Length < 7)
            {
                issueList.Add("Provider number must be at least 7 characters");
            }
            else if (value.Length > 8)
            {
                issueList.Add("Provider number can be no more than 8 characters");
            }
            else if (value.Length == 7)
            {
                paddedProviderNumber = '0' + value;
            }
            else
            {
                paddedProviderNumber = value;
            }

            if (paddedProviderNumber.Length == 8)
            {
                // Check that the 7th character is an allowed practice location character
                char seventhCharacter = paddedProviderNumber[6];

                if (!practiceLocationCharacterList.Contains(seventhCharacter))
                {
                    issueList.Add(String.Format(@"Practice location character [{0}]", seventhCharacter));
                }

                int characterValue1 = int.Parse(paddedProviderNumber[0].ToString());
                int characterValue2 = int.Parse(paddedProviderNumber[1].ToString());
                int characterValue3 = int.Parse(paddedProviderNumber[2].ToString());
                int characterValue4 = int.Parse(paddedProviderNumber[3].ToString());
                int characterValue5 = int.Parse(paddedProviderNumber[4].ToString());
                int characterValue6 = int.Parse(paddedProviderNumber[5].ToString());

                int validationCheckDigitValue =
                    (characterValue1 * 3 +
                     characterValue2 * 5 +
                     characterValue3 * 8 +
                     characterValue4 * 4 +
                     characterValue5 * 2 +
                     characterValue6 +
                     practiceLocationCharacterList.IndexOf(seventhCharacter) * 6) % 11;

                char validationCheckDigit;

                practiceLocationCheckDigitValueDictionary.TryGetValue(validationCheckDigitValue + 1, out validationCheckDigit);

                if (validationCheckDigit != paddedProviderNumber[7])
                {
                    issueList.Add("Check digit character is not valid.");
                }
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
