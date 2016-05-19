using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MedicalNumberGenerator
{
  public class PatientIdentifierDefinition : Object
  {
    private string name = "";
    private string maskFormat = "";
    private PatientIdentifierStyle style;

    public string Name
    {
      get { return name; }
      set { name = value; }
    }
    public string MaskFormat
    {
      get { return maskFormat; }
      set { maskFormat = value; }
    }
    public PatientIdentifierStyle Style
    {
      get { return style; }
      set { style = value; }
    }
  }

  public class PatientIdentifierDefinitionListBuilder : Object
  {
    private List<PatientIdentifierDefinition> definitionList = null;

    public void Build()
    {
      PatientIdentifierDefinition definition;

      // Australian Medicare Number
      definition = new PatientIdentifierDefinition
      {
        Name = "Australian Medicare Number",
        MaskFormat = "9999 99999 9-9",
        Style = PatientIdentifierStyle.AustralianMedicareNumber
      };
      DefinitionList.Add(definition);

      // Australian Department Of Veterans Affairs File Number
      definition = new PatientIdentifierDefinition
      {
        Name = "Australian Department Of Veterans Affairs File Number",
        MaskFormat = "LAAAAAAAA",
        Style = PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber
      };
      DefinitionList.Add(definition);

      //// Western Australian Unit Medicare Record Number
      //definition = new PatientIdentifierDefinition();
      //definition.Name = "Western Australian Unit Medical Record Number";
      //definition.MaskFormat = "L9999999";
      //definition.Style = PatientIdentifierStyle.WesternAustralianUnitMedicalRecordNumber;
      //DefinitionList.Add(definition);

      // New Zealand Health Index Number
      definition = new PatientIdentifierDefinition
      { 
        Name = "New Zealand National Health Index Number",
        MaskFormat = "LLL9999",
        Style = PatientIdentifierStyle.NewZealandNationalHealthIndexNumber
      };
      DefinitionList.Add(definition);
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
