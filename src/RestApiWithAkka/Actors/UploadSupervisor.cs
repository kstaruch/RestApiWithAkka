using System;
using Akka.Actor;
using RestApiWithAkka.Actors.Messages;

namespace RestApiWithAkka.Actors
{
    public class UploadSupervisor: ReceiveActor
    {
        
        private readonly IActorNameNormalizer nameNormalizer;

        public UploadSupervisor(IActorNameNormalizer nameNormalizer)
        {
            this.nameNormalizer = nameNormalizer;
            Receive<FileUploadRequest>(req => Handle(req));
            Receive<FileUploadCompleted>(req => Handle(req));
        }

        protected void Handle(FileUploadCompleted req)
        {
            Console.WriteLine("FileUploadCompleted received: " + req.FileName + " on " + Self.Path + " [" + Self.GetHashCode() + " ]");
        }

        protected void Handle(FileUploadRequest req)
        {
            var childName = nameNormalizer.NormalizeName(req.FileName);
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
}