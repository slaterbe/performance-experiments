public static class RunRenderer
{
    public static void ExecuteRender()
    {
        #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() =>
        {
            Thread.Sleep(3000);
            TriangleWindow.xVertex = new int[] { 150 };
            TriangleWindow.yVertex = new int[] { 150 };

            TriangleWindow.UpdateVertices();
        });
        #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed


        TriangleWindow.Main(512);
    }
}
