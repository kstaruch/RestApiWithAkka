using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using RestApiWithAkka.Actors;

namespace RestApiWithAkka
{
    public class Bootstrapper
    {
        public void Run()
        {
            var system = ActorSystem.Create("my-system");

            var uploader = system.ActorOf(UploadSupervisor.Props(), "supervisor");

            uploader.Tell(new FileUploadRequest("some file name"));
            uploader.Tell(new FileUploadRequest("some file name"));
        }
    }
}
