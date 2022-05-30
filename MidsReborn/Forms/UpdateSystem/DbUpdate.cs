﻿using System;
using System.Windows.Forms;
using System.Xml;
using mrbBase;
using mrbBase.Base.Master_Classes;
using static Mids_Reborn.Forms.UpdateSystem.clsXMLUpdate;

namespace Mids_Reborn.Forms.UpdateSystem
{
    public class DbUpdate
    {
        private static bool Mandatory { get; set; }
        private static Version Version { get; set; }
        public static string ChangeLog { get; set; }

        public static bool IsAvailable
        {
            get
            {
                var settings = new XmlReaderSettings
                {
                    XmlResolver = null,
                    DtdProcessing = DtdProcessing.Ignore
                };
                using var xmlReader = XmlReader.Create(DatabaseAPI.ServerData.ManifestUri, settings);
                while (xmlReader.Read())
                {
                    try
                    {
                        switch (xmlReader.Name)
                        {
                            case "version":
                            {
                                Version = Version.Parse(xmlReader.ReadElementContentAsString());
                                break;
                            }
                            case "changelog":
                            {
                                ChangeLog = xmlReader.ReadElementContentAsString();
                                MidsContext.Config.DbChangeLog = ChangeLog;
                                break;
                            }
                            case "mandatory":
                            {
                                Mandatory = bool.Parse(xmlReader.ReadElementContentAsString());
                                break;
                            }
                        }
                    }
                    catch (XmlException)
                    {
                        MessageBox.Show(@"An error occurred while attempting to read from the manifest.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                return CompareVersions(Version, DatabaseAPI.Database.Version);
            }
        }

        public static void InitiateQuery(frmMain parent)
        {
            if (!Mandatory)
            {
                var dbResult = new UpdateQuery(parent)
                {
                    Type = UpdateType.Database.ToString()
                };
                dbResult.ShowDialog();
                switch (dbResult.DialogResult)
                {
                    case DialogResult.Yes:
                    {
                        var patchNotes = new PatchNotes(parent, true)
                        {
                            Type = UpdateType.Database.ToString(),
                            Version = Version.ToString()
                        };
                        patchNotes.ShowDialog();
                        break;
                    }
                    case DialogResult.No:
                        dbResult.Close();
                        break;
                    case DialogResult.OK:
                        Update(DatabaseAPI.ServerData.ManifestUri, Version.ToString(), Files.BaseDataPath);
                        break;
                }
            }
            else
            {
                Update(DatabaseAPI.ServerData.ManifestUri, Version.ToString(), Files.BaseDataPath);
            }
        }
    }
}
