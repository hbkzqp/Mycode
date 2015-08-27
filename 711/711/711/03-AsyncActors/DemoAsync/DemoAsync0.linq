<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static int TID() { 
    return Thread.CurrentThread.ManagedThreadId; 
}

void Main () {
    AwaitedCall().Wait();
    Console.WriteLine();
    
    CallPlusAwait().Wait();
    Console.WriteLine();
    
    JustCall().Wait();
    Console.WriteLine();
    
    Console.WriteLine("[{0}] ... <end>", TID());
}

async Task AwaitedCall() {
    Console.WriteLine("[{0}] ... a1", TID());
    await DemoAsync();
    Console.WriteLine("[{0}] ... a2", TID());
}

async Task CallPlusAwait() {
    Console.WriteLine("[{0}] ... c1", TID());
    Task t = DemoAsync();
    Console.WriteLine("[{0}] ... c1.5", TID());
    await t;
    Console.WriteLine("[{0}] ... c2", TID());
}

async Task JustCall() {
    Console.WriteLine("[{0}] ... j1", TID());
    DemoAsync();
    Console.WriteLine("[{0}] ... j2", TID());
}

async Task DemoAsync () {
    Console.WriteLine("[{0}] ... d1", TID());
    await Task.Delay(3000);
    Console.WriteLine("[{0}] ... d2", TID());
}