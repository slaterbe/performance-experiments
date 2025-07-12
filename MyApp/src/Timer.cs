using System;
using System.Diagnostics;

public static class Timer
{
    public static void Time(string message, Action action)
    {
        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        Console.WriteLine($"{message}: {stopwatch.ElapsedMilliseconds} ms");
    }

    public static void RunExperiment(string message, int count, Action action)
    {
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < count; i++)
        {
            action();
        }
        stopwatch.Stop();
        Console.WriteLine($"{message}: {stopwatch.ElapsedMilliseconds / count} ms");
    }
}
