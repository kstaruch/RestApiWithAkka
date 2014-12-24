using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace RestApiWithAkka.Actors
{
    public class FileUploadRequest
    {
        public string FileName { get; private set; }

        public FileUploadRequest(string fileName)
        {
            FileName = fileName;
        }
    }

    public class FileUploader: ReceiveActor
    {
        public FileUploader()
        {
            Receive<FileUploadRequest>(req => Handle(req));
        }

        protected void Handle(FileUploadRequest req)
        {
            Console.WriteLine("FileUploadRequest received: " + req.FileName + " on " + Self.Path + " [" + Self.GetHashCode() + " ]");
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FileUploader());
        }
    }
}
