using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]

[SimpleJob(-1, -1, 20)]
public class GameWorldBenchmark
{
    GameWorldVersion1 gameWorld;


    [GlobalSetup]
    public void Setup()
    {
        gameWorld = new GameWorldVersion1();
        gameWorld.Instantiate(1024, 2048, 2048);
    }

    [Benchmark]
    public void GameWorldVersion1()
    {
        gameWorld.Increment();
    }
}