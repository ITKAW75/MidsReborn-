﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Mids_Reborn.Controls;
using Mids_Reborn.Core;
using Mids_Reborn.Core.Base.Master_Classes;
using Mids_Reborn.Forms.Controls;
using MRBResourceLib;
using Newtonsoft.Json;

namespace Mids_Reborn.Forms.OptionsMenuItems.DbEditor
{
    public partial class FrmGCMEditor : Form
    {
        private readonly frmMain _myParent;
        private frmBusy _bFrm;

		private void BusyHide()
		{
	        if (_bFrm == null) return;
	        _bFrm.Close();
        }

        private void BusyMsg(string sMessage)
        {
	        _bFrm.SetMessage(sMessage);
            _bFrm.Show(this);
        }

		public FrmGCMEditor(ref frmMain iParent)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            Load += frmGCMEditor_Load;
            InitializeComponent();
            Icon = Resources.MRB_Icon_Concept;
            _myParent = iParent;
            _bFrm = new frmBusy();
        }

        private void frmGCMEditor_Load(object sender, EventArgs e)
        {
            CenterToParent();
            PopulateInfo();
        }

        private void PopulateInfo()
        {
            lvModifiers.BeginUpdate();
            lvModifiers.Items.Clear();
            foreach (var effectId in DatabaseAPI.Database.EffectIds)
            {
                lvModifiers.Items.Add(effectId);
            }

            lvModifiers.Columns[0].Text = @"Current Modifiers";
            lvModifiers.EndUpdate();
        }

        private void ListView_Leave(object sender, EventArgs e)
        {
	        var lvControl = (ctlListViewColored)sender;
	        lvControl.LostFocusItem = lvControl.FocusedItem.Index;
        }

        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
	        e.DrawDefault = true;
        }

        private void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
	        var lvControl = (ctlListViewColored)sender;
	        if (e.Item.Selected)
	        {
		        if (lvControl.LostFocusItem == e.Item.Index)
		        {
			        e.Item.BackColor = Color.Goldenrod;
			        e.Item.ForeColor = Color.Black;
			        lvControl.LostFocusItem = -1;
		        }
		        else if (lvControl.Focused)
		        {
			        e.Item.ForeColor = SystemColors.HighlightText;
			        e.Item.BackColor = SystemColors.Highlight;
		        }
	        }
	        else
	        {
		        e.Item.BackColor = lvControl.BackColor;
		        e.Item.ForeColor = lvControl.ForeColor;
	        }
	        e.DrawBackground();
	        e.DrawText();
        }

        private void btnRemoveMod_Click(object sender, EventArgs e)
        {
	        DatabaseAPI.Database.EffectIds.Remove(lvModifiers.SelectedItems[0].Text);
            lvModifiers.SelectedItems[0].Remove();
        }

        private void btnAddMod_Click(object sender, EventArgs e)
        {
	        var newGCM = string.Empty;
	        InputBoxResult result = InputBox.Show("Enter the modifier you wish to add.", "Add Modifier", false, "Enter the modifier here", InputBox.InputBoxIcon.Info, inputBox_Validating);
	        if (result.OK) { newGCM = result.Text; }
            DatabaseAPI.Database.EffectIds.Add(newGCM);
            lvModifiers.Items.Add(newGCM);
        }

        private void btnImportMods_Click(object sender, EventArgs e)
        {
	        BusyMsg("Importing Global Chance Modifiers from JSON...");
            lvModifiers.Items.Clear();
	        var effects = new List<string>();
            var fileImportDialog = new OpenFileDialog
            {
                InitialDirectory = $"{Application.StartupPath}\\Data\\",
                Title = @"Select JSON formatted GCM file",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "json",
                Filter = @"JSON files (*.json)|*.json",
                FilterIndex = 1,
                RestoreDirectory = true,
                ShowReadOnly = true
            };
            if (fileImportDialog.ShowDialog() == DialogResult.OK)
            {
	            var jsonText = File.ReadAllText(fileImportDialog.FileName);
                effects = JsonConvert.DeserializeObject<List<string>>(jsonText, Serializer.SerializerSettings);
            }
            DatabaseAPI.Database.EffectIds = effects;
			PopulateInfo();
			BusyHide();
        }

        private void btnExportMods_Click(object sender, EventArgs e)
        {
	        BusyMsg("Exporting Global Chance Modifiers to JSON...");
            var path = $"{Application.StartupPath}\\Data\\GlobalMods.json";
            File.WriteAllText(path, JsonConvert.SerializeObject(DatabaseAPI.Database.EffectIds, Formatting.Indented));
            BusyHide();
        }

		private void btnSave_Click(object sender, EventArgs e)
		{
			BusyMsg("Saving EffectIds...");
			DatabaseAPI.SaveEffectIdsDatabase(MidsContext.Config.SavePath);
			BusyHide();
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Hide();
        }

		private static void inputBox_Validating(object sender, InputBoxValidatingArgs e)
		{
			if (e.Text.Trim().Length != 0) return;
			e.Cancel = true;
			e.Message = "Required";
		}
    }
}
