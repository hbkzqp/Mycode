<Query Kind="Program">
  <Reference Relative="System.Threading.Tasks.Dataflow.dll">D:\_users_radu_15\2015-S2-711\FactActors\System.Threading.Tasks.Dataflow.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Threading.Tasks.Dataflow</Namespace>
</Query>

void Main() {
    MainAsync().Wait();
}

async Task MainAsync() {
    var input = new TransformBlock<int, Tuple<int,int,int>> (
        n => Tuple.Create(n, n, 1) 
    );
    
    var fact = new TransformBlock<Tuple<int,int,int>, Tuple<int,int,int>> (
        nmf => { 
            var n = nmf.Item1;
            var m = nmf.Item2;
            var f = nmf.Item3;
            //Console.WriteLine("fact ({0}, {1}, {2})", n, m, f);
            if (m <= 0) return nmf; // Tuple.Create(n, m, f);
            else return Tuple.Create(n, m-1, f*m);
        }
    );
    
    var print = new ActionBlock<Tuple<int,int,int>> (
        nmf => {
            var n = nmf.Item1;
            var m = nmf.Item2;
            var f = nmf.Item3;
            Console.WriteLine("Fact({0}) = {1}", n, f);
        }
    );
    
    var dump = DataflowBlock.NullTarget<Tuple<int,int,int>>();
    
    input.LinkTo(fact);
    fact.LinkTo(fact, nmf => nmf.Item2 > 0);
    fact.LinkTo(print, nmf => nmf.Item2 == 0);
    fact.LinkTo(dump, nmf => nmf.Item2 < 0);
    
    input.Post(2);
    input.Post(6);
    await Task.Delay(1000);
    
    input.Post(0);
    input.Post(1);
    input.Post(3);
    input.Post(4);
    input.Post(5);
    input.Post(-1);
    
    await Task.Delay(1000); // not a good termination
    Console.WriteLine("done?");
}

//