using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

[MemoryDiagnoser]

[SimpleJob(-1, -1, 20)]
public class DistanceBenchmark
{
    public int size = 20000000;

    public float[] xPosition1;
    public float[] yPosition1;

    public float[] xPosition2;
    public float[] yPosition2;

    private Consumer consumer = new Consumer();

    [GlobalSetup]
    public void Setup()
    {
        xPosition1 = RandomNumberGenerator.GetRandomFloatValues(size, 50);
        yPosition1 = RandomNumberGenerator.GetRandomFloatValues(size, 50);

        xPosition2 = RandomNumberGenerator.GetRandomFloatValues(size, 50);
        yPosition2 = RandomNumberGenerator.GetRandomFloatValues(size, 50);
    }

    [Benchmark]
    public void EculidanDistance()
    {
        float total = 0;
        for (int i = 0; i < size; i++)
        {
            float x1 = xPosition1[i];
            float y1 = yPosition1[i];

            float x2 = xPosition2[i];
            float y2 = yPosition2[i];

            total += PrimitiveExtension.EculidanDistance(x1, y1, x2, y2);
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public void EculidanDistanceWithThread10()
    {
        Parallel.For(0, 4, counter =>
        {
            float total = 0;
            int step = size / 4;
            int start = counter * step;
            int end = step * (counter + 1);

            for (int i = start; i < end; i++)
            {
                float x1 = xPosition1[i];
                float y1 = yPosition1[i];

                float x2 = xPosition2[i];
                float y2 = yPosition2[i];

                total += PrimitiveExtension.EculidanDistance(x1, y1, x2, y2);
            }
            consumer.Consume(total);
        });
    }

        [Benchmark]
    public void EculidanDistanceWithThread20()
    {
        Parallel.For(0, 20, counter =>
        {
            float total = 0;
            int step = size / 20;
            int start = step * counter;
            int end = step * (counter + 1);

            for (int i = start; i < end; i++)
            {
                float x1 = xPosition1[i];
                float y1 = yPosition1[i];

                float x2 = xPosition2[i];
                float y2 = yPosition2[i];

                total += PrimitiveExtension.EculidanDistance(x1, y1, x2, y2);
            }
            consumer.Consume(total);
        });
    }

    [Benchmark]
    public void EculidanReciprocalDistance()
    {
        float total = 0;
        for (int i = 0; i < size; i++)
        {
            float x1 = xPosition1[i];
            float y1 = yPosition1[i];

            float x2 = xPosition2[i];
            float y2 = yPosition2[i];

            total += PrimitiveExtension.EculidanReciprocal(x1, y1, x2, y2);
        }
        consumer.Consume(total);
    }


    [Benchmark]
    public void SquaredEculidanDistance()
    {
        float total = 0;
        for (int i = 0; i < size; i++)
        {
            float x1 = xPosition1[i];
            float y1 = yPosition1[i];

            float x2 = xPosition2[i];
            float y2 = yPosition2[i];

            total += PrimitiveExtension.SquaredEculidanDistance(x1, y1, x2, y2);
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public void ManhattenDistancev1()
    {
        float total = 0;
        for (int i = 0; i < size; i++)
        {
            float x1 = xPosition1[i];
            float y1 = yPosition1[i];

            float x2 = xPosition2[i];
            float y2 = yPosition2[i];

            total += PrimitiveExtension.ManhattenDistanceV1(x1, y1, x2, y2);
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public void ManhattenDistancev2()
    {
        float total = 0;
        for (int i = 0; i < size; i++)
        {
            float x1 = xPosition1[i];
            float y1 = yPosition1[i];

            float x2 = xPosition2[i];
            float y2 = yPosition2[i];

            total += PrimitiveExtension.ManhattenDistanceV2(x1, y1, x2, y2);
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public void OctileDistance()
    {
        float total = 0;
        for (int i = 0; i < size; i++)
        {
            float x1 = xPosition1[i];
            float y1 = yPosition1[i];

            float x2 = xPosition2[i];
            float y2 = yPosition2[i];

            total += PrimitiveExtension.OctileDistance(x1, y1, x2, y2);
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public void FasterHypDistance()
    {
        float total = 0;
        for (int i = 0; i < size; i++)
        {
            float x1 = xPosition1[i];
            float y1 = yPosition1[i];

            float x2 = xPosition2[i];
            float y2 = yPosition2[i];

            total += PrimitiveExtension.FasterHypDistance(x1, y1, x2, y2);
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public void SquaredMultipleAddDistance()
    {
        float total = 0;
        for (int i = 0; i < size; i++)
        {
            float x1 = xPosition1[i];
            float y1 = yPosition1[i];

            float x2 = xPosition2[i];
            float y2 = yPosition2[i];

            total += PrimitiveExtension.SquaredMultipleAddDistance(x1, y1, x2, y2);
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public void SIMDEuclideanDistance()
    {
        int n = xPosition1.Length, vsz = Vector<float>.Count, i = 0;
        var acc = Vector<float>.Zero;

        for (; i <= n - vsz; i += vsz)
        {
            var dx = new Vector<float>(xPosition1, i) - new Vector<float>(xPosition2, i);
            var dy = new Vector<float>(yPosition1, i) - new Vector<float>(yPosition2, i);
            acc += Vector.SquareRoot(dx * dx + dy * dy);
        }

        float total = 0f;
        for (int lane = 0; lane < vsz; lane++) total += acc[lane];
        for (; i < n; i++) {
            float dx = xPosition1[i] - xPosition2[i], dy = yPosition1[i] - yPosition2[i];
            total += MathF.Sqrt(dx*dx + dy*dy);
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public void SIMDEuclideanSquaredDistance()
    {
        int n = xPosition1.Length, vsz = Vector<float>.Count, i = 0;
        var acc = Vector<float>.Zero;

        for (; i <= n - vsz; i += vsz)
        {
            var dx = new Vector<float>(xPosition1, i) - new Vector<float>(xPosition2, i);
            var dy = new Vector<float>(yPosition1, i) - new Vector<float>(yPosition2, i);
            acc += dx * dx + dy * dy;
        }

        float total = 0f;
        for (int lane = 0; lane < vsz; lane++) total += acc[lane];
        for (; i < n; i++) {
            float dx = xPosition1[i] - xPosition2[i], dy = yPosition1[i] - yPosition2[i];
            total += dx;
        }
        consumer.Consume(total);
    }

    [Benchmark]
    public unsafe float SumEuclideanAvx2()
    {
        if (xPosition1 is null || xPosition2 is null || yPosition1 is null || yPosition2 is null)
            throw new ArgumentNullException();
        int n = xPosition1.Length;
        if (xPosition2.Length != n || yPosition1.Length != n || yPosition2.Length != n)
            throw new ArgumentException("All arrays must have the same length.");

        if (!Avx.IsSupported)
            throw new PlatformNotSupportedException("AVX required.");

        int i = 0;
        float sum;

        Vector256<float> vacc = Vector256<float>.Zero;
        int width = Vector256<float>.Count;  // 8
        int last  = n - (n % width);

        fixed (float* px1 = xPosition1)
        fixed (float* px2 = xPosition2)
        fixed (float* py1 = yPosition1)
        fixed (float* py2 = yPosition2)
        {
            for (; i < last; i += width)
            {
                var vx1 = Avx.LoadVector256(px1 + i);
                var vx2 = Avx.LoadVector256(px2 + i);
                var vy1 = Avx.LoadVector256(py1 + i);
                var vy2 = Avx.LoadVector256(py2 + i);

                var dx = Avx.Subtract(vx1, vx2);
                var dy = Avx.Subtract(vy1, vy2);

                // len2 = dx*dx + dy*dy  (use FMA when available)
                Vector256<float> dy2 = Avx.Multiply(dy, dy);
                Vector256<float> len2 = Fma.IsSupported
                    ? Fma.MultiplyAdd(dx, dx, dy2)
                    : Avx.Add(Avx.Multiply(dx, dx), dy2);

                var len = Avx.Sqrt(len2);           // per-lane sqrt
                vacc = Avx.Add(vacc, len);          // accumulate distances
            }

            // Horizontal reduce 8 lanes
            float* tmp = stackalloc float[8];
            Avx.Store(tmp, vacc);
            sum = tmp[0] + tmp[1] + tmp[2] + tmp[3] + tmp[4] + tmp[5] + tmp[6] + tmp[7];

            // Scalar tail
            for (; i < n; i++)
            {
                float dx = xPosition1[i] - xPosition2[i];
                float dy = yPosition1[i] - yPosition2[i];
                sum += MathF.Sqrt(dx * dx + dy * dy);
            }
        }

        return sum;
    }
}