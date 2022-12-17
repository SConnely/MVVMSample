
namespace MVVM_Sample.Model.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using MVVM_Sample.Model.Interface;

    public class EntityBase : INotifyPropertyChanged, IModel, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string changedText = string.Empty;

        private ObservableCollection<ErrorMessage> errors = new ObservableCollection<ErrorMessage>();

        public bool HasEntityChanged { get; set; }

        public string ChangedText
        {
            get => this.changedText;
            set => this.SetProperty(ref this.changedText, value);
        }

        public ObservableCollection<ErrorMessage> Errors
        {
            get => this.errors;
            set => this.SetProperty(ref this.errors, value);
        }

        public string Error => throw new NotImplementedException();

        public string this[string propertyName]
        {
            get
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    throw new ArgumentException("Invalid property name", propertyName);
                }

                string error = string.Empty;

                var value = GetPropertyValue(propertyName);

                var results = new List<ValidationResult>(1);

                var result = Validator.TryValidateProperty(
                    value,
                    new ValidationContext(this, null, null)
                    {
                        MemberName = propertyName
                    },
                    results);

                if (!result)
                {
                    error = results.First().ErrorMessage;

                    if (this.Errors.Any(x => x.PropertyName == propertyName))
                    {
                        var errorMessage = this.Errors.FirstOrDefault(x => x.PropertyName == propertyName);
                        errorMessage.Message = error;
                    }
                    else
                    {
                        this.Errors.Add(new ErrorMessage()
                        {
                            PropertyName = propertyName,
                            Message = error
                        });
                    }
                }

                return error;
            }
        }

        private object GetPropertyValue(string propertyName)
        {
            var propInfo = GetType().GetProperty(propertyName);
            return propInfo.GetValue(this);
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
                var existingError = this.Errors.FirstOrDefault(x => x.PropertyName == propertyName);

                if (existingError != null)
                {
                    this.Errors.Remove(existingError);
                }

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
