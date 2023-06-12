namespace dotFile.Models
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Bytes { get; set; }
        public int IdUser { get; set; }
        public bool Private { get; set; }
    }
}
