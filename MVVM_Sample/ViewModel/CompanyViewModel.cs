
namespace MVVM_Sample.ViewModel
{
    using MVVM_Sample.Model;
    using MVVM_Sample.ViewModel.Base;

    public class CompanyViewModel : BaseViewModel<Company>
    {
        public CompanyViewModel()
            : base()
        {
            this.Entity = new Company();
        }

        public override void OnSaveEvent(string buttonName)
        {
            base.OnSaveEvent(buttonName);

            this.ChangedText = "Save Called";
        }

        public override void OnCancelEvent(string buttonName)
        {
            base.OnCancelEvent(buttonName);

            this.Entity = new Company();

            this.ChangedText = "Cancel Called";
        }

        public override void OnDeleteEvent(string buttonName)
        {
            base.OnDeleteEvent(buttonName);

            this.Entity = new Company();

            this.ChangedText = "Company Deleted";
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
