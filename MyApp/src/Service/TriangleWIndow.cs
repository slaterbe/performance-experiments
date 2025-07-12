using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class TriangleWindow : GameWindow
{
    public static TriangleWindow Singleton;

    public static int SIZE = 512;

    int _vertexBufferObject;
    int _vertexArrayObject;
    int _shaderProgram;

    private bool _bufferInitialized = false;

    public static int[] xVertex;
    public static int[] yVertex;

    public static void UpdateVertices()
    {
        if (Singleton == null) return;

        var newVertices = new float[xVertex.Length * 6];
        var offset = 10f;

        for (int i = 0; i < xVertex.Length; i++)
        {
            var newX = (float)xVertex[i];
            var newY = (float)yVertex[i];

            newVertices[i * 6] = newX - offset;
            newVertices[i * 6 + 1] = newY - offset;

            newVertices[i * 6 + 2] = newX;
            newVertices[i * 6 + 3] = newY;

            newVertices[i * 6 + 4] = newX + offset;
            newVertices[i * 6 + 5] = newY - offset;
        }

        Singleton._vertices = newVertices;
    }

    float[] _vertices = {
         0.0f,  0.2f, // top
        -0.2f, -0.2f, // left
         0.2f, -0.2f  // right
    };

    string _vertexShaderSrc = @"
        #version 330 core

        layout(location = 0) in vec2 aPosition;

        uniform mat4 uProjection;  // Model-View-Projection matrix

        void main()
        {
            gl_Position = uProjection * vec4(aPosition, 0.0, 1.0);
        }
    ";

    string _fragmentShaderSrc = @"
        #version 330 core
        out vec4 FragColor;
        void main()
        {
            FragColor = vec4(1.0, 0.0, 0.0, 1.0); // red
        }
    ";

    public TriangleWindow(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) { }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0f, 0f, 0f, 1f); // black background
        GL.Viewport(0, 0, SIZE, SIZE);

        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);

        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, _vertexShaderSrc);
        GL.CompileShader(vertexShader);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, _fragmentShaderSrc);
        GL.CompileShader(fragmentShader);

        _shaderProgram = GL.CreateProgram();
        GL.AttachShader(_shaderProgram, vertexShader);
        GL.AttachShader(_shaderProgram, fragmentShader);
        GL.LinkProgram(_shaderProgram);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);
        int[] viewport = new int[4];
        GL.GetInteger(GetPName.Viewport, viewport);

        GL.Viewport(0, 0, SIZE * 2, SIZE * 2);

        // Upload new data every frame
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, _vertices.Length * sizeof(float), _vertices);

        GL.UseProgram(_shaderProgram);

        // Now set up the orthographic projection
        Matrix4 ortho = Matrix4.CreateOrthographicOffCenter(0, SIZE, 0, SIZE, -1, 1);
        int matrixLocation = GL.GetUniformLocation(_shaderProgram, "uProjection");
        GL.UniformMatrix4(matrixLocation, false, ref ortho);

        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 2);

        SwapBuffers();
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        GL.DeleteBuffer(_vertexBufferObject);
        GL.DeleteVertexArray(_vertexArrayObject);
        GL.DeleteProgram(_shaderProgram);
    }

    public static void Main(int size)
    {
        var gws = GameWindowSettings.Default;
        var nws = new NativeWindowSettings()
        {
            Size = new Vector2i(SIZE, SIZE),
            Title = "OpenTK Triangle"
        };
        SIZE = size;
        using var window = new TriangleWindow(gws, nws);
        Singleton = window;

        xVertex = new int[] { 10, 128, 200, SIZE - 40, 200 };
        yVertex = new int[] { 10, 128, 200, SIZE - 40, 10 };

        UpdateVertices();
            // Execute();

        window.Run();
    }

    public static void Execute()
    {
        xVertex = new int[] { 10, 128, 200, SIZE - 40, 200 };
        yVertex = new int[] { 150, 128, 200, SIZE - 40, 10 };

        UpdateVertices();
        Console.WriteLine("Updating vertexes");
    }
}