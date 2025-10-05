using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class TriangleWindow : GameWindow
{
    public static TriangleWindow Singleton;

    public static int SIZE = 1024;

    int _vertexBufferObject;
    int _vertexArrayObject;
    int _shaderProgram;

    private bool _bufferInitialized = false;

    public static float[] xVertex;
    public static float[] yVertex;

    public static float[] xAlignment;
    public static float[] yAlignment;

    public static void UpdateVertices()
    {
        lock (Singleton._vertices)
        {
            if (Singleton == null) return;

            var newVertices = new float[xVertex.Length * 6];
            var offset = 10f;

            for (int i = 0; i < xVertex.Length; i++)
            {
                var newX = xVertex[i];
                var newY = yVertex[i];

                newVertices[i * 6] = newX - offset;
                newVertices[i * 6 + 1] = newY - offset;

                newVertices[i * 6 + 2] = newX;
                newVertices[i * 6 + 3] = newY;

                newVertices[i * 6 + 4] = newX + offset;
                newVertices[i * 6 + 5] = newY - offset;
            }

            Singleton._vertices = newVertices;
        }
    }

    float[] _vertices = {};

    string _vertexShaderSrc = @"
        #version 330 core

        layout(location = 0) in vec2 aPosition;

        uniform mat4 uProjection;  // Model-View-Projection matrix
        uniform mat4 uRotation;

        void main()
        {
            gl_Position = uRotation * uProjection * vec4(aPosition, 0.0, 1.0);
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
        lock (_vertices)
        {
            base.OnRenderFrame(args);

            float angleInRadians = MathHelper.DegreesToRadians(0);

            Matrix4 rotation = Matrix4.CreateRotationZ(angleInRadians);

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

            int rotationLocation = GL.GetUniformLocation(_shaderProgram, "uRotation");
            GL.UniformMatrix4(rotationLocation, false, ref rotation);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 2);

            SwapBuffers();   
        }
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        GL.DeleteBuffer(_vertexBufferObject);
        GL.DeleteVertexArray(_vertexArrayObject);
        GL.DeleteProgram(_shaderProgram);
    }

    public static void Main(int size, int boidCount)
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

        xVertex = new float[boidCount];
        xVertex[0] = 10f;
        xVertex[1] = 128f;
        xVertex[2] = 200f;
        xVertex[3] = size - 40f;
        xVertex[4] = 200f;

        yVertex = new float[boidCount];
        xVertex[0] = 10f;
        xVertex[1] = 128f;
        xVertex[2] = 200f;
        xVertex[3] = size - 40f;
        xVertex[4] = 10;

        UpdateVertices();

        window.Run();
    }

    public static void Execute()
    {
        xVertex = new float[] { 10, 128, 200, SIZE - 40, 200 };
        yVertex = new float[] { 150, 128, 200, SIZE - 40, 10 };

        UpdateVertices();
        Console.WriteLine("Updating vertexes");
    }
}