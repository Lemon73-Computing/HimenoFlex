using Himeno = HimenoBMT.HimenoBMT;

namespace HimenoCLI;

internal class Program
{
    static int Main(string[] args)
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
