using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Cv_Core
{
    public static class ZipManager
    {

        public static void Unzip(string pathZip, string pathDest)
        {
            if (!Directory.Exists(pathDest))
            {
                Directory.CreateDirectory(pathDest);
            }

            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(pathZip))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.FullName.Equals("/"))
                        {
                            continue;
                        }
                        using (var entryStream = entry.Open())
                        {
                            CopyStream(entryStream, Path.Combine(pathDest, entry.FullName));
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void CopyStream(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}