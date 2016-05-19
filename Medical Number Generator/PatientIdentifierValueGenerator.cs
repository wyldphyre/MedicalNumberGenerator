using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalNumberGenerator
{
  class PatientIdentifierValueGenerator
  {
    #region private

    private PatientIdentifierDefinition definition;
    private string value = "";

    #endregion

    #region public

    public void Execute()
    {
      switch (Definition.Style)
      {
        case PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber:
          VeteransAffairsPatientIdentifierValueGenerator veteransAffairsGenerator = new VeteransAffairsPatientIdentifierValueGenerator();
          veteransAffairsGenerator.Execute();

          value = veteransAffairsGenerator.Value;

          break;
        default:
          MaskedTextRandomValueGenerator maskedTextRandomValueGenerator = new MaskedTextRandomValueGenerator();

          maskedTextRandomValueGenerator.MaskFormat = Definition.MaskFormat;
          maskedTextRandomValueGenerator.Execute();

          value = maskedTextRandomValueGenerator.Text;

          break;
      }
    }

    #endregion

    #region properties

    public string Value
    {
      get { return value; }
    }

    public PatientIdentifierDefinition Definition
    {
      get
      {
        System.Diagnostics.Debug.Assert(definition != null, "definition is not assigned.");

        return definition;
      }

      set
      {
        definition = value;
      }
    }

    #endregion
  }

  public class VeteransAffairsPatientIdentifierValueGenerator
  {
    private string value = "";

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

    public void Execute()
    {
      // To Do: generate a value using random data from the library, and random data for
      // the componet that doesn't come from the library

      var valueBuilder = new StringBuilder("");
      const string MaskFormat = "LAAAAAAAA";
      var remainingCharacterCount = MaskFormat.Length;

      value = "";

      var rnd = new Random((int)DateTime.Now.Ticks);

      valueBuilder.Append(veteransAffairsLibrary.StateCodeCharacters[rnd.Next(veteransAffairsLibrary.StateCodeCharacters.Length)]);
      valueBuilder.Append(warCodesList[rnd.Next(veteransAffairsLibrary.WarCodeNameDictionary.Count)]);
      remainingCharacterCount -= valueBuilder.Length;

      for (int i = 1; i <= remainingCharacterCount; i++)
      {
        valueBuilder.Append(numericCharacters[rnd.Next(numericCharacters.Length)]);
      }

      value = valueBuilder.ToString(); ;

      System.Diagnostics.Debug.Assert(value.Length == MaskFormat.Length, String.Format("Length of generated value \"{0}\" is too long", value));
    }

    public string Value
    {
      get { return this.value; }
    }
  }
}
