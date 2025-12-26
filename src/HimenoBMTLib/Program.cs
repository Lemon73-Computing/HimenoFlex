using Himeno = HimenoBMT.HimenoBMT;

namespace HimenoBMTLib;

public static class Program
{
    public static int Main()
    {
        try
        {
            Himeno.Run(256);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return 1;
        }

        return 0;
    }
}
