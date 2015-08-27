<Query Kind="Program">
  <Reference Relative="System.Threading.Tasks.Dataflow.dll">D:\_users_radu_15\2015-S2-711\GruneActors\System.Threading.Tasks.Dataflow.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Threading.Tasks.Dataflow</Namespace>
</Query>

void Main() {
    MainAsync().Wait();
}

class Grune {
    public Grune(char left, char right) {
        this.left = left;
        this.right = right;
    }
    
    char left;
    char right;
    bool leftfound = false;
    
    public IEnumerable<char> Xform(char ch) { 
        //Console.WriteLine("leftfound='{0}', ch='{1}'", leftfound, ch);
        switch (leftfound) {
        case false: 
            if (ch == left) {
                leftfound = true; 
            } else {
                yield return ch;
            }
            break;

        case true: 
            leftfound = false;
            if (ch == left) {
                yield return right;
            } else { 
                yield return left; 
                yield return ch;
            }
            break;
        }
    }
}

async Task MainAsync() {
    var input = new BufferBlock<char> ();
     
    Func<char, IEnumerable<char>> gab = new Grune('a', 'b').Xform;
    
    var grune_ab = new TransformManyBlock<char, char> (gab);
    
    Func<char, IEnumerable<char>> gbc = new Grune('b', 'c').Xform;
    
    var grune_bc = new TransformManyBlock<char, char> (gbc);
    
    ITargetBlock<char> print = new ActionBlock<char> (
        ch => {
            Console.Write("{0}", ch);
            if (ch == '.') Console.WriteLine();
        }
    );
    
    input.LinkTo(grune_ab);
    //grune_ab.LinkTo(print);
    grune_ab.LinkTo(grune_bc);
    grune_bc.LinkTo(print);

    input.Completion.ContinueWith(t => grune_ab.Complete());
    grune_ab.Completion.ContinueWith(t => grune_bc.Complete());
    grune_bc.Completion.ContinueWith(t => print.Complete());
    
    foreach (char c in "abc.") input.Post(c);
    foreach (char c in "aa.bb.aab.baa.") input.Post(c);
    input.Complete();
    
    await print.Completion;
    Console.WriteLine("... done");
}

//