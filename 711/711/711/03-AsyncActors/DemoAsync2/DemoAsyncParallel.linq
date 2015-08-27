<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static int TID() { 
    return Thread.CurrentThread.ManagedThreadId; 
}

void Main () {
    ForkJoin().Wait();
}
    
async Task ForkJoin() {
    // fork
//    var tasks = new List<Task<int>>();
//    for (int i = 1; i <= 7; i++ ) {
//        var t = FunParallel(i);
//        tasks.Add(t);
//    }
    var tasks = Enumerable.Range(1,7)
                          .Select(i => FunParallel(i))
                          .ToList();

    Console.WriteLine("[{0}] ...", TID());
    
    // join
    var res = await Task.WhenAll(tasks);
    res.Dump("res");
}

async Task<int> FunParallel(int id) {
    var delay = id * 10 % 3 + 1;
    Console.WriteLine("[{0}] {1} {2}", TID(), id, delay);
    await Task.Delay(delay * 3000);
    Console.WriteLine("[{0}] {1} !", TID(), id);
    return id * 10;
}