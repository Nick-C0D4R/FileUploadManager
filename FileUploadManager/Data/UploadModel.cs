using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace FileUploadManager.Data
{
    public class UploadModel
    {
        [Required]
        public IBrowserFile File { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
