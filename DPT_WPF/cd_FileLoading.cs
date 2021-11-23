using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DPT_WPF
{
    public class cd_FileLoading
    {
        public string[] GetFileList(string ftpName)
        {
            string _ftpUri = ftpName + "Job Files/";
            Uri ftpUri = new Uri(_ftpUri);

            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpUri);
            req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
            req.Timeout = 120000;
            req.Method = WebRequestMethods.Ftp.ListDirectory;
            //req.Method = WebRequestMethods.Ftp.GetDateTimestamp;

            //req.Method = WebRequestMethods.Ftp.DeleteFile;
            //req.Method = WebRequestMethods.Ftp.DownloadFile;
            string[] filesinDirectory = null;
            try
            {
                FtpWebResponse resFtp = (FtpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(resFtp.GetResponseStream(), System.Text.Encoding.Default);
                
                string strData = reader.ReadToEnd();
                
                
                filesinDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //string[] filesinDirectory = null;
               
                                resFtp.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message); //랜선 연결 불안정
            }

            return filesinDirectory;

        }


        public string[] GetFileList1(string addFileName, string ftpName)
        {

            string _strftpUri = ftpName + "Job Files/";
            string strftpUri = _strftpUri + addFileName + "/";
            Uri ftpUri = new Uri(strftpUri);

            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpUri);
            req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
            req.Timeout = 120000;
            req.Method = WebRequestMethods.Ftp.ListDirectory;
            //req.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse resFtp = (FtpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(resFtp.GetResponseStream(), System.Text.Encoding.Default, true);

            string strData = reader.ReadToEnd();
            string[] filesinDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            resFtp.Close();

            return filesinDirectory;
        }


        public string[] GetFolderList(string pathName, string ftpName)
        {

            //string _strftpUri = ftpName + "Job Files/";

            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpName + pathName);
            req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
            req.Timeout = 120000;
            req.Method = WebRequestMethods.Ftp.ListDirectory;
            //req.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse resFtp = (FtpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(resFtp.GetResponseStream(), System.Text.Encoding.Default, true);

            string strData = reader.ReadToEnd();
            string[] filesinDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            resFtp.Close();

            return filesinDirectory;
        }

    }
}
