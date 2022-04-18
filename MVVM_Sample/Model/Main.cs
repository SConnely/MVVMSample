
namespace MVVM_Sample.Model
{
    using System.Collections.Generic;
    using MVVM_Sample.Model.Base;

    public class Main : EntityBase
    {
        public string ActiveModel { get; set; } = "Company";

        public List<string> OpenModels { get; set; } = new List<string>();
    }
}
