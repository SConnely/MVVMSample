namespace MVVM_Sample.ViewModel
{
    using MVVM_Sample.Model;
    using MVVM_Sample.ViewModel.Base;

    public class DepartmentViewModel : BaseViewModel<Department>
    {
        public DepartmentViewModel()
           : base()
        {
            this.Entity = new Department();
        }

        public override void OnSaveEvent(string buttonName)
        {
            base.OnSaveEvent(buttonName);

            this.ChangedText = "Save Called";
        }

        public override void OnCancelEvent(string buttonName)
        {
            base.OnCancelEvent(buttonName);

            this.Entity = new Department();

            this.ChangedText = "Cancel Called";
        }

        public override void OnDeleteEvent(string buttonName)
        {
            base.OnDeleteEvent(buttonName);

            this.Entity = new Department();

            this.ChangedText = "Department Deleted";
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
