namespace DioLive.GaStEn.Engine.Mastermind
{
    public class TestResult
    {
        public TestResult(int bulls, int cows)
        {
            this.Bulls = bulls;
            this.Cows = cows;

            this.Success = true;
        }

        private TestResult()
        {
            this.Success = false;
        }

        public static TestResult Failed { get; } = new TestResult();

        public int Bulls { get; }

        public int Cows { get; }

        public bool Success { get; }
    }
}