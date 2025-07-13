public static class PrimitiveExtension
{
    public static float[] GetNormalisedVector(this float[] vec)
    {
        var length = MathF.Sqrt(vec[0] * vec[0] + vec[1] * vec[1]);

        if (length > 0f)
            return new float[] { vec[0] / length, vec[1] / length };   
        

        return new float[] { 0f, 0f };
    }
}