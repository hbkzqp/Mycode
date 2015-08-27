<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static int TID() { 
    return Thread.CurrentThread.ManagedThreadId; 
}

void Main () {
    Console.WriteLine("[{0}] ... 01", TID());
    Fun1().Wait();
    Console.WriteLine("[{0}] ... 02", TID());
}
    
async Task Fun1() {
    Console.WriteLine("[{0}] ... 11", TID());
    await Fun2();
    Console.WriteLine("[{0}] ... 12", TID());
}

async Task Fun2() {
    Console.WriteLine("[{0}] ... 21", TID());
    await Fun3();
    Console.WriteLine("[{0}] ... 22", TID());
}

async Task Fun3() {
    Console.WriteLine("[{0}] ... 31", TID());
    await Task.Delay(1000);
    Console.WriteLine("[{0}] ... 32", TID());
    //await Task.Delay(1000);
    //Console.WriteLine("[{0}] ... 33", TID());
}