
namespace MVVM_Sample.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Threading.Tasks;
    using MVVM_Sample.Model.Base;

    public partial class Company : EntityBase
    {
        private int id = 0;
        private string name = string.Empty;
        private string address1 = string.Empty;
        private string address2 = string.Empty;
        private string city = string.Empty;
        private string country = string.Empty;
        private string province = string.Empty;
        private string postalCode = string.Empty;

        public int Id 
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        [Required]
        [StringLength(200, ErrorMessage = "Company Name cannot be more than 200 characters or less than 10", MinimumLength = 10)]
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        [Required]
        public string Address1
        {
            get => this.address1;
            set => this.SetProperty(ref this.address1, value);
        }

        public string Address2
        {
            get => this.address2;
            set => this.SetProperty(ref this.address2, value);
        }

        [Required]
        public string City
        {
            get => this.city;
            set => this.SetProperty(ref this.city, value);
        }

        [Required]
        public string Country
        {
            get => this.country;
            set => this.SetProperty(ref this.country, value);
        }

        [Required]
        public string Province
        {
            get => this.province;
            set => this.SetProperty(ref this.province, value);
        }

        [Required]
        [RegularExpression("^([ABCEGHJKLMNPRSTVXY]\\d[ABCEGHJKLMNPRSTVWXYZ])\\ {0,1}(\\d[ABCEGHJKLMNPRSTVWXYZ]\\d)$", ErrorMessage = "Invalid Postal Code")]
        public string PostalCode
        {
            get => this.postalCode;
            set => this.SetProperty(ref this.postalCode, value);
        }
    }
}
