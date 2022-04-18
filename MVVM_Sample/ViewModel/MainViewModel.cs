
namespace MVVM_Sample.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using MVVM_Sample.Model;
    using MVVM_Sample.View;
    using MVVM_Sample.ViewModel.Base;

    public class MainViewModel : BaseViewModel<Main>
    {
        private Assembly rootAssembly = null;

        public UserControl childControl = null;

        public MainViewModel()
            : base()
        {
            this.rootAssembly = Assembly.GetExecutingAssembly();

            this.ChildControl = new MainView();
            this.ChildControl.DataContext = this;
        }

        public UserControl ChildControl 
        {
            get => this.childControl;
            set => this.SetProperty(ref this.childControl, value, true);
        }

        public override void OnClickEvent(string buttonName)
        {
            base.OnClickEvent(buttonName);

            switch (buttonName)
            {
                default:
                    this.LoadControl(buttonName);
                    break;
            }
        }
    }
}
