using System;
using System.Drawing;
using System.Windows.Forms;
using MedicalNumber;
using MedicalNumber.Library;
using NLog;

namespace MedicalNumberGenerator
{
  public partial class MedicalNumberGeneratorApplicationForm : Form
  {
    private readonly PatientIdentifierStyleHelper helper;
    private readonly PatientIdentifierDefinition[] patientIdentifierDefinitionList;
    private readonly Logger logger = LogManager.GetCurrentClassLogger();

    public MedicalNumberGeneratorApplicationForm()
    {
      InitializeComponent();

      patientIdentifierDefinitionList = PatientIdentifierDefinitionListBuilder.Definitions;
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

      logger.Info(new { Action = "Generate", definition.Style, definition.MaskFormat, GenerateInvalid = GenerateInvalidCheckBox.Checked, GenerateFormatted = GenerateInvalidCheckBox.Checked });

      var valueGenerator = new PatientIdentifierValueGenerator();
      var validationEngine = new PatientIdentifierValidationEngine();
      var oldCursor = Cursor;
      Cursor = Cursors.WaitCursor;
      try
      {
        var valid = false;
        var value = "";
        do
        {
          value = valueGenerator.Generate(definition);

          valid = validationEngine.Validate(value, definition.Style);

          if (GenerateInvalidCheckBox.Checked)
            valid = !valid;
        } while (!valid);

        var generatedIdentifier = value;

        if (!GenerateFormattedCheckBox.Checked)
          generatedIdentifier = RemoveFormatting(generatedIdentifier);

        logger.Debug(new { GeneratedIdentifier = generatedIdentifier, definition.Style });

        GeneratedPatientIdentifierLinkLabel.Text = generatedIdentifier;
      }
      finally
      {
        Cursor = oldCursor;
      }
    }
    private void MedicareProviderNumberGenerateButton_Click(object sender, EventArgs e)
    {
      MedicareProviderNumberCopyButton.Enabled = true;

      logger.Info(new { Style = "MedicareProviderNumber", GenerateInvalid = GenerateInvalidCheckBox.Checked, GenerateFormatted = GenerateInvalidCheckBox.Checked });

      var maskedTextRandomValueGenerator = new MaskedTextRandomValueGenerator();
      var oldCursor = Cursor;

      Cursor = Cursors.WaitCursor;
      try
      {
        var valid = false;
        string generatedProviderNumber;
        do
        {
          generatedProviderNumber = maskedTextRandomValueGenerator.Generate("999999AL");

          valid = MedicareProviderNumberHelper.Validate(generatedProviderNumber, out var _);

          if (GenerateInvalidCheckBox.Checked)
            valid = !valid;
        }
        while (!valid);

        if (!GenerateFormattedCheckBox.Checked)
          generatedProviderNumber = RemoveFormatting(generatedProviderNumber);

        logger.Debug(new { GeneratedIdentifier = generatedProviderNumber, Style = "MedicareProviderNumber" });

        GeneratedMedicareProviderNumberLinkLabel.Text = generatedProviderNumber;
      }
      finally
      {
        Cursor = oldCursor;
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
          var validationEngine = new PatientIdentifierValidationEngine();
          ValidatePatientIdentifierTextBox.BackColor = validationEngine.Validate(ValidatePatientIdentifierTextBox.Text, definition.Style) ? Color.LightGreen : Color.Red;
        }
      }
    }
    private void ValidateProviderNumberTextBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidateProviderNumberTextBox.Text == String.Empty)
        ValidateProviderNumberTextBox.BackColor = Color.White;
      else
      {
        ValidateProviderNumberTextBox.BackColor = MedicareProviderNumberHelper.Validate(ValidateProviderNumberTextBox.Text, out var _) ? Color.LightGreen : Color.Red;
      }
    }
    private string RemoveFormatting(string text)
    {
      return text?.Replace("-", "").Replace(" ", "");
    }

    private void MedicalNumberGeneratorApplicationForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      NLog.LogManager.Shutdown();
    }
  }
}