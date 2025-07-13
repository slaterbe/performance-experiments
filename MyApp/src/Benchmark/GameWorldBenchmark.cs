using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]

[SimpleJob(-1, -1, 20)]
public class GameWorldBenchmark
{
    GameWorldVersion1 gameWorld;

    [GlobalSetup]
    public void Setup()
    {
        var configuration = new FlockingConfiguration();
        configuration.WorldWidth = 2048;
        configuration.WorldHeight = 2048;
        configuration.BoidCount = 1024;

        configuration.PerceptionDistance = 100f;
        configuration.DesiredSeparation = 20f;

        configuration.AlignmentWeight = 1.0f;
        configuration.CohesionWeight = 1.0f;
        configuration.SeparationWeight = 1.0f;

        gameWorld = new GameWorldVersion1(configuration);
    }

    [Benchmark]
    public void GameWorldVersion1()
    {
        gameWorld.Increment();
    }
}