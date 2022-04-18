
namespace MVVM_Sample.Model.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using MVVM_Sample.Model.Interface;

    public class EntityBase : INotifyPropertyChanged, IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string changedText = string.Empty;

        public bool HasEntityChanged { get; set; }

        public string ChangedText
        {
            get => this.changedText;
            set => this.SetProperty(ref this.changedText, value);
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "Unknown")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

                if (propertyName != nameof(this.HasEntityChanged))
                {
                    this.HasEntityChanged = true;
                }
            }
        }

        public virtual void SetProperty<T>(
          ref T privateValue,
          T newValue,
          bool alwaysUpdate = false,
          [CallerMemberName] string propertyName = "Unknown")
        {
            if (privateValue == null)
            {
                this.HasEntityChanged = false;
                privateValue = newValue;
            }

            if (propertyName == nameof(this.HasEntityChanged)
                || propertyName == nameof(this.ChangedText))
            {
                privateValue = newValue;
            }

            if ((propertyName != nameof(this.HasEntityChanged) 
                && propertyName != nameof(this.ChangedText))
                || alwaysUpdate)
            {
                bool valueChanged = false;

                string changedValue = $"{privateValue} -> {newValue}";

                if (newValue == null && privateValue == null)
                {
                    valueChanged = false;
                }

                if ((newValue != null && privateValue == null)
                    || (newValue == null && privateValue != null))
                {
                    valueChanged = true;
                }

                if (newValue != null && privateValue != null)
                {
                    valueChanged = !newValue.Equals(privateValue);
                }

                if (alwaysUpdate)
                {
                    valueChanged = true;
                }

                if (valueChanged)
                {
                    this.ChangedText = $"Property: {propertyName} \nOld Value: {privateValue}\n New Value: {newValue}";
                    this.HasEntityChanged = true;
                }

                privateValue = newValue;
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
