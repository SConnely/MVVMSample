namespace MVVM_Sample.ViewModel
{
    using MVVM_Sample.Model;
    using MVVM_Sample.ViewModel.Base;

    public class DepartmentViewModel : BaseViewModel<Department>
    {
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
