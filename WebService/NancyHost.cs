using System;
using Nancy.Hosting.Self;

namespace WebService
{
    public class NancyHost
    {
        public void Start()
        {
            var config = new HostConfiguration
            {
                UrlReservations = new UrlReservations {CreateAutomatically = true}
            };
            var uri = new Uri("http://localhost:80/");
            using (var host = new Nancy.Hosting.Self.NancyHost(config, uri))
            {
                host.Start();
                Console.WriteLine("Listening at port 80, press a key to stop");
                Console.ReadKey();
            }
        }
    }
}
