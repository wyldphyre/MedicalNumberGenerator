using System;
using System.Drawing;
using System.Windows.Forms;

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
      PatientIdentifierCopyHintLabel.Visible = false;
      ProviderNumberCopyHintLabel.Visible = false;

      foreach (var definition in patientIdentifierDefinitionList)
        PatientIdentifierStyleComboBox.Items.Add(definition.Name);

      PatientIdentifierStyleComboBox.SelectedIndex = 0;
    }
    private void PatientIdentifierTypeGenerateButton_Click(object sender, EventArgs e)
    {
      var style = helper.GetPatientIdentifierStyleByName(PatientIdentifierStyleComboBox.Text);
      var definition = helper.GetPatientIdentifierDefinitionByStyle(style);

      if (definition == null)
      {
        // raise an error of some kind, or fail in some other fashion.
      }
      else
      {
        var valueGenerator = new PatientIdentifierValueGenerator();
        var validationEngine = new PatientIdentifierValidationEngine();
        validationEngine.PatientIdentifierStyle = definition.Style;
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
          generatedProviderNumber  = maskedTextRandomValueGenerator.Generate();

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
    private void GeneratedMedicareProviderNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Clipboard.SetText(GeneratedMedicareProviderNumberLinkLabel.Text);
    }
    private void GeneratedPatientIdentifierLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Clipboard.SetDataObject(GeneratedPatientIdentifierLinkLabel.Text, true);
    }
    private void GeneratedPatientIdentifierLinkLabel_MouseEnter(object sender, EventArgs e)
    {
      PatientIdentifierCopyHintLabel.Visible = true;
    }
    private void GeneratedPatientIdentifierLinkLabel_MouseLeave(object sender, EventArgs e)
    {
      PatientIdentifierCopyHintLabel.Visible = false;
    }
    private void GeneratedMedicareProviderNumberLinkLabel_MouseEnter(object sender, EventArgs e)
    {
      ProviderNumberCopyHintLabel.Visible = true;
    }
    private void GeneratedMedicareProviderNumberLinkLabel_MouseLeave(object sender, EventArgs e)
    {
      ProviderNumberCopyHintLabel.Visible = false;
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
          validationEngine.PatientIdentifierStyle = definition.Style;

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