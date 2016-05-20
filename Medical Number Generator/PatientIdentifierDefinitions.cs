using System.Collections.Generic;
using System.Diagnostics;

namespace MedicalNumberGenerator
{
  public class PatientIdentifierDefinition
  {
    public string Name { get; set; }
    public string MaskFormat { get; set; }
    public PatientIdentifierStyle Style { get; set; }
  }

  public class PatientIdentifierDefinitionListBuilder
  {
    private List<PatientIdentifierDefinition> definitionList = null;

    public void Build()
    {
      DefinitionList.Add(new PatientIdentifierDefinition
      {
        Name = "Australian Medicare Number",
        MaskFormat = "9999 99999 9-9",
        Style = PatientIdentifierStyle.AustralianMedicareNumber
      });

      DefinitionList.Add(new PatientIdentifierDefinition
      {
        Name = "Australian Department Of Veterans Affairs File Number",
        MaskFormat = "LAAAAAAAA",
        Style = PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber
      });

      //definition = new PatientIdentifierDefinition();
      //definition.Name = "Western Australian Unit Medical Record Number";
      //definition.MaskFormat = "L9999999";
      //definition.Style = PatientIdentifierStyle.WesternAustralianUnitMedicalRecordNumber;
      //DefinitionList.Add(definition);

      DefinitionList.Add(new PatientIdentifierDefinition
      { 
        Name = "New Zealand National Health Index Number",
        MaskFormat = "LLL9999",
        Style = PatientIdentifierStyle.NewZealandNationalHealthIndexNumber
      });
    }

    public List<PatientIdentifierDefinition> DefinitionList
    {
      get
      {
        Debug.Assert(definitionList != null, "definitionList is null. Must be assigned before calling Build.");

        return definitionList;
      }
      set
      {
        Debug.Assert(value != null, "Cannot assign a null value to DefinitionList.");

        definitionList = value;
      }
    }
  }
}
