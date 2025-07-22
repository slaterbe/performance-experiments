public static class RunRenderer
{
    public static void ExecuteRender()
    {
        var configuration = new FlockingConfiguration();
        configuration.WorldWidth = 1024;
        configuration.WorldHeight = 1024;
        configuration.BoidCount = 80;

        configuration.PerceptionDistance = 40f;
        configuration.DesiredSeparation = 40f;

        configuration.AlignmentWeight = 1.0f;
        configuration.CohesionWeight = 1.0f;
        configuration.SeparationWeight = 1.0f;

        var gameWorld = new GameWorldVersion1(configuration);

        #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        Task.Run(() =>
        {
            Thread.Sleep(500);
            while (true)
            {
                TriangleWindow.xVertex = gameWorld.GetBoidXPosition();
                TriangleWindow.yVertex = gameWorld.GetBoidYPosition();
                TriangleWindow.UpdateVertices();

                Thread.Sleep(30);
                gameWorld.Increment();
            }
        });
        #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed


        TriangleWindow.Main(configuration.WorldWidth, configuration.BoidCount);
    }
}
