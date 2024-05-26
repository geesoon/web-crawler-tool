namespace BLBConcordance.Core.Interfaces
{
    public interface IWebOperation<out TOutput, in TContext> : IOperation
    {
        TOutput Operate(IWebCrawler webCrawler, TContext context);
    }
}