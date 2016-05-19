Unit MedicareProviderNumberValidators;


{! 4 !}


Interface


Uses
  MathSupport,
  StringSupport,
  AdvObjects,
  AdvStringLists;


Type
  TMedicareProviderNumberValidator = Class(TAdvObject)
    Private
      FOwningEntityName : String;
      FProviderNumber : String;
      FIssueList : TAdvStringList;

    Public
      Constructor Create; Override;
      Destructor Destroy; Override;

      Procedure Validate;

      Property OwningEntityName : String Read FOwningEntityName Write FOwningEntityName;
      Property ProviderNumber : String Read FProviderNumber Write FProviderNumber;
      Property IssueList : TAdvStringList Read FIssueList;
  End;


Implementation


Constructor TMedicareProviderNumberValidator.Create;
Begin
  Inherited;

  FIssueList := TAdvStringList.Create;
  FIssueList.PreventDuplicates;
  FIssueList.Symbol := cReturn;
End;


Destructor TMedicareProviderNumberValidator.Destroy;
Begin
  FIssueList.Free;

  Inherited;
End;


Procedure TMedicareProviderNumberValidator.Validate;
Const
  PRACTICELOCATIONCHARACTER_TABLE = '0123456789ABCDEFGHJKLMNPQRTUVWXY';
  PRACTICELOCATIONCHECKDIGIT_TABLE = 'YXWTLKJHFBA';
Var
  iValidationCheckDigit : Integer;
  iProviderNumberLength : Integer;
  sPaddedProviderNumber : String;
Begin
  Assert(Condition(FOwningEntityName <> '', 'Validate', 'Owning entity name has not been provided.'));

  FIssueList.Clear;

  iProviderNumberLength := Length(ProviderNumber);

  If iProviderNumberLength < 7 Then
    FIssueList.Add('Provider number must be at least 7 characters');

  If iProviderNumberLength > 8 Then
    FIssueList.Add('Provider number can be no more than 8 characters');

  If iProviderNumberLength = 7 Then
    sPaddedProviderNumber := '0' + ProviderNumber
  Else
    sPaddedProviderNumber := ProviderNumber;

  If Length(sPaddedProviderNumber) = 8 Then
  Begin
    If Not StringExists(PRACTICELOCATIONCHARACTER_TABLE, sPaddedProviderNumber[7]) Then
      FIssueList.Add('Practice location character [' + sPaddedProviderNumber[7] + '] is not valid');

    iValidationCheckDigit :=
      ((Ord(sPaddedProviderNumber[1]) - Ord('0')) * 3) +
      ((Ord(sPaddedProviderNumber[2]) - Ord('0')) * 5) +
      ((Ord(sPaddedProviderNumber[3]) - Ord('0')) * 8) +
      ((Ord(sPaddedProviderNumber[4]) - Ord('0')) * 4) +
      ((Ord(sPaddedProviderNumber[5]) - Ord('0')) * 2) +
       (Ord(sPaddedProviderNumber[6]) - Ord('0')) +
      ((Pos(sPaddedProviderNumber[7], PRACTICELOCATIONCHARACTER_TABLE) - 1) * 6);

    If sPaddedProviderNumber[8] <> PRACTICELOCATIONCHECKDIGIT_TABLE[SignedMod(iValidationCheckDigit, 11) + 1] Then
    Begin
      FIssueList.Add('Check digit character is not valid [Please consult the ' + FOwningEntityName + ' to ensure the validity of the identifier]');
    End;
  End;
End;


End. // MedicareProviderNumbers //
