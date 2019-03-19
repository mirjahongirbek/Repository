
using System.ComponentModel.DataAnnotations;

namespace GenericController.Modals
{
    public partial class LoginViewModal
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }

    }
}
