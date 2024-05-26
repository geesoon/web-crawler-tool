namespace BLBConcordance.Core.Interfaces
{
    /// <summary>
    /// Chaining operation pipeline across multiple web site and aggregate the results.
    /// </summary>
    public interface IWorkFlow
    {
        public IWorkFlow AddPipeline(IWebOperationPipeline pipeline);
        public IWorkFlow RemovePipeline(IWebOperationPipeline pipeline);
        public IWorkFlow Execute();
        public IWorkFlow OutputResults(IFileWriter fileWriter);
    }
}