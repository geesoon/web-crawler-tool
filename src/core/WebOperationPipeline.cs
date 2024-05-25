using EnsureThat;

namespace BLBConcordance.Core
{
    public sealed class WebOperationPipeline : IWebOperationPipeline
    {
        public IEnumerable<IOperation> Operations { get; set; } = Enumerable.Empty<IOperation>();
        public IWebCrawler WebCrawler { get; set; }
        private IEnumerable<object> results = Enumerable.Empty<object>();

        public WebOperationPipeline(IWebCrawler webCrawler)
        {
            this.WebCrawler = EnsureArg.IsNotNull(webCrawler, nameof(webCrawler));
        }

        public void AddOperation(IOperation operation)
        {
            if (!this.Operations.Contains(operation))
            {
                this.Operations = this.Operations.Append(operation);
            }
        }

        public void RemoveOperation(IOperation operation)
        {
            var operations = this.Operations.ToList();
            if (operations.Remove(operation))
                this.Operations = operations;
        }

        public void Execute()
        {
            object nextInput = null;
            foreach (var operation in this.Operations)
            {
                nextInput = operation.Operate(this.WebCrawler, nextInput);
                this.results = this.results.Append(nextInput);
            }
        }

        public IEnumerable<object> GetResults(){
            return this.results;
        }
    }
}