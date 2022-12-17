namespace MVVM_Sample.Model
{
    using System.ComponentModel.DataAnnotations;
    using MVVM_Sample.Model.Base;

    public class Department : EntityBase
    {
        private int id = 0;
        private string name = string.Empty;
        private string description = string.Empty;

        public int Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        [Required]
        [StringLength(50, ErrorMessage = "Invalid Length", MinimumLength = 5)]
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        public string Description
        {
            get => this.description;
            set => this.SetProperty(ref this.description, value);
        }
    }
}
