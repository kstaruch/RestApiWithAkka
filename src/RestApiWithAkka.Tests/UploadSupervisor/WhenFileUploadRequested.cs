using Akka.Actor;
using FluentTime;
using Moq;
using NUnit.Framework;
using RestApiWithAkka.Actors.Messages;
using Should;

namespace RestApiWithAkka.Tests.UploadSupervisor
{
    [TestFixture]
    public class WhenFileUploadRequested : WithUploadSupervisor
    {
        public override void When()
        {
            var msg = new FileUploadRequest("test_file_name"/*UploaderProbe.Ref.Path.Name*/);
            
            Sut.Tell(msg);
        }
        
        [Test]
        public void FileUploaderIsCreated()
        {   
            var actorSelection = new ActorSelection(Sut, /*UploaderProbe.Ref.Path.Name*/ "test_file_name");

            Within(2.Seconds(), () => AwaitAssert(() =>
            {
                var resolveOne = actorSelection.ResolveOne(100.Milliseconds());
                var isNobody = resolveOne.Result.IsNobody();
                isNobody.ShouldBeFalse();
            }));
        }

        [Test]
        public void NameGetsNormalized()
        {
            NameNormalizer.Verify(normalizer => normalizer.NormalizeName("test_file_name"/*UploaderProbe.Ref.Path.Name*/), Times.Once);
        }

        [Test]
        public void FileUploaderReceivesUploadRequest()
        {
            //var actorSelection = new ActorSelection(Sut, "test_file_name");


            //Within(2.Seconds(), () => ExpectMsg<FileUploadRequest>());
        }
        
    }

    
}