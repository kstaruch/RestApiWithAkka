using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using System.Threading;

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
        // two states initial and uploading for msg indempotence

        private ActorNameNormalizer nameNormalizer;

        public FileUploader(ActorNameNormalizer nameNormalizer)
        {
            this.nameNormalizer = nameNormalizer;
            Receive<FileUploadRequest>(req => HandleInitial(req));
        }

        protected void HandleWhenUploading(FileUploadRequest req)
        {
            Console.WriteLine("Ignoring - already uploading " + req.FileName);
        }

        protected void HandleInitial(FileUploadRequest req)
        {
            //Console.WriteLine("FileUploadRequest received: " + req.FileName + " on " + Self.Path + " [" + Self.GetHashCode() + " ]");
            var newReq = new CloudFileUploadRequest(req.FileName);
            var child = Context.ActorOf(CloudFileUpload.Props(), "cloud_" + nameNormalizer.NormalizeName(req.FileName));
            child.Tell(newReq);
            Become(Uploading);
        }

        protected void Handle(CloudUploadCompleted req)
        {
            Console.WriteLine("CloudUploadCompleted received: " + req.FileName + " on " + Self.Path + " [" + Self.GetHashCode() + " ]");
            
            Self.Tell(PoisonPill.Instance); ;
        }

        protected override void PostStop()
        {
            base.PostStop();
            Console.WriteLine("PostStop for FileUploader: " + Self.Path + " [" + Self.GetHashCode() + " ]");
        }

        private void Uploading()
        {
            Receive<FileUploadRequest>(req => HandleWhenUploading(req));
            Receive<CloudUploadCompleted>(req => Handle(req));
            //TODO: add sth when upload confirmed
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FileUploader(new SimpleActorNameNormalizer()));
        }
    }

    public class CloudFileUploadRequest
    {
        public string FileName { get; private set; }

        public CloudFileUploadRequest(string fileName)
        {
            FileName = fileName;
        }
    }

    public class CloudUploadCompleted
    {
        public string FileName { get; private set; }

        public CloudUploadCompleted(string fileName)
        {
            FileName = fileName;
        }
    }

    public class CloudFileUpload: ReceiveActor
    {
        private ActorNameNormalizer nameNormalizer;

        public CloudFileUpload(ActorNameNormalizer nameNormalizer)
        {
            this.nameNormalizer = nameNormalizer;
            Receive<CloudFileUploadRequest>(req => Handle(req));
        }

        protected void Handle(CloudFileUploadRequest req)
        {
            Console.WriteLine("CloudFileUploadRequest received: " + req.FileName + " on " + Self.Path + " [" + Self.GetHashCode() + " ]");
            Thread.Sleep(1000);
            Console.WriteLine("Upload done");
            Context.Parent.Tell(new CloudUploadCompleted(req.FileName));
        }

        protected override void PostStop()
        {
            base.PostStop();
            Console.WriteLine("PostStop for CloudFileUpload: " + Self.Path + " [" + Self.GetHashCode() + " ]");
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new CloudFileUpload(new SimpleActorNameNormalizer()));
        }
    }

    public class UploadSupervisor: ReceiveActor
    {
        
        private ActorNameNormalizer nameNormalizer;

        public UploadSupervisor(ActorNameNormalizer nameNormalizer)
        {
            this.nameNormalizer = nameNormalizer;
            Receive<FileUploadRequest>(req => Handle(req));

        }

        protected void Handle(FileUploadRequest req)
        {
            string childName = nameNormalizer.NormalizeName(req.FileName);
            var child = Context.Child(childName);
            if (child.IsNobody())
            {
                child = Context.ActorOf(FileUploader.Props(), childName);
            }
            
            child.Tell(req);
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new UploadSupervisor(new SimpleActorNameNormalizer()));
        }
    }

    public interface ActorNameNormalizer
    {
        string NormalizeName(string name);
    }

    public class SimpleActorNameNormalizer: ActorNameNormalizer
    {
        public string NormalizeName(string name)
        {
            return name.Replace(" ", "-");
        }

    }

}
