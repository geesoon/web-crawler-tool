using BLBConcordance.Core.Interfaces;

namespace BLBConcordance.Core.Services
{
    /// <summary>
    /// A base workflow implementation
    /// </summary>
    public sealed class WorkFlowBase : IWorkFlow
    {
        private IEnumerable<IWebOperationPipeline> Pipelines { get; set; } = [];
        public WorkFlowBase()
        {
        }

        public IWorkFlow AddPipeline(IWebOperationPipeline pipeline)
        {
            this.Pipelines = this.Pipelines.Append(pipeline);
            return this;
        }

        public IWorkFlow RemovePipeline(IWebOperationPipeline pipeline)
        {
            var pipelines = this.Pipelines.ToList();

            if (pipelines.Remove(pipeline))
            {
                this.Pipelines = pipelines;
            }
            return this;
        }

        public IWorkFlow Execute()
        {
            foreach (var pipeline in this.Pipelines)
            {
                pipeline.Execute();
            }
            return this;
        }

        public IWorkFlow OutputResults(IFileWriter fileWriter)
        {
            var allResults = new List<object>();
            foreach (var pipeline in this.Pipelines)
            {
                var results = pipeline.GetResults().ToList();
                allResults = [.. allResults, .. results];
            }
            fileWriter.WriteToFile($"./output/{GetType().Name}_result.json", allResults);
            return this;
        }
    }
}