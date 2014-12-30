using System;
using System.Threading;
using Akka.Actor;
using RestApiWithAkka.Actors.Messages;

namespace RestApiWithAkka.Actors
{
    public class CloudFileUploader: ReceiveActor
    {
        public CloudFileUploader()
        {
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
            Console.WriteLine("PostStop for CloudFileUploader: " + Self.Path + " [" + Self.GetHashCode() + " ]");
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new CloudFileUploader());
        }
    }
}