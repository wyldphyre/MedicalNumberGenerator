Unit PatientIdentifierValidationEngines;


{! 9 !}


Interface


Uses
  StringSupport, MathSupport,
  AdvObjects, AdvCodeMasks, AdvStringLists, AdvStringMatches, AdvStringIntegerMatches,
  PatientIdentifierDefinitionEntities, PatientIdentifierStyles;


Type
  TPatientIdentifierValidationEngine = Class(TAdvObject)
    Private
      FValue : String;
      FDefinitionEntity : TPatientIdentifierDefinitionEntity;

      FCodeMask : TAdvCodeMask;
      FIssueList : TAdvStringList;
      FWarCodeNameStringMatch : TAdvStringMatch;
      FNewZealandNationalHealthIndexAlphabetIntegerMatch : TAdvStringIntegerMatch;

      Function GetDefinitionEntity : TPatientIdentifierDefinitionEntity;
      Procedure SetDefinitionEntity(Const Value : TPatientIdentifierDefinitionEntity);

      Procedure ValidateMask;

    Public
      Constructor Create; Override;
      Destructor Destroy; Override;

      Procedure Execute;

      Procedure ProduceDefinitionEntity;

      Property Value : String Read FValue Write FValue;
      Property DefinitionEntity : TPatientIdentifierDefinitionEntity Read GetDefinitionEntity Write SetDefinitionEntity;
      Property IssueList : TAdvStringList Read FIssueList;
  End;


Implementation


Constructor TPatientIdentifierValidationEngine.Create;
Begin
  Inherited;

  FWarCodeNameStringMatch := TAdvStringMatch.Create;
  FWarCodeNameStringMatch.Add('', 'Australian Forces 1914');
  FWarCodeNameStringMatch.Add('X', 'Australian Forces 1939');
  FWarCodeNameStringMatch.Add('KM', 'Korea Malaya');
  FWarCodeNameStringMatch.Add('SR', 'Far East Strategic Reserve');
  FWarCodeNameStringMatch.Add('SS', 'Special Overseas Act');
  FWarCodeNameStringMatch.Add('SM', 'Serving Members');
  FWarCodeNameStringMatch.Add('SWP', 'Seamans War Pension 1939');
  FWarCodeNameStringMatch.Add('AGX', 'Act of Grace 1939');
  FWarCodeNameStringMatch.Add('BW', 'Boer War');
  FWarCodeNameStringMatch.Add('GW', 'Gulf War Australian');
  FWarCodeNameStringMatch.Add('CGW', 'Gulf War British Commonwealth');
  FWarCodeNameStringMatch.Add('P', 'British Pension 1914');
  FWarCodeNameStringMatch.Add('PX', 'British Pension 1939');
  FWarCodeNameStringMatch.Add('AD', 'British Admiralty');
  FWarCodeNameStringMatch.Add('PAM', 'British Air Ministry');
  FWarCodeNameStringMatch.Add('PCA', 'Government and Admin');
  FWarCodeNameStringMatch.Add('PCR', 'British Service Department CRO');
  FWarCodeNameStringMatch.Add('PCV', 'British Civilians');
  FWarCodeNameStringMatch.Add('PMS', 'British Merchant Seamen 1914');
  FWarCodeNameStringMatch.Add('PSW', 'British Merchant Seamen 1939');
  FWarCodeNameStringMatch.Add('PWO', 'British War Office');
  FWarCodeNameStringMatch.Add('HKX', 'Hong Kong 1939');
  FWarCodeNameStringMatch.Add('MAL', 'Malaysia');
  FWarCodeNameStringMatch.Add('N', 'New Zealand 1914');
  FWarCodeNameStringMatch.Add('NX', 'New Zealand 1939');
  FWarCodeNameStringMatch.Add('NSW', 'New Zealand Merchant Navy');
  FWarCodeNameStringMatch.Add('CN', 'Canada 1914');
  FWarCodeNameStringMatch.Add('CNX', 'Canada 1939');
  FWarCodeNameStringMatch.Add('IV', 'Indigenous Veterans PNG');
  FWarCodeNameStringMatch.Add('NF', 'Newfoundland');
  FWarCodeNameStringMatch.Add('NG', 'New Guinea Civilians');
  FWarCodeNameStringMatch.Add('RD', 'Southern Rhodesia 1914');
  FWarCodeNameStringMatch.Add('RDX', 'Southern Rhodesia 1939');
  FWarCodeNameStringMatch.Add('SA', 'South Africa 1914');
  FWarCodeNameStringMatch.Add('SAX', 'South Africa 1939');
  FWarCodeNameStringMatch.Add('A', 'Allied Forces');
  FWarCodeNameStringMatch.Add('BUR', 'Burma');
  FWarCodeNameStringMatch.Add('CNK', 'Canada Korea');
  FWarCodeNameStringMatch.Add('CNS', 'Canada Special Forces');
  FWarCodeNameStringMatch.Add('FIJ', 'Fiji');
  FWarCodeNameStringMatch.Add('GHA', 'Ghana');
  FWarCodeNameStringMatch.Add('HKS', 'Hong Kong');
  FWarCodeNameStringMatch.Add('IND', 'India');
  FWarCodeNameStringMatch.Add('KYA', 'Kenya');
  FWarCodeNameStringMatch.Add('MAU', 'Mauritius');
  FWarCodeNameStringMatch.Add('MLS', 'Malaysia Singapore');
  FWarCodeNameStringMatch.Add('MTX', 'Malta');
  FWarCodeNameStringMatch.Add('MWI', 'Malawi');
  FWarCodeNameStringMatch.Add('NK', 'New Zealand Korea');
  FWarCodeNameStringMatch.Add('NGR', 'Nigeria');
  FWarCodeNameStringMatch.Add('NRD', 'Northern Rhodesia');
  FWarCodeNameStringMatch.Add('NSS', 'New Zealand Special Overseas');
  FWarCodeNameStringMatch.Add('PK', 'British Korea Malaya');
  FWarCodeNameStringMatch.Add('SL', 'Sierra Leone');
  FWarCodeNameStringMatch.Add('SUD', 'Sudan');
  FWarCodeNameStringMatch.Add('TZA', 'Tanzania');
  FWarCodeNameStringMatch.Sorted;

  FNewZealandNationalHealthIndexAlphabetIntegerMatch := TAdvStringIntegerMatch.Create;
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('A', 1);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('B', 2);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('C', 3);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('D', 4);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('E', 5);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('F', 6);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('G', 7);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('H', 8);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('J', 9);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('K', 10);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('L', 11);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('M', 12);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('N', 13);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('P', 14);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('Q', 15);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('R', 16);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('S', 17);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('T', 18);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('U', 19);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('V', 20);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('W', 21);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('X', 22);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('Y', 23);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Add('Z', 24);
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Sorted;


  FCodeMask := TAdvCodeMask.Create;

  FIssueList := TAdvStringList.Create;
  FIssueList.Symbol := cReturn;

  FDefinitionEntity := Nil;
End;


Destructor TPatientIdentifierValidationEngine.Destroy;
Begin
  FNewZealandNationalHealthIndexAlphabetIntegerMatch.Free;
  FWarCodeNameStringMatch.Free;
  FDefinitionEntity.Free;
  FIssueList.Free;
  FCodeMask.Free;

  Inherited;
End;


Procedure TPatientIdentifierValidationEngine.ProduceDefinitionEntity;
Begin
  Assert(Condition(Not Assigned(FDefinitionEntity), 'ProduceDefinitionEntity', 'FDefinitionEntity is already assigned.'));

  FDefinitionEntity := TPatientIdentifierDefinitionEntity.Create;
End;


Procedure TPatientIdentifierValidationEngine.Execute;
Const
  VeteranAffairsValidStateCodeCharacterSet = ['N', 'V', 'Q', 'S', 'W', 'T'];
Var
  iChecksumKey : Integer;
  iVeteranWorkingIndex : Integer;
  iCharacterIndex : Integer;

  sChecksumValue : String;
  sLocalValue : String;
  sVeteranState : Char;
  sVeteranWarCode : String;
  sVeteranNumber : String;
  sVeteranWorking : String;
Begin
  FIssueList.Clear;

  FCodeMask.Format := DefinitionEntity.MaskFormat;
  FCodeMask.MinLength := DefinitionEntity.MaskMinimumLength;
  FCodeMask.MaxLength := DefinitionEntity.MaskMaximumLength;

  If Not DefinitionEntity.HasStyle Or DefinitionEntity.IsStyleInternalPatientIdentifier Then
  Begin
    ValidateMask;
  End
  Else
  Begin
    Case DefinitionEntity.Style Of
      PatientIdentifierStyleAustralianMedicareNumber :
      Begin
        If FValue <> '' Then
        Begin
          sLocalValue := StringKeep(FValue, setNumbers);

          If Not IntegerBetween(10, Length(sLocalValue), 11) Then
          Begin
            FIssueList.Add('Valid Medicare numbers must contain between 10 and 11 numerals.');
          End
          Else
          Begin
            If Not IntegerBetween(2, Ord(sLocalValue[1]) - Ord('0'), 6) Then
            Begin
              FIssueList.Add('The first digit of a Medicare number must be in the range [2..6].');
            End
            Else
            Begin
              If Ord(sLocalValue[9]) - Ord('0') <> SignedMod(
               ((Ord(sLocalValue[1]) - Ord('0'))    ) +
               ((Ord(sLocalValue[2]) - Ord('0')) * 3) +
               ((Ord(sLocalValue[3]) - Ord('0')) * 7) +
               ((Ord(sLocalValue[4]) - Ord('0')) * 9) +
               ((Ord(sLocalValue[5]) - Ord('0'))    ) +
               ((Ord(sLocalValue[6]) - Ord('0')) * 3) +
               ((Ord(sLocalValue[7]) - Ord('0')) * 7) +
               ((Ord(sLocalValue[8]) - Ord('0')) * 9), 10) Then
              Begin
                FIssueList.Add('Invalid Australian Medicare number.');
              End;
            End;
          End;
        End;
      End;

      PatientIdentifierStyleAustralianDepartmentOfVeteransAffairsFileNumber :
      Begin
        If Length(FValue) = 0 Then
        Begin
          FIssueList.Add('Veteran''s Affairs numbers cannot be blank.');
        End
        Else
        Begin
          If Not StringIsAlphaNumeric(FValue) Then
          Begin
            FIssueList.Add('Veteran''s Affairs numbers must be alphanumeric.');
          End
          Else
          Begin
            If Length(FValue) < 4 Then
            Begin
              FIssueList.Add('Veteran''s Affairs numbers must be at least 4 characters in length.');
            End
            Else
            Begin
              sVeteranWorking := StringUpper(FValue);
              sVeteranState := StringGet(sVeteranWorking, 1);

              If Not (sVeteranState In setAlphabet) Then
              Begin
                FIssueList.Add('Veteran''s Affairs numbers must be start with a letter.');
              End
              Else
              Begin
                iVeteranWorkingIndex := 2;

                While (iVeteranWorkingIndex <= Length(sVeteranWorking)) And (StringGet(sVeteranWorking, iVeteranWorkingIndex) In setAlphabet) Do
                Begin
                  sVeteranWarCode := sVeteranWarCode + StringGet(sVeteranWorking, iVeteranWorkingIndex);

                  Inc(iVeteranWorkingIndex);
                End;

                sVeteranNumber := Copy(sVeteranWorking, iVeteranWorkingIndex, Length(sVeteranWorking));

                If Length(sVeteranNumber) <= 0 Then
                Begin
                  FIssueList.Add('Veteran''s Affairs numbers must end with a number.');
                End
                Else
                Begin
                  If StringIsAlphabetic(Copy(sVeteranNumber, Length(sVeteranNumber), 1)) Then  // strip off the dependency suffix if it exists
                    sVeteranNumber := Copy(sVeteranNumber, 1, Length(sVeteranNumber) - 1);

                  If Not StringIsInteger(sVeteranNumber) Then
                    FIssueList.Add(sVeteranNumber + ' is not a valid number.')
                  Else If Not (sVeteranState In VeteranAffairsValidStateCodeCharacterSet) Then
                    FIssueList.Add(sVeteranState + ' is not a valid Veteran''s Affairs state code.')
                  Else If Not FWarCodeNameStringMatch.ExistsByKey(sVeteranWarCode) Then
                    FIssueList.Add(sVeteranWarCode + ' is not a valid Veteran''s Affairs war code.')
                  Else If Length(sVeteranNumber) > 6 Then
                    FIssueList.Add('The numeric portion of a Veteran''s Affairs number cannot exceed 6 characters.');
                End;
              End;
            End;
          End;
        End;
      End;

      PatientIdentifierStyleWesternAustralianUnitMedicalRecordNumber :
      Begin
        If Length(FValue) <> 8 Then
        Begin
          FIssueList.Add('WA Unit Medical Record numbers must be exactly 8 characters long.');
        End
        Else
        Begin
          sChecksumValue := Copy(FValue, 2, 7);

          If Not StringIsInteger(sChecksumValue) Then
          Begin
            FIssueList.Add('The last 7 characters of WA Unit Medical Record numbers must be numeric.');
          End
          Else
          Begin
            // This used to just be StringToInteger(FValue), but what if FValue[1] is a non-numeric character?

            iChecksumKey := SignedMod(StringToInteger(sChecksumValue), 11);

            If IntegerBetween(iChecksumKey, 0, 7) Then
              Inc(iChecksumKey);

            If Not StringEquals(Chr(65 + iChecksumKey), StringGet(FValue, 1)) Then
              FIssueList.Add('This WA Unit Medical Record number has an invalid first letter.');
          End;
        End;
      End;

      PatientIdentifierStyleNewZealandNationalHealthIndexNumber :
      Begin
        If FValue <> '' Then
        Begin
          sLocalValue := FValue;

          If Length(sLocalValue) <> 7 Then
          Begin
            FIssueList.Add('Valid New Zealand National Health Index numbers must have a length of 7 characters.');
          End
          Else
          Begin
            For iCharacterIndex := 1 To 3 Do
            Begin
              If (Not (sLocalValue[iCharacterIndex] In setUppercase)) Or (sLocalValue[iCharacterIndex] = 'I') Or (sLocalValue[iCharacterIndex] = 'O') Then
                FIssueList.Add(StringFormat('The %s character "%s" must be an uppercase letter other than "I" and "O"', [RankToString(iCharacterIndex), sLocalValue[iCharacterIndex]]));
            End;

            For iCharacterIndex := 4 To 7 Do
            Begin
              If Not StringIsNumeric(sLocalValue[iCharacterIndex]) Then
                FIssueList.Add(StringFormat('The %s character "%s" must be a number in the range [0..9]', [RankToString(iCharacterIndex), sLocalValue[iCharacterIndex]]));
            End;

            If FIssueList.IsEmpty Then
            Begin
              iCheckSumKey :=
                ((FNewZealandNationalHealthIndexAlphabetIntegerMatch.GetValueByKey(sLocalValue[1]) * 7) +
                 (FNewZealandNationalHealthIndexAlphabetIntegerMatch.GetValueByKey(sLocalValue[2]) * 6) +
                 (FNewZealandNationalHealthIndexAlphabetIntegerMatch.GetValueByKey(sLocalValue[3]) * 5) +
                 (StringToInteger(sLocalValue[4]) * 4) +
                 (StringToInteger(sLocalValue[5]) * 3) +
                 (StringToInteger(sLocalValue[6]) * 2)) Mod 11;

              If iCheckSumKey = 0 Then
              Begin
                FIssueList.Add('Cannot calculate a valid check sum because the New Zealand National Health Index number is incorrect.');
              End
              Else
              Begin
                iCheckSumKey := 11 - iCheckSumKey;

                If iCheckSumkey = 10 Then
                  iCheckSumKey := 0;

                sCheckSumValue := IntegerToString(iCheckSumKey);

                If sCheckSumValue <> sLocalValue[7] Then
                  FIssueList.Add(StringFormat('Checksum digit "%s" is incorrect', [sLocalValue[7]]));
              End;
            End;
          End;
        End;
      End;
    Else
      Error('Execute', 'Unhandled patient identifier style.');
    End;
  End;
End;


Procedure TPatientIdentifierValidationEngine.ValidateMask;
Begin
  If Length(FValue) < FCodeMask.MinLength Then
  Begin
    FIssueList.Add('Identifier must be at least ''' + IntegerToString(FCodeMask.MinLength) + ''' characters long.');
  End
  Else If StringEquals(FValue, FCodeMask.LeadingFixedText) Then
  Begin
    FIssueList.Add('Identifier must be longer than the leading fixed length of the identifier type mask.');
  End
  Else
  Begin
    If (FCodeMask.MaxLength > 0) And (Length(FValue) > FCodeMask.MaxLength) Then
    Begin
      FIssueList.Add('Identifier must be no more than ''' + IntegerToString(FCodeMask.MaxLength) + ''' characters long.');
    End
    Else
    Begin
      If Not FCodeMask.Conforms(FValue) Then
      Begin
        FIssueList.Add('Identifier ''' + FValue + ''' does not conform to the required mask (' + FCodeMask.Format + ').');
      End;
    End;
  End;
End;


Function TPatientIdentifierValidationEngine.GetDefinitionEntity : TPatientIdentifierDefinitionEntity;
Begin
  Assert(Invariants('GetDefinitionEntity', FDefinitionEntity, TPatientIdentifierDefinitionEntity, 'FDefinitionEntity'));

  Result := FDefinitionEntity;
End;


Procedure TPatientIdentifierValidationEngine.SetDefinitionEntity(Const Value : TPatientIdentifierDefinitionEntity);
Begin
  Assert(Invariants('SetDefinitionEntity', Value, TPatientIdentifierDefinitionEntity, 'Value'));

  FDefinitionEntity.Free;
  FDefinitionEntity := Value;
End;


End. // PatientIdentifierValidationEngines //
