﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using mrbBase.Base.Master_Classes;
using Newtonsoft.Json;

namespace mrbBase.Utils
{
    public sealed class PatchCompressor
    {
        private PatchCompressor(EPatchType patchType)
        {
            PatchType = patchType;
        }

        public static PatchCompressor AppPatchCompressor { get; } = new(EPatchType.Application);
        public static PatchCompressor DbPatchCompressor { get; } = new(EPatchType.Database);

        private enum EPatchType
        {
            Application,
            Database
        }

        private EPatchType PatchType { get; set; }

        private const string PatchFolderName = @"Patches";
        private const string HashFileName = @"FileHash.json";

        private string TopLevelFolder
        {
            get
            {
                var value = PatchType switch
                {
                    EPatchType.Application => AppContext.BaseDirectory,
                    EPatchType.Database => Path.Combine(AppContext.BaseDirectory, Files.RoamingFolder),
                    _ => string.Empty
                };

                return value;
            }
        }

        private string PatchPath
        {
            get
            {
                var value = PatchType switch
                {
                    EPatchType.Application => Path.Combine(AppContext.BaseDirectory, PatchFolderName),
                    EPatchType.Database => Path.Combine(MidsContext.Config.DataPath, PatchFolderName),
                    _ => string.Empty
                };

                return value;
            }
        }

        private string PatchFile
        {
            get
            {
                var value = PatchType switch
                {
                    EPatchType.Application => Path.Combine(PatchPath, $"{MidsContext.AssemblyFileVersion}.mru"),
                    EPatchType.Database => Path.Combine(PatchPath, $"{DatabaseAPI.Database.Version}.mru"),
                    _ => string.Empty
                };

                return value;
            }
        }

        private string HashFile => Path.Combine(PatchPath, HashFileName);


        // private static string TopLevelFolder => Path.Combine(AppContext.BaseDirectory, Files.RoamingFolder);
        // private static string PatchPath => Path.Combine(MidsContext.Config.DataPath, "Patches");
        // private static string PatchFile => Path.Combine(PatchPath, $"{DatabaseAPI.Database.Version}.mru");
        // private static string HashFile => Path.Combine(PatchPath, "FileHash.json");

        private IEnumerable<FileData> CompileList(string path)
        {
            List<FileHash>? hashes = null;
            var fileQueue = new List<FileData>();
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(x => !x.Contains("Patches") && !x.Contains("Data")).ToList();

            if (File.Exists(Path.Combine(PatchPath, HashFile)))
            {
                hashes = JsonConvert.DeserializeObject<List<FileHash>>(File.ReadAllText(HashFile));
            }

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                var name = fileInfo.Name;
                var directory = fileInfo.DirectoryName?.Replace(TopLevelFolder, string.Empty);
                var data = File.ReadAllBytes(file);
                if (directory == null) continue;
                var newFile = new FileHash(directory, fileInfo.Name, FileHash.ComputeHash(file));
                if (hashes != null)
                {
                    var priorFile = hashes.FirstOrDefault(x => x.FileName == name && x.Directory == directory);
                    if (priorFile != null)
                    {
                        var hashCompareResult = FileHash.Compare(newFile.Hash, priorFile.Hash);
                        if (hashCompareResult) continue;
                        hashes.First(x => x.FileName == newFile.FileName).Hash = newFile.Hash;
                        fileQueue.Add(new FileData { FileName = name, Data = data, Path = directory });
                    }
                    else
                    {
                        hashes.Add(newFile);
                        fileQueue.Add(new FileData { FileName = name, Data = data, Path = directory });
                    }
                }
                else
                {
                    hashes = new List<FileHash> { newFile };
                    fileQueue.Add(new FileData { FileName = name, Data = data, Path = directory });
                }
            }

            File.WriteAllText(HashFile, JsonConvert.SerializeObject(hashes, Formatting.Indented));
            return fileQueue;
        }

        public async Task<bool> CreatePatchFile(string path)
        {
            var cSource = new TaskCompletionSource<bool>();
            var compressedData = CompressData(path);
            if (compressedData != null)
            {
                DeletePriorPatch(path);
                var generated = GenerateFile(compressedData);
                cSource.TrySetResult(generated);
            }
            else
            {
                cSource.TrySetResult(false);
            }

            return await cSource.Task;
        }

        private byte[]? CompressData(string path)
        {
            byte[]? outData;
            MemoryStream patchStream;
            BinaryWriter writer;
            try
            {
                if (!Directory.Exists(PatchPath))
                {
                    Directory.CreateDirectory(PatchPath);
                }


                patchStream = new MemoryStream();
                writer = new BinaryWriter(patchStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Failed to create patch container: " + ex.Message);
                return null;
            }

            try
            {
                var files = CompileList(path).ToList();
                writer.Write("Mids Reborn Patch Data");
                writer.Write(files.Count);
                foreach (var file in files)
                {
                    writer.Write(file.Data.Length);
                    writer.Write(file.FileName);
                    writer.Write(file.Path);
                    writer.Write(file.Data);
                }

                writer.Close();
                outData = patchStream.ToArray();
                patchStream.Close();
            }
            catch (Exception e)
            {
                writer.Close();
                patchStream.Close();
                MessageBox.Show($@"Message: {e.Message}\r\nTrace: {e.StackTrace}");
                return null;
            }

            return outData;
        }

        private void DeletePriorPatch(string path)
        {
            var filesToRemove = Directory.GetFiles(PatchPath, "*.mru").ToList();
            if (!filesToRemove.Any()) return;
            foreach (var file in filesToRemove)
            {
                File.Delete(file);
            }
        }

        private bool GenerateFile(byte[] byteArray)
        {
            FileStream fileStream;
            DeflaterOutputStream outputStream;
            try
            {
                fileStream = new FileStream(PatchFile, FileMode.Create);
                outputStream = new DeflaterOutputStream(fileStream, new Deflater(9));
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Patch generation failed: " + e.Message);
                return false;
            }

            try
            {
                outputStream.Write(byteArray, 0, byteArray.Length);
                outputStream.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                outputStream.Close();
                fileStream.Close();
                MessageBox.Show($@"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                return false;
            }

            return true;
        }

        private List<FileData>? DecompressData()
        {
            FileStream fileStream;
            MemoryStream decompressed;
            InflaterInputStream inflaterStream;
            BinaryReader reader;
            var patchFiles = new List<FileData>();
            try
            {
                fileStream = new FileStream(PatchFile, FileMode.Open, FileAccess.Read);
                decompressed = new MemoryStream();
                inflaterStream = new InflaterInputStream(fileStream);
                reader = new BinaryReader(inflaterStream);
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Failed to read patch data: " + e.Message);
                return null;
            }

            try
            {
                var headerFound = true;
                var header = reader.ReadString();
                if (header != "Mids Reborn Patch Data")
                {
                    headerFound = false;
                }

                if (!headerFound)
                {
                    MessageBox.Show(@"Expected MRB header, got something else!", @"Error Reading Patch Data");
                    return null;
                }

                var fileCount = reader.ReadInt32();
                for (var i = 0; i < fileCount; i++)
                {
                    var dataLength = reader.ReadInt32();
                    patchFiles.Add(new FileData{FileName = reader.ReadString(), Path = reader.ReadString(), Data = reader.ReadBytes(dataLength)});
                }
                reader.Close();
                decompressed.Close();
                inflaterStream.Close();
                fileStream.Close();

                // foreach (var patchFile in patchFiles)
                // {
                //     var fileInfo = new FileInfo(Path.Combine(PatchPath, patchFile.Path, patchFile.FileName));
                //     fileInfo.Directory?.Create();
                //     File.WriteAllBytes(fileInfo.FullName, patchFile.Data);
                // }
            }
            catch (Exception ex)
            {
                reader.Close();
                decompressed.Close();
                inflaterStream.Close();
                fileStream.Close();
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                return null;
            }

            return patchFiles;
        }
    }
}
