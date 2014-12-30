using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.VsTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApiWithAkka.Actors;
using RestApiWithAkka.Actors.Messages;
using Moq;
using Should;


namespace RestApiWithAkka.Tests
{
    public class Class1
    {
    }
    
//    [TestClass]
//    public class UploadSupervisorSpec : TestKit
//    {
//        [TestMethod]
//        public void WhenFileUploadRequested_FileUploaderIsCreated()
//        {
//            var sut = Sys.ActorOf(Actors.UploadSupervisor.Props());
//            var msg = new FileUploadRequest("test_file_name");

//            var actorSelection = new ActorSelection(sut, "test_file_name");

////            var hasNoUploader = false;
////            
////            try
////            {
////                actorSelection.ResolveOne(TimeSpan.FromMilliseconds(100)).Wait();
////            }
////            catch (Exception e)
////            {   
////                hasNoUploader = true;
////            }
////
////            hasNoUploader.ShouldBeTrue();
//            sut.Tell(msg);
            
//            AwaitAssert(() =>
//            {
//                var resolveOne = actorSelection.ResolveOne(TimeSpan.FromMilliseconds(100));
//                var isNobody = resolveOne.Result.IsNobody();
//                isNobody.ShouldBeFalse();
//            }, duration: TimeSpan.FromSeconds(1), interval: TimeSpan.FromMilliseconds(100));
//        }
//    }
}
