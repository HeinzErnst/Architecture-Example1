using WebService;

namespace WebServiceConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new NancyServer();
            server.Start();
        }
    }
}
