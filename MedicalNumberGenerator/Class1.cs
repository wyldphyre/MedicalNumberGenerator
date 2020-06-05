using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalNumberGenerator
{
    class PatientIdentifierValueGenerator : Object
    {
        #region private

        private PatientIdentifierStyle style;
        private string value = "";

        #endregion

        #region public

        public void Execute()
        {
            switch (Style)
            {
                case PatientIdentifierStyle.AustralianDepartmentOfVeteransAffairsFileNumber:
                    break;
                default:
                    break;                
            }
        }

        #endregion

        #region properties

        public string Value
        {
            get { return value; }
        }

        #endregion
    }
}
