namespace ModuleB.Views
{
    public interface IActivityView
    {
        void SetTitle(string title);

        void SetCustomerId(string customerId);

        void AddContent(string content);
    }
}
