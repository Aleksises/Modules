namespace MvcMusicStore.Monitoring
{
    public static class CounterConstants
    {
        public const string MusicStoreCountersCategory = "MusicStoreCounters";
        public const string MusicStoreCountersHelper = "Counters for MusicStore App";
        public const string SuccessLogInCounterName = "SuccessLogInCounter";
        public const string SuccessLogInCounterHelper = "Counts a number of successful log in attempts.";
        public const string SuccessLogOffCounterName = "SuccessLogOffCounter";
        public const string SuccessLogOffCounterHelper = "Counts a number of successful log off attempts.";
        public const string RequestAverageTimeCounterName = "RequestAverageTimeCounter";
        public const string RequestAverageTimeCounterHelper = "Counts average time of requests.";
        public const string BaseAverageCounterName = "BaseAverageCounter";
        public const string BaseAverageCounterHelper = "The base counter for average counters.";
    }
}