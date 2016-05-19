using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MedicalNumberGenerator
{
  public partial class MedicalNumberGeneratorApplicationForm : Form
  {
    private PatientIdentifierStyleHelper helper = new PatientIdentifierStyleHelper();
    private List<PatientIdentifierDefinition> patientIdentifierDefinitionList = new List<PatientIdentifierDefinition>();

    public MedicalNumberGeneratorApplicationForm()
    {
      InitializeComponent();

      PatientIdentifierDefinitionListBuilder listBuilder = new PatientIdentifierDefinitionListBuilder();

      listBuilder.DefinitionList = patientIdentifierDefinitionList;
      listBuilder.Build();

      helper.PatientIdentifierDefinitionList = patientIdentifierDefinitionList;
      helper.Prepare();
    }

    private void MedicalNumberGeneratorApplicationForm_Load(object sender, EventArgs e)
    {
      PatientIdentifierCopyHintLabel.Visible = false;
      ProviderNumberCopyHintLabel.Visible = false;

      foreach (PatientIdentifierDefinition definition in patientIdentifierDefinitionList)
      {
        PatientIdentifierStyleComboBox.Items.Add(definition.Name);
      }

      PatientIdentifierStyleComboBox.SelectedIndex = 0;
    }

    private void PatientIdentifierTypeGenerateButton_Click(object sender, EventArgs e)
    {
      //MaskedTextRandomValueGenerator generator = new MaskedTextRandomValueGenerator();
      PatientIdentifierStyle style = helper.GetPatientIdentifierStyleByName(PatientIdentifierStyleComboBox.Text);
      PatientIdentifierDefinition definition = helper.GetPatientIdentifierDefinitionByStyle(style);

      if (definition == null)
      {
        // raise an error of some kind, or fail in some other fashion.
      }
      else
      {
        PatientIdentifierValueGenerator valueGenerator = new PatientIdentifierValueGenerator();
        PatientIdentifierValidationEngine validationEngine = new PatientIdentifierValidationEngine();
        validationEngine.PatientIdentifierStyle = definition.Style;
        Cursor oldCursor = this.Cursor;
        string generatedIdentifier = "";

        this.Cursor = Cursors.WaitCursor;
        try
        {
          bool valid = false;
          do
          {
            valueGenerator.Definition = definition;
            valueGenerator.Execute();

            validationEngine.Value = valueGenerator.Value;
            validationEngine.Execute();

            valid = !validationEngine.HasIssues();

            if (GenerateInvalidCheckBox.Checked)
            {
              valid = !valid;
            }
          }
          while (!valid);

          generatedIdentifier = valueGenerator.Value;

          if (!GenerateFormattedCheckBox.Checked)
          {
            generatedIdentifier = RemoveFormatting(generatedIdentifier);
          }

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
      MaskedTextRandomValueGenerator maskedTextRandomValueGenerator = new MaskedTextRandomValueGenerator();
      MedicareProviderNumberValidator validator = new MedicareProviderNumberValidator();
      Cursor oldCursor = this.Cursor;
      string generatedProviderNumber = "";

      maskedTextRandomValueGenerator.MaskFormat = "999999AL";

      this.Cursor = Cursors.WaitCursor;
      try
      {
        bool valid = false;
        do
        {
          maskedTextRandomValueGenerator.Execute();

          validator.Value = maskedTextRandomValueGenerator.Text;
          validator.Execute();

          valid = !validator.HasIssues();

          if (GenerateInvalidCheckBox.Checked)
          {
            valid = !valid;
          }
        }
        while (!valid);

        generatedProviderNumber = maskedTextRandomValueGenerator.Text;

        if (!GenerateFormattedCheckBox.Checked)
        {
          generatedProviderNumber = RemoveFormatting(generatedProviderNumber);
        }

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

    private string RemoveFormatting(string text)
    {
      return text.Replace("-", "").Replace(" ", "");
    }

    private void ValidatePatientIdentifierTextBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidatePatientIdentifierTextBox.Text == string.Empty)
      {
        ValidatePatientIdentifierTextBox.BackColor = Color.White;
      }
      else
      {
        PatientIdentifierStyle style = helper.GetPatientIdentifierStyleByName(PatientIdentifierStyleComboBox.Text);
        PatientIdentifierDefinition definition = helper.GetPatientIdentifierDefinitionByStyle(style);

        if (definition == null)
        {
          // raise an error of some kind, or fail in some other fashion.
        }
        else
        {
          PatientIdentifierValidationEngine validationEngine = new PatientIdentifierValidationEngine();
          validationEngine.PatientIdentifierStyle = definition.Style;

          validationEngine.Value = ValidatePatientIdentifierTextBox.Text;
          validationEngine.Execute();

          if (validationEngine.HasIssues())
          {
            ValidatePatientIdentifierTextBox.BackColor = Color.Red;
          }
          else
          {
            ValidatePatientIdentifierTextBox.BackColor = Color.LightGreen;
          }
        }
      }
    }

    private void ValidateProviderNumberTextBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidateProviderNumberTextBox.Text == String.Empty)
      {
        ValidateProviderNumberTextBox.BackColor = Color.White;
      }
      else
      {
        MedicareProviderNumberValidator validator = new MedicareProviderNumberValidator();

        validator.Value = ValidateProviderNumberTextBox.Text;
        validator.Execute();

        if (validator.HasIssues())
        {
          ValidateProviderNumberTextBox.BackColor = Color.Red;
        }
        else
        {
          ValidateProviderNumberTextBox.BackColor = Color.LightGreen;
        }
      }
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
  }
}