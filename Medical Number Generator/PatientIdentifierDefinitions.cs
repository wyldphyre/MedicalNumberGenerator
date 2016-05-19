using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MedicalNumberGenerator
{
    public class PatientIdentifierDefinition : Object
    {
        #region private

        private string name = "";
        private string maskFormat = "";
        private PatientIdentifierStyle style;
        
        #endregion

        #region public properties

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

        #endregion
    }

    public class PatientIdentifierDefinitionListBuilder : Object
    {
        private List<PatientIdentifierDefinition> definitionList = null;

        public void Build()
        {
            PatientIdentifierDefinition definition;

            // Australian Medicare Number
            definition = new PatientIdentifierDefinition();
            definition.Name = "Australian Medicare Number";
            definition.MaskFormat = "9999 99999 9-9";
            definition.Style = PatientIdentifierStyle.AustralianMedicareNumber;
            DefinitionList.Add(definition);

            // Australian Departement Of Veterans Affairs File Number
            definition = new PatientIdentifierDefinition();
            definition.Name = "Australian Department Of Veterans Affairs File Number";
            definition.MaskFormat = "LAAAAAAAA";
            definition.Style = PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber;
            DefinitionList.Add(definition);

            //// Western Australian Unit Medicare Record Number
            //definition = new PatientIdentifierDefinition();
            //definition.Name = "Western Australian Unit Medical Record Number";
            //definition.MaskFormat = "L9999999";
            //definition.Style = PatientIdentifierStyle.WesternAustralianUnitMedicalRecordNumber;
            //DefinitionList.Add(definition);

            // New Zealand Health Index Number
            definition = new PatientIdentifierDefinition();
            definition.Name = "New Zealand National Health Index Number";
            definition.MaskFormat = "LLL9999";
            definition.Style = PatientIdentifierStyle.NewZealandNationalHealthIndexNumber;
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
