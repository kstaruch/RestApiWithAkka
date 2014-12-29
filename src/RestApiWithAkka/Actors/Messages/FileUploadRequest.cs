namespace RestApiWithAkka.Actors.Messages
{
    public class FileUploadRequest
    {
        public string FileName { get; private set; }

        public FileUploadRequest(string fileName)
        {
            FileName = fileName;
        }
    }
}