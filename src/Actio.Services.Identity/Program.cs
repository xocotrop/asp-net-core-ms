using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
       {
            // CreateWebHostBuilder(args).Build().Run();
            ServiceHost.Create<Startup>(args)
            .UserRabbitMq()
            .SubscribeToCommand<CreateUser>()
            .Build()
            .Run();
        }

        // public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>();
    }
}
