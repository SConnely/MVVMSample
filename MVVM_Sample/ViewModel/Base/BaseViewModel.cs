
namespace MVVM_Sample.ViewModel.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using MVVM_Sample.Model.Interface;
    using MVVM_Sample.Relay;

    public class BaseViewModel<TModel> : INotifyPropertyChanged
        where TModel : class, IModel
    {
        private TModel? entity = (TModel?)Activator.CreateInstance(typeof(TModel));

        private string changedText = string.Empty;

        private readonly Dictionary<string, string> changedItems = new Dictionary<string, string>();

        private readonly ICommand buttonClickEvent;
        private readonly ICommand saveClickEvent;
        private readonly ICommand cancelClickEvent;
        private readonly ICommand deleteClickEvent;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {
            this.buttonClickEvent = new RelayCommand(OnClickCommand);
            this.saveClickEvent = new RelayCommand(OnSaveCommand);
            this.cancelClickEvent = new RelayCommand(OnCancelCommand);
            this.deleteClickEvent = new RelayCommand(OnDeleteCommand);
        }

        public ICommand ButtonClickEvent => this.buttonClickEvent;
        public ICommand SaveClickEvent => this.saveClickEvent;
        public ICommand CancelClickEvent => this.cancelClickEvent;
        public ICommand DeleteClickEvent => this.deleteClickEvent;

        public bool HasViewModelChanged { get; set; }

        public TModel? Entity
        {
            get => this.entity;
            set => this.SetProperty(ref this.entity, value);
        }

        public Dictionary<string, string> ChangedItems => this.changedItems;

        public string ChangedText
        {
            get => this.changedText;
            set => this.SetProperty(ref this.changedText, value);
        }


        public virtual void OnClickEvent(string buttonName)
        {

        }

        public virtual void OnSaveEvent(string buttonName)
        {

        }

        public virtual void OnCancelEvent(string buttonName)
        {

        }

        public virtual void OnDeleteEvent(string buttonName)
        {

        }

        public virtual void OnEntityPropertyChanged<T>(string propertyName, T originalValue, T newValue)
        {
            //this.ChangedText = $"Property: {propertyName} \nOld Value: {originalValue}\nNew Value: {newValue}";
        }

        public void LoadControl(string name)
        {
            var app = Application.Current as App;

            if (app != null)
            {
                app.ActivateControl(name);
            }
        }

        private void OnSaveCommand(object obj)
        {
            var saveCommandName = obj as string;

            this.OnSaveEvent(saveCommandName);
        }

        private void OnCancelCommand(object obj)
        {
            var cancelCommandName = obj as string;

            this.OnCancelEvent(cancelCommandName);
        }

        private void OnDeleteCommand(object obj)
        {
            var deleteCommandName = obj as string;

            this.OnDeleteEvent(deleteCommandName);
        }

        private void OnClickCommand(object obj)
        {
            if (obj is Button button)
            {
                this.OnClickEvent(button.Name);
            }
            else if (obj is string callbackName)
            {
                this.OnClickEvent(callbackName);
            }
            else
            {
                var buttonName = obj.GetType().GetProperty("Name")?.GetValue(obj, null)?.ToString() ?? "Unknown";

                this.OnClickEvent(buttonName);
            }
        }
      
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "Unknown")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

                if (propertyName != nameof(this.HasViewModelChanged))
                {
                    this.HasViewModelChanged = true;
                }
            }
        }

        public virtual void SetProperty<T>(
            ref T privateValue, 
            T newValue, 
            bool alwaysUpdate = false,
            [CallerMemberName]string propertyName = "Unknown")
        {
            if (privateValue == null)
            {
                this.HasViewModelChanged = false;
                privateValue = newValue;
            }

            if (propertyName == nameof(this.HasViewModelChanged)
               || propertyName == nameof(this.ChangedText))
            {
                privateValue = newValue;
            }

            if ((propertyName != nameof(this.HasViewModelChanged)
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

                if (propertyName == nameof(this.Entity))
                {
                    this.OnEntityPropertyChanged(propertyName, privateValue, newValue);
                }

                privateValue = newValue;

                if (valueChanged && !string.IsNullOrEmpty(propertyName))
                {
                    if (this.ChangedItems.ContainsKey(propertyName))
                    {
                        this.ChangedItems[propertyName] = changedValue;
                    }
                    else
                    {
                        this.ChangedItems.Add(propertyName, changedValue);
                    }

                    this.HasViewModelChanged = true;
                }
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
