namespace DioLive.GaStEn.Engine
{
    public class ProcessResult
    {
        private ProcessResult(ProcessResults result, string statusMessage)
            : this(result, statusMessage, null)
        {
        }

        private ProcessResult(ProcessResults result, string statusMessage, State nextState)
        {
            this.Result = result;
            this.StatusMessage = statusMessage;
            this.State = nextState;
        }

        public string StatusMessage { get; }

        public bool Success => this.Result == ProcessResults.Success;

        internal ProcessResults Result { get; }

        internal State State { get; }

        public static ProcessResult Ok(State newState, string description = "OK")
            => new ProcessResult(ProcessResults.Success, description, newState);

        public static ProcessResult NoAction(string description = "No action was performed")
            => new ProcessResult(ProcessResults.NoAction, description);

        public static ProcessResult Failed(string errorDescription)
            => new ProcessResult(ProcessResults.Failed, errorDescription);

        public static ProcessResult NotSupported()
            => Failed("Message type is not supported");
    }
}