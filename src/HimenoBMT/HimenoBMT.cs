using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HimenoBMT;

public static class HimenoBMT
{
    // DLLImport (START)
    private const string NATIVE_LIBRARY_32 = "himenoBMT_32";

    [DllImport(NATIVE_LIBRARY_32, CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitMT_32();

    [DllImport(NATIVE_LIBRARY_32, CallingConvention = CallingConvention.Cdecl)]
    public static extern float Jacobi_32(int nn);

    [DllImport(NATIVE_LIBRARY_32, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Fflop_32(int mx, int my, int mz);

    [DllImport(NATIVE_LIBRARY_32, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Mflops_32(int nn, double cpu, double flop);
    // ---
    private const string NATIVE_LIBRARY_64 = "himenoBMT_64";

    [DllImport(NATIVE_LIBRARY_64, CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitMT_64();

    [DllImport(NATIVE_LIBRARY_64, CallingConvention = CallingConvention.Cdecl)]
    public static extern float Jacobi_64(int nn);

    [DllImport(NATIVE_LIBRARY_64, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Fflop_64(int mx, int my, int mz);

    [DllImport(NATIVE_LIBRARY_64, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Mflops_64(int nn, double cpu, double flop);
    // ---
    private const string NATIVE_LIBRARY_128 = "himenoBMT_128";

    [DllImport(NATIVE_LIBRARY_128, CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitMT_128();

    [DllImport(NATIVE_LIBRARY_128, CallingConvention = CallingConvention.Cdecl)]
    public static extern float Jacobi_128(int nn);

    [DllImport(NATIVE_LIBRARY_128, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Fflop_128(int mx, int my, int mz);

    [DllImport(NATIVE_LIBRARY_128, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Mflops_128(int nn, double cpu, double flop);
    // ---
    private const string NATIVE_LIBRARY_256 = "himenoBMT_256";

    [DllImport(NATIVE_LIBRARY_256, CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitMT_256();

    [DllImport(NATIVE_LIBRARY_256, CallingConvention = CallingConvention.Cdecl)]
    public static extern float Jacobi_256(int nn);

    [DllImport(NATIVE_LIBRARY_256, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Fflop_256(int mx, int my, int mz);

    [DllImport(NATIVE_LIBRARY_256, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Mflops_256(int nn, double cpu, double flop);
    // ---
    private const string NATIVE_LIBRARY_512 = "himenoBMT_512";

    [DllImport(NATIVE_LIBRARY_512, CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitMT_512();

    [DllImport(NATIVE_LIBRARY_512, CallingConvention = CallingConvention.Cdecl)]
    public static extern float Jacobi_512(int nn);

    [DllImport(NATIVE_LIBRARY_512, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Fflop_512(int mx, int my, int mz);

    [DllImport(NATIVE_LIBRARY_512, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Mflops_512(int nn, double cpu, double flop);
    // DLLImport (END)

    public static void InitMT(int size)
    {
        Action? action = size switch
        {
            32 => () => InitMT_32(),
            64 => () => InitMT_64(),
            128 => () => InitMT_128(),
            256 => () => InitMT_256(),
            512 => () => InitMT_512(),
            _ => null
        };
        action?.Invoke();
    }

    public static float Jacobi(int size, int nn)
    {
        float number = size switch
        {
            32 => Jacobi_32(nn),
            64 => Jacobi_64(nn),
            128 => Jacobi_128(nn),
            256 => Jacobi_256(nn),
            512 => Jacobi_512(nn),
            _ => 0
        };
        return number;
    }

    public static double Fflop(int size, int mx, int my, int mz)
    {
        double number = size switch
        {
            32 => Fflop_32(mx, my, mz),
            64 => Fflop_64(mx, my, mz),
            128 => Fflop_128(mx, my, mz),
            256 => Fflop_256(mx, my, mz),
            512 => Fflop_512(mx, my, mz),
            _ => 0
        };
        return number;
    }

    public static double Mflops(int size, int nn, double cpu, double flop)
    {
        double number = size switch
        {
            32 => Mflops_32(nn, cpu, flop),
            64 => Mflops_64(nn, cpu, flop),
            128 => Mflops_128(nn, cpu, flop),
            256 => Mflops_256(nn, cpu, flop),
            512 => Mflops_512(nn, cpu, flop),
            _ => 0
        };
        return number;
    }

// -------------------------------------
// MAIN
// -------------------------------------

    public static string[] Run(int size)
    {
        switch (size)
        {
            case 32:
            case 64:
            case 128:
            case 256:
            // case 512:
                break;
            default:
                return [];
        }

        int MIMAX = size + 1;
        int MJMAX = size + 1;
        int MKMAX = size * 2 + 1;

        int /*i, j, k,*/ nn;
        float gosa;
        double cpu, /*cpu0, cpu1,*/ flop, target;
        target = 60.0;
        // float omega = 0.8;
        int imax = MIMAX - 1;
        int jmax = MJMAX - 1;
        int kmax = MKMAX - 1;

        InitMT(size);
        Console.WriteLine($"mimax = {MIMAX} mjmax = {MJMAX} mkmax = {MKMAX}");
        Console.WriteLine($"imax = {imax} jmax = {jmax} kmax = {kmax}");

        nn = 3;
        Console.WriteLine($" Start rehearsal measurement process.");
        Console.WriteLine($" Measure the performance in {nn} times.");
        Console.WriteLine();

        Stopwatch sw = new();
        sw.Start();
        //   cpu0= second();
        gosa = Jacobi(size, nn);
        sw.Stop();
        //   cpu1= second();
        //   cpu= cpu1 - cpu0;
        cpu = sw.Elapsed.TotalSeconds;

        flop = Fflop(size, imax, jmax, kmax);

        Console.WriteLine($" MFLOPS: {Mflops(size, nn, cpu, flop)} time(s): {cpu} {gosa}");
        Console.WriteLine();

        nn = (int)(target / (cpu / 3.0));

        Console.WriteLine($" Now, start the actual measurement process.");
        Console.WriteLine($" The loop will be excuted in {nn} times");
        Console.WriteLine($" This will take about one minute.");
        Console.WriteLine($" Wait for a while");

        /*
         *    Start measuring
         */
        sw.Restart();
        //   cpu0 = second();
        gosa = Jacobi(size, nn);
        sw.Stop();
        //   cpu1 = second();
        cpu = sw.Elapsed.TotalSeconds;
        //   cpu= cpu1 - cpu0;

        Console.WriteLine($" Loop executed for {nn} times");
        Console.WriteLine($" Gosa : {gosa} ");
        Console.WriteLine($" MFLOPS measured : {Mflops(size, nn, cpu, flop)}\tcpu : {cpu}");
        Console.WriteLine($" Score based on Pentium III 600MHz : {Mflops(size, nn, cpu, flop) / 82.84}");

        //   return (0);
        return [];
    }
}
