using ExamPortal.DTOS;
using ExamPortal.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExamPortal.Utilities
{
    public class FirebaseUpload
    {
        private string ApiKey = "AIzaSyBvU1BhdtjG5FLwV1KFNDZlh7C_jNHcyDU";
        private string Bucket = "exam-portal-292805.appspot.com";
        private string AuthEmail = "exam@gmail.com";
        private string AuthPassword = "Exam123";
        public string Uploader(DescriptivePaperDTO DesPaper)
        {
            FileStream stream;
            
            string path = "";
            path = Path.Combine(@"C:\Users\Harshit\Desktop\", DesPaper.paper.FileName);
            stream = new FileStream(path, FileMode.Open);
            Task<string> task= Task.Run(() => Upload(stream, DesPaper.paper.FileName, DesPaper.PaperCode));
            task.Wait();

            return task.Result;
        }
        public async Task<string> Upload(FileStream stream, string FileName, string papercode)
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
                .Child(papercode)
                .Child(FileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                string link = await task;
                System.Diagnostics.Debug.WriteLine(link);
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
