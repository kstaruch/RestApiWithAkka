using Akka.Actor;
using Akka.TestKit.VsTest;
using NUnit.Framework;

namespace RestApiWithAkka.Tests
{
    public abstract class ActorTestSpecification: TestKit
    {
        [TestFixtureSetUp]
        public void Init()
        {
            Given();
            When();
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            Shutdown();
        }

        protected virtual ActorRef Sut { get; set; }

        public virtual void Given() { }
        public virtual void When() { }
    }
}