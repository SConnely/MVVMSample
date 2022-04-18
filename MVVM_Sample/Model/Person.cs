namespace MVVM_Sample.Model
{
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

        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }

        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        public string Phone
        {
            get => this.phone;
            set => this.SetProperty(ref this.phone, value);
        }
    }
}
