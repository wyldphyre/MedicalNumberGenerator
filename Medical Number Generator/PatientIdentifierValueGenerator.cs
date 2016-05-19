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

        // for internal use
        private List<char> numericCharacterList = new List<char>();
        private List<string> warCodesList = new List<string>();
        private PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary veteransAffairsLibrary = new PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary();

        public VeteransAffairsPatientIdentifierValueGenerator()
        {
            foreach (KeyValuePair<string, string> kvp in veteransAffairsLibrary.WarCodeNameDictionary)
            {
                warCodesList.Add(kvp.Key);
            }

            numericCharacterList.Add('1');
            numericCharacterList.Add('2');
            numericCharacterList.Add('3');
            numericCharacterList.Add('4');
            numericCharacterList.Add('5');
            numericCharacterList.Add('6');
            numericCharacterList.Add('7');
            numericCharacterList.Add('8');
            numericCharacterList.Add('9');
            numericCharacterList.Add('0');
        }

        public void Execute()
        {
            // To Do: generate a value using random data from the library, and random data for
            // the componet that doesn't come from the library

            StringBuilder valueBuilder = new StringBuilder("");
            const string MaskFormat = "LAAAAAAAA";
            int remainingCharacterCount = MaskFormat.Length;

            value = "";

            Random rnd = new Random((int)DateTime.Now.Ticks);

            valueBuilder.Append(veteransAffairsLibrary.StateCodeCharacterList[rnd.Next(veteransAffairsLibrary.StateCodeCharacterList.Count)]);
            valueBuilder.Append(warCodesList[rnd.Next(veteransAffairsLibrary.WarCodeNameDictionary.Count)]);
            remainingCharacterCount -= valueBuilder.Length;

            for (int i = 1; i <= remainingCharacterCount; i++)
            {
                valueBuilder.Append(numericCharacterList[rnd.Next(numericCharacterList.Count)]);
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
