using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface IPdfReportHandler
    {
        public Task<HttpResponseMessage> GetAllPdLogs();
        public Task<HttpResponseMessage> GetPdfDocumentById(string documentId);        
    }
}
