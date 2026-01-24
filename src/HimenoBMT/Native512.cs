using System.Runtime.InteropServices;

namespace HimenoBMT;

internal static class Native512
{
    private const string NATIVE_LIBRARY = "himenoBMT_ELARGE";

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl, EntryPoint = "initmt")]
    public static extern void InitMT();

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jacobi")]
    public static extern float Jacobi(int nn);

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl, EntryPoint = "fflop")]
    public static extern double Fflop(int mx, int my, int mz);

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl, EntryPoint = "mflops")]
    public static extern double Mflops(int nn, double cpu, double flop);
}

internal sealed class HimenoNative512 : HimenoBMT.IHimenoNative
{
    public void InitMT() => Native512.InitMT();
    public float Jacobi(int nn) => Native512.Jacobi(nn);
    public double Fflop(int mx, int my, int mz) => Native512.Fflop(mx, my, mz);
    public double Mflops(int nn, double cpu, double flop) => Native512.Mflops(nn, cpu, flop);
}
