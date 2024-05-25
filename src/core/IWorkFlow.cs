namespace BLBConcordance.Core {
    /// <summary>
    /// Chaining operation pipeline across multiple web site and aggregate the results.
    /// </summary>
    public interface IWorkFlow {
        
        public void AddPipeline(IWebOperationPipeline pipeline);
        public void RemovePipeline(IWebOperationPipeline pipeline);
        public void Execute();
    }
}