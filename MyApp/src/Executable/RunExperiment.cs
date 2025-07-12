using System.Diagnostics;
using System.Numerics;


public static class RunExperiments
{
    public static void ExecuteTests()
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Warm up command");

        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Stop();

        int size = 2000000;
        int experimentCount = 5;
        int[] arrayTest;
        int[] indexTest;
        int[] indexes;
        int[] incrementValue;

        List<int> listTest;
        int vectorSize = Vector<int>.Count;


        Timer.Time("Time Console Print", () =>
        {
            Console.WriteLine("Hello, World!");
        });

        Timer.RunExperiment("Allocate an array of ints", experimentCount, () =>
        {
            var arrayTest = new int[size];
            for (int i = 0; i < size; i++)
            {
                arrayTest[i] = i;
            }
        });

        Timer.RunExperiment("Allocate a list of ints with loop", experimentCount, () =>
        {
            var intList = new List<int>(size);
            for (int i = 0; i < size; i++)
            {
                intList.Add(i);
            }
        });

        arrayTest = RandomNumberGenerator.GetRandomValues(size, size - 1);
        Timer.RunExperiment("Increment an array of ints sequentially", experimentCount, () =>
        {
            for (int i = 0; i < size; i++)
            {
                arrayTest[i]++;
            }
        });

        arrayTest = RandomNumberGenerator.GetRandomValues(size, size - experimentCount - 2);
        indexes = RandomNumberGenerator.GetRandomValues(size, size - 1);
        incrementValue = RandomNumberGenerator.GetRandomValues(size, size - 1);
        Timer.RunExperiment("Increment an array jumping around", experimentCount, () =>
        {
            for (int i = 0; i < size; i++)
            {
                var index = indexes[i];
                arrayTest[index] += incrementValue[index];
            }
        });


        arrayTest = RandomNumberGenerator.GetRandomValues(size, size - experimentCount - 2);
        indexes = RandomNumberGenerator.GetRandomValues(size, size - 1);
        incrementValue = RandomNumberGenerator.GetRandomValues(size, size - 1);
        Timer.RunExperiment("Increment an array jumping around with inconsistent value", experimentCount, () =>
        {
            for (int i = 0; i < size; i++)
            {
                var index = indexes[i];
                arrayTest[index] += incrementValue[index];
            }
        });

        arrayTest = RandomNumberGenerator.GetRandomValues(size, size - experimentCount - 2);
        Vector<int> increment = new Vector<int>(1);
        Timer.RunExperiment("Increment a vector", experimentCount, () =>
        {
            int i = 0;
            for (; i <= arrayTest.Length - vectorSize; i += vectorSize)
            {
                var vec = new Vector<int>(arrayTest, i);
                vec += increment;
                vec.CopyTo(arrayTest, i);
            }

            // Handle remainder elements if array length not divisible by vector size
            for (; i < arrayTest.Length; i++)
            {
                arrayTest[i]++;
            }
        });

    }
}