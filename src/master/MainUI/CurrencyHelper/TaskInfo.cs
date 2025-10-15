namespace MainUI.CurrencyHelper
{
    public class TaskInfo
    {
        public string TaskName { get; set; }
        public Task Task { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
    }

}