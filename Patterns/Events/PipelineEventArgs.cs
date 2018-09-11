namespace Patterns.Events
{
    public class PipelineEventArgs
    {
        public string name;

        public PipelineEventArgs(string name)
        {
            this.name = name;
        }
    }
}