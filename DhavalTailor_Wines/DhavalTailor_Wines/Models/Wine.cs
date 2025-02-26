using System.ComponentModel.DataAnnotations;

namespace DhavalTailor_Wines.Models
{
    public class Wine : Auditable, IValidatableObject
    {
        //SUMMARY
        #region
        [Display(Name = "Wine Description")]
        public string WineDescription => $"{WineName} - {WineYear}";
        #endregion

        public int ID { get; set; }

        //WineName, Required, max length 70
        [Required(ErrorMessage = "Wine Name is required.")]
        [MaxLength(70, ErrorMessage = "Wine Name cannot exceed 70 characters.")]
        [Display(Name = "Wine Name")]
        public string WineName { get; set; }

        //WineYear,Required string, exactly 4 digits
        [Required(ErrorMessage = "Wine Year is required.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Wine Year must be a 4-digit number.")]
        [Display(Name = "Wine Year")]
        public string WineYear { get; set; }

        //WinePrice, Required double, less than $1000, Currency
        [Required(ErrorMessage = "Wine Price is required.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Wine Price")]
        public double WinePrice { get; set; }

        //WineHarvest,Short Date
        [Required(ErrorMessage = "Harvest Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Harvest Date")]
        public DateTime WineHarvest { get; set; }

        //foreign key
        [Required(ErrorMessage = "Wine Type is required.")]
        [Display(Name = "Wine Type")]
        public int Wine_TypeID { get; set; }
        public Wine_Type Wine_Type { get; set; }

        //validation
        #region
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //harvest year must be in the same year
            if (WineHarvest.Year.ToString() != WineYear)
            {
                yield return new ValidationResult("Harvest date must be within the specified year.", new[] { "WineHarvest" });
            }
        }
        #endregion


    }
}
