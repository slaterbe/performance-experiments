using System.Numerics;

public static class RandomNumberGenerator
{
    public static int[] GetRandomValues(int size, int range)
    {
        var rand = new Random();
        var arrayTest = new int[size];
        for (int i = 0; i < size; i++)
        {
            arrayTest[i] = rand.Next(range);
        }

        return arrayTest;
    }

    public static Vector<int> GenerateVector(int size, int range)
    {
        var values = GetRandomValues(size, range);
        return new Vector<int>(values, 0);
    }
}