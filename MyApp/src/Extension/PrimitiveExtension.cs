using System.ComponentModel;

public static class PrimitiveExtension
{
    public static float[] GetNormalisedVector(this float[] vec)
    {
        var length = MathF.Sqrt(vec[0] * vec[0] + vec[1] * vec[1]);

        if (length > 0f)
            return new float[] { vec[0] / length, vec[1] / length };


        return new float[] { 0f, 0f };
    }

    public static float[] GetInvertedLinearForce(this float[] vec, float distance)
    {
        var length = MathF.Sqrt(vec[0] * vec[0] + vec[1] * vec[1]);

        var invertedLength = distance - length;

        if (length > 0f)
            return new float[] { (vec[0]) / distance, (vec[1]) / distance };

        return new float[] { 0f, 0f };
    }

    public static float[] DivideByCount(this float[] vec, float count)
    {
        return new float[]{
            vec[0] / count,
            vec[1] / count
        };
    }
}