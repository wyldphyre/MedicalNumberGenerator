using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MedicalNumber.Library
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
    // for internal use
    private readonly Dictionary<PatientIdentifierStyle, string> nameByStyleDictionary = new Dictionary<PatientIdentifierStyle, string>();
    private readonly Dictionary<PatientIdentifierStyle, string> maskFormatByStyleDictionary = new Dictionary<PatientIdentifierStyle, string>();
    private readonly Dictionary<string, PatientIdentifierStyle> styleByNameDictionary = new Dictionary<string, PatientIdentifierStyle>();

    public PatientIdentifierStyleHelper(PatientIdentifierDefinition[] patientIdentifierDefinitionList)
    {
      this.PatientIdentifierDefinitionList = patientIdentifierDefinitionList;

      foreach (var definition in patientIdentifierDefinitionList)
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

    public PatientIdentifierDefinition[] PatientIdentifierDefinitionList { get; }
  }

  public static class PatientIdentifierStyleVeteransAffairsFileNumberComponentLibrary
  {
    static PatientIdentifierStyleVeteransAffairsFileNumberComponentLibrary()
    {
      WarCodeNameDictionary = new Dictionary<string, string>
      {
        {"", "Australian Forces 1914"},
        {"X", "Australian Forces 1939"},
        {"KM", "Korea Malaya"},
        {"SR", "Far East Strategic Reserve"},
        {"SS", "Special Overseas Act"},
        {"SM", "Serving Members"},
        {"SWP", "Seamans War Pension 1939"},
        {"AGX", "Act of Grace 1939"},
        {"BW", "Boer War"},
        {"GW", "Gulf War Australian"},
        {"CGW", "Gulf War British Commonwealth"},
        {"P", "British Pension 1914"},
        {"PX", "British Pension 1939"},
        {"AD", "British Admiralty"},
        {"PAM", "British Air Ministry"},
        {"PCA", "Government and Admin"},
        {"PCR", "British Service Department CRO"},
        {"PCV", "British Civilians"},
        {"PMS", "British Merchant Seamen 1914"},
        {"PSW", "British Merchant Seamen 1939"},
        {"PWO", "British War Office"},
        {"HKX", "Hong Kong 1939"},
        {"MAL", "Malaysia"},
        {"N", "New Zealand 1914"},
        {"NX", "New Zealand 1939"},
        {"NSW", "New Zealand Merchant Navy"},
        {"CN", "Canada 1914"},
        {"CNX", "Canada 1939"},
        {"IV", "Indigenous Veterans PNG"},
        {"NF", "Newfoundland"},
        {"NG", "New Guinea Civilians"},
        {"RD", "Southern Rhodesia 1914"},
        {"RDX", "Southern Rhodesia 1939"},
        {"SA", "South Africa 1914"},
        {"SAX", "South Africa 1939"},
        {"A", "Allied Forces"},
        {"BUR", "Burma"},
        {"CNK", "Canada Korea"},
        {"CNS", "Canada Special Forces"},
        {"FIJ", "Fiji"},
        {"GHA", "Ghana"},
        {"HKS", "Hong Kong"},
        {"IND", "India"},
        {"KYA", "Kenya"},
        {"MAU", "Mauritius"},
        {"MLS", "Malaysia Singapore"},
        {"MTX", "Malta"},
        {"MWI", "Malawi"},
        {"NK", "New Zealand Korea"},
        {"NGR", "Nigeria"},
        {"NRD", "Northern Rhodesia"},
        {"NSS", "New Zealand Special Overseas"},
        {"PK", "British Korea Malaya"},
        {"SL", "Sierra Leone"},
        {"SUD", "Sudan"},
        {"TZA", "Tanzania"}
      };
    }

    public static readonly char[] StateCodeCharacters = new[] { 'N', 'V', 'Q', 'S', 'W', 'T' };

    public static readonly Dictionary<string, string> WarCodeNameDictionary;
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
