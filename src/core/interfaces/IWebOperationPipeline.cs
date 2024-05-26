namespace BLBConcordance.Core.Interfaces
{
    /// <summary>
    /// This IWebOperationPipeline responsible for chaining operations into a pipeline
    /// </summary>
    public interface IWebOperationPipeline
    {
        public void AddOperation(IOperation operation);
        public void RemoveOperation(IOperation operation);
        public void Execute();
        public IEnumerable<object> GetResults();
    }
}