namespace MVVM_Sample.Model.Interface
{
    public interface IModel
    {
        bool HasEntityChanged { get; set; }

        string ChangedText { get; set; }
    }
}
