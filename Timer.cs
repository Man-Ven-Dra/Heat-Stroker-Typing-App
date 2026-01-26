using System.Diagnostics;

class Timer
{
    static Stopwatch sw = new Stopwatch();
    static internal void startWatch()
    {
        sw.Start();
    }
    static internal Stopwatch stopWatch()
    {
        sw.Stop();
        return sw;
    }
}