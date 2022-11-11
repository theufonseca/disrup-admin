namespace dirup_empresa_admin_api.Models
{
    public class PhotoUploadModel
    {
        public int CompanyId { get; set; }
        public IFormFile? File { get; set; }
        public bool IsThumb { get; set; }
    }
}
