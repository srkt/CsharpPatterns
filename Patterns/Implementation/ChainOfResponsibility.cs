using Patterns;
using Patterns.Events;
using System;
using System.Collections.Generic;

namespace PatternConsole
{
    public class FirstStep : IStep<int, string>
    {
        IStep nextStep;

        public void next(IStep nextStep)
        {
            this.nextStep = nextStep;
        }


        public object Transform(object input)
        {
            var r =  this.Transform((int)input);
            return r;
        }

        public string Transform(int input)
        {
            var x =  this.nextStep.Transform(input).ToString();
            return x;

        }
    }

    public class StepIntToString : IStep<int, String>
    {
        IStep nextStep;

        public void next(IStep nextStep) => this.nextStep = nextStep;

        public string Transform(int input)
        {

            if (input == 10) throw new Exception($"Value of {nameof(input)} cannot be 10");

            Console.WriteLine(this.ToString());
            var result = (input * 50).ToString();
            if (this.nextStep != null)
            {
                var x =  this.nextStep.Transform(result).ToString();
                return x;
            }
            return result;
        }

        public object Transform(object input)
        {
            return this.Transform((int)input);
        }


    }

    public class StepStringToInt : IStep<string, int>
    {
        public IStep nextStep { get; set; }
        public void next(IStep nextStep)
        {
            this.nextStep = nextStep;
        }

        public int Transform(string input)
        {
            Console.WriteLine(this.ToString());
            var result =  int.Parse(input + "000");
            if (this.nextStep != null)
            {
                var x = (int)this.nextStep.Transform(result);
                return x;
            }
            return result;

        }

        public object Transform(object input)
        {
            return (int)this.Transform(input as string);
        }


    }

    public class Pipeline
    {
        readonly IList<IStep> steps;
        public event PipelineStarted pipelineStarted;
        public event PipelineCompleted pipelineCompleted;
        public event PipelineError pipelineError;

        

        public Pipeline(IList<IStep> steps)
        {
            this.steps = steps;
        }

        public Pipeline(params IStep[] steps)
        {
            this.steps = steps;
            this.buildPipeline();
        }


        public void buildPipeline()
        {
            IStep previousStep = null;
            foreach (var step in steps)
            {
                if (previousStep != null) previousStep.next(step);
                previousStep = step;
            }
        }

        public void Start<T>(T input)
        {
            if (pipelineStarted != null)
            {
                pipelineStarted(new PipelineEventArgs("Pipeline Started"));
            }
            try
            {
                var result = this.steps[0].Transform(input);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                if(this.pipelineError != null)
                {
                    pipelineError(new PipelineEventArgs("Pipeline error " +ex.Message));
                }
            }
            finally
            {
                if(pipelineCompleted != null)
                {
                    pipelineCompleted(new PipelineEventArgs("Pipeline Finished"));
                }
            }

        }

    }
    
    
}
