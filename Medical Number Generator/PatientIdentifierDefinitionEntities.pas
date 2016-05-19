Unit PatientIdentifierDefinitionEntities;


{! 3 !}


Interface


Uses
  MathSupport, StringSupport,
  AdvPersistentLists,
  PatientIdentifierStyles;


Type
  TPatientIdentifierDefinitionEntity = Class(TAdvPersistent)
    Private
      FAllowDuplicates : Boolean;
      FMaskFormat : String;
      FMaskMinimumLength : Integer;
      FMaskMaximumLength : Integer;
      FHasStyle : Boolean;
      FStyle : TPatientIdentifierStyle;

      Function GetStyle : TPatientIdentifierStyle;
      Procedure SetStyle(Const Value : TPatientIdentifierStyle);

      Function GetHasStyle : Boolean;
      Procedure SetHasStyle(Const Value : Boolean);

    Public
      Function Link : TPatientIdentifierDefinitionEntity; Overload;
      Function Clone : TPatientIdentifierDefinitionEntity; Overload;

      Procedure Assign(oObject : TAdvObject); Override;
      Procedure Define(oFiler : TAdvFiler); Override;

      Function IsStyleInternalPatientIdentifier : Boolean;
      Function IsStyleAustralianMedicareNumber : Boolean;

      Property AllowDuplicates : Boolean Read FAllowDuplicates Write FAllowDuplicates;
      Property MaskFormat : String Read FMaskFormat Write FMaskFormat;
      Property MaskMinimumLength : Integer Read FMaskMinimumLength Write FMaskMinimumLength;
      Property MaskMaximumLength : Integer Read FMaskMaximumLength Write FMaskMaximumLength;
      Property HasStyle : Boolean Read GetHasStyle Write SetHasStyle;
      Property Style : TPatientIdentifierStyle Read GetStyle Write SetStyle;
  End;

  TPatientIdentifierDefinitionEntityList = Class(TAdvPersistentList)
    Private
      Function GetDefinitionEntityByIndex(Const iIndex : Integer) : TPatientIdentifierDefinitionEntity;

    Protected
      Function ItemClass : TAdvObjectClass; Override;

      Function CompareByStyle(pA, pB : Pointer) : Integer;

    Public
      Function Link : TPatientIdentifierDefinitionEntityList; Overload;

      Function GetByStyle(Const aPatientIdentifierStyle : TPatientIdentifierStyle) : TPatientIdentifierDefinitionEntity;
      Function IndexByStyle(Const aPatientIdentifierStyle : TPatientIdentifierStyle) : Integer;

      Property DefinitionEntityByIndex[Const iIndex : Integer] : TPatientIdentifierDefinitionEntity Read GetDefinitionEntityByIndex; Default;
  End;

  TPatientIdentifierDefinitionEntityListBuilder = Class(TAdvObject)
    Private
      FDefinitionEntityList : TPatientIdentifierDefinitionEntityList;

      Function GetDefinitionEntityList : TPatientIdentifierDefinitionEntityList;
      Procedure SetDefinitionEntityList(Const Value : TPatientIdentifierDefinitionEntityList);

    Public
      Constructor Create; Override;
      Destructor Destroy; Override;

      Procedure Build;

      Property DefinitionEntityList : TPatientIdentifierDefinitionEntityList Read GetDefinitionEntityList Write SetDefinitionEntityList;
  End;


Implementation


Uses
  AdvFactories;


Function TPatientIdentifierDefinitionEntity.Link : TPatientIdentifierDefinitionEntity;
Begin
  Result := TPatientIdentifierDefinitionEntity(Inherited Link);
End;


Function TPatientIdentifierDefinitionEntity.Clone : TPatientIdentifierDefinitionEntity;
Begin
  Result := TPatientIdentifierDefinitionEntity(Inherited Clone);
End;


Procedure TPatientIdentifierDefinitionEntity.Assign(oObject : TAdvObject);
Begin
  Inherited;

  FAllowDuplicates := TPatientIdentifierDefinitionEntity(oObject).AllowDuplicates;
  FMaskFormat := TPatientIdentifierDefinitionEntity(oObject).MaskFormat;
  FMaskMinimumLength := TPatientIdentifierDefinitionEntity(oObject).MaskMinimumLength;
  FMaskMaximumLength := TPatientIdentifierDefinitionEntity(oObject).MaskMaximumLength;
  FHasStyle := TPatientIdentifierDefinitionEntity(oObject).HasStyle;
  FStyle := TPatientIdentifierDefinitionEntity(oObject).Style;
End;


Procedure TPatientIdentifierDefinitionEntity.Define(oFiler: TAdvFiler);
Begin
  Inherited;

  oFiler['AllowDuplicates'].DefineBoolean(FAllowDuplicates);
  oFiler['MaskFormat'].DefineString(FMaskFormat);
  oFiler['MaskMinimumLength'].DefineInteger(FMaskMinimumLength);
  oFiler['MaskMaximumLength'].DefineInteger(FMaskMaximumLength);
  oFiler['HasStyle'].DefineBoolean(FHasStyle);
  oFiler['Style'].DefineEnumerated(FStyle, PatientIdentifierStyleNameArray);
End;


Function TPatientIdentifierDefinitionEntity.IsStyleInternalPatientIdentifier : Boolean;
Begin
  Result := HasStyle And (FStyle = PatientIdentifierStyleInternalPatientIdentifier);
End;


Function TPatientIdentifierDefinitionEntity.IsStyleAustralianMedicareNumber : Boolean;
Begin
  Result := HasStyle And (FStyle = PatientIdentifierStyleAustralianMedicareNumber);
End;


Function TPatientIdentifierDefinitionEntity.GetStyle : TPatientIdentifierStyle;
Begin
  Assert(Condition(HasStyle, 'GetHasStyle', 'No style has been defined.'));

  Result := FStyle;
End;


Procedure TPatientIdentifierDefinitionEntity.SetStyle(Const Value : TPatientIdentifierStyle);
Begin
  FStyle := Value;
End;


Function TPatientIdentifierDefinitionEntity.GetHasStyle : Boolean;
Begin
  Result := FHasStyle;
End;


Procedure TPatientIdentifierDefinitionEntity.SetHasStyle(Const Value : Boolean);
Begin
  FHasStyle := Value;
End;


Function TPatientIdentifierDefinitionEntityList.Link : TPatientIdentifierDefinitionEntityList;
Begin
  Result := TPatientIdentifierDefinitionEntityList(Inherited Link);
End;


Function TPatientIdentifierDefinitionEntityList.ItemClass : TAdvObjectClass;
Begin
  Result := TPatientIdentifierDefinitionEntity;
End;


Function TPatientIdentifierDefinitionEntityList.CompareByStyle(pA, pB : Pointer) : Integer;
Begin
  Result := BooleanCompare(TPatientIdentifierDefinitionEntity(pA).HasStyle, TPatientIdentifierDefinitionEntity(pB).HasStyle);

  If Result = 0 Then
  Begin
    If TPatientIdentifierDefinitionEntity(pA).HasStyle Then
      Result := IntegerCompare(Integer(TPatientIdentifierDefinitionEntity(pA).Style), Integer(TPatientIdentifierDefinitionEntity(pB).Style))
    Else
      Result := StringCompare(TPatientIdentifierDefinitionEntity(pA).MaskFormat, TPatientIdentifierDefinitionEntity(pB).MaskFormat);
  End;
End;


Function TPatientIdentifierDefinitionEntityList.GetByStyle(Const aPatientIdentifierStyle : TPatientIdentifierStyle) : TPatientIdentifierDefinitionEntity;
Begin
  Result := TPatientIdentifierDefinitionEntity(Get(IndexByStyle(aPatientIdentifierStyle)));
End;


Function TPatientIdentifierDefinitionEntityList.IndexByStyle(Const aPatientIdentifierStyle : TPatientIdentifierStyle) : Integer;
Var
  oDefinitionEntity : TPatientIdentifierDefinitionEntity;
Begin
  oDefinitionEntity := TPatientIdentifierDefinitionEntity.Create;
  Try
    oDefinitionEntity.HasStyle := True;
    oDefinitionEntity.Style := aPatientIdentifierStyle;

    Result := IndexBy(oDefinitionEntity, CompareByStyle);
  Finally
    oDefinitionEntity.Free;
  End;
End;


Function TPatientIdentifierDefinitionEntityList.GetDefinitionEntityByIndex(Const iIndex : Integer) : TPatientIdentifierDefinitionEntity;
Begin
  Result := TPatientIdentifierDefinitionEntity(ObjectByIndex[iIndex]);
End;


Constructor TPatientIdentifierDefinitionEntityListBuilder.Create;
Begin
  Inherited;

  FDefinitionEntityList := Nil;
End;


Destructor TPatientIdentifierDefinitionEntityListBuilder.Destroy;
Begin
  FDefinitionEntityList.Free;

  Inherited;
End;


Procedure TPatientIdentifierDefinitionEntityListBuilder.Build;
Var
  oDefinitionEntity : TPatientIdentifierDefinitionEntity;
Begin
  DefinitionEntityList.Clear;

  oDefinitionEntity := TPatientIdentifierDefinitionEntity.Create;
  oDefinitionEntity.HasStyle := True;
  oDefinitionEntity.Style := PatientIdentifierStyleInternalPatientIdentifier;
  oDefinitionEntity.AllowDuplicates := True;
  oDefinitionEntity.MaskFormat := '9999999999';
  DefinitionEntityList.Add(oDefinitionEntity);

  oDefinitionEntity := TPatientIdentifierDefinitionEntity.Create;
  oDefinitionEntity.HasStyle := True;
  oDefinitionEntity.Style := PatientIdentifierStyleAustralianMedicareNumber;
  oDefinitionEntity.AllowDuplicates := True;
  oDefinitionEntity.MaskFormat := '9999 99999 9-9';
  oDefinitionEntity.MaskMinimumLength := 12;
  oDefinitionEntity.MaskMaximumLength := 14;
  DefinitionEntityList.Add(oDefinitionEntity);

  oDefinitionEntity := TPatientIdentifierDefinitionEntity.Create;
  oDefinitionEntity.HasStyle := True;
  oDefinitionEntity.Style := PatientIdentifierStyleAustralianDepartmentOfVeteransAffairsFileNumber;
  oDefinitionEntity.AllowDuplicates := False;
  oDefinitionEntity.MaskFormat := 'LAAAAAAAA';
  oDefinitionEntity.MaskMaximumLength := 9;
  DefinitionEntityList.Add(oDefinitionEntity);

  oDefinitionEntity := TPatientIdentifierDefinitionEntity.Create;
  oDefinitionEntity.HasStyle := True;
  oDefinitionEntity.Style := PatientIdentifierStyleWesternAustralianUnitMedicalRecordNumber;
  oDefinitionEntity.AllowDuplicates := False;
  oDefinitionEntity.MaskFormat := 'L9999999';
  DefinitionEntityList.Add(oDefinitionEntity);

  oDefinitionEntity := TPatientIdentifierDefinitionEntity.Create;
  oDefinitionEntity.HasStyle := True;
  oDefinitionEntity.Style := PatientIdentifierStyleNewZealandNationalHealthIndexNumber;
  oDefinitionEntity.AllowDuplicates := False;
  oDefinitionEntity.MaskFormat := 'LLL9999';
  oDefinitionEntity.MaskMinimumLength := 7;
  oDefinitionEntity.MaskMaximumLength := 7;
  DefinitionEntityList.Add(oDefinitionEntity);
End;


Function TPatientIdentifierDefinitionEntityListBuilder.GetDefinitionEntityList : TPatientIdentifierDefinitionEntityList;
Begin
  Assert(Invariants('GetDefinitionEntityList', FDefinitionEntityList, TPatientIdentifierDefinitionEntityList, 'FDefinitionEntityList'));

  Result := FDefinitionEntityList;
End;


Procedure TPatientIdentifierDefinitionEntityListBuilder.SetDefinitionEntityList(Const Value : TPatientIdentifierDefinitionEntityList);
Begin
  Assert(Invariants('SetDefinitionEntityList', Value, TPatientIdentifierDefinitionEntityList, 'Value'));

  FDefinitionEntityList.Free;
  FDefinitionEntityList := Value;
End;


Initialization
  Factory.RegisterClassArray([TPatientIdentifierDefinitionEntity, TPatientIdentifierDefinitionEntityList]);
End. // PatientIdentifierDefinitionEntities //
