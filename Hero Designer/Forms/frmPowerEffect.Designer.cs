using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hero_Designer.Forms
{
    public partial class frmPowerEffect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.chkStack = new System.Windows.Forms.CheckBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.clbSuppression = new System.Windows.Forms.CheckedListBox();
            this.rbIfPlayer = new System.Windows.Forms.RadioButton();
            this.rbIfCritter = new System.Windows.Forms.RadioButton();
            this.rbIfAny = new System.Windows.Forms.RadioButton();
            this.chkFXResistable = new System.Windows.Forms.CheckBox();
            this.chkFXBuffable = new System.Windows.Forms.CheckBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.txtFXProb = new System.Windows.Forms.TextBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.txtFXDelay = new System.Windows.Forms.TextBox();
            this.Label24 = new System.Windows.Forms.Label();
            this.txtFXTicks = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.txtFXDuration = new System.Windows.Forms.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.txtFXMag = new System.Windows.Forms.TextBox();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.cbFXClass = new System.Windows.Forms.ComboBox();
            this.cbFXSpecialCase = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblEffectDescription = new System.Windows.Forms.Label();
            this.chkVariable = new System.Windows.Forms.CheckBox();
            this.cbPercentageOverride = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtFXScale = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.cbAffects = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cbAttribute = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cbAspect = new System.Windows.Forms.ComboBox();
            this.chkNearGround = new System.Windows.Forms.CheckBox();
            this.lblAffectsCaster = new System.Windows.Forms.Label();
            this.lvEffectType = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvSubAttribute = new System.Windows.Forms.ListView();
            this.chSub = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblProb = new System.Windows.Forms.Label();
            this.lvSubSub = new System.Windows.Forms.ListView();
            this.chSubSub = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.btnCSV = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.cmbEffectId = new System.Windows.Forms.ComboBox();
            this.IgnoreED = new System.Windows.Forms.CheckBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.txtOverride = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.txtPPM = new System.Windows.Forms.TextBox();
            this.cbFXSpecialCase2 = new System.Windows.Forms.ComboBox();
            this.cbFXOperator = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dGVActive = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvPowerType = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPower = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbModifier = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.GroupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVActive)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(928, 497);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(150, 26);
            this.btnPaste.TabIndex = 116;
            this.btnPaste.Text = "Paste Effect Data";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(928, 465);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(150, 26);
            this.btnCopy.TabIndex = 115;
            this.btnCopy.Text = "Copy Effect Data";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // chkStack
            // 
            this.chkStack.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkStack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkStack.Location = new System.Drawing.Point(3, 3);
            this.chkStack.Name = "chkStack";
            this.chkStack.Size = new System.Drawing.Size(173, 20);
            this.chkStack.TabIndex = 112;
            this.chkStack.Text = "Effect Can Stack";
            this.chkStack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkStack.CheckedChanged += new System.EventHandler(this.chkFxNoStack_CheckedChanged);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.clbSuppression);
            this.GroupBox3.Location = new System.Drawing.Point(525, 8);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(386, 105);
            this.GroupBox3.TabIndex = 107;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Suppress Effect When:";
            // 
            // clbSuppression
            // 
            this.clbSuppression.FormattingEnabled = true;
            this.clbSuppression.HorizontalScrollbar = true;
            this.clbSuppression.Location = new System.Drawing.Point(10, 19);
            this.clbSuppression.MultiColumn = true;
            this.clbSuppression.Name = "clbSuppression";
            this.clbSuppression.Size = new System.Drawing.Size(370, 79);
            this.clbSuppression.TabIndex = 0;
            this.clbSuppression.SelectedIndexChanged += new System.EventHandler(this.clbSuppression_SelectedIndexChanged);
            // 
            // rbIfPlayer
            // 
            this.rbIfPlayer.Location = new System.Drawing.Point(59, 19);
            this.rbIfPlayer.Name = "rbIfPlayer";
            this.rbIfPlayer.Size = new System.Drawing.Size(62, 20);
            this.rbIfPlayer.TabIndex = 88;
            this.rbIfPlayer.Text = "Players";
            this.rbIfPlayer.CheckedChanged += new System.EventHandler(this.rbIfACP_CheckedChanged);
            // 
            // rbIfCritter
            // 
            this.rbIfCritter.Location = new System.Drawing.Point(6, 45);
            this.rbIfCritter.Name = "rbIfCritter";
            this.rbIfCritter.Size = new System.Drawing.Size(60, 20);
            this.rbIfCritter.TabIndex = 87;
            this.rbIfCritter.Text = "Critters";
            this.rbIfCritter.CheckedChanged += new System.EventHandler(this.rbIfACP_CheckedChanged);
            // 
            // rbIfAny
            // 
            this.rbIfAny.Checked = true;
            this.rbIfAny.Location = new System.Drawing.Point(6, 19);
            this.rbIfAny.Name = "rbIfAny";
            this.rbIfAny.Size = new System.Drawing.Size(47, 20);
            this.rbIfAny.TabIndex = 86;
            this.rbIfAny.TabStop = true;
            this.rbIfAny.Text = "Any";
            this.rbIfAny.CheckedChanged += new System.EventHandler(this.rbIfACP_CheckedChanged);
            // 
            // chkFXResistable
            // 
            this.chkFXResistable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFXResistable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkFXResistable.Location = new System.Drawing.Point(3, 81);
            this.chkFXResistable.Name = "chkFXResistable";
            this.chkFXResistable.Size = new System.Drawing.Size(173, 20);
            this.chkFXResistable.TabIndex = 90;
            this.chkFXResistable.Text = "Effect is Unresistible";
            this.chkFXResistable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFXResistable.CheckedChanged += new System.EventHandler(this.chkFXResistable_CheckedChanged);
            // 
            // chkFXBuffable
            // 
            this.chkFXBuffable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFXBuffable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkFXBuffable.Location = new System.Drawing.Point(3, 29);
            this.chkFXBuffable.Name = "chkFXBuffable";
            this.chkFXBuffable.Size = new System.Drawing.Size(173, 20);
            this.chkFXBuffable.TabIndex = 89;
            this.chkFXBuffable.Text = "Ignore Buffs / Enhancements";
            this.chkFXBuffable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFXBuffable.CheckedChanged += new System.EventHandler(this.chkFXBuffable_CheckedChanged);
            // 
            // Label26
            // 
            this.Label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label26.Location = new System.Drawing.Point(3, 158);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(76, 26);
            this.Label26.TabIndex = 101;
            this.Label26.Text = "Probability %:";
            this.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFXProb
            // 
            this.txtFXProb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFXProb.Location = new System.Drawing.Point(85, 161);
            this.txtFXProb.Name = "txtFXProb";
            this.txtFXProb.Size = new System.Drawing.Size(91, 20);
            this.txtFXProb.TabIndex = 85;
            this.txtFXProb.Text = "1";
            this.txtFXProb.TextChanged += new System.EventHandler(this.txtFXProb_TextChanged);
            this.txtFXProb.Leave += new System.EventHandler(this.txtFXProb_Leave);
            // 
            // Label25
            // 
            this.Label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label25.Location = new System.Drawing.Point(3, 132);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(76, 26);
            this.Label25.TabIndex = 100;
            this.Label25.Text = "Delay Time:";
            this.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFXDelay
            // 
            this.txtFXDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFXDelay.Location = new System.Drawing.Point(85, 135);
            this.txtFXDelay.Name = "txtFXDelay";
            this.txtFXDelay.Size = new System.Drawing.Size(91, 20);
            this.txtFXDelay.TabIndex = 84;
            this.txtFXDelay.Text = "0";
            this.txtFXDelay.TextChanged += new System.EventHandler(this.txtFXDelay_TextChanged);
            this.txtFXDelay.Leave += new System.EventHandler(this.txtFXDelay_Leave);
            // 
            // Label24
            // 
            this.Label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label24.Location = new System.Drawing.Point(3, 106);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(76, 26);
            this.Label24.TabIndex = 99;
            this.Label24.Text = "Ticks:";
            this.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFXTicks
            // 
            this.txtFXTicks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFXTicks.Location = new System.Drawing.Point(85, 109);
            this.txtFXTicks.Name = "txtFXTicks";
            this.txtFXTicks.Size = new System.Drawing.Size(91, 20);
            this.txtFXTicks.TabIndex = 83;
            this.txtFXTicks.Text = "0";
            this.txtFXTicks.TextChanged += new System.EventHandler(this.txtFXTicks_TextChanged);
            this.txtFXTicks.Leave += new System.EventHandler(this.txtFXTicks_Leave);
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label23.Location = new System.Drawing.Point(3, 54);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(76, 26);
            this.Label23.TabIndex = 98;
            this.Label23.Text = "Duration:";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFXDuration
            // 
            this.txtFXDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFXDuration.Location = new System.Drawing.Point(85, 57);
            this.txtFXDuration.Name = "txtFXDuration";
            this.txtFXDuration.Size = new System.Drawing.Size(91, 20);
            this.txtFXDuration.TabIndex = 82;
            this.txtFXDuration.Text = "0";
            this.txtFXDuration.TextChanged += new System.EventHandler(this.txtFXDuration_TextChanged);
            this.txtFXDuration.Leave += new System.EventHandler(this.txtFXDuration_Leave);
            // 
            // Label22
            // 
            this.Label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label22.Location = new System.Drawing.Point(3, 80);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(76, 26);
            this.Label22.TabIndex = 97;
            this.Label22.Text = "Magnitude:";
            this.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFXMag
            // 
            this.txtFXMag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFXMag.Location = new System.Drawing.Point(85, 83);
            this.txtFXMag.Name = "txtFXMag";
            this.txtFXMag.Size = new System.Drawing.Size(91, 20);
            this.txtFXMag.TabIndex = 80;
            this.txtFXMag.Text = "0";
            this.txtFXMag.TextChanged += new System.EventHandler(this.txtFXMag_TextChanged);
            this.txtFXMag.Leave += new System.EventHandler(this.txtFXMag_Leave);
            // 
            // Label28
            // 
            this.Label28.Location = new System.Drawing.Point(272, 514);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(82, 20);
            this.Label28.TabIndex = 104;
            this.Label28.Text = "DIsplay Priority:";
            this.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label30
            // 
            this.Label30.Location = new System.Drawing.Point(964, 430);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(92, 20);
            this.Label30.TabIndex = 105;
            this.Label30.Text = "Special Case(s):";
            this.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbFXClass
            // 
            this.cbFXClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFXClass.Location = new System.Drawing.Point(368, 512);
            this.cbFXClass.Name = "cbFXClass";
            this.cbFXClass.Size = new System.Drawing.Size(132, 22);
            this.cbFXClass.TabIndex = 93;
            this.cbFXClass.SelectedIndexChanged += new System.EventHandler(this.cbFXClass_SelectedIndexChanged);
            // 
            // cbFXSpecialCase
            // 
            this.cbFXSpecialCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFXSpecialCase.Location = new System.Drawing.Point(941, 559);
            this.cbFXSpecialCase.Name = "cbFXSpecialCase";
            this.cbFXSpecialCase.Size = new System.Drawing.Size(136, 22);
            this.cbFXSpecialCase.TabIndex = 94;
            this.cbFXSpecialCase.SelectedIndexChanged += new System.EventHandler(this.cbFXSpecialCase_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(1003, 614);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 36);
            this.btnOK.TabIndex = 119;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(928, 614);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 36);
            this.btnCancel.TabIndex = 118;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblEffectDescription
            // 
            this.lblEffectDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEffectDescription.Location = new System.Drawing.Point(8, 11);
            this.lblEffectDescription.Name = "lblEffectDescription";
            this.lblEffectDescription.Size = new System.Drawing.Size(511, 102);
            this.lblEffectDescription.TabIndex = 120;
            this.lblEffectDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEffectDescription.UseMnemonic = false;
            // 
            // chkVariable
            // 
            this.chkVariable.Location = new System.Drawing.Point(512, 362);
            this.chkVariable.Name = "chkVariable";
            this.chkVariable.Size = new System.Drawing.Size(187, 20);
            this.chkVariable.TabIndex = 126;
            this.chkVariable.Text = "Enable Power Scaling (Override)";
            this.chkVariable.CheckedChanged += new System.EventHandler(this.chkVariable_CheckedChanged);
            // 
            // cbPercentageOverride
            // 
            this.cbPercentageOverride.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPercentageOverride.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPercentageOverride.Location = new System.Drawing.Point(85, 3);
            this.cbPercentageOverride.Name = "cbPercentageOverride";
            this.cbPercentageOverride.Size = new System.Drawing.Size(91, 22);
            this.cbPercentageOverride.TabIndex = 127;
            this.cbPercentageOverride.SelectedIndexChanged += new System.EventHandler(this.cbPercentageOverride_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label2.Location = new System.Drawing.Point(3, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(76, 28);
            this.Label2.TabIndex = 128;
            this.Label2.Text = "Percentage:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label1.Location = new System.Drawing.Point(3, 28);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(76, 26);
            this.Label1.TabIndex = 130;
            this.Label1.Text = "Scale:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFXScale
            // 
            this.txtFXScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFXScale.Location = new System.Drawing.Point(85, 31);
            this.txtFXScale.Name = "txtFXScale";
            this.txtFXScale.Size = new System.Drawing.Size(91, 20);
            this.txtFXScale.TabIndex = 129;
            this.txtFXScale.Text = "0";
            this.txtFXScale.TextChanged += new System.EventHandler(this.txtFXScale_TextChanged);
            this.txtFXScale.Leave += new System.EventHandler(this.txtFXScale_Leave);
            // 
            // Label3
            // 
            this.Label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label3.Location = new System.Drawing.Point(3, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(76, 28);
            this.Label3.TabIndex = 132;
            this.Label3.Text = "Affects:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // cbAffects
            // 
            this.cbAffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAffects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAffects.Location = new System.Drawing.Point(85, 3);
            this.cbAffects.Name = "cbAffects";
            this.cbAffects.Size = new System.Drawing.Size(91, 22);
            this.cbAffects.TabIndex = 131;
            this.cbAffects.SelectedIndexChanged += new System.EventHandler(this.cbAffects_SelectedIndexChanged);
            // 
            // Label4
            // 
            this.Label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label4.Location = new System.Drawing.Point(3, 210);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(76, 28);
            this.Label4.TabIndex = 134;
            this.Label4.Text = "AttribType:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbAttribute
            // 
            this.cbAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAttribute.Location = new System.Drawing.Point(85, 213);
            this.cbAttribute.Name = "cbAttribute";
            this.cbAttribute.Size = new System.Drawing.Size(91, 22);
            this.cbAttribute.TabIndex = 133;
            this.cbAttribute.SelectedIndexChanged += new System.EventHandler(this.cbAttribute_SelectedIndexChanged);
            // 
            // Label5
            // 
            this.Label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label5.Location = new System.Drawing.Point(3, 238);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(76, 28);
            this.Label5.TabIndex = 136;
            this.Label5.Text = "Aspect:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbAspect
            // 
            this.cbAspect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAspect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAspect.Location = new System.Drawing.Point(85, 241);
            this.cbAspect.Name = "cbAspect";
            this.cbAspect.Size = new System.Drawing.Size(91, 22);
            this.cbAspect.TabIndex = 135;
            this.cbAspect.SelectedIndexChanged += new System.EventHandler(this.cbAspect_SelectedIndexChanged);
            // 
            // chkNearGround
            // 
            this.chkNearGround.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkNearGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkNearGround.Location = new System.Drawing.Point(3, 107);
            this.chkNearGround.Name = "chkNearGround";
            this.chkNearGround.Size = new System.Drawing.Size(173, 26);
            this.chkNearGround.TabIndex = 139;
            this.chkNearGround.Text = "Target must be Near Ground";
            this.chkNearGround.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAffectsCaster
            // 
            this.lblAffectsCaster.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAffectsCaster.Location = new System.Drawing.Point(85, 28);
            this.lblAffectsCaster.Name = "lblAffectsCaster";
            this.lblAffectsCaster.Size = new System.Drawing.Size(91, 32);
            this.lblAffectsCaster.TabIndex = 140;
            this.lblAffectsCaster.Text = "Power also affects caster";
            this.lblAffectsCaster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvEffectType
            // 
            this.lvEffectType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1});
            this.lvEffectType.FullRowSelect = true;
            this.lvEffectType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvEffectType.HideSelection = false;
            this.lvEffectType.Location = new System.Drawing.Point(275, 559);
            this.lvEffectType.MultiSelect = false;
            this.lvEffectType.Name = "lvEffectType";
            this.lvEffectType.Size = new System.Drawing.Size(225, 68);
            this.lvEffectType.TabIndex = 141;
            this.lvEffectType.UseCompatibleStateImageBehavior = false;
            this.lvEffectType.View = System.Windows.Forms.View.Details;
            this.lvEffectType.SelectedIndexChanged += new System.EventHandler(this.lvEffectType_SelectedIndexChanged);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Effect Attribute";
            this.ColumnHeader1.Width = 202;
            // 
            // lvSubAttribute
            // 
            this.lvSubAttribute.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSub});
            this.lvSubAttribute.FullRowSelect = true;
            this.lvSubAttribute.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSubAttribute.HideSelection = false;
            this.lvSubAttribute.Location = new System.Drawing.Point(668, 488);
            this.lvSubAttribute.MultiSelect = false;
            this.lvSubAttribute.Name = "lvSubAttribute";
            this.lvSubAttribute.Size = new System.Drawing.Size(225, 46);
            this.lvSubAttribute.TabIndex = 142;
            this.lvSubAttribute.UseCompatibleStateImageBehavior = false;
            this.lvSubAttribute.View = System.Windows.Forms.View.Details;
            this.lvSubAttribute.SelectedIndexChanged += new System.EventHandler(this.lvSubAttribute_SelectedIndexChanged);
            // 
            // chSub
            // 
            this.chSub.Text = "Sub-Attribute";
            this.chSub.Width = 203;
            // 
            // lblProb
            // 
            this.lblProb.Location = new System.Drawing.Point(272, 465);
            this.lblProb.Name = "lblProb";
            this.lblProb.Size = new System.Drawing.Size(50, 20);
            this.lblProb.TabIndex = 143;
            this.lblProb.Text = "(100%)";
            this.lblProb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvSubSub
            // 
            this.lvSubSub.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSubSub});
            this.lvSubSub.FullRowSelect = true;
            this.lvSubSub.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSubSub.HideSelection = false;
            this.lvSubSub.Location = new System.Drawing.Point(668, 544);
            this.lvSubSub.MultiSelect = false;
            this.lvSubSub.Name = "lvSubSub";
            this.lvSubSub.Size = new System.Drawing.Size(225, 64);
            this.lvSubSub.TabIndex = 144;
            this.lvSubSub.UseCompatibleStateImageBehavior = false;
            this.lvSubSub.View = System.Windows.Forms.View.Details;
            this.lvSubSub.SelectedIndexChanged += new System.EventHandler(this.lvSubSub_SelectedIndexChanged);
            // 
            // chSubSub
            // 
            this.chSubSub.Text = "Sub-Sub";
            this.chSubSub.Width = 203;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(801, 430);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(10, 20);
            this.Label7.TabIndex = 145;
            this.Label7.Text = "s";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(476, 465);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(13, 14);
            this.Label8.TabIndex = 146;
            this.Label8.Text = "s";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCSV
            // 
            this.btnCSV.Location = new System.Drawing.Point(928, 529);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(150, 26);
            this.btnCSV.TabIndex = 147;
            this.btnCSV.Text = "Import from CSV String";
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(925, 353);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(160, 19);
            this.Label9.TabIndex = 149;
            this.Label9.Text = "GlobalChanceMod Flag:";
            // 
            // cmbEffectId
            // 
            this.cmbEffectId.FormattingEnabled = true;
            this.cmbEffectId.Location = new System.Drawing.Point(927, 369);
            this.cmbEffectId.Name = "cmbEffectId";
            this.cmbEffectId.Size = new System.Drawing.Size(150, 22);
            this.cmbEffectId.TabIndex = 150;
            this.cmbEffectId.TextChanged += new System.EventHandler(this.cmbEffectId_TextChanged);
            // 
            // IgnoreED
            // 
            this.IgnoreED.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IgnoreED.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IgnoreED.Location = new System.Drawing.Point(3, 55);
            this.IgnoreED.Name = "IgnoreED";
            this.IgnoreED.Size = new System.Drawing.Size(173, 20);
            this.IgnoreED.TabIndex = 151;
            this.IgnoreED.Text = "Ignore ED";
            this.IgnoreED.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IgnoreED.CheckedChanged += new System.EventHandler(this.IgnoreED_CheckedChanged);
            // 
            // Label10
            // 
            this.Label10.Location = new System.Drawing.Point(924, 393);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(160, 20);
            this.Label10.TabIndex = 152;
            this.Label10.Text = "Override:";
            // 
            // txtOverride
            // 
            this.txtOverride.Location = new System.Drawing.Point(927, 407);
            this.txtOverride.Name = "txtOverride";
            this.txtOverride.Size = new System.Drawing.Size(150, 20);
            this.txtOverride.TabIndex = 153;
            this.txtOverride.TextChanged += new System.EventHandler(this.txtOverride_TextChanged);
            // 
            // Label11
            // 
            this.Label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label11.Location = new System.Drawing.Point(3, 184);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(76, 26);
            this.Label11.TabIndex = 155;
            this.Label11.Text = "PPM:";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPPM
            // 
            this.txtPPM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPPM.Location = new System.Drawing.Point(85, 187);
            this.txtPPM.Name = "txtPPM";
            this.txtPPM.Size = new System.Drawing.Size(91, 20);
            this.txtPPM.TabIndex = 154;
            this.txtPPM.Text = "0";
            this.txtPPM.TextChanged += new System.EventHandler(this.txtPPM_TextChanged);
            this.txtPPM.Leave += new System.EventHandler(this.txtPPM_Leave);
            // 
            // cbFXSpecialCase2
            // 
            this.cbFXSpecialCase2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFXSpecialCase2.Location = new System.Drawing.Point(942, 587);
            this.cbFXSpecialCase2.Name = "cbFXSpecialCase2";
            this.cbFXSpecialCase2.Size = new System.Drawing.Size(136, 22);
            this.cbFXSpecialCase2.TabIndex = 156;
            this.cbFXSpecialCase2.SelectedIndexChanged += new System.EventHandler(this.cbFXSpecialCase2_SelectedIndexChanged);
            // 
            // cbFXOperator
            // 
            this.cbFXOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFXOperator.Location = new System.Drawing.Point(994, 667);
            this.cbFXOperator.Name = "cbFXOperator";
            this.cbFXOperator.Size = new System.Drawing.Size(62, 22);
            this.cbFXOperator.TabIndex = 157;
            this.cbFXOperator.SelectedIndexChanged += new System.EventHandler(this.cbFXOperator_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbIfAny);
            this.groupBox1.Controls.Add(this.rbIfCritter);
            this.groupBox1.Controls.Add(this.rbIfPlayer);
            this.groupBox1.Location = new System.Drawing.Point(600, 663);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 69);
            this.groupBox1.TabIndex = 158;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "If Target =";
            // 
            // dGVActive
            // 
            this.dGVActive.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dGVActive.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dGVActive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVActive.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dGVActive.Location = new System.Drawing.Point(416, 14);
            this.dGVActive.Name = "dGVActive";
            this.dGVActive.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dGVActive.RowTemplate.ReadOnly = true;
            this.dGVActive.Size = new System.Drawing.Size(265, 200);
            this.dGVActive.TabIndex = 159;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Power Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Active";
            this.Column2.Items.AddRange(new object[] {
            "True",
            "False"});
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Target";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvPowerType);
            this.groupBox2.Controls.Add(this.lvPower);
            this.groupBox2.Controls.Add(this.dGVActive);
            this.groupBox2.Location = new System.Drawing.Point(928, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(687, 220);
            this.groupBox2.TabIndex = 160;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conditionals";
            // 
            // lvPowerType
            // 
            this.lvPowerType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvPowerType.FullRowSelect = true;
            this.lvPowerType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPowerType.HideSelection = false;
            this.lvPowerType.Location = new System.Drawing.Point(6, 14);
            this.lvPowerType.MultiSelect = false;
            this.lvPowerType.Name = "lvPowerType";
            this.lvPowerType.Size = new System.Drawing.Size(179, 200);
            this.lvPowerType.TabIndex = 161;
            this.lvPowerType.UseCompatibleStateImageBehavior = false;
            this.lvPowerType.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Effect Attribute";
            this.columnHeader3.Width = 158;
            // 
            // lvPower
            // 
            this.lvPower.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvPower.FullRowSelect = true;
            this.lvPower.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPower.HideSelection = false;
            this.lvPower.Location = new System.Drawing.Point(191, 14);
            this.lvPower.MultiSelect = false;
            this.lvPower.Name = "lvPower";
            this.lvPower.Size = new System.Drawing.Size(219, 200);
            this.lvPower.TabIndex = 160;
            this.lvPower.UseCompatibleStateImageBehavior = false;
            this.lvPower.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Effect Attribute";
            this.columnHeader2.Width = 202;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Controls.Add(this.Label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbPercentageOverride, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFXScale, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label11, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.Label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label22, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtPPM, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.Label23, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtFXDuration, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label24, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtFXMag, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtFXTicks, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label25, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtFXDelay, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Label26, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtFXProb, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.Label4, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.cbAttribute, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.Label5, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.cbAspect, 1, 9);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 116);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(179, 275);
            this.tableLayoutPanel1.TabIndex = 161;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.cbModifier, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 393);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.55814F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.44186F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(179, 43);
            this.tableLayoutPanel2.TabIndex = 162;
            // 
            // cbModifier
            // 
            this.cbModifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModifier.Location = new System.Drawing.Point(3, 17);
            this.cbModifier.Name = "cbModifier";
            this.cbModifier.Size = new System.Drawing.Size(173, 22);
            this.cbModifier.TabIndex = 137;
            this.cbModifier.SelectedIndexChanged += new System.EventHandler(this.cbModifier_SelectedIndexChanged);
            // 
            // Label6
            // 
            this.Label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label6.Location = new System.Drawing.Point(3, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(173, 14);
            this.Label6.TabIndex = 138;
            this.Label6.Text = "Modifier Table:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel3.Controls.Add(this.lblAffectsCaster, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbAffects, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.Label3, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(8, 442);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 13;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(179, 63);
            this.tableLayoutPanel3.TabIndex = 163;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.chkStack, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.chkFXBuffable, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.IgnoreED, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.chkFXResistable, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.chkNearGround, 0, 4);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(8, 544);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(179, 136);
            this.tableLayoutPanel4.TabIndex = 164;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(8, 505);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(179, 33);
            this.tableLayoutPanel5.TabIndex = 165;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "Any",
            "Mob(s)",
            "Player(s)"});
            this.comboBox1.Location = new System.Drawing.Point(92, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(84, 22);
            this.comboBox1.TabIndex = 166;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 33);
            this.label12.TabIndex = 166;
            this.label12.Text = "Target:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmPowerEffect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 744);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Label28);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.cbFXClass);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbFXOperator);
            this.Controls.Add(this.cbFXSpecialCase2);
            this.Controls.Add(this.txtOverride);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.lblProb);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.cmbEffectId);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.btnCSV);
            this.Controls.Add(this.lvSubSub);
            this.Controls.Add(this.lvSubAttribute);
            this.Controls.Add(this.lvEffectType);
            this.Controls.Add(this.chkVariable);
            this.Controls.Add(this.lblEffectDescription);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.Label30);
            this.Controls.Add(this.cbFXSpecialCase);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPowerEffect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Effect";
            this.GroupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGVActive)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        Button btnCancel;
        Button btnCopy;
        Button btnCSV;
        Button btnOK;
        Button btnPaste;
        ComboBox cbAffects;
        ComboBox cbAspect;
        ComboBox cbAttribute;
        ComboBox cbFXClass;
        ComboBox cbFXSpecialCase;
        ComboBox cbPercentageOverride;
        CheckBox chkFXBuffable;
        CheckBox chkFXResistable;
        CheckBox chkNearGround;
        CheckBox chkStack;
        CheckBox chkVariable;
        ColumnHeader chSub;
        ColumnHeader chSubSub;
        CheckedListBox clbSuppression;
        ComboBox cmbEffectId;
        ColumnHeader ColumnHeader1;
        GroupBox GroupBox3;
        CheckBox IgnoreED;
        Label Label1;
        Label Label10;
        Label Label11;
        Label Label2;
        Label Label22;
        Label Label23;
        Label Label24;
        Label Label25;
        Label Label26;
        Label Label28;
        Label Label3;
        Label Label30;
        Label Label4;
        Label Label5;
        Label Label7;
        Label Label8;
        Label Label9;
        Label lblAffectsCaster;
        Label lblEffectDescription;
        Label lblProb;
        ListView lvEffectType;
        ListView lvSubAttribute;
        ListView lvSubSub;
        RadioButton rbIfAny;
        RadioButton rbIfCritter;
        RadioButton rbIfPlayer;
        TextBox txtFXDelay;
        TextBox txtFXDuration;
        TextBox txtFXMag;
        TextBox txtFXProb;
        TextBox txtFXScale;
        TextBox txtFXTicks;
        TextBox txtOverride;
        TextBox txtPPM;
        private ComboBox cbFXSpecialCase2;
        private ComboBox cbFXOperator;
        private GroupBox groupBox1;
        private DataGridView dGVActive;
        private GroupBox groupBox2;
        private ListView lvPowerType;
        private ColumnHeader columnHeader3;
        private ListView lvPower;
        private ColumnHeader columnHeader2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewComboBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private ComboBox cbModifier;
        private Label Label6;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private ComboBox comboBox1;
        private Label label12;
    }
}