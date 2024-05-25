namespace BLBConcordance.Core {
    public interface IOperation {
        public object Operate(IWebCrawler webCrawler, dynamic input);
    }
}