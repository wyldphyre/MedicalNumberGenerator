using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalNumberGenerator
{
  class PatientIdentifierValidationEngine : Object
  {
    private PatientIdentifierStyle patientIdentifierStyle;
    
    // for internal use
    private Dictionary<char, int> newZealandNationalHealthIndexAlphabetIntegerDictionary = new Dictionary<char, int>();
    private PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary veteransAffairsLibrary = new PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary();

    private bool StringIsAlphanumeric(string value)
    {
      return value.All(C => Char.IsLetterOrDigit(C));
    }
    private bool StringIsNumeric(string value)
    {
      foreach (var c in value)
      {
        if (!char.IsNumber(c))
          return false;
      }

      return true;
    }
    private bool StringIsAlphabetic(string value)
    {
      foreach (var c in value)
      {
        if (!char.IsLetter(c))
          return false;
      }

      return true;
    }

    public PatientIdentifierValidationEngine()
    {
      IssueList = new List<string>();

      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('A', 1);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('B', 2);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('C', 3);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('D', 4);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('E', 5);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('F', 6);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('G', 7);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('H', 8);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('J', 9);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('K', 10);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('L', 11);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('M', 12);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('N', 13);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('P', 14);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('Q', 15);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('R', 16);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('S', 17);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('T', 18);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('U', 19);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('V', 20);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('W', 21);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('X', 22);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('Y', 23);
      newZealandNationalHealthIndexAlphabetIntegerDictionary.Add('Z', 24);
    }

    public bool Validate(string Value)
    {
      IssueList.Clear();

      switch (patientIdentifierStyle)
      {
        case PatientIdentifierStyle.AustralianMedicareNumber:
          
          // remove all non numeric characters from the value
          var localValue = new string(Value.Where(C => Char.IsDigit(C)).ToArray());

          if ((localValue.Length < 10) || (localValue.Length > 11))
            IssueList.Add("Valid Medicare numbers must contain between 10 and 11 numerals.");
          else
          {
            var firstCharacter = int.Parse(localValue[0].ToString());

            if ((firstCharacter < 2) || (firstCharacter > 6))
              IssueList.Add("The first digit of a Medicare number must be in the range [2..6].");
            else
            {
              int checkSum =
                  (int.Parse(localValue[0].ToString()) +
                   int.Parse(localValue[1].ToString()) * 3 +
                   int.Parse(localValue[2].ToString()) * 7 +
                   int.Parse(localValue[3].ToString()) * 9 +
                   int.Parse(localValue[4].ToString()) +
                   int.Parse(localValue[5].ToString()) * 3 +
                   int.Parse(localValue[6].ToString()) * 7 +
                   int.Parse(localValue[7].ToString()) * 9) % 10;

              var ninthCharacter = int.Parse(localValue[8].ToString());

              if (ninthCharacter != checkSum)
                IssueList.Add("Invalid Australian Medicare number.");
            }
          }

          break;

        case PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber:
          if (Value == "")
            IssueList.Add("Veteran's Affairs numbers cannot be blank.");
          else
          {
            if (!StringIsAlphanumeric(Value))
              IssueList.Add("Veteran's Affairs numbers must be alphanumeric.");
            else
            {
              if (Value.Length < 4)
                IssueList.Add("Veteran's Affairs numbers must be at least 4 characters in length.");
              else
              {
                var uppercaseValue = Value.ToUpper();
                var veteranState = uppercaseValue[0];

                if (!Char.IsLetter(veteranState))
                  IssueList.Add("Veteran's Affairs numbers must start with a letter.");
                else
                {
                  var uppercaseValueIndex = 1; // start on the second character
                  var veteranWarCode = "";

                  while ((uppercaseValueIndex < uppercaseValue.Length) && (Char.IsLetter(uppercaseValue[uppercaseValueIndex])))
                  {
                    veteranWarCode += uppercaseValue[uppercaseValueIndex];

                    uppercaseValueIndex++;
                  }

                  var veteranNumber = "";

                  if (uppercaseValueIndex < uppercaseValue.Length)
                    veteranNumber = uppercaseValue.Substring(uppercaseValueIndex, uppercaseValue.Length - uppercaseValueIndex);
                  
                  if (String.IsNullOrEmpty(veteranNumber))
                    IssueList.Add("Veteran's Affairs numbers must end with a number.");
                  else
                  {
                    // strip off the dependency suffix if it exists
                    if (StringIsAlphabetic(veteranNumber[veteranNumber.Length - 1].ToString()))
                      veteranNumber = veteranNumber.Remove(veteranNumber.Length - 1);
             
                    if (!StringIsNumeric(veteranNumber))
                      IssueList.Add($"\"{veteranNumber}\" is not a valid number.");
                    else if (!veteransAffairsLibrary.StateCodeCharacters.Contains(veteranState))
                      IssueList.Add($"\"{veteranState}\" is not a valid Veteran's Affairs state code.");
                    else if (!veteransAffairsLibrary.WarCodeNameDictionary.ContainsKey(veteranWarCode))
                      IssueList.Add($"\"{veteranWarCode}\" is not a valid Veteran's Affairs war code.");
                    else if (veteranNumber.Length > 6)
                      IssueList.Add("The numeric portion of a Veteran's Affairs number cannot exceed 6 characters.");
                  }
                }
              }
            }
          }

          break;
        case PatientIdentifierStyle.WesternAustralianUnitMedicalRecordNumber:
          System.Diagnostics.Debug.Assert(false, "Validation of Western Auatralian Unit Medical record number has not been implemented");

          //if (Value.Length != 8)
          //{
          //    issueList.Add("WA Unit Medical Record numbers must be exactly eight (8) characters long.");
          //}
          //else
          //{
          //    string checksumValue = Value.Substring(1, 7);

          //    if (!StringIsNumeric(checksumValue))
          //    {
          //        issueList.Add("The last 7 characters of WA Unit Medical Record numbers must be numeric.");
          //    }
          //    else
          //    {
          //        //    // This used to just be StringToInteger(FValue), but what if FValue[1] is a non-numeric character?

          //        //    iChecksumKey := SignedMod(StringToInteger(sChecksumValue), 11);

          //        //    If IntegerBetween(iChecksumKey, 0, 7) Then
          //        //      Inc(iChecksumKey);

          //        //    If Not StringEquals(Chr(65 + iChecksumKey), StringGet(FValue, 1)) Then
          //        //      FIssueList.Add('This WA Unit Medical Record number has an invalid first letter.');       
          //    }
          //}

          break;
        case PatientIdentifierStyle.NewZealandNationalHealthIndexNumber:
          if (Value.Length != 7)
            IssueList.Add("Valid New Zealand National Health Index numbers must have a length of 7 characters.");
          else
          {
            for (int i = 0; i < 3; i++)
            {
              if (!Char.IsUpper(Value[i]) || (Value[i] == 'I') || (Value[i] == 'O'))
                IssueList.Add(String.Format("The character \"{0}\" in position {1} must be an uppercase letter other than \"I\" or \"O\".", Value[i], i));
            }

            for (int i = 3; i < Value.Length; i++)
            {
              if (!Char.IsNumber(Value[i]))
                IssueList.Add(String.Format("The character \"{0}\" in position {1} must be a number in the range 0..9", Value[i], i));
            }

            if (IssueList.Count == 0)
            {
              int characterValue1;
              int characterValue2;
              int characterValue3;

              newZealandNationalHealthIndexAlphabetIntegerDictionary.TryGetValue(Value[0], out characterValue1);
              newZealandNationalHealthIndexAlphabetIntegerDictionary.TryGetValue(Value[1], out characterValue2);
              newZealandNationalHealthIndexAlphabetIntegerDictionary.TryGetValue(Value[2], out characterValue3);
              int characterValue4 = int.Parse(Value[3].ToString());
              int characterValue5 = int.Parse(Value[4].ToString());
              int characterValue6 = int.Parse(Value[5].ToString());

              int checkSumKey =
                  (characterValue1 * 7 +
                   characterValue2 * 6 +
                   characterValue3 * 5 +
                   characterValue4 * 4 +
                   characterValue5 * 3 +
                   characterValue6 * 2) % 11;

              if (checkSumKey == 0)
                IssueList.Add("Cannot calculate a valid check sum because the New Zealand National Health Index number is incorrect.");
              else
              {
                checkSumKey = 11 - checkSumKey;

                if (checkSumKey == 10)
                  checkSumKey = 0;
          
                char checkSumKeyAsChar = checkSumKey.ToString()[0];

                if (checkSumKeyAsChar != Value[Value.Length - 1])
                  IssueList.Add(String.Format("Checksum digit \"{0}\" is incorrect.", checkSumKeyAsChar));
              }
            }
          }
          //      iCheckSumKey :=
          //        ((FNewZealandNationalHealthIndexAlphabetIntegerMatch.GetValueByKey(sLocalValue[1]) * 7) +
          //         (FNewZealandNationalHealthIndexAlphabetIntegerMatch.GetValueByKey(sLocalValue[2]) * 6) +
          //         (FNewZealandNationalHealthIndexAlphabetIntegerMatch.GetValueByKey(sLocalValue[3]) * 5) +
          //         (StringToInteger(sLocalValue[4]) * 4) +
          //         (StringToInteger(sLocalValue[5]) * 3) +
          //         (StringToInteger(sLocalValue[6]) * 2)) Mod 11;

          //      If iCheckSumKey = 0 Then
          //      Begin
          //        FIssueList.Add('Cannot calculate a valid check sum because the New Zealand National Health Index number is incorrect.');
          //      End
          //      Else
          //      Begin
          //        iCheckSumKey := 11 - iCheckSumKey;

          //        If iCheckSumkey = 10 Then
          //          iCheckSumKey := 0;

          //        sCheckSumValue := IntegerToString(iCheckSumKey);

          //        If sCheckSumValue <> sLocalValue[7] Then
          //          FIssueList.Add(StringFormat('Checksum digit "%s" is incorrect', [sLocalValue[7]]));
          //      End;

          break;
        default:
          throw new Exception(String.Format("Patient identifier style \"{0}\" is not handled by the validation engine.", patientIdentifierStyle.ToString()));
      }

      return !IssueList.Any();
    }
    public bool HasIssues()
    {
      return IssueList.Count > 0;
    }

    public PatientIdentifierStyle PatientIdentifierStyle
    {
      get { return patientIdentifierStyle; }
      set { patientIdentifierStyle = value; }
    }
    public List<string> IssueList { get; private set; }
  }
}
