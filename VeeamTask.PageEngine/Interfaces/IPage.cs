namespace VeeamTask.PageEngine.Interfaces
{
    public interface IPage
    {
        void Open();

        void Close();

        bool CheckOpened();

        void Refresh();
    }
}