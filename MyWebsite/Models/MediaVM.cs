namespace MyWebsite.Models
{
    public class MediaVM
    {
        public int MediaId { get; set; }

        public string FileName { get; set; } = null!;

        public string FilePath { get; set; } = null!;
        public string? Description { get; set; }

        public int? UploadedBy { get; set; }

        public DateTime? UploadedAt { get; set; }
    }
}
