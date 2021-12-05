using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
namespace grpcserver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(kerstrel =>
                    {
                        kerstrel.Listen(IPAddress.Any, 5000, o => o.Protocols = HttpProtocols.Http1AndHttp2);
                        kerstrel.Listen(IPAddress.Any, 5001, listenOptions =>
                    {
                        string serverPath = AppDomain.CurrentDomain.BaseDirectory + "cert\\server.pfx";

                        X509Certificate2 serverCertificate = new X509Certificate2(serverPath, "123456789");

                        HttpsConnectionAdapterOptions httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions()
                        {
                            ClientCertificateMode = ClientCertificateMode.AllowCertificate,
                            SslProtocols = SslProtocols.Tls12,
                            ClientCertificateValidation = (cer, chain, error) => chain.Build(cer),
                            ServerCertificate = serverCertificate
                        };

                        listenOptions.UseHttps(httpsConnectionAdapterOptions);

                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    });


                    });

                });
        }
    }
}
