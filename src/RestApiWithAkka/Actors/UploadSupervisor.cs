using System;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using Akka.Actor;
using RestApiWithAkka.Actors.Messages;

namespace RestApiWithAkka.Actors
{
    public class UploadSupervisor: ReceiveActor
    {
        private readonly IActorNameNormalizer nameNormalizer;
        private readonly Func<ActorRefFactory, string, ActorRef> uploaderCreator;
        private readonly Props fileUploaderProps;

//        public UploadSupervisor(IActorNameNormalizer nameNormalizer, Props fileUploaderProps)
//        {
//            this.nameNormalizer = nameNormalizer;
//            this.fileUploaderProps = fileUploaderProps;
//            Receive<FileUploadRequest>(req => Handle(req));
//            Receive<FileUploadCompleted>(req => Handle(req));
//        }

        public UploadSupervisor(IActorNameNormalizer nameNormalizer, Func<ActorRefFactory, string, ActorRef> uploaderCreator)
        {
            this.nameNormalizer = nameNormalizer;
            this.uploaderCreator = uploaderCreator;
            this.fileUploaderProps = fileUploaderProps;
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
                child = uploaderCreator(Context, childName);
            }
            
            child.Tell(req);
        }

        public static Props Props()
        {
            return Props(new SimpleActorNameNormalizer(), FileUploader.Props());
        }

        public static Props Props(IActorNameNormalizer nameNormalizer, Props fileUploaderProps)
        {
            return Props(nameNormalizer, (ctx, name) => ctx.ActorOf(fileUploaderProps, name));
            //return Akka.Actor.Props.Create(() => new UploadSupervisor(nameNormalizer, Context.ActorOf(fileUploaderProps)));
        }

        public static Props Props(IActorNameNormalizer nameNormalizer, Func<ActorRefFactory, string, ActorRef> maker)
        {
            return Akka.Actor.Props.Create(() => new UploadSupervisor(nameNormalizer, maker));
        }
    }
}