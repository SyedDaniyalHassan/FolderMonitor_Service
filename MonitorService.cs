using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace K180239_Q3
{
    public class MonitorService
    {
        private readonly Timer __timer ; 
        private bool changes ;
        private int counter;

        public MonitorService()
        {
            //Initial Timer of 2 Minute
            __timer = new Timer(120000){AutoReset = true};
            __timer.Elapsed += Monitoring;
            counter = 0;
        }

        private void Monitoring(object sender , ElapsedEventArgs e)
        {
            changes =false;
            using (var fsw = new FileSentry(@"/home/daniyal/Desktop/K180239_Q3/Monitor_folder"))
        {
            
            fsw.Created += fsw_Created;
            fsw.Changed += fsw_Created;
            fsw.EnableRaisingEvents = true;
            if(!changes)
        {
            if(counter <3600000)
            {
                counter +=120000;
                __timer.Interval = counter;
            }
        }
            Console.WriteLine("Interval changes to {0}",counter);

            Console.ReadLine();
        }
        

        }


        private void fsw_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Event triggered");
            File.Copy(@"/home/daniyal/Desktop/K180239_Q3/Monitor_folder/"+e.Name ,@"/home/daniyal/Desktop/K180239_Q3/Copy_folder/"+e.Name , true);
            __timer.Interval = 120000;
            changes = true;
 
        }

        

        public void start ()
        {
            __timer.Start();
        }
        public void Stop()
        {
            __timer.Stop();
        }
    }
}