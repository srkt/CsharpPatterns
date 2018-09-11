using Patterns;
using Patterns.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipeline = new Pipeline(
                new FirstStep(),
                new StepIntToString(),
                new StepStringToInt()
            );



            pipeline.pipelineStarted += (PipelineEventArgs pipelineEventArgs) => Console.WriteLine(pipelineEventArgs.name);
            pipeline.pipelineCompleted += (PipelineEventArgs pipelineEventArgs) => Console.WriteLine(pipelineEventArgs.name);
            pipeline.pipelineError += (PipelineEventArgs pipelineEventArgs) => Console.WriteLine(pipelineEventArgs.name);


            pipeline.Start(5);
            Console.ReadKey();
        }


    }
}
