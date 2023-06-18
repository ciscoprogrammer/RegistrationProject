using System;
using System.Collections.Generic;

namespace RegistrationProject.Models
{
    public partial class TableeImage
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
    }
}
