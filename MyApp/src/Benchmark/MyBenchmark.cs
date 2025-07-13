using System.Numerics;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]

[SimpleJob(-1, -1, 20)]
public class MyBenchmark
{
    public int size = 2000000;

    public int[] arrayTest;
    public float[] arrayFloatTest;
    public int[] indexes;
    public int[] incrementValues;
    public float[] incrementFloatValues;
    public int vectorSize = Vector<int>.Count;
    public Vector<int> increment = new Vector<int>(1);


    [GlobalSetup]
    public void Setup()
    {
        arrayTest = RandomNumberGenerator.GetRandomValues(size, size - 200 - 1);
        arrayTest = RandomNumberGenerator.GetRandomValues(size, size - 200 - 2);
        indexes = RandomNumberGenerator.GetRandomValues(size, size - 1);
        incrementValues = RandomNumberGenerator.GetRandomValues(size, size - 1);

        arrayFloatTest = RandomNumberGenerator.GetRandomFloatValues(size, size -200 - 1);
        incrementFloatValues = RandomNumberGenerator.GetRandomFloatValues(size, size - 1);
    }

    [Benchmark]
    public void AllocateArrayOfInts()
    {
        var arrayTest = new int[size];

        for (int i = 0; i < size; i++)
        {
            arrayTest[i] = i;
        }
    }

    [Benchmark]
    public void AllocateListOfInts()
    {
        var intList = new List<int>(size);
        for (int i = 0; i < size; i++)
        {
            intList.Add(i);
        }
    }

    [Benchmark]
    public void IncrementArrayOfInts()
    {
        for (int i = 0; i < size; i++)
        {
            arrayTest[i]++;
        }
    }

    [Benchmark]
    public void IncrementArrayJumpingAround()
    {
        for (int i = 0; i < size; i++)
        {
            var index = indexes[i];
            arrayTest[index] += incrementValues[index];
        }
    }


    [Benchmark]
    public void IncrementArrayJumpingAroundInconsistently()
    {
        for (int i = 0; i < size; i++)
        {
            var index = indexes[i];
            arrayTest[index] += incrementValues[index];
        }
    }

    [Benchmark]
    public void SIMDIncrementArray()
    {
        int i = 0;
        for (; i <= arrayTest.Length - vectorSize; i += vectorSize)
        {
            var vec = new Vector<int>(arrayTest, i);
            var inc = new Vector<int>(incrementValues, i);
            vec += increment;
            vec.CopyTo(arrayTest, i);
        }

        // Handle remainder elements if array length not divisible by vector size
        for (; i < arrayTest.Length; i++)
        {
            arrayTest[i]++;
        }
    }

    [Benchmark]
    public void SIMDIntIncrementArrayRandomVector()
    {
        int i = 0;
        for (; i <= arrayTest.Length - vectorSize; i += vectorSize)
        {
            var vec = new Vector<int>(arrayTest, i);
            var inc = new Vector<int>(incrementValues, i);
            vec += inc;
            vec.CopyTo(arrayTest, i);
        }

        // Handle remainder elements if array length not divisible by vector size
        for (; i < arrayTest.Length; i++)
        {
            arrayTest[i]++;
        }
    }
    
    [Benchmark]
    public void SIMDFloatIncrementArrayRandomVector()
    {
            int i = 0;
            for (; i <= arrayFloatTest.Length - vectorSize; i += vectorSize)
            {
                var vec = new Vector<float>(arrayFloatTest, i);
                var inc = new Vector<float>(incrementFloatValues, i);
                vec += inc;
                vec.CopyTo(arrayFloatTest, i);
            }

            // Handle remainder elements if array length not divisible by vector size
            for (; i < arrayFloatTest.Length; i++)
            {
                arrayFloatTest[i]++;
            }
    }
}