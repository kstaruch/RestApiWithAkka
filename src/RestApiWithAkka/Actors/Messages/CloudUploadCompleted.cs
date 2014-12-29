namespace RestApiWithAkka.Actors.Messages
{
    public class CloudUploadCompleted
    {
        public string FileName { get; private set; }

        public CloudUploadCompleted(string fileName)
        {
            FileName = fileName;
        }
    }
}