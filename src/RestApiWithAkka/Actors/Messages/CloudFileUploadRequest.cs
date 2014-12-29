namespace RestApiWithAkka.Actors.Messages
{
    public class CloudFileUploadRequest
    {
        public string FileName { get; private set; }

        public CloudFileUploadRequest(string fileName)
        {
            FileName = fileName;
        }
    }
}