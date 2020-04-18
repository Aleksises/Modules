using System;
using System.Diagnostics;

namespace MvcMusicStore.Monitoring
{
    public static class PerformanceCountersMonitor
    {
        public static void CreateCounterCategory()
        {
            if (PerformanceCounterCategory.Exists(CounterConstants.MusicStoreCountersCategory))
            {
                PerformanceCounterCategory.Delete(CounterConstants.MusicStoreCountersCategory);
            }

            var counterCreationDataCollection = new CounterCreationDataCollection
            {
                new CounterCreationData(
                    CounterConstants.SuccessLogInCounterName,
                    CounterConstants.SuccessLogInCounterHelper,
                    PerformanceCounterType.NumberOfItems32),
                new CounterCreationData(
                    CounterConstants.SuccessLogOffCounterName,
                    CounterConstants.SuccessLogOffCounterHelper,
                    PerformanceCounterType.NumberOfItems32),
                new CounterCreationData(
                    CounterConstants.RequestAverageTimeCounterName,
                    CounterConstants.RequestAverageTimeCounterHelper,
                    PerformanceCounterType.CounterDelta32),
            };

            try
            {
                PerformanceCounterCategory.Create(
                    CounterConstants.MusicStoreCountersCategory,
                    CounterConstants.MusicStoreCountersHelper,
                    PerformanceCounterCategoryType.SingleInstance,
                    counterCreationDataCollection);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Do not have permissions. You should run app as Admin");
            }
        }

        public static void IncrementLogInCounter()
        {
            using (var logInCounter = new PerformanceCounter(
                CounterConstants.MusicStoreCountersCategory,
                CounterConstants.SuccessLogInCounterName,
                false))
            {
                logInCounter.Increment();
            }
        }

        public static void IncrementLogOffCounter()
        {
            using (var logOffCounter = new PerformanceCounter(
                CounterConstants.MusicStoreCountersCategory,
                CounterConstants.SuccessLogOffCounterName,
                false))
            {
                logOffCounter.Increment();
            }
        }

        public static void IncrementRequestAverageTimeCounter(long milliseconds)
        {
            using (var requestAverageTimeCounter = new PerformanceCounter(
                CounterConstants.MusicStoreCountersCategory,
                CounterConstants.RequestAverageTimeCounterName,
                false))
            {
                requestAverageTimeCounter.IncrementBy(milliseconds);
            }
        }
    }
}