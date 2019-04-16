using System;
using System.Drawing;
using System.Windows.Forms;
using MedicalNumber;

namespace MedicalNumberGenerator
{
  public partial class MedicalNumberGeneratorApplicationForm : Form
  {
    private PatientIdentifierStyleHelper helper;
    private PatientIdentifierDefinition[] patientIdentifierDefinitionList;
    private readonly MedicareProviderNumberValidator validator;

    public MedicalNumberGeneratorApplicationForm()
    {
      InitializeComponent();

      patientIdentifierDefinitionList = PatientIdentifierDefinitionListBuilder.Build();
      validator = new MedicareProviderNumberValidator();
      helper = new PatientIdentifierStyleHelper(patientIdentifierDefinitionList);
    }

    private void MedicalNumberGeneratorApplicationForm_Load(object sender, EventArgs e)
    {
      PatientIdentifierCopyButton.Enabled = false;
      MedicareProviderNumberCopyButton.Enabled = false;

      foreach (var definition in patientIdentifierDefinitionList)
        PatientIdentifierStyleComboBox.Items.Add(definition.Name);

      PatientIdentifierStyleComboBox.SelectedIndex = 0;
    }
    private void PatientIdentifierTypeGenerateButton_Click(object sender, EventArgs e)
    {
      PatientIdentifierCopyButton.Enabled = true;
      var style = helper.GetPatientIdentifierStyleByName(PatientIdentifierStyleComboBox.Text);
      var definition = helper.GetPatientIdentifierDefinitionByStyle(style);

      if (definition == null)
      {
        // raise an error of some kind, or fail in some other fashion.
      }
      else
      {
        var valueGenerator = new PatientIdentifierValueGenerator();
        var validationEngine = new PatientIdentifierValidationEngine()
        {
          PatientIdentifierStyle = definition.Style
        };
        var oldCursor = this.Cursor;
        string generatedIdentifier = "";

        this.Cursor = Cursors.WaitCursor;
        try
        {
          var valid = false;
          var value = "";
          do
          {
            value = valueGenerator.Generate(definition);

            valid = validationEngine.Validate(value);

            if (GenerateInvalidCheckBox.Checked)
              valid = !valid;
          }
          while (!valid);

          generatedIdentifier = value;

          if (!GenerateFormattedCheckBox.Checked)
            generatedIdentifier = RemoveFormatting(generatedIdentifier);

          GeneratedPatientIdentifierLinkLabel.Text = generatedIdentifier;
        }
        finally
        {
          this.Cursor = oldCursor;
        }
      }
    }
    private void MedicareProviderNumberGenerateButton_Click(object sender, EventArgs e)
    {
      MedicareProviderNumberCopyButton.Enabled = true;

      var maskedTextRandomValueGenerator = new MaskedTextRandomValueGenerator();
      var oldCursor = this.Cursor;
      var generatedProviderNumber = "";

      maskedTextRandomValueGenerator.MaskFormat = "999999AL";

      this.Cursor = Cursors.WaitCursor;
      try
      {
        var valid = false;
        do
        {
          generatedProviderNumber = maskedTextRandomValueGenerator.Generate();

          valid = validator.Validate(generatedProviderNumber);

          if (GenerateInvalidCheckBox.Checked)
            valid = !valid;
        }
        while (!valid);

        if (!GenerateFormattedCheckBox.Checked)
          generatedProviderNumber = RemoveFormatting(generatedProviderNumber);

        GeneratedMedicareProviderNumberLinkLabel.Text = generatedProviderNumber;
      }
      finally
      {
        this.Cursor = oldCursor;
      }
    }
    private void MedicareProviderNumberCopyButton_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(GeneratedMedicareProviderNumberLinkLabel.Text);
    }
    private void CopyButton_Click(object sender, EventArgs e)
    {
      Clipboard.SetDataObject(GeneratedPatientIdentifierLinkLabel.Text, true);
    }
    private void ValidatePatientIdentifierTextBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidatePatientIdentifierTextBox.Text == string.Empty)
        ValidatePatientIdentifierTextBox.BackColor = Color.White;
      else
      {
        var style = helper.GetPatientIdentifierStyleByName(PatientIdentifierStyleComboBox.Text);
        var definition = helper.GetPatientIdentifierDefinitionByStyle(style);

        if (definition == null)
        {
          // raise an error of some kind, or fail in some other fashion.
        }
        else
        {
          var validationEngine = new PatientIdentifierValidationEngine()
          {
            PatientIdentifierStyle = definition.Style
          };
          ValidatePatientIdentifierTextBox.BackColor = validationEngine.Validate(ValidatePatientIdentifierTextBox.Text) ? Color.LightGreen : Color.Red;
        }
      }
    }
    private void ValidateProviderNumberTextBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidateProviderNumberTextBox.Text == String.Empty)
        ValidateProviderNumberTextBox.BackColor = Color.White;
      else
      {
        ValidateProviderNumberTextBox.BackColor = validator.Validate(ValidateProviderNumberTextBox.Text) ? Color.LightGreen : Color.Red;
      }
    }
    private string RemoveFormatting(string text)
    {
      return text.Replace("-", "").Replace(" ", "");
    }
  }
}