using System;
using System.Threading;
using Akka.Actor;
using RestApiWithAkka.Actors.Messages;

namespace RestApiWithAkka.Actors
{
    public class CloudFileUpload: ReceiveActor
    {
        public CloudFileUpload()
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
            Console.WriteLine("PostStop for CloudFileUpload: " + Self.Path + " [" + Self.GetHashCode() + " ]");
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new CloudFileUpload());
        }
    }
}