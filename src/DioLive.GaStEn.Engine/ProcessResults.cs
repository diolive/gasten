namespace DioLive.GaStEn.Engine
{
    public enum ProcessResults
    {
        /// <summary>
        /// Message was successfully processed
        /// </summary>
        Success,

        /// <summary>
        /// Message was not processed due to wrong message sequence or game state
        /// </summary>
        NoAction,

        /// <summary>
        /// Message processing was failed
        /// </summary>
        Failed,
    }
}