﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Mids_Reborn.Core;
using Mids_Reborn.Core.Base.Master_Classes;

namespace Mids_Reborn.Forms.ImportExportItems
{
    public partial class frmForumExport : Form
    {
        private List<ForumColorTheme> ForumThemes;
        private Dictionary<string, TagsFormatType> FormatTypes;
        private ForumColorsHex ThemeColorsHex;

        public frmForumExport()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var includeAccolades = chkOptAccolades.Checked;
            var includeIncarnates = chkOptIncarnates.Checked;
            var longFormat = chkOptLongFormat.Checked;
            var formatType = lbFormatCodeType.SelectedIndex >= 0
                ? FormatTypes[lbFormatCodeType.Items[lbFormatCodeType.SelectedIndex].ToString()]
                : TagsFormatType.None;
            var themes = ForumColorThemes.GetThemes();
            var activeTheme = themes.First(e => e.Name == lbColorTheme.Items[lbColorTheme.SelectedIndex >= 0 ? lbColorTheme.SelectedIndex : 0].ToString());
            var tg = new TagGenerator(formatType, activeTheme);

            var txt = tg.Header($"{(string.IsNullOrWhiteSpace(MidsContext.Character.Name) ? "" : $"{MidsContext.Character.Name} - ")}{MidsContext.Character.Powersets[0]?.DisplayName} / {MidsContext.Character.Powersets[1]?.DisplayName} {MidsContext.Character.Alignment} {MidsContext.Character.Archetype.DisplayName}");
            var appVersionChunks = MidsContext.AssemblyFileVersion.Split('.');
            var appVersion = appVersionChunks.Length < 4
                ? MidsContext.AssemblyVersion
                : $"{appVersionChunks[0]}.{appVersionChunks[1]}.{appVersionChunks[2]} rev. {appVersionChunks[3]}";

            txt += tg.Size(6,
                tg.Bold(tg.Color(activeTheme.Text,
                    $"{(string.IsNullOrWhiteSpace(MidsContext.Character.Name) ? "" : $"{MidsContext.Character.Name} - ")}{MidsContext.Character.Alignment} {MidsContext.Character.Archetype.DisplayName}")));
            txt += tg.BlankLine();
            txt += tg.Size(4, tg.Color(activeTheme.Text, tg.Italic($"Build plan made with {MidsContext.AppName} v{appVersion}")));
            
            txt += tg.SeparatorLine();

            txt += tg.List();
            txt += tg.ListItem(tg.Bold(tg.Color(activeTheme.Headings, "Primary powerset: ") + tg.Color(activeTheme.Text, MidsContext.Character.Powersets[0]?.DisplayName)));
            txt += tg.ListItem(tg.Bold(tg.Color(activeTheme.Headings, "Secondary powerset: ") + tg.Color(activeTheme.Text, MidsContext.Character.Powersets[1]?.DisplayName)));

            var k = 1;
            for (var i = 3; i < 7; i++)
            {
                if (MidsContext.Character.Powersets[i] == null)
                {
                    continue;
                }

                txt += tg.ListItem(tg.Bold(tg.Color(activeTheme.Headings, $"Pool powerset (#{k++}): ") + tg.Color(activeTheme.Text, MidsContext.Character.Powersets[i]?.DisplayName)));
            }

            if (MidsContext.Character.Powersets[7] != null)
            {
                txt += tg.ListItem(tg.Bold(tg.Color(activeTheme.Headings, $"{(MidsContext.Character.Alignment is Enums.Alignment.Hero or Enums.Alignment.Vigilante or Enums.Alignment.Resistance ? "Epic" : "Ancillary")} powerset: ") + tg.Color(activeTheme.Text, MidsContext.Character.Powersets[7]?.DisplayName)));
            }

            txt += tg.List(true);
            txt += tg.SeparatorLine();

            txt += tg.Size(6, tg.Color(activeTheme.Text, tg.Bold("Powers taken:")));
            txt += tg.BlankLine() + tg.BlankLine();
            foreach (var pe in MidsContext.Character.CurrentBuild.Powers)
            {
                if (pe?.Power == null)
                {
                    continue;
                }

                if (pe.Power.InherentType != Enums.eGridType.None)
                {
                    continue;
                }

                if (pe.Power.GetPowerSet()?.FullName.StartsWith("Incarnate.") == true)
                {
                    continue;
                }

                txt += tg.Bold(tg.Color(activeTheme.Levels, $"Level {pe.Level + 1}: ") + tg.Color(activeTheme.Text, pe.Power.DisplayName));
                txt += tg.BlankLine();
                //if (longFormat && pe.Slots.Length > 0)
                if (pe.Slots.Length > 0)
                {
                    txt += tg.List();
                    for (var i = 0; i < pe.Slots.Length; i++)
                    {
                        txt += tg.ListItem(tg.Color(activeTheme.Slots, pe.Slots[i].Enhancement.Enh < 0
                            ? "(Empty)"
                            : $"{tg.Color(activeTheme.Levels, pe.Slots[i].Level <= pe.Level ? "A" : $"{pe.Slots[i].Level + 1}")}: {tg.Color(activeTheme.Slots, DatabaseAPI.Database.Enhancements[pe.Slots[i].Enhancement.Enh].LongName)}"));
                    }

                    txt += tg.List(true);
                }

                txt += tg.BlankLine() + tg.BlankLine();
            }

            txt += tg.SeparatorLine();

            txt += tg.Size(6, tg.Color(activeTheme.Text, tg.Bold("Inherents:")));
            txt += tg.BlankLine() + tg.BlankLine();
            foreach (var pe in MidsContext.Character.CurrentBuild.Powers)
            {
                if (pe?.Power == null)
                {
                    continue;
                }

                if (pe.Power.InherentType == Enums.eGridType.None)
                {
                    continue;
                }

                if (pe.Power.GetPowerSet()?.FullName.StartsWith("Incarnate.") == true)
                {
                    continue;
                }

                if (pe.Power.GetPowerSet()?.FullName.StartsWith("Temporary_Powers.Accolades") == true)
                {
                    continue;
                }

                txt += tg.Bold(tg.Color(activeTheme.Levels, $"Level {pe.Level + 1}: ") +
                               tg.Color(activeTheme.Text, pe.Power.DisplayName));
                txt += tg.BlankLine();
                //if (longFormat && pe.Slots.Length > 0)
                if (pe.Slots.Length > 0)
                {
                    txt += tg.List();
                    for (var i = 0; i < pe.Slots.Length; i++)
                    {
                        txt += tg.ListItem(tg.Color(activeTheme.Slots, pe.Slots[i].Enhancement.Enh < 0
                            ? "(Empty)"
                            : $"{tg.Color(activeTheme.Levels, pe.Slots[i].Level <= pe.Level ? "A" : $"{pe.Slots[i].Level + 1}")}: {tg.Color(activeTheme.Slots, DatabaseAPI.Database.Enhancements[pe.Slots[i].Enhancement.Enh].LongName)}"));
                    }

                    txt += tg.List(true);
                }

                txt += tg.BlankLine() + tg.BlankLine();
            }

            if (includeAccolades)
            {
                k = 0;
                foreach (var pe in MidsContext.Character.CurrentBuild.Powers)
                {
                    if (pe?.Power == null)
                    {
                        continue;
                    }

                    if (pe.Power.GetPowerSet()?.FullName.StartsWith("Temporary_Powers.Accolades") != true)
                    {
                        continue;
                    }

                    if (k++ == 0)
                    {
                        txt += tg.SeparatorLine();
                        txt += tg.Size(6, tg.Color(activeTheme.Text, tg.Bold("Accolades:")));
                        txt += tg.BlankLine() + tg.BlankLine();
                    }
                    
                    txt += tg.Bold(tg.Color(activeTheme.Text, pe.Power.DisplayName));
                    txt += tg.BlankLine();
                    
                }

                if (k > 0)
                {
                    txt += tg.BlankLine() + tg.BlankLine();
                }
            }

            if (includeIncarnates)
            {
                k = 0;
                foreach (var pe in MidsContext.Character.CurrentBuild.Powers)
                {
                    if (pe?.Power == null)
                    {
                        continue;
                    }

                    if (pe.Power.GetPowerSet()?.FullName.StartsWith("Incarnate.") != true)
                    {
                        continue;
                    }

                    if (k++ == 0)
                    {
                        txt += tg.SeparatorLine();
                        txt += tg.Size(6, tg.Color(activeTheme.Text, tg.Bold("Incarnates:")));
                        txt += tg.BlankLine() + tg.BlankLine();
                    }

                    txt += tg.Bold(tg.Color(activeTheme.Text, pe.Power.DisplayName));
                }
            }

            txt += tg.BlankLine() + tg.BlankLine();
            txt += tg.Footer();

            Clipboard.SetText(txt);

            MessageBox.Show("Build has been exported and placed into the clipboard.", "Info", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lbColorTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbColorTheme.SelectedIndex < 0)
            {
                return;
            }

            var theme = ForumThemes.First(e => e.Name == lbColorTheme.Items[lbColorTheme.SelectedIndex].ToString());
            ThemeColorsHex = new ForumColorsHex
            {
                Text = ForumColorThemes.ColorToHex(theme.Text),
                Headings = ForumColorThemes.ColorToHex(theme.Headings),
                Levels = ForumColorThemes.ColorToHex(theme.Levels),
                Slots = ForumColorThemes.ColorToHex(theme.Slots)
            };

            panelColorTitle.BackColor = theme.Text;
            panelColorHeadings.BackColor = theme.Headings;
            panelColorLevels.BackColor = theme.Levels;
            panelColorSlots.BackColor = theme.Slots;
        }

        private void rbLightThemes_CheckedChanged(object sender, EventArgs e)
        {
            lbColorTheme.BeginUpdate();
            lbColorTheme.Items.Clear();
            foreach (var theme in ForumThemes)
            {
                if (theme.DarkTheme)
                {
                    continue;
                }

                lbColorTheme.Items.Add(theme.Name);
            }

            lbColorTheme.EndUpdate();
            lbColorTheme.SetSelected(0, true);
        }

        private void rbDarkThemes_CheckedChanged(object sender, EventArgs e)
        {
            lbColorTheme.BeginUpdate();
            lbColorTheme.Items.Clear();
            foreach (var theme in ForumThemes)
            {
                if (!theme.DarkTheme)
                {
                    continue;
                }

                lbColorTheme.Items.Add(theme.Name);
            }

            lbColorTheme.EndUpdate();
            lbColorTheme.SetSelected(0, true);
        }

        private void rbAllThemes_CheckedChanged(object sender, EventArgs e)
        {
            lbColorTheme.BeginUpdate();
            lbColorTheme.Items.Clear();
            foreach (var theme in ForumThemes)
            {
                lbColorTheme.Items.Add(theme.Name);
            }

            lbColorTheme.EndUpdate();
            lbColorTheme.SetSelected(0, true);
        }

        private void frmForumExport_Load(object sender, EventArgs e)
        {
            FormatTypes = new Dictionary<string, TagsFormatType>
            {
                {"HTML" , TagsFormatType.HTML},
                {"BBCode", TagsFormatType.BBCode},
                {"Markdown", TagsFormatType.Markdown},
                {"No Codes", TagsFormatType.None}
            };

            ForumThemes = ForumColorThemes.GetThemes();
            var themeNames = ForumThemes.Select(e => e.Name).ToList();

            lbColorTheme.BeginUpdate();
            lbColorTheme.Items.Clear();
            foreach (var th in themeNames)
            {
                lbColorTheme.Items.Add(th);
            }

            lbColorTheme.EndUpdate();
            lbColorTheme.SetSelected(0, true);

            lbFormatCodeType.BeginUpdate();
            lbFormatCodeType.Items.Clear();
            foreach (var type in FormatTypes)
            {
                lbFormatCodeType.Items.Add(type.Key);
            }

            lbFormatCodeType.EndUpdate();
            lbFormatCodeType.SetSelected(0, true);

            rbAllThemes.Checked = true;
        }
    }
}
