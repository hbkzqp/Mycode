<Query Kind="Program">
  <Reference Relative="..\TPL-Async-TDF\System.Threading.Tasks.Dataflow.dll">D:\_users_radu_14\TPL-Async-TDF\System.Threading.Tasks.Dataflow.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Threading.Tasks.Dataflow</Namespace>
</Query>

int TID() { return Thread.CurrentThread.ManagedThreadId; }

async Task<int> ProduceAsync(int id, ITargetBlock<int> target) {
    //Console.WriteLine("[{0}] P{1} started", TID(), id);
    var count = 3;
    for (int c = 1; c <= count; c++) {
        await Task.Delay(100);
        var x = id * 100 + c;
        //target.Post(x);           
        await target.SendAsync(x);  
        //Console.WriteLine("[{0}] P{1} x: {2}", TID(), id, x);
    }
    Console.WriteLine("[{0}] P{1} count: {2}", TID(), id, count);
    return count;
}

async Task<int> ConsumeAsync(int id, ISourceBlock<int> source) {
    //Console.WriteLine("[{0}] C{1} started", TID(), id);
    int count = 0;
    while (true) {
        try {
            //int x = source.Receive();
            int x = await source.ReceiveAsync();
            Console.WriteLine("[{0}] C{1} x: {2}", TID(), id, x);
            count += 1;
            await Task.Delay(50);
        } catch (InvalidOperationException ex) {
            Console.WriteLine("[{0}] C{1} count: {2}", TID(), id, count);
            return count;
        }
    }
}

async void Main() {
    //Console.WriteLine("[{0}] starting", TID());
    var buffer = new BufferBlock<int>();
    
    var producer1 = ProduceAsync(1, buffer);
    var producer2 = ProduceAsync(2, buffer);
    var producer3 = ProduceAsync(3, buffer);

    var consumer1 = ConsumeAsync(1, buffer);
    var consumer2 = ConsumeAsync(2, buffer);
    
    //Task.WaitAll(producer1, producer2, producer3);
    int p1 = await producer1;
    int p2 = await producer2;
    int p3 = await producer3;
    
    buffer.Complete();
    await buffer.Completion;

    //Task.WaitAll(consumer1, consumer2);
    int c1 = await consumer1;
    int c2 = await consumer2;

    Console.WriteLine("[{0}] total prod count: {1}; total cons count: {2}", 
        TID(), p1 + p2 + p3, c1 + c2);
}

//