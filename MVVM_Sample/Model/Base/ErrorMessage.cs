
namespace MVVM_Sample.Model
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class ErrorMessage : INotifyPropertyChanged
    {
        private string propertyName = string.Empty;
        private string message = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string PropertyName
        {
            get => this.propertyName;
            set
            {
                this.propertyName = value;

                this.OnPropertyChanged();
            }
        }

        public string Message
        {
            get => this.message;
            set
            {
                this.message = value;

                this.OnPropertyChanged();
            }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "Unknown")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
