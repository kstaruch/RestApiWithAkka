using System;
using Akka.Actor;
using Akka.TestKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApiWithAkka.Actors;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestApiWithAkka.Tests.UploadSupervisor
{
    public abstract class WithUploadSupervisor: ActorTestSpecification
    {
        protected Mock<IActorNameNormalizer> NameNormalizer { get; private set; }
        protected TestProbe UploaderProbe { get; private set; }

        protected WithUploadSupervisor()
        {
            NameNormalizer = new Mock<IActorNameNormalizer>();
            NameNormalizer.Setup(normalizer => normalizer.NormalizeName(It.IsAny<string>()))
                .Returns((string s) => s);

            UploaderProbe = new TestProbe(Sys, Assertions, "test_file_name");
        }

        public override void Given()
        {
            //Sut = Sys.ActorOf(Actors.UploadSupervisor.Props(NameNormalizer.Object, (f, s) => UploaderProbe));
            Sut = Sys.ActorOf(Actors.UploadSupervisor.Props(NameNormalizer.Object, Actors.FileUploader.Props()));
        }
    }
}