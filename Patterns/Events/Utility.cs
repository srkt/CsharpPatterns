using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Events
{

    public delegate void PipelineStarted(PipelineEventArgs pipelineEventArgs);
    public delegate void PipelineCompleted(PipelineEventArgs pipelineEventArgs);
    public delegate void PipelineError(PipelineEventArgs pipelineEventArgs);


}
