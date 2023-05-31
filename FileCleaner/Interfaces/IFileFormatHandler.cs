namespace FileCleaner.Interfaces
{
    public interface IFileFormatHandler<T>
    {
        bool IsFormatValid(byte[] fileBytes);

        public byte[] Sanitize(IFormFile formFile);
    }
}
