using System;
using System.Threading;
using System.Threading.Tasks;

public static class Tools
{
    public static async Task<bool> WaitFor(Func<bool> predicate, CancellationToken token)
    {
        while (!await Task.Run(predicate, token)) {
            if (token.IsCancellationRequested)
                return false;

            continue;
        }

        return GameManager.IsRunning;
    }
}