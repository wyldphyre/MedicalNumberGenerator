namespace MedicalNumberGenerator
{
  partial class MedicalNumberGeneratorApplicationForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.PatientIdentifierCopyButton = new System.Windows.Forms.Button();
      this.ValidatePatientIdentifierTextBox = new System.Windows.Forms.TextBox();
      this.ValidatePatientIdentifierLable = new System.Windows.Forms.Label();
      this.GeneratedPatientIdentifierLinkLabel = new System.Windows.Forms.LinkLabel();
      this.PatientIdentifierTypeGenerateButton = new System.Windows.Forms.Button();
      this.PatientIdentifierTypeLabel = new System.Windows.Forms.Label();
      this.PatientIdentifierStyleComboBox = new System.Windows.Forms.ComboBox();
      this.MedicareProviderNumberTabPage = new System.Windows.Forms.TabPage();
      this.MedicareProviderNumberCopyButton = new System.Windows.Forms.Button();
      this.ValidateProviderNumberTextBox = new System.Windows.Forms.TextBox();
      this.ValidateProviderNumberLabel = new System.Windows.Forms.Label();
      this.GeneratedMedicareProviderNumberLinkLabel = new System.Windows.Forms.LinkLabel();
      this.MedicareProviderNumberGenerateButton = new System.Windows.Forms.Button();
      this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
      this.GenerateFormattedCheckBox = new System.Windows.Forms.CheckBox();
      this.GenerateInvalidCheckBox = new System.Windows.Forms.CheckBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.MedicareProviderNumberTabPage.SuspendLayout();
      this.OptionsGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.MedicareProviderNumberTabPage);
      this.tabControl1.Location = new System.Drawing.Point(12, 12);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(504, 125);
      this.tabControl1.TabIndex = 1;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.PatientIdentifierCopyButton);
      this.tabPage1.Controls.Add(this.ValidatePatientIdentifierTextBox);
      this.tabPage1.Controls.Add(this.ValidatePatientIdentifierLable);
      this.tabPage1.Controls.Add(this.GeneratedPatientIdentifierLinkLabel);
      this.tabPage1.Controls.Add(this.PatientIdentifierTypeGenerateButton);
      this.tabPage1.Controls.Add(this.PatientIdentifierTypeLabel);
      this.tabPage1.Controls.Add(this.PatientIdentifierStyleComboBox);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(496, 99);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Patient Identifiers";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // PatientIdentifierCopyButton
      // 
      this.PatientIdentifierCopyButton.Enabled = false;
      this.PatientIdentifierCopyButton.Location = new System.Drawing.Point(207, 37);
      this.PatientIdentifierCopyButton.Name = "PatientIdentifierCopyButton";
      this.PatientIdentifierCopyButton.Size = new System.Drawing.Size(38, 23);
      this.PatientIdentifierCopyButton.TabIndex = 11;
      this.PatientIdentifierCopyButton.Text = "copy";
      this.PatientIdentifierCopyButton.UseVisualStyleBackColor = true;
      this.PatientIdentifierCopyButton.Click += new System.EventHandler(this.CopyButton_Click);
      // 
      // ValidatePatientIdentifierTextBox
      // 
      this.ValidatePatientIdentifierTextBox.Location = new System.Drawing.Point(121, 66);
      this.ValidatePatientIdentifierTextBox.Name = "ValidatePatientIdentifierTextBox";
      this.ValidatePatientIdentifierTextBox.Size = new System.Drawing.Size(286, 20);
      this.ValidatePatientIdentifierTextBox.TabIndex = 11;
      this.ValidatePatientIdentifierTextBox.TextChanged += new System.EventHandler(this.ValidatePatientIdentifierTextBox_TextChanged);
      // 
      // ValidatePatientIdentifierLable
      // 
      this.ValidatePatientIdentifierLable.AutoSize = true;
      this.ValidatePatientIdentifierLable.Location = new System.Drawing.Point(6, 69);
      this.ValidatePatientIdentifierLable.Name = "ValidatePatientIdentifierLable";
      this.ValidatePatientIdentifierLable.Size = new System.Drawing.Size(88, 13);
      this.ValidatePatientIdentifierLable.TabIndex = 10;
      this.ValidatePatientIdentifierLable.Text = "Validate Identifier";
      // 
      // GeneratedPatientIdentifierLinkLabel
      // 
      this.GeneratedPatientIdentifierLinkLabel.AutoSize = true;
      this.GeneratedPatientIdentifierLinkLabel.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.GeneratedPatientIdentifierLinkLabel.Location = new System.Drawing.Point(119, 41);
      this.GeneratedPatientIdentifierLinkLabel.Name = "GeneratedPatientIdentifierLinkLabel";
      this.GeneratedPatientIdentifierLinkLabel.Size = new System.Drawing.Size(68, 12);
      this.GeneratedPatientIdentifierLinkLabel.TabIndex = 9;
      this.GeneratedPatientIdentifierLinkLabel.TabStop = true;
      this.GeneratedPatientIdentifierLinkLabel.Text = "<unknown>";
      // 
      // PatientIdentifierTypeGenerateButton
      // 
      this.PatientIdentifierTypeGenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.PatientIdentifierTypeGenerateButton.Location = new System.Drawing.Point(413, 6);
      this.PatientIdentifierTypeGenerateButton.Name = "PatientIdentifierTypeGenerateButton";
      this.PatientIdentifierTypeGenerateButton.Size = new System.Drawing.Size(80, 21);
      this.PatientIdentifierTypeGenerateButton.TabIndex = 7;
      this.PatientIdentifierTypeGenerateButton.Text = "Generate";
      this.PatientIdentifierTypeGenerateButton.UseVisualStyleBackColor = true;
      this.PatientIdentifierTypeGenerateButton.Click += new System.EventHandler(this.PatientIdentifierTypeGenerateButton_Click);
      // 
      // PatientIdentifierTypeLabel
      // 
      this.PatientIdentifierTypeLabel.AutoSize = true;
      this.PatientIdentifierTypeLabel.Location = new System.Drawing.Point(6, 10);
      this.PatientIdentifierTypeLabel.Name = "PatientIdentifierTypeLabel";
      this.PatientIdentifierTypeLabel.Size = new System.Drawing.Size(110, 13);
      this.PatientIdentifierTypeLabel.TabIndex = 6;
      this.PatientIdentifierTypeLabel.Text = "Patient Identifier Type";
      // 
      // PatientIdentifierStyleComboBox
      // 
      this.PatientIdentifierStyleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.PatientIdentifierStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.PatientIdentifierStyleComboBox.FormattingEnabled = true;
      this.PatientIdentifierStyleComboBox.Location = new System.Drawing.Point(122, 6);
      this.PatientIdentifierStyleComboBox.Name = "PatientIdentifierStyleComboBox";
      this.PatientIdentifierStyleComboBox.Size = new System.Drawing.Size(285, 21);
      this.PatientIdentifierStyleComboBox.TabIndex = 5;
      // 
      // MedicareProviderNumberTabPage
      // 
      this.MedicareProviderNumberTabPage.Controls.Add(this.MedicareProviderNumberCopyButton);
      this.MedicareProviderNumberTabPage.Controls.Add(this.ValidateProviderNumberTextBox);
      this.MedicareProviderNumberTabPage.Controls.Add(this.ValidateProviderNumberLabel);
      this.MedicareProviderNumberTabPage.Controls.Add(this.GeneratedMedicareProviderNumberLinkLabel);
      this.MedicareProviderNumberTabPage.Controls.Add(this.MedicareProviderNumberGenerateButton);
      this.MedicareProviderNumberTabPage.Location = new System.Drawing.Point(4, 22);
      this.MedicareProviderNumberTabPage.Name = "MedicareProviderNumberTabPage";
      this.MedicareProviderNumberTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.MedicareProviderNumberTabPage.Size = new System.Drawing.Size(496, 99);
      this.MedicareProviderNumberTabPage.TabIndex = 1;
      this.MedicareProviderNumberTabPage.Text = "Medicare Provider Number";
      this.MedicareProviderNumberTabPage.UseVisualStyleBackColor = true;
      // 
      // MedicareProviderNumberCopyButton
      // 
      this.MedicareProviderNumberCopyButton.Enabled = false;
      this.MedicareProviderNumberCopyButton.Location = new System.Drawing.Point(183, 6);
      this.MedicareProviderNumberCopyButton.Name = "MedicareProviderNumberCopyButton";
      this.MedicareProviderNumberCopyButton.Size = new System.Drawing.Size(38, 23);
      this.MedicareProviderNumberCopyButton.TabIndex = 15;
      this.MedicareProviderNumberCopyButton.Text = "copy";
      this.MedicareProviderNumberCopyButton.UseVisualStyleBackColor = true;
      this.MedicareProviderNumberCopyButton.Click += new System.EventHandler(this.MedicareProviderNumberCopyButton_Click);
      // 
      // ValidateProviderNumberTextBox
      // 
      this.ValidateProviderNumberTextBox.Location = new System.Drawing.Point(138, 39);
      this.ValidateProviderNumberTextBox.Name = "ValidateProviderNumberTextBox";
      this.ValidateProviderNumberTextBox.Size = new System.Drawing.Size(286, 20);
      this.ValidateProviderNumberTextBox.TabIndex = 13;
      this.ValidateProviderNumberTextBox.TextChanged += new System.EventHandler(this.ValidateProviderNumberTextBox_TextChanged);
      // 
      // ValidateProviderNumberLabel
      // 
      this.ValidateProviderNumberLabel.AutoSize = true;
      this.ValidateProviderNumberLabel.Location = new System.Drawing.Point(3, 42);
      this.ValidateProviderNumberLabel.Name = "ValidateProviderNumberLabel";
      this.ValidateProviderNumberLabel.Size = new System.Drawing.Size(127, 13);
      this.ValidateProviderNumberLabel.TabIndex = 12;
      this.ValidateProviderNumberLabel.Text = "Validate Provider Number";
      // 
      // GeneratedMedicareProviderNumberLinkLabel
      // 
      this.GeneratedMedicareProviderNumberLinkLabel.AutoSize = true;
      this.GeneratedMedicareProviderNumberLinkLabel.Location = new System.Drawing.Point(98, 11);
      this.GeneratedMedicareProviderNumberLinkLabel.Name = "GeneratedMedicareProviderNumberLinkLabel";
      this.GeneratedMedicareProviderNumberLinkLabel.Size = new System.Drawing.Size(63, 13);
      this.GeneratedMedicareProviderNumberLinkLabel.TabIndex = 1;
      this.GeneratedMedicareProviderNumberLinkLabel.TabStop = true;
      this.GeneratedMedicareProviderNumberLinkLabel.Text = "<unknown>";
      // 
      // MedicareProviderNumberGenerateButton
      // 
      this.MedicareProviderNumberGenerateButton.Location = new System.Drawing.Point(6, 6);
      this.MedicareProviderNumberGenerateButton.Name = "MedicareProviderNumberGenerateButton";
      this.MedicareProviderNumberGenerateButton.Size = new System.Drawing.Size(86, 22);
      this.MedicareProviderNumberGenerateButton.TabIndex = 0;
      this.MedicareProviderNumberGenerateButton.Text = "Generate";
      this.MedicareProviderNumberGenerateButton.UseVisualStyleBackColor = true;
      this.MedicareProviderNumberGenerateButton.Click += new System.EventHandler(this.MedicareProviderNumberGenerateButton_Click);
      // 
      // OptionsGroupBox
      // 
      this.OptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.OptionsGroupBox.Controls.Add(this.GenerateFormattedCheckBox);
      this.OptionsGroupBox.Controls.Add(this.GenerateInvalidCheckBox);
      this.OptionsGroupBox.Location = new System.Drawing.Point(12, 143);
      this.OptionsGroupBox.Name = "OptionsGroupBox";
      this.OptionsGroupBox.Size = new System.Drawing.Size(503, 50);
      this.OptionsGroupBox.TabIndex = 10;
      this.OptionsGroupBox.TabStop = false;
      this.OptionsGroupBox.Text = "Options";
      // 
      // GenerateFormattedCheckBox
      // 
      this.GenerateFormattedCheckBox.AutoSize = true;
      this.GenerateFormattedCheckBox.Location = new System.Drawing.Point(142, 19);
      this.GenerateFormattedCheckBox.Name = "GenerateFormattedCheckBox";
      this.GenerateFormattedCheckBox.Size = new System.Drawing.Size(120, 17);
      this.GenerateFormattedCheckBox.TabIndex = 11;
      this.GenerateFormattedCheckBox.Text = "Generate Formatted";
      this.GenerateFormattedCheckBox.UseVisualStyleBackColor = true;
      // 
      // GenerateInvalidCheckBox
      // 
      this.GenerateInvalidCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.GenerateInvalidCheckBox.AutoSize = true;
      this.GenerateInvalidCheckBox.Location = new System.Drawing.Point(6, 19);
      this.GenerateInvalidCheckBox.Name = "GenerateInvalidCheckBox";
      this.GenerateInvalidCheckBox.Size = new System.Drawing.Size(103, 17);
      this.GenerateInvalidCheckBox.TabIndex = 10;
      this.GenerateInvalidCheckBox.Text = "Generate invalid";
      this.GenerateInvalidCheckBox.UseVisualStyleBackColor = true;
      // 
      // MedicalNumberGeneratorApplicationForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(528, 205);
      this.Controls.Add(this.OptionsGroupBox);
      this.Controls.Add(this.tabControl1);
      this.Name = "MedicalNumberGeneratorApplicationForm";
      this.Text = "Medical Number Generator";
      this.Load += new System.EventHandler(this.MedicalNumberGeneratorApplicationForm_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.MedicareProviderNumberTabPage.ResumeLayout(false);
      this.MedicareProviderNumberTabPage.PerformLayout();
      this.OptionsGroupBox.ResumeLayout(false);
      this.OptionsGroupBox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.LinkLabel GeneratedPatientIdentifierLinkLabel;
    private System.Windows.Forms.Button PatientIdentifierTypeGenerateButton;
    private System.Windows.Forms.Label PatientIdentifierTypeLabel;
    private System.Windows.Forms.ComboBox PatientIdentifierStyleComboBox;
    private System.Windows.Forms.TabPage MedicareProviderNumberTabPage;
    private System.Windows.Forms.LinkLabel GeneratedMedicareProviderNumberLinkLabel;
    private System.Windows.Forms.Button MedicareProviderNumberGenerateButton;
    private System.Windows.Forms.GroupBox OptionsGroupBox;
    private System.Windows.Forms.CheckBox GenerateInvalidCheckBox;
    private System.Windows.Forms.CheckBox GenerateFormattedCheckBox;
    private System.Windows.Forms.TextBox ValidatePatientIdentifierTextBox;
    private System.Windows.Forms.Label ValidatePatientIdentifierLable;
    private System.Windows.Forms.TextBox ValidateProviderNumberTextBox;
    private System.Windows.Forms.Label ValidateProviderNumberLabel;
    private System.Windows.Forms.Button PatientIdentifierCopyButton;
    private System.Windows.Forms.Button MedicareProviderNumberCopyButton;
  }
}

