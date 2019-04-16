using System.Collections.Generic;
using System.Diagnostics;

namespace MedicalNumber
{
  public class PatientIdentifierDefinition
  {
    public string Name { get; set; }
    public string MaskFormat { get; set; }
    public PatientIdentifierStyle Style { get; set; }
  }

  public static class PatientIdentifierDefinitionListBuilder
  {
    public static PatientIdentifierDefinition[] Build()
    {
      var DefinitionList = new List<PatientIdentifierDefinition>
      {
        new PatientIdentifierDefinition
        {
          Name = "Australian Medicare Number",
          MaskFormat = "9999 99999 9-9",
          Style = PatientIdentifierStyle.AustralianMedicareNumber
        },

        new PatientIdentifierDefinition
        {
          Name = "Australian Department Of Veterans Affairs File Number",
          MaskFormat = "LAAAAAAAA",
          Style = PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber
        },

        //definition = new PatientIdentifierDefinition();
        //definition.Name = "Western Australian Unit Medical Record Number";
        //definition.MaskFormat = "L9999999";
        //definition.Style = PatientIdentifierStyle.WesternAustralianUnitMedicalRecordNumber;
        //DefinitionList.Add(definition);

        new PatientIdentifierDefinition
        {
          Name = "New Zealand National Health Index Number",
          MaskFormat = "LLL9999",
          Style = PatientIdentifierStyle.NewZealandNationalHealthIndexNumber
        }
      };
      return DefinitionList.ToArray();
    }
  }
}
