using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MedicalNumberGenerator
{
  public enum PatientIdentifierStyle
  {
    AustralianMedicareNumber,
    AustralianDepartmentOfVeteransAffairsFileNumber,
    WesternAustralianUnitMedicalRecordNumber,
    NewZealandNationalHealthIndexNumber
  }

  public class PatientIdentifierDefinitionStyleComparer : IComparer<PatientIdentifierDefinition>
  {
    public int Compare(PatientIdentifierDefinition definitionA, PatientIdentifierDefinition definitionB)
    {
      if (definitionA == null)
      {
        if (definitionB == null)
        {
          return 0;
        }
        else
        {
          // If x is null and y is not null, y
          // is greater.
          return -1;
        }
      }
      else
      {
        if (definitionB == null)
        {
          // definitionA is greater
          return 1;
        }
        else
        {
          // neither object is null, so compare their styles
          return definitionA.Style.CompareTo(definitionB.Style);
        }
      }
    }
  }

  public class PatientIdentifierStyleHelper : Object
  {
    #region private

    // Provided by user of the class
    private List<PatientIdentifierDefinition> patientIdentifierDefinitionList = null;

    // for internal use
    private Dictionary<PatientIdentifierStyle, string> nameByStyleDictionary = new Dictionary<PatientIdentifierStyle, string>();
    private Dictionary<PatientIdentifierStyle, string> maskFormatByStyleDictionary = new Dictionary<PatientIdentifierStyle, string>();
    private Dictionary<string, PatientIdentifierStyle> styleByNameDictionary = new Dictionary<string, PatientIdentifierStyle>();

    #endregion

    #region public

    public void Prepare()
    {
      foreach (PatientIdentifierDefinition definition in PatientIdentifierDefinitionList)
      {
        nameByStyleDictionary.Add(definition.Style, definition.Name);
        maskFormatByStyleDictionary.Add(definition.Style, definition.MaskFormat);
        styleByNameDictionary.Add(definition.Name, definition.Style);
      }
    }

    public string GetPatientIdentifierStyleName(PatientIdentifierStyle style)
    {
      Debug.Assert(nameByStyleDictionary.ContainsKey(style), String.Format("Style \"{0}\" is not a known patient identifier style.", style.ToString()));

      string styleName;

      nameByStyleDictionary.TryGetValue(style, out styleName);

      return styleName;
    }

    public PatientIdentifierStyle GetPatientIdentifierStyleByName(string name)
    {
      Debug.Assert(styleByNameDictionary.ContainsKey(name), String.Format("Name \"{0}\" is not a known patient identifier style name.", name));

      PatientIdentifierStyle style;

      styleByNameDictionary.TryGetValue(name, out style);

      return style;
    }

    public string GetMaskFormatByPatientIdentifierStyle(PatientIdentifierStyle style)
    {
      Debug.Assert(maskFormatByStyleDictionary.ContainsKey(style), String.Format("Style \"{0}\" is not a known patient identifier style.", style.ToString()));

      string maskFormat;

      maskFormatByStyleDictionary.TryGetValue(style, out maskFormat);

      return maskFormat;
    }

    public PatientIdentifierDefinition GetPatientIdentifierDefinitionByStyle(PatientIdentifierStyle style)
    {
      PatientIdentifierDefinition definition = new PatientIdentifierDefinition();
      definition.Style = style;

      int index = PatientIdentifierDefinitionList.BinarySearch(definition, new PatientIdentifierDefinitionStyleComparer());

      if (index >= 0)
      {
        return PatientIdentifierDefinitionList[index];
      }
      else
      {
        return null;
      }
    }

    #endregion

    #region public properties

    public List<PatientIdentifierDefinition> PatientIdentifierDefinitionList
    {
      get
      {
        Debug.Assert(patientIdentifierDefinitionList != null, "definitionList is null. Must be assigned before calling Build.");

        return patientIdentifierDefinitionList;
      }
      set
      {
        Debug.Assert(value != null, "Cannot assign a null value to DefinitionList.");

        patientIdentifierDefinitionList = value;
      }
    }

    #endregion
  }

  public class PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary
  {
    private List<char> stateCodeCharacterList = new List<char>();
    private Dictionary<string, string> warCodeNameDictionary = new Dictionary<string, string>();

    public PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary()
    {
      warCodeNameDictionary.Add("", "Australian Forces 1914");
      warCodeNameDictionary.Add("X", "Australian Forces 1939");
      warCodeNameDictionary.Add("KM", "Korea Malaya");
      warCodeNameDictionary.Add("SR", "Far East Strategic Reserve");
      warCodeNameDictionary.Add("SS", "Special Overseas Act");
      warCodeNameDictionary.Add("SM", "Serving Members");
      warCodeNameDictionary.Add("SWP", "Seamans War Pension 1939");
      warCodeNameDictionary.Add("AGX", "Act of Grace 1939");
      warCodeNameDictionary.Add("BW", "Boer War");
      warCodeNameDictionary.Add("GW", "Gulf War Australian");
      warCodeNameDictionary.Add("CGW", "Gulf War British Commonwealth");
      warCodeNameDictionary.Add("P", "British Pension 1914");
      warCodeNameDictionary.Add("PX", "British Pension 1939");
      warCodeNameDictionary.Add("AD", "British Admiralty");
      warCodeNameDictionary.Add("PAM", "British Air Ministry");
      warCodeNameDictionary.Add("PCA", "Government and Admin");
      warCodeNameDictionary.Add("PCR", "British Service Department CRO");
      warCodeNameDictionary.Add("PCV", "British Civilians");
      warCodeNameDictionary.Add("PMS", "British Merchant Seamen 1914");
      warCodeNameDictionary.Add("PSW", "British Merchant Seamen 1939");
      warCodeNameDictionary.Add("PWO", "British War Office");
      warCodeNameDictionary.Add("HKX", "Hong Kong 1939");
      warCodeNameDictionary.Add("MAL", "Malaysia");
      warCodeNameDictionary.Add("N", "New Zealand 1914");
      warCodeNameDictionary.Add("NX", "New Zealand 1939");
      warCodeNameDictionary.Add("NSW", "New Zealand Merchant Navy");
      warCodeNameDictionary.Add("CN", "Canada 1914");
      warCodeNameDictionary.Add("CNX", "Canada 1939");
      warCodeNameDictionary.Add("IV", "Indigenous Veterans PNG");
      warCodeNameDictionary.Add("NF", "Newfoundland");
      warCodeNameDictionary.Add("NG", "New Guinea Civilians");
      warCodeNameDictionary.Add("RD", "Southern Rhodesia 1914");
      warCodeNameDictionary.Add("RDX", "Southern Rhodesia 1939");
      warCodeNameDictionary.Add("SA", "South Africa 1914");
      warCodeNameDictionary.Add("SAX", "South Africa 1939");
      warCodeNameDictionary.Add("A", "Allied Forces");
      warCodeNameDictionary.Add("BUR", "Burma");
      warCodeNameDictionary.Add("CNK", "Canada Korea");
      warCodeNameDictionary.Add("CNS", "Canada Special Forces");
      warCodeNameDictionary.Add("FIJ", "Fiji");
      warCodeNameDictionary.Add("GHA", "Ghana");
      warCodeNameDictionary.Add("HKS", "Hong Kong");
      warCodeNameDictionary.Add("IND", "India");
      warCodeNameDictionary.Add("KYA", "Kenya");
      warCodeNameDictionary.Add("MAU", "Mauritius");
      warCodeNameDictionary.Add("MLS", "Malaysia Singapore");
      warCodeNameDictionary.Add("MTX", "Malta");
      warCodeNameDictionary.Add("MWI", "Malawi");
      warCodeNameDictionary.Add("NK", "New Zealand Korea");
      warCodeNameDictionary.Add("NGR", "Nigeria");
      warCodeNameDictionary.Add("NRD", "Northern Rhodesia");
      warCodeNameDictionary.Add("NSS", "New Zealand Special Overseas");
      warCodeNameDictionary.Add("PK", "British Korea Malaya");
      warCodeNameDictionary.Add("SL", "Sierra Leone");
      warCodeNameDictionary.Add("SUD", "Sudan");
      warCodeNameDictionary.Add("TZA", "Tanzania");

      stateCodeCharacterList.Add('N');
      stateCodeCharacterList.Add('V');
      stateCodeCharacterList.Add('Q');
      stateCodeCharacterList.Add('S');
      stateCodeCharacterList.Add('W');
      stateCodeCharacterList.Add('T');
    }

    public List<char> StateCodeCharacterList
    {
      get { return stateCodeCharacterList; }
    }

    public Dictionary<string, string> WarCodeNameDictionary
    {
      get { return warCodeNameDictionary; }
    }
  }

  //public class PatientIdenitiferStyleNewZealandNationalHealthIndexComponentLibrary
  //{
  //    private Dictionary<char, int> alphabetIntegerDictionary = new Dictionary<char, int>();

  //    public PatientIdenitiferStyleNewZealandNationalHealthIndexComponentLibrary()
  //    {
  //        alphabetIntegerDictionary.Add('A', 1);
  //        alphabetIntegerDictionary.Add('B', 2);
  //        alphabetIntegerDictionary.Add('C', 3);
  //        alphabetIntegerDictionary.Add('D', 4);
  //        alphabetIntegerDictionary.Add('E', 5);
  //        alphabetIntegerDictionary.Add('F', 6);
  //        alphabetIntegerDictionary.Add('G', 7);
  //        alphabetIntegerDictionary.Add('H', 8);
  //        alphabetIntegerDictionary.Add('J', 9);
  //        alphabetIntegerDictionary.Add('K', 10);
  //        alphabetIntegerDictionary.Add('L', 11);
  //        alphabetIntegerDictionary.Add('M', 12);
  //        alphabetIntegerDictionary.Add('N', 13);
  //        alphabetIntegerDictionary.Add('P', 14);
  //        alphabetIntegerDictionary.Add('Q', 15);
  //        alphabetIntegerDictionary.Add('R', 16);
  //        alphabetIntegerDictionary.Add('S', 17);
  //        alphabetIntegerDictionary.Add('T', 18);
  //        alphabetIntegerDictionary.Add('U', 19);
  //        alphabetIntegerDictionary.Add('V', 20);
  //        alphabetIntegerDictionary.Add('W', 21);
  //        alphabetIntegerDictionary.Add('X', 22);
  //        alphabetIntegerDictionary.Add('Y', 23);
  //        alphabetIntegerDictionary.Add('Z', 24);
  //    }

  //    public Dictionary<char, int> AlphabetIntegerDictionary
  //    {
  //        get { return alphabetIntegerDictionary; }
  //    }
  //}
}
