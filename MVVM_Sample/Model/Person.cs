namespace MVVM_Sample.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using MVVM_Sample.Model.Base;

    public class Person : EntityBase
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private string phone;

        public int Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        [Required]
        [StringLength(50, ErrorMessage = "Invalid Length",  MinimumLength = 5)]
        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }

        [Required]
        [StringLength(50, ErrorMessage = "Invalid Length", MinimumLength = 5)]
        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }

        [Required]
        //[RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*", ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone
        {
            get => this.phone;
            set => this.SetProperty(ref this.phone, value);
        }
    }
}
