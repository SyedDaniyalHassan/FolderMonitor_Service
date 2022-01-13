using System;
using System.Runtime.InteropServices;
using Topshelf;
using Topshelf.Runtime.DotNetCore;

namespace K180239_Q3
{
    class Program
    {
        static void Main(string[] args)

        {
            var is_windows = false;
            var exitcode = HostFactory.Run(x =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||RuntimeInformation.IsOSPlatform(OSPlatform.Linux) )
                {
                    x.UseEnvironmentBuilder(
                      target => new DotNetCoreEnvironmentBuilder(target)
                    );
                    is_windows = false;
                }
                else
                {
                    is_windows = true;
                }
                x.Service<MonitorService>(s =>
                {
                    s.ConstructUsing(monitor => new MonitorService());
                    s.WhenStarted(monitor => monitor.start());
                    s.WhenStopped(monitor => monitor.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("FileMonitoringService");
                x.SetDisplayName("File Monitoring Service");
                x.SetDescription("This Service is use to Monitor the Specific Folder and Keep Track the changes");
            });
            if(is_windows==true)
            {
                int exitCodeValue = (int)Convert.ChangeType(exitcode , exitcode.GetTypeCode());
                Environment.ExitCode = exitCodeValue;
            }
            
        }
    }
}
