<Query Kind="Program">
  <Reference Relative="System.Threading.Tasks.Dataflow.dll">D:\_users_radu_15\2015-S2-711\GruneActors\System.Threading.Tasks.Dataflow.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Threading.Tasks.Dataflow</Namespace>
</Query>

void Main() {
    MainLinq();
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

IEnumerable<char> Input() {
    foreach (char c in "abc.") {
        yield return c;
    }
    foreach (char c in "aa.bb.aab.baa.") {
        yield return c;
    }
}

void Print (char ch) {
    Console.Write("{0}", ch);
    if (ch == '.') Console.WriteLine();
}

void MainLinq() {
    Func<char, IEnumerable<char>> Gab = new Grune('a', 'b').Xform;
   
    Func<char, IEnumerable<char>> Gbc = new Grune('b', 'c').Xform;
    
    var Output = Input().SelectMany(Gab).SelectMany(Gbc);
    
    foreach (var ch in Output) {
        Print(ch);
    }

    Console.WriteLine("... done");
}

//