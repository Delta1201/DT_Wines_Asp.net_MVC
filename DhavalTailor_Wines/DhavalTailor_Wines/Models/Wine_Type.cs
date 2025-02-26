using System.ComponentModel.DataAnnotations;

namespace DhavalTailor_Wines.Models
{
    public class Wine_Type: Auditable
    {
            public int ID { get; set; }
            
            //WineTypeName, Required, max length 50
            [Required(ErrorMessage = "Wine Type Name is required.")]
            [MaxLength(50, ErrorMessage = "Wine Type Name cannot exceed 50 characters.")]
            [Display(Name = "Wine Type Name")]
            public string WineTypeName { get; set; }

            // foreign key
            public ICollection<Wine> Wines { get; set; }
    }
}
