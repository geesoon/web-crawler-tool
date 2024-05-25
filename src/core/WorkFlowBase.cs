using BLBConcordance.Core;

namespace BLBConcordance.WorkFlow
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

        public void AddPipeline(IWebOperationPipeline pipeline)
        {
            this.Pipelines = this.Pipelines.Append(pipeline);
        }

        public void RemovePipeline(IWebOperationPipeline pipeline)
        {
            var pipelines = this.Pipelines.ToList();

            if (pipelines.Remove(pipeline))
            {
                this.Pipelines = pipelines;
            }
        }

        public void Execute()
        {
            foreach (var pipeline in this.Pipelines)
            {
                pipeline.Execute();
                var results = pipeline.GetResults().ToList();
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}