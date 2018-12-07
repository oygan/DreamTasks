namespace Dream.Business.Services.TaskFlowStrategies
{
    /// <summary>
    /// Validate model is used by workflow strategies.
    /// </summary>
    internal class ValidateItem
    {
        public bool IsSuccess { get; set; } = true;
        public string ErrorText { get; set; }           
    }
}