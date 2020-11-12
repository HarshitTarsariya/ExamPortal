using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ExamPortal.Utilities
{
    //Firebase is used for uploading pdf files
    //Please remove Apikey,Bucket,AuthEmail,AuthPassword during pushing
    public interface IFirebaseUpload
    {
        public string Ampersand => "__AMP__";
        Task<string> Upload(Stream stream, string name, string papercode);
        Task DeleteEverything(string papercode, List<string> paperssubmitted);
    }
    public class FirebaseUpload : IFirebaseUpload
    {
        private string ApiKey = "AIzaSyBvU1BhdtjG5FLwV1KFNDZlh7C_jNHcyDU";
        private string Bucket = "exam-portal-292805.appspot.com";
        private string AuthEmail = "exam@gmail.com";
        private string AuthPassword = "Exam123";

        private string FileTypeUploaded = ".pdf";
        public async Task DeleteEverything(string papercode, List<string> paperssubmitted)
        {

            foreach (var response in paperssubmitted)
            {
                await Delete(papercode + "_" + response + FileTypeUploaded);
            }
            await Delete(papercode + FileTypeUploaded);
        }
        private async Task Delete(string FileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var aa = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(aa.FirebaseToken)
                    }
                )
                .Child("Papers")
                .Child(FileName)
                .DeleteAsync();
            try
            {
                await task;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }
        }

        public async Task<string> Upload(Stream stream, string name, string papercode)
        {
            //Stream stream = DesPaper.paper.OpenReadStream();
            string FileName = papercode;
            if (name != null)
            {
                FileName += "_" + name;
            }
            FileName += FileTypeUploaded;

            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var aa = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(aa.FirebaseToken)
                    }
                )
                .Child("Papers") //folder name in the bucket
                .Child(FileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                string link = await task;
                System.Diagnostics.Debug.Print(link);
                return link;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception :{0}", ex);
                return "";
            }
        }

    }
}
