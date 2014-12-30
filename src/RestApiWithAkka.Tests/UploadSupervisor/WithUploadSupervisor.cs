using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApiWithAkka.Actors;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestApiWithAkka.Tests.UploadSupervisor
{
    public abstract class WithUploadSupervisor: ActorTestSpecification
    {
        protected Mock<IActorNameNormalizer> NameNormalizer { get; private set; }

        protected WithUploadSupervisor()
        {
            NameNormalizer = new Mock<IActorNameNormalizer>();
            NameNormalizer.Setup(normalizer => normalizer.NormalizeName(It.IsAny<string>()))
                .Returns((string s) => s);
        }

        public override void Given()
        {
            Sut = Sys.ActorOf(Actors.UploadSupervisor.Props(NameNormalizer.Object, FileUploader.Props()));
        }
    }
}