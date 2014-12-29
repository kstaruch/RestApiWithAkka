using System;
using Akka.Actor;
using RestApiWithAkka.Actors.Messages;

namespace RestApiWithAkka.Actors
{
    public class FileUploader: ReceiveActor
    {
        // two states initial and uploading for msg indempotence

        private readonly IActorNameNormalizer nameNormalizer;

        public FileUploader(IActorNameNormalizer nameNormalizer)
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
            var newReq = new CloudFileUploadRequest(req.FileName);
            var child = Context.ActorOf(CloudFileUpload.Props(), "cloud_" + nameNormalizer.NormalizeName(req.FileName));
            child.Tell(newReq);
            Become(Uploading);
        }

        protected void Handle(CloudUploadCompleted req)
        {
            Console.WriteLine("CloudUploadCompleted received: " + req.FileName + " on " + Self.Path + " [" + Self.GetHashCode() + " ]");

            Context.Sender.Tell(PoisonPill.Instance);
            Context.Parent.Tell(new FileUploadCompleted(req.FileName));
            Context.System.Scheduler.ScheduleOnce(TimeSpan.FromMilliseconds(100), Self, PoisonPill.Instance);
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
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FileUploader(new SimpleActorNameNormalizer()));
        }
    }
}
