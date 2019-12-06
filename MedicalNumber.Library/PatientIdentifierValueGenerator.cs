using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalNumber.Library
{
  public class PatientIdentifierValueGenerator
  {
    public string Generate(PatientIdentifierDefinition definition)
    {
      switch (definition.Style)
      {
        case PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber:
          return new VeteransAffairsPatientIdentifierGenerator().Generate();

        default:
          return new MaskedTextRandomValueGenerator().Generate(definition.MaskFormat);
      }
    }
  }

  public class VeteransAffairsPatientIdentifierGenerator
  {
    private readonly char[] numericCharacters = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
    private readonly List<string> warCodesList = new List<string>();

    public VeteransAffairsPatientIdentifierGenerator()
    {
      foreach (var kvp in PatientIdentifierStyleVeteransAffairsFileNumberComponentLibrary.WarCodeNameDictionary)
      {
        warCodesList.Add(kvp.Key);
      }
    }

    public string Generate()
    {
      // To Do: generate a value using random data from the library, and random data for
      // the componet that doesn't come from the library

      var valueBuilder = new StringBuilder(string.Empty);
      const string MaskFormat = "LAAAAAAAA";
      var remainingCharacterCount = MaskFormat.Length;

      var rnd = new Random((int)DateTime.Now.Ticks);

      valueBuilder.Append(PatientIdentifierStyleVeteransAffairsFileNumberComponentLibrary.StateCodeCharacters[rnd.Next(PatientIdentifierStyleVeteransAffairsFileNumberComponentLibrary.StateCodeCharacters.Length)]);
      valueBuilder.Append(warCodesList[rnd.Next(PatientIdentifierStyleVeteransAffairsFileNumberComponentLibrary.WarCodeNameDictionary.Count)]);
      remainingCharacterCount -= valueBuilder.Length;

      for (int i = 1; i <= remainingCharacterCount; i++)
      {
        valueBuilder.Append(numericCharacters[rnd.Next(numericCharacters.Length)]);
      }

      var value = valueBuilder.ToString();

      System.Diagnostics.Debug.Assert(value.Length == MaskFormat.Length, String.Format("Length of generated value \"{0}\" is too long", value));

      return value;
    }
  }
}
