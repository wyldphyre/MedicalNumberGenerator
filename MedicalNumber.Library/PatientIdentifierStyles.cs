using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MedicalNumber
{
  public enum PatientIdentifierStyle
  {
    AustralianMedicareNumber,
    AustralianDepartmentOfVeteransAffairsFileNumber,
    WesternAustralianUnitMedicalRecordNumber,
    NewZealandNationalHealthIndexNumber
  }

  public class PatientIdentifierStyleHelper
  {
    private readonly PatientIdentifierDefinition[] patientIdentifierDefinitionList;

    // for internal use
    private readonly Dictionary<PatientIdentifierStyle, string> nameByStyleDictionary = new Dictionary<PatientIdentifierStyle, string>();
    private readonly Dictionary<PatientIdentifierStyle, string> maskFormatByStyleDictionary = new Dictionary<PatientIdentifierStyle, string>();
    private readonly Dictionary<string, PatientIdentifierStyle> styleByNameDictionary = new Dictionary<string, PatientIdentifierStyle>();

    public PatientIdentifierStyleHelper(PatientIdentifierDefinition[] PatientIdentifierDefinitionList)
    {
      patientIdentifierDefinitionList = PatientIdentifierDefinitionList;

      foreach (var definition in PatientIdentifierDefinitionList)
      {
        nameByStyleDictionary.Add(definition.Style, definition.Name);
        maskFormatByStyleDictionary.Add(definition.Style, definition.MaskFormat);
        styleByNameDictionary.Add(definition.Name, definition.Style);
      }
    }

    public string GetPatientIdentifierStyleName(PatientIdentifierStyle style)
    {
      Debug.Assert(nameByStyleDictionary.ContainsKey(style), $"Style \"{style.ToString()}\" is not a known patient identifier style.");

      nameByStyleDictionary.TryGetValue(style, out string styleName);

      return styleName;
    }

    public PatientIdentifierStyle GetPatientIdentifierStyleByName(string name)
    {
      Debug.Assert(styleByNameDictionary.ContainsKey(name), $"Name \"{name}\" is not a known patient identifier style name.");

      styleByNameDictionary.TryGetValue(name, out PatientIdentifierStyle style);

      return style;
    }

    public string GetMaskFormatByPatientIdentifierStyle(PatientIdentifierStyle style)
    {
      Debug.Assert(maskFormatByStyleDictionary.ContainsKey(style), $"Style \"{style.ToString()}\" is not a known patient identifier style.");

      maskFormatByStyleDictionary.TryGetValue(style, out string maskFormat);

      return maskFormat;
    }

    public PatientIdentifierDefinition GetPatientIdentifierDefinitionByStyle(PatientIdentifierStyle style)
    {
      return PatientIdentifierDefinitionList.FirstOrDefault(Definition => Definition.Style == style);
    }

    public PatientIdentifierDefinition[] PatientIdentifierDefinitionList => patientIdentifierDefinitionList;
  }

  public class PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary
  {
    public PatientIdenitiferStyleVeteransAffairsFileNumberComponentLibrary()
    {
      WarCodeNameDictionary.Add("", "Australian Forces 1914");
      WarCodeNameDictionary.Add("X", "Australian Forces 1939");
      WarCodeNameDictionary.Add("KM", "Korea Malaya");
      WarCodeNameDictionary.Add("SR", "Far East Strategic Reserve");
      WarCodeNameDictionary.Add("SS", "Special Overseas Act");
      WarCodeNameDictionary.Add("SM", "Serving Members");
      WarCodeNameDictionary.Add("SWP", "Seamans War Pension 1939");
      WarCodeNameDictionary.Add("AGX", "Act of Grace 1939");
      WarCodeNameDictionary.Add("BW", "Boer War");
      WarCodeNameDictionary.Add("GW", "Gulf War Australian");
      WarCodeNameDictionary.Add("CGW", "Gulf War British Commonwealth");
      WarCodeNameDictionary.Add("P", "British Pension 1914");
      WarCodeNameDictionary.Add("PX", "British Pension 1939");
      WarCodeNameDictionary.Add("AD", "British Admiralty");
      WarCodeNameDictionary.Add("PAM", "British Air Ministry");
      WarCodeNameDictionary.Add("PCA", "Government and Admin");
      WarCodeNameDictionary.Add("PCR", "British Service Department CRO");
      WarCodeNameDictionary.Add("PCV", "British Civilians");
      WarCodeNameDictionary.Add("PMS", "British Merchant Seamen 1914");
      WarCodeNameDictionary.Add("PSW", "British Merchant Seamen 1939");
      WarCodeNameDictionary.Add("PWO", "British War Office");
      WarCodeNameDictionary.Add("HKX", "Hong Kong 1939");
      WarCodeNameDictionary.Add("MAL", "Malaysia");
      WarCodeNameDictionary.Add("N", "New Zealand 1914");
      WarCodeNameDictionary.Add("NX", "New Zealand 1939");
      WarCodeNameDictionary.Add("NSW", "New Zealand Merchant Navy");
      WarCodeNameDictionary.Add("CN", "Canada 1914");
      WarCodeNameDictionary.Add("CNX", "Canada 1939");
      WarCodeNameDictionary.Add("IV", "Indigenous Veterans PNG");
      WarCodeNameDictionary.Add("NF", "Newfoundland");
      WarCodeNameDictionary.Add("NG", "New Guinea Civilians");
      WarCodeNameDictionary.Add("RD", "Southern Rhodesia 1914");
      WarCodeNameDictionary.Add("RDX", "Southern Rhodesia 1939");
      WarCodeNameDictionary.Add("SA", "South Africa 1914");
      WarCodeNameDictionary.Add("SAX", "South Africa 1939");
      WarCodeNameDictionary.Add("A", "Allied Forces");
      WarCodeNameDictionary.Add("BUR", "Burma");
      WarCodeNameDictionary.Add("CNK", "Canada Korea");
      WarCodeNameDictionary.Add("CNS", "Canada Special Forces");
      WarCodeNameDictionary.Add("FIJ", "Fiji");
      WarCodeNameDictionary.Add("GHA", "Ghana");
      WarCodeNameDictionary.Add("HKS", "Hong Kong");
      WarCodeNameDictionary.Add("IND", "India");
      WarCodeNameDictionary.Add("KYA", "Kenya");
      WarCodeNameDictionary.Add("MAU", "Mauritius");
      WarCodeNameDictionary.Add("MLS", "Malaysia Singapore");
      WarCodeNameDictionary.Add("MTX", "Malta");
      WarCodeNameDictionary.Add("MWI", "Malawi");
      WarCodeNameDictionary.Add("NK", "New Zealand Korea");
      WarCodeNameDictionary.Add("NGR", "Nigeria");
      WarCodeNameDictionary.Add("NRD", "Northern Rhodesia");
      WarCodeNameDictionary.Add("NSS", "New Zealand Special Overseas");
      WarCodeNameDictionary.Add("PK", "British Korea Malaya");
      WarCodeNameDictionary.Add("SL", "Sierra Leone");
      WarCodeNameDictionary.Add("SUD", "Sudan");
      WarCodeNameDictionary.Add("TZA", "Tanzania");
    }

    public char[] StateCodeCharacters { get; } = new[] { 'N', 'V', 'Q', 'S', 'W', 'T' };

    public Dictionary<string, string> WarCodeNameDictionary { get; } = new Dictionary<string, string>();
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
