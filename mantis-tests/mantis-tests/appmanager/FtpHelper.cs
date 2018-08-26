using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.FtpClient;

namespace MantisTests
{
    public class FtpHelper : HelperBase
    {
        private FtpClient client;

        public FtpHelper(ApplicationManager appmanager) : base(appmanager)
        {
            client = new FtpClient();
            client.Host = "localhost";
            client.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
            client.Connect();
        }


        public void BackupFile(String path)
        {
            // Making config backup right on the server
            String backupPath = path + ".bak";

            if (!client.FileExists(backupPath))
            {
                client.Rename(path, backupPath);
            }
        }


        public void RestoreBackupFile(String path)
        {
            String backupPath = path + ".bak";

            // Exit if no backup was found
            if (!client.FileExists(backupPath))
            {
                Console.WriteLine("Can't restore config on Mantis Server");
                return;
            }

            // Restore
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }
            client.Rename(backupPath, path);
        }


        public void Upload(String destDath, Stream localFile)
        {
            if (client.FileExists(destDath))
            {
                client.DeleteFile(destDath);
            }

            using (Stream ftpStream = client.OpenWrite(destDath))
            {
                byte[] buffer = new byte[8 * 1024];

                int count = localFile.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    ftpStream.Write(buffer, 0, count);
                    count = localFile.Read(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
