using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Electronique_Labo.Models
{
    [NotMapped]
    public class GoogleDriveFilesRepository
    {
      
        //defined scope.
        public static string[] Scopes = { DriveService.Scope.Drive };

        //create Drive API service.
        public static DriveService GetService()
        {
            //get Credentials from client_secret.json file 
            UserCredential credential;
            using (var stream = new FileStream(@"D:\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                String FolderPath = @"D:\";
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }

            //create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveRestAPI-v3",
            });
            return service;
        }

        //file Upload to the Google Drive.
        public static void FileUpload(IEnumerable<HttpPostedFileBase> files, List<string> drivetitle,int idexpiriment)
        {
             var _context = new ApplicationDbContext();
            DriveService service = GetService();
            List<string> titList = drivetitle;
            foreach (var file in files)
            {
                if (file != null )
                {
                    foreach (var titre in titList)
                    {
                        string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                        Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    var FileMetaData = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = Path.GetFileName(file.FileName),
                        MimeType = MimeMapping.GetMimeMapping(path),
                        Parents = new List<string>
                        {
                            "1fd7wCtl5UWbWiRjY8TQv52mZzO8cbKys"
                        }
                    };

                    Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;
                    using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                    {
                        request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                        request.Fields = "id";
                        request.Upload();
                    }

                      var fileUpload = request.ResponseBody;

                        var GoogleDrive =new GoogleDriveFile();
                        GoogleDrive.TiTle = titre;
                        GoogleDrive.FileId = fileUpload.Id;
                        GoogleDrive.Name = fileUpload.Name;
                        GoogleDrive.Size = fileUpload.Size;
                        GoogleDrive.Version = fileUpload.Version;
                        GoogleDrive.ExpirimentId = idexpiriment;
                        _context.GoogleDriveFiles.Add(GoogleDrive);
                        _context.SaveChanges();
                        titList.Remove(titre);
                        goto CONTINUE;

                    }

                   
                }

                CONTINUE: continue;
            }          
        }
        // file save to server path
        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        //Delete file from the Google drive
        public static void DeleteFile(GoogleDriveFile files)
        {
            DriveService service = GetService();
            try
            {
                // Initial validation.
                if (service == null)
                    throw new ArgumentNullException("service");

                if (files == null)
                    throw new ArgumentNullException(files.FileId);

                // Make the request.
                service.Files.Delete(files.FileId).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }
    }
}