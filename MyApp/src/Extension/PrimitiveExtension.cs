using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

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

    public static float EculidanDistance(float x1, float y1, float x2, float y2){
        float value = (x1-x2)*(x1-x2) + (y1-y2)*(y1-y2);
        return (float)Math.Sqrt(value);
    }

    public static float EculidanReciprocal(float x1, float y1, float x2, float y2){
        float value = (x1-x2)*(x1-x2) + (y1-y2)*(y1-y2);
        float map = MathF.ReciprocalSqrtEstimate(value);
        return 1f/map;
    }

    public static float SquaredEculidanDistance(float x1, float y1, float x2, float y2){
        float value = (x1-x2)*(x1-x2) + (y1-y2)*(y1-y2);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ManhattenDistanceV1(float x1, float y1, float x2, float y2){
        float value = MathF.Abs(x1-x2) + MathF.Abs(y1-y2);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float AbsBit(float x)
    {
        int bits = BitConverter.SingleToInt32Bits(x);
        bits &= 0x7FFFFFFF;          // clear sign bit
        return BitConverter.Int32BitsToSingle(bits);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ManhattenDistanceV2(float x1, float y1, float x2, float y2){
        var dx = x1 - x2;
        var dy = y1 - y2;
        return AbsBit(dx) + AbsBit(dy);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OctileDistance(float x1, float y1, float x2, float y2)
    {
        float dx = MathF.Abs(x1 - x2);
        float dy = MathF.Abs(y1 - y2);

        // Manual min/max is usually a hair faster than MathF.Min/Max in a hot loop
        if (dx < dy) { var t = dx; dx = dy; dy = t; }

        // Precomputed (√2 - 1)
        const float k = 0.41421356237f;
        return dx + k * dy;
    }

    public static float FasterHypDistance(float x1, float y1, float x2, float y2){
        float dx = x1 - x2;
        float dy = y1 - y2;
        float s = dx*dx + dy*dy;
        if (Sse.IsSupported) {
            var v = Vector128.Create(s);
            // Approximate 1/sqrt(s)
            var inv = Sse.ReciprocalSqrt(v);              // 1/√s (approx)
            // Optional 1 Newton step: inv = inv * (1.5 - 0.5*s*inv*inv)
            var half = Vector128.Create(0.5f);
            var threeHalfs = Vector128.Create(1.5f);
            inv = Sse.Multiply(inv, Sse.Subtract(threeHalfs,
                    Sse.Multiply(half, Sse.Multiply(v, Sse.Multiply(inv, inv)))));
            float invScalar = inv.ToScalar();
            return s * invScalar; // ≈ √s
        }
        return MathF.Sqrt(s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SquaredMultipleAddDistance(float x1, float y1, float x2, float y2)
    {
        float dx = x1 - x2, dy = y1 - y2;
        return MathF.FusedMultiplyAdd(dx, dx, dy * dy); // dx*dx + dy*dy in one op where supported
    }
}