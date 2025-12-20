using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HimenoBMT;

public static class HimenoBMT
{
    // DLLImport (START)
    private const string NATIVE_LIBRARY = "himenoBMT_32";

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitMT();

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern float Jacobi(int nn);

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Fflop(int mx, int my, int mz);

    [DllImport(NATIVE_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern double Mflops(int nn, double cpu, double flop);
    // DLLImport (END)

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

        int i, j, k, nn;
        float gosa;
        double cpu, cpu0, cpu1, flop, target;
        target = 60.0;
        float omega = 0.8;
        int imax = MIMAX - 1;
        int jmax = MJMAX - 1;
        int kmax = MKMAX - 1;

        InitMT();
        Console.WriteLine($"mimax = {MIMAX} mjmax = {MJMAX} mkmax = {MKMAX}");
        Console.WriteLine($"imax = {imax} jmax = {jmax} kmax = {kmax}");

        nn = 3;
        Console.WriteLine($" Start rehearsal measurement process.");
        Console.WriteLine($" Measure the performance in {nn} times.");
        Console.WriteLine();

        Stopwatch sw = new();
        sw.Start();
        //   cpu0= second();
        gosa = Jacobi(nn);
        sw.Stop();
        //   cpu1= second();
        //   cpu= cpu1 - cpu0;
        cpu = sw.Elapsed.TotalSeconds;

        flop = Fflop(imax, jmax, kmax);

        Console.WriteLine($" MFLOPS: {Mflops(nn,cpu,flop)} time(s): {cpu} {gosa}");
        Console.WriteLine();

        nn= (int)(target/(cpu/3.0));

        Console.WriteLine($" Now, start the actual measurement process.");
        Console.WriteLine($" The loop will be excuted in {nn} times");
        Console.WriteLine($" This will take about one minute.");
        Console.WriteLine($" Wait for a while");

        /*
         *    Start measuring
         */
        sw.Restart();
        //   cpu0 = second();
        gosa = Jacobi(nn);
        sw.Stop();
        //   cpu1 = second();
        cpu = sw.Elapsed.TotalSeconds;
        //   cpu= cpu1 - cpu0;
  
        Console.WriteLine($" Loop executed for {nn} times");
        Console.WriteLine($" Gosa : {gosa} ");
        Console.WriteLine($" MFLOPS measured : {Mflops(nn,cpu,flop)}\tcpu : {cpu}");
        Console.WriteLine($" Score based on Pentium III 600MHz : {Mflops(nn,cpu,flop) / 82.84}");

        //   return (0);
        return [];
    }
}
