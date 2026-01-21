using System.Runtime.InteropServices;

namespace HimenoBMT;

internal static class Native64
{
    private const string NATIVE_LIBRARY = "himenoBMT_SMALL";

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl, EntryPoint = "initmt")]
    public static extern void InitMT();

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jacobi")]
    public static extern float Jacobi(int nn);

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl, EntryPoint = "fflop")]
    public static extern double Fflop(int mx, int my, int mz);

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl, EntryPoint = "mflops")]
    public static extern double Mflops(int nn, double cpu, double flop);
}

internal sealed class HimenoNative64 : HimenoBMT.IHimenoNative
{
    public void InitMT() => Native64.InitMT();
    public float Jacobi(int nn) => Native64.Jacobi(nn);
    public double Fflop(int mx, int my, int mz) => Native64.Fflop(mx, my, mz);
    public double Mflops(int nn, double cpu, double flop) => Native64.Mflops(nn, cpu, flop);
}
