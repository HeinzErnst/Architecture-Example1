using WebService;

namespace WebServiceConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new NancyHost();
            server.Start();
        }
    }
}
