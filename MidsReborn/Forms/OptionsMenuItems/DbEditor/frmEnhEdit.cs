using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mids_Reborn.Core;
using Mids_Reborn.Core.Base.Display;
using Mids_Reborn.Core.Base.Extensions;
using Mids_Reborn.Core.Base.Master_Classes;
using MRBResourceLib;

namespace Mids_Reborn.Forms.OptionsMenuItems.DbEditor
{
    public partial class frmEnhEdit : Form
    {
        private frmBusy _bFrm;
        private frmMain _frmMain;

        public frmEnhEdit()
        {
            Load += frmEnhEdit_Load;
            InitializeComponent();
            Name = nameof(frmEnhEdit);
            Icon = Resources.MRB_Icon_Concept;
        }

        private void AddListItem(int index)
        {
            var enhancement = DatabaseAPI.Database.Enhancements[index];
            var item = new[]
            {
                $"{enhancement.Name} ({enhancement.ShortName}) - {enhancement.StaticIndex}",
                Enum.GetName(typeof(Enums.eType), enhancement.TypeID),
                $"{enhancement.LevelMin + 1}-{enhancement.LevelMax + 1}",
                $"{enhancement.Effect.Length}",
                string.Join(", ", enhancement.ClassID.Select(c => DatabaseAPI.Database.EnhancementClasses[c].ShortName)),
                "",
                enhancement.UID,
                enhancement.LongName
            };

            if (enhancement.nIDSet > -1)
            {
                item[5] = DatabaseAPI.Database.EnhancementSets[enhancement.nIDSet].DisplayName;
                item[0] = $"{item[5]}: {item[0]}";
            }

            var lvi = new ListViewItem(item, index)
            {
                ToolTipText = item[0]
            };
            
            lvEnh.Items.Add(lvi);
            lvEnh.Items[lvEnh.Items.Count - 1].Selected = true;
            lvEnh.Items[lvEnh.Items.Count - 1].EnsureVisible();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IEnhancement iEnh = new Enhancement();
            using var frmEnhData = new frmEnhData(ref iEnh, DatabaseAPI.Database.Enhancements[DatabaseAPI.Database.Enhancements.Length - 1].StaticIndex + 1);
            frmEnhData.ShowDialog();
            if (frmEnhData.DialogResult != DialogResult.OK)
            {
                return;
            }
            
            var dbEnhancements = DatabaseAPI.Database.Enhancements.ToList();
            var newEnhancement = new Enhancement(frmEnhData.myEnh) { IsNew = true };
            dbEnhancements.Add(newEnhancement);
            DatabaseAPI.Database.Enhancements = dbEnhancements.ToArray();
            if (newEnhancement.nIDSet > 0)
            {
                var es = DatabaseAPI.Database.EnhancementSets[newEnhancement.nIDSet];
                var setEnhancements = es.Enhancements.ToList();
                setEnhancements.Add(newEnhancement.StaticIndex);
                es.Enhancements = setEnhancements.ToArray();
            }

            ImageUpdate();
            AddListItem(DatabaseAPI.Database.Enhancements.Length - 1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            if (lvEnh.SelectedIndices.Count <= 0)
                return;
            var oldEnhancement = DatabaseAPI.Database.Enhancements[DatabaseAPI.GetEnhancementByUIDName(lvEnh.SelectedItems[0].SubItems[6].Text)];
            using var frmEnhData = new frmEnhData(ref oldEnhancement, DatabaseAPI.Database.Enhancements[DatabaseAPI.Database.Enhancements.Length].StaticIndex + 1);
            frmEnhData.ShowDialog();
            if (frmEnhData.DialogResult != DialogResult.OK)
            {
                return;
            }

            var dbEnhancements = DatabaseAPI.Database.Enhancements.ToList();
            var newEnhancement = new Enhancement(frmEnhData.myEnh) { IsNew = true, StaticIndex = -1 };
            dbEnhancements.Add(newEnhancement);
            DatabaseAPI.Database.Enhancements = dbEnhancements.ToArray();
            ImageUpdate();
            AddListItem(DatabaseAPI.Database.Enhancements.Length - 1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvEnh.SelectedIndices.Count <= 0 || MessageBox.Show($"Really delete enhancement: {lvEnh.SelectedItems[0].Text}?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            var enhancementArray = new Enhancement[DatabaseAPI.Database.Enhancements.Length];
            var selectedIndex = DatabaseAPI.GetEnhancementByUIDName(lvEnh.SelectedItems[0].SubItems[6].Text);
            var index1 = 0;
            var enh = DatabaseAPI.Database.Enhancements[selectedIndex];
            if (enh.nIDSet > -1)
            {
                //Remove it from the enhancement set too.

                DatabaseAPI.Database.EnhancementSets[enh.nIDSet].Enhancements = DatabaseAPI.Database.EnhancementSets[enh.nIDSet].Enhancements.Where(staticIndex => staticIndex != enh.StaticIndex).ToArray();
            }

            for (var index2 = 0; index2 < DatabaseAPI.Database.Enhancements.Length; index2++)
            {
                if (index2 == selectedIndex)
                {
                    continue;
                }

                enhancementArray[index1] = new Enhancement(DatabaseAPI.Database.Enhancements[index2]);
                ++index1;
            }

            DatabaseAPI.Database.Enhancements = new IEnhancement[DatabaseAPI.Database.Enhancements.Length - 1];
            for (var index2 = 0; index2 < DatabaseAPI.Database.Enhancements.Length; index2++)
            {
                DatabaseAPI.Database.Enhancements[index2] = new Enhancement(enhancementArray[index2]);
            }

            DisplayList();
            if (lvEnh.Items.Count <= 0)
            {
                return;
            }

            if (lvEnh.Items.Count > selectedIndex)
            {
                lvEnh.Items[selectedIndex].Selected = true;
                lvEnh.Items[selectedIndex].EnsureVisible();
            }
            else if (lvEnh.Items.Count == selectedIndex)
            {
                lvEnh.Items[selectedIndex - 1].Selected = true;
                lvEnh.Items[selectedIndex - 1].EnsureVisible();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lvEnh.SelectedIndices.Count <= 0)
            {
                return;
            }

            var enhIndex = DatabaseAPI.GetEnhancementByUIDName(lvEnh.SelectedItems[0].SubItems[6].Text);
            var selectedIndex = DatabaseAPI.GetEnhancementByUIDName(lvEnh.SelectedItems[0].SubItems[6].Text);
            if (selectedIndex >= lvEnh.Items.Count - 1)
            {
                return;
            }

            IEnhancement[] enhancementArray =
            {
                new Enhancement(DatabaseAPI.Database.Enhancements[enhIndex]),
                new Enhancement(DatabaseAPI.Database.Enhancements[enhIndex + 1])
            };

            DatabaseAPI.Database.Enhancements[enhIndex + 1] = new Enhancement(enhancementArray[0]);
            DatabaseAPI.Database.Enhancements[enhIndex] = new Enhancement(enhancementArray[1]);
            DisplayList();
            lvEnh.Items[selectedIndex + 1].Selected = true;
            lvEnh.Items[selectedIndex + 1].EnsureVisible();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvEnh.SelectedIndices.Count <= 0)
            {
                return;
            }

            //Console.WriteLine(lvEnh.SelectedItems[0].SubItems[6].Text);
            var selectedIndex = DatabaseAPI.GetEnhancementByUIDName(lvEnh.SelectedItems[0].SubItems[6].Text);
            //var selectedIndex = DatabaseAPI.GetEnhancementByName(lvEnh.SelectedItems[0].SubItems[6].Text);
            using var frmEnhData = new frmEnhData(ref DatabaseAPI.Database.Enhancements[selectedIndex], 0);
            frmEnhData.ShowDialog();
            if (frmEnhData.DialogResult != DialogResult.OK)
            {
                return;
            }

            var newEnhancement = new Enhancement(frmEnhData.myEnh) { IsModified = true };
            DatabaseAPI.Database.Enhancements[lvEnh.SelectedIndices[0]] = newEnhancement;
            ImageUpdate();
            UpdateListItem(selectedIndex);
        }

        private void BusyHide()
        {
            _bFrm.Close();
        }

        private void BusyMsg(string sMessage)
        {
            _bFrm.SetMessage(sMessage);
            _bFrm.Show(this);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            BusyMsg("Saving, please wait...");
            await I9Gfx.LoadEnhancements();
            foreach (var power in DatabaseAPI.Database.Power)
            {
                power.BaseRechargeTime = power.RechargeTime;
            }

            Array.Sort(DatabaseAPI.Database.Power);
            var serializer = Serializer.GetSerializer();
            DatabaseAPI.AssignStaticIndexValues(serializer, false);
            DatabaseAPI.AssignRecipeIDs();
            DatabaseAPI.SaveEnhancementDb(serializer, MidsContext.Config.DataPath);
            DatabaseAPI.MatchAllIDs();
            Task.Delay(1000).Wait();
            DatabaseAPI.SaveMainDatabase(serializer, MidsContext.Config.DataPath);
            frmMain.MainInstance.UpdateTitle();
            BusyHide();
            DialogResult = DialogResult.OK;
            Hide();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lvEnh.SelectedIndices.Count <= 0)
                return;
            var enhIndex = DatabaseAPI.GetEnhancementByUIDName(lvEnh.SelectedItems[0].SubItems[6].Text);
            var selectedIndex = lvEnh.SelectedIndices[0];
            if (selectedIndex < 1)
                return;
            IEnhancement[] enhancementArray =
            {
                new Enhancement(DatabaseAPI.Database.Enhancements[enhIndex]),
                new Enhancement(DatabaseAPI.Database.Enhancements[enhIndex - 1])
            };
            DatabaseAPI.Database.Enhancements[enhIndex - 1] = new Enhancement(enhancementArray[0]);
            DatabaseAPI.Database.Enhancements[enhIndex] = new Enhancement(enhancementArray[1]);
            DisplayList();
            lvEnh.Items[selectedIndex - 1].Selected = true;
            lvEnh.Items[selectedIndex - 1].EnsureVisible();
        }

        private void DisplayList()
        {
            ImageUpdate();
            lvEnh.BeginUpdate();
            lvEnh.Items.Clear();
            var num = DatabaseAPI.Database.Enhancements.Length - 1;
            for (var index = 0; index <= num; ++index)
            {
                /*if (string.IsNullOrEmpty(txtFilter.Text) || DatabaseAPI.Database.Enhancements[index].Name
                    .ToUpper(CultureInfo.InvariantCulture)
                    .Contains(txtFilter.Text.ToUpper(CultureInfo.InvariantCulture)))
                {
                    AddListItem(index);
                    continue;
                }

                if (DatabaseAPI.Database.Enhancements[index].nIDSet <= -1) continue;

                if (DatabaseAPI.Database.EnhancementSets[DatabaseAPI.Database.Enhancements[index].nIDSet]
                    .DisplayName.ToUpper(CultureInfo.InvariantCulture)
                    .Contains(txtFilter.Text.ToUpper(CultureInfo.InvariantCulture)))
                    AddListItem(index);
                */
                if (string.IsNullOrEmpty(txtFilter.Text) ||
                    DatabaseAPI.Database.Enhancements[index].nIDSet >= 0 &
                    DatabaseAPI.Database.EnhancementSets[DatabaseAPI.Database.Enhancements[index].nIDSet]
                        .DisplayName.ToUpper(CultureInfo.InvariantCulture)
                        .Contains(txtFilter.Text.ToUpper(CultureInfo.InvariantCulture)))
                {
                    AddListItem(index);
                }
            }

            if (lvEnh.Items.Count > 0)
            {
                lvEnh.Items[0].Selected = true;
                lvEnh.Items[0].EnsureVisible();
            }

            lvEnh.EndUpdate();
        }

        private void FillImageList()
        {
            var imageSize1 = ilEnh.ImageSize;
            var width1 = imageSize1.Width;
            imageSize1 = ilEnh.ImageSize;
            var height1 = imageSize1.Height;
            using var extendedBitmap = new ExtendedBitmap(width1, height1);
            ilEnh.Images.Clear();
            var num = DatabaseAPI.Database.Enhancements.Length - 1;
            for (var index = 0; index <= num; ++index)
            {
                var enhancement = DatabaseAPI.Database.Enhancements[index];
                if (enhancement.ImageIdx > -1)
                {
                    extendedBitmap.Graphics.Clear(Color.Transparent);
                    var graphics = extendedBitmap.Graphics;
                    I9Gfx.DrawEnhancement(ref graphics, DatabaseAPI.Database.Enhancements[index].ImageIdx,
                        I9Gfx.ToGfxGrade(enhancement.TypeID));
                    ilEnh.Images.Add(extendedBitmap.Bitmap);
                }
                else
                {
                    var images = ilEnh.Images;
                    var imageSize2 = ilEnh.ImageSize;
                    var width2 = imageSize2.Width;
                    imageSize2 = ilEnh.ImageSize;
                    var height2 = imageSize2.Height;
                    var bitmap = new Bitmap(width2, height2);
                    images.Add(bitmap);
                }
            }
        }

        private void frmEnhEdit_Load(object sender, EventArgs e)
        {
            _bFrm = new frmBusy();
            lvEnh.EnableDoubleBuffer();
            Show();
            Refresh();
            DisplayList();
            lblLoading.Visible = false;
            lvEnh.Select();
        }

        private async void ImageUpdate()
        {
            if (NoReload.Checked)
                return;
            await I9Gfx.LoadEnhancements();
            FillImageList();
        }

        private void lvEnh_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(RuntimeHelpers.GetObjectValue(sender), e);
        }

        private void NoReload_CheckedChanged(object sender, EventArgs e)
        {
            ImageUpdate();
        }

        private void UpdateListItem(int Index)
        {
            var strArray1 = new string[7];
            var enhancement = DatabaseAPI.Database.Enhancements[Index];
            strArray1[0] = $"{enhancement.Name} ({enhancement.ShortName}) - {enhancement.StaticIndex}";
            strArray1[1] = Enum.GetName(typeof(Enums.eType), enhancement.TypeID);
            strArray1[2] = $"{enhancement.LevelMin + 1}-{enhancement.LevelMax + 1}";
            strArray1[3] = $"{enhancement.Effect.Length}";
            strArray1[4] = string.Join(", ", enhancement.ClassID.Select(c => DatabaseAPI.Database.EnhancementClasses[c].ShortName));
            strArray1[6] = enhancement.UID;

            if (enhancement.nIDSet > -1)
            {
                strArray1[5] = DatabaseAPI.Database.EnhancementSets[enhancement.nIDSet].DisplayName;
                strArray1[0] = $"{strArray1[5]}: {strArray1[0]}";
            }
            else
            {
                strArray1[5] = "";
            }

            var num4 = strArray1.Length - 1;
            for (var index = 0; index <= num4; ++index)
                lvEnh.Items[Index].SubItems[index].Text = strArray1[index];
            lvEnh.Items[Index].ImageIndex = Index;
            lvEnh.Items[Index].EnsureVisible();
            lvEnh.Refresh();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DisplayList();
            btnUp.Enabled = string.IsNullOrEmpty(txtFilter.Text);
            btnDown.Enabled = string.IsNullOrEmpty(txtFilter.Text);
        }
    }
}