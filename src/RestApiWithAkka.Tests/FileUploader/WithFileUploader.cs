using System;
using NUnit.Framework;

namespace RestApiWithAkka.Tests.FileUploader
{
    public abstract class WithFileUploader: ActorTestSpecification
    {
         
    }

    [TestFixture]
    public class WhenFileUploadRequested : WithFileUploader
    {
        [Test]
        public void FileStagingIsRequested()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CloudUploadIsRequested()
        {
            throw new NotImplementedException();
        }


    }

    public abstract class WithFileUploaderAlreadyUploading: ActorTestSpecification
    {
        
    }

    [TestFixture]
    public class WhenTheSameUploadIsRequested : WithFileUploaderAlreadyUploading
    {
        
    }

    [TestFixture]
    public class WhenCloudUploadIsCompleted : WithFileUploaderAlreadyUploading
    {
        [Test]
        public void ScheduledDeletingFromStagingIsRequested()
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class WhenCloudUploadThrowsException : WithFileUploaderAlreadyUploading
    {
        [Test]
        public void CloudUploadIsRescheduled()
        {
            throw new NotImplementedException();
        }
    }



}