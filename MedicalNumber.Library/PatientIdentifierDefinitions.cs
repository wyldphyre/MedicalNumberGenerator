namespace MedicalNumber.Library
{
  public class PatientIdentifierDefinition
  {
    public PatientIdentifierDefinition(string name, string maskFormat, PatientIdentifierStyle style)
    {
      Name = name;
      MaskFormat = maskFormat;
      Style = style;
    }
    public readonly string Name;
    public readonly string MaskFormat;
    public readonly PatientIdentifierStyle Style;
  }

  public static class PatientIdentifierDefinitionListBuilder
  {
    static PatientIdentifierDefinitionListBuilder()
    {
      Definitions = new[]
      {
        new PatientIdentifierDefinition("Australian Medicare Number", "9999 99999 9-9", PatientIdentifierStyle.AustralianMedicareNumber),

        new PatientIdentifierDefinition("Australian Department Of Veterans Affairs File Number", "LAAAAAAAA", PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber),

        new PatientIdentifierDefinition("New Zealand National Health Index Number", "LLL9999", PatientIdentifierStyle.NewZealandNationalHealthIndexNumber)
      };
    }

    public static readonly PatientIdentifierDefinition[] Definitions;
  }
}
