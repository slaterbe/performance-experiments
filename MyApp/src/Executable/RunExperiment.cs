using BenchmarkDotNet.Running;

public static class RunExperiments
{
    public static void ExecuteTests()
    {
       BenchmarkRunner.Run<MyBenchmark>();
    }
}