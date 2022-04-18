namespace MVVM_Sample.ViewModel
{
    using MVVM_Sample.Model;
    using MVVM_Sample.ViewModel.Base;

    public class PersonViewModel : BaseViewModel<Person>
    {
        public PersonViewModel()
            : base()
        {
            this.Entity = new Person();
        }

        public override void OnSaveEvent(string buttonName)
        {
            base.OnSaveEvent(buttonName);

            this.ChangedText = "Save Called";
        }

        public override void OnCancelEvent(string buttonName)
        {
            base.OnCancelEvent(buttonName);

            this.Entity = new Person();

            this.ChangedText = "Cancel Called";
        }

        public override void OnDeleteEvent(string buttonName)
        {
            base.OnDeleteEvent(buttonName);

            this.Entity = new Person();

            this.ChangedText = "Person Deleted";
        }

        public override void OnClickEvent(string buttonName)
        {
            base.OnClickEvent(buttonName);

            switch (buttonName)
            {
                case "Main":
                    {
                        this.LoadControl(buttonName);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
