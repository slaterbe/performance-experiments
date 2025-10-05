using BenchmarkDotNet.Running;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

Console.WriteLine($"SSE:  {Sse.IsSupported}");
Console.WriteLine($"SSE2: {Sse2.IsSupported}");
Console.WriteLine($"AVX:  {Avx.IsSupported}");
Console.WriteLine($"AVX2: {Avx2.IsSupported}");
Console.WriteLine($"FMA:  {Fma.IsSupported}");
Console.WriteLine($"Vector<float>.Count = {Vector<float>.Count}"); // 4 = 128-bit, 8 = 256-bit
Console.WriteLine($"Process arch: {RuntimeInformation.ProcessArchitecture}");

//RunRenderer.ExecuteRender();
RunExperiments.ExecuteTests();