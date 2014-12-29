namespace RestApiWithAkka.Actors.Messages
{
    public class FileUploadCompleted
    {
        public string FileName { get; private set; }

        public FileUploadCompleted(string fileName)
        {
            FileName = fileName;
        }
    }
}