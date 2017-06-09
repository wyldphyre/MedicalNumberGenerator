using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalNumberGenerator
{
  class PatientIdentifierValueGenerator
  {
    public string Generate(PatientIdentifierDefinition definition)
    {
      switch (definition.Style)
      {
        case PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber:
          VeteransAffairsPatientIdentifierValueGenerator veteransAffairsGenerator = new VeteransAffairsPatientIdentifierValueGenerator();
          return veteransAffairsGenerator.Generate();

        default:
          MaskedTextRandomValueGenerator maskedTextRandomValueGenerator = new MaskedTextRandomValueGenerator()
          {
            MaskFormat = definition.MaskFormat
          };
          return maskedTextRandomValueGenerator.Generate();
      }
    }
  }

  public class VeteransAffairsPatientIdentifierValueGenerator
  {
    private char[] numericCharacters = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
    private List<string> warCodesList = new List<string>();
    private PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary veteransAffairsLibrary = new PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary();

    public VeteransAffairsPatientIdentifierValueGenerator()
    {
      foreach (var kvp in veteransAffairsLibrary.WarCodeNameDictionary)
      {
        warCodesList.Add(kvp.Key);
      }
    }

    public string Generate()
    {
      // To Do: generate a value using random data from the library, and random data for
      // the componet that doesn't come from the library

      var valueBuilder = new StringBuilder("");
      const string MaskFormat = "LAAAAAAAA";
      var remainingCharacterCount = MaskFormat.Length;

      var rnd = new Random((int)DateTime.Now.Ticks);

      valueBuilder.Append(veteransAffairsLibrary.StateCodeCharacters[rnd.Next(veteransAffairsLibrary.StateCodeCharacters.Length)]);
      valueBuilder.Append(warCodesList[rnd.Next(veteransAffairsLibrary.WarCodeNameDictionary.Count)]);
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
