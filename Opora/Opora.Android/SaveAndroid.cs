using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using Opora.Droid;

[assembly: Dependency(typeof(SaveAndroid))]
namespace Opora.Droid
{
    public class SaveAndroid : ISave
    {
        public async void Save(string filename, byte[] data)
        {
            var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            var localPath = Path.Combine(path, filename);
            Java.IO.File file = new Java.IO.File(path, filename);
            //Remove if the file exists
            if (file.Exists()) file.Delete();

            //Write the stream into the file
            FileOutputStream outs = new FileOutputStream(file);
            outs.Write(data);

            outs.Flush();
            outs.Close();

            //Invoke the created file for viewing
            if (file.Exists())
            {
                Android.Net.Uri path2 = Android.Net.Uri.FromFile(file);
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(path2, mimeType);
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Открыть в"));
            }
        }
    }
}