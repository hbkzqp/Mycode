using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace _711
{
    public class Byz
    {
        public static void Main(String[] args)
        {
            //if (args == null || args.Length == 0)
            //{
            //    Console.Error.WriteLine("args problem, not valad");
            //}
            try
            {
                //string[] lines = System.IO.File.ReadAllLines(arg);
                string[] lines = System.IO.File.ReadAllLines("huhu.txt");
                Byz byz = new Byz();
                byz.buildNodesNum(lines[0]);

                for (int i = 1; i <=(ByzNode.nodeCount); i++)
                {
                    byz.buildNodes(lines[i]);
                }
                for (int i = ByzNode.nodeCount + 1; i < lines.Length; i++)
                {
                    byz.buildPath(lines[i]);
                }
                var sw = Stopwatch.StartNew();
                byz.startByz().Wait();
                sw.Stop();
                foreach (int nid in Node.Vertex.Keys)
                {
                    if (Node.Vertex[nid] is FaultyNode)
                    {
                        Console.WriteLine("{0} *", Node.Vertex[nid].Nid);
                    }
                    else
                    {
                        Console.WriteLine("{0} {1}", Node.Vertex[nid].Nid, ((ByzNode)(Node.Vertex[nid])).myValue);
                    }
                    
                }
                Node.Reset();
                Console.WriteLine("fk");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("problem happens in Node building, message is {0}.", e.Message);
            }
            //foreach (String arg in args)
            //{
            //    try
            //    {
            //        //string[] lines = System.IO.File.ReadAllLines(arg);
            //        string[] lines = System.IO.File.ReadAllLines("haha.txt");
            //        Byz Byz = new Byz();
            //        Byz.buildNodes(lines[0]);
            //        foreach (String li in lines)
            //        {
            //            Byz.buildPath(li);
            //        }
            //        var sw = Stopwatch.StartNew();
            //        Byz.startByz().Wait();
            //        sw.Stop();
            //        Console.WriteLine("total time {0} ms",sw.ElapsedMilliseconds);
            //        Console.WriteLine("node count {0}",Byz.getNodeCount());
            //        Console.WriteLine("edge count {0}",(int)(Node.MessageCount/2));
            //        Console.WriteLine("message count {0}",Node.MessageCount);
            //        Console.WriteLine("\n\nNode Parent");
            //        foreach(int nid in Node.Vertex.Keys)
            //        {
            //            Console.WriteLine("{0} {1}", Node.Vertex[nid], Node.Vertex[nid].Parent);
            //        }
            //        Console.WriteLine("fk");
            //    }
            //    catch (Exception e)
            //    {
            //        Console.Error.WriteLine("problem happens in Node building, message is {0}.", e.Message);
            //    }

            //}
        }
        private void buildNodesNum(String nodeInfo)
        {
            try
            {
                String[] nodesInfos = nodeInfo.Split(' ');
                if (nodesInfos.Length == 2)
                {
                    int nodeNum = Int32.Parse(nodesInfos[0]);
                    ByzNode.nodeCount = nodeNum;
                    String defaultV = nodesInfos[1];
                    ByzNode.defaultVal = defaultV;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("node info in line one is not legal exception is " + e.Message);
            }
           
        }
        private void buildNodes(String nodeInfo)
        {
            try
            {
                String[] nodeInfos = nodeInfo.Split(';');
                if (nodeInfos.Length != 0)
                {
                    String[] nodeType = nodeInfos[0].Split(' ');
                    if (nodeType.Length == 3)
                    {
                        int nodeId = Int32.Parse(nodeType[0]);
                        String defaultV = nodeType[1];
                        int isFaulty = Int32.Parse(nodeType[2]);
                        if (isFaulty == 1)
                        {
                            FaultyNode fn = new FaultyNode(nodeId);
                            String[] firstValues = nodeInfos[1].Split(' ');
                            String[] fv = deleteSpace(firstValues);
                            if (fv.Length == ByzNode.nodeCount)
                            {
                                for (int i = 0; i < fv.Length; i++)
                                {
                                    fn.firstSend.Add(i + 1, fv[i].ToString());
                                }
                                //foreach (String v in firstValues)
                                //{
                                //    fn.firstSend.Add(nodeId, v);
                                //}
                            }
                            String[] secondValues = nodeInfos[2].Split(' ');
                            String[] sv = deleteSpace(secondValues);
                            if (sv.Length == ByzNode.nodeCount)
                            {
                                for (int i = 0; i < fv.Length; i++)
                                {
                                    fn.secondSend.Add(i + 1, sv[i].ToString());
                                }
                                //foreach (String v in firstValues)
                                //{
                                //    fn.firstSend.Add(nodeId, v);
                                //}
                            }
                        }
                        else if(isFaulty==0)
                        {
                            CommonNode cn = new CommonNode(nodeId);
                            cn.myValue = defaultV;
                        }
                        else
                        {
                            throw new Exception("text is invalid");
                        }
                    }
                }
                else
                {
                    throw new Exception("text is invalid");
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
           
        }
        private String[] deleteSpace(String[] cs)
        {
            List<String> sb = new List<string>();
            if (cs.Length != 0)
            {
                foreach (String c in cs)
                {
                    if (!c.Equals(" ")&&!c.Equals(""))
                    {
                        sb.Add(c);
                    }
                }
            }
            return sb.ToArray();
        }
        private void buildPath(String pathInfo)
        {
            String[] info = pathInfo.Split(' ');
            try
            {
                if (info.Length == 3)
                {
                    ByzNode.LinkSingleDire(Int32.Parse(info[0]), Int32.Parse(info[1]), Int32.Parse(info[2]));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("sorry, path build problem, text file is not legal");
            }

        }
        public async Task startByz()
        {
            await Network.Start();
        }
        protected internal sealed class Network
        {
            static internal async Task Start()
            {
                int source = Node.Vertex.First().Key;
                var tasks = new List<Task>();
                foreach (int nid in Node.Vertex.Keys)
                {
                    if (nid != source)
                        tasks.Add(Node.Vertex[nid].Run()); // no await yet!
                }

                await Node.Vertex[source].Run();
                await Task.WhenAll(tasks);

            }
        }

        // --- your actual Byz implementation ---

        protected internal class ByzMessage
        {
            protected internal ByzMessage(String code, int roundNum, int source)
            {
                this.transferCode = code;
                this.roundNum = roundNum;
                this.sourceNode = source;
            }

            protected internal String transferCode;
            protected internal int roundNum;
            protected internal int sourceNode;

            public override string ToString()
            {
                return string.Format("{0}", this.transferCode);
            }
        }

        protected internal class ByzNode : Node
        {
            static protected internal readonly int MSG_DIRECT = 0;
            static protected internal readonly int MSG_REFLECT = 1;
            public Dictionary<int, String> firstSend = new Dictionary<int, String>();
            public Dictionary<int, String> secondSend = new Dictionary<int, String>();
            public Dictionary<int, String> finalResult = new Dictionary<int, String>();
            public static void LinkSingleDire(int nid1, int nid2, int del12)
            {
                EdgeCount += 1;
                Vertex[nid1].NeighDelay[nid2] = del12;
            }
            protected static internal int nodeCount = 1;
            protected static internal String defaultVal = "0";
            protected internal String myValue = "0";
            protected internal void incNodeCount(int source)
            {
                //Interlocked.Increment(ref nodeCount);
                nodeCount = source + nodeCount;
            }
            protected internal ByzNode(int nid)
                : base(nid)
            {
            }
        }

        protected internal class FaultyNode : ByzNode
        {
            
            protected internal FaultyNode(int nid)
                : base(nid)
            {
            }

            protected internal override async Task<object> Run()
            {
                Init();
                try
                {
                    foreach (int n in NeighDelay.Keys)
                    {
                        //await 
                        //Console.WriteLine("{0}->[{1},{2}]->{3}:{4}", Nid, MSG_DIRECT, 0, n, NeighDelay[n]);

                        PostAsync(new ByzMessage(this.firstSend[n], 1, Nid), n, NeighDelay[n]);
                    }
                    int recNum1 = 0;
                    int recNum2 = 0;
                    Boolean hasSent = false;
                    while (true)
                    {
                        var ntok = await ReceiveAsync();
                        if (ntok.Item2 is ByzMessage)
                        {
                            ByzMessage msg = (ByzMessage)(ntok.Item2);
                            if (msg.roundNum == 1)
                            {
                                //到底怎么搞间谍的东西到底肯定有没有
                                recNum1++;
                            }
                            if (msg.roundNum == 2)
                            {
                                //到底怎么搞
                                recNum2++;
                            }
                            if (recNum1 == ByzNode.nodeCount && (!hasSent))
                            {
                                foreach (int n in NeighDelay.Keys)
                                {
                                    //await 
                                    //Console.WriteLine("{0}->[{1},{2}]->{3}:{4}", Nid, MSG_DIRECT, 0, n, NeighDelay[n]);
                                    
                                    PostAsync(new ByzMessage(this.secondSend[n], 2, Nid), n, NeighDelay[n]);
                                }
                                hasSent = true;
                            }
                        }
                        if (recNum2 == ByzNode.nodeCount)
                        {
                            break;
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
                

                return Nid;
            }
        }

        protected internal class CommonNode : ByzNode
        {
            protected internal CommonNode(int nid)
                : base(nid)
            {
            }

            protected internal override async Task<object> Run()
            {
                Init();
                try 
                {
                    foreach (int n in NeighDelay.Keys)
                    {
                        //await 
                        //Console.WriteLine("{0}->[{1},{2}]->{3}:{4}", Nid, MSG_DIRECT, 0, n, NeighDelay[n]);

                        PostAsync(new ByzMessage(this.myValue, 1, Nid), n, NeighDelay[n]);
                    }
                    int recNum1 = 0;
                    int recNum2 = 0;
                    Boolean hasSent = false;
                    while (true)
                    {
                        var ntok = await ReceiveAsync();
                        if (ntok.Item2 is ByzMessage)
                        {
                            ByzMessage msg = (ByzMessage)(ntok.Item2);
                            if (msg.roundNum == 1)
                            {
                                this.firstSend.Add(msg.sourceNode, msg.transferCode);
                                recNum1++;
                            }
                            if (msg.roundNum == 2)
                            {
                                this.secondSend.Add(msg.sourceNode, msg.transferCode);
                                recNum2++;
                            }
                            if (recNum1 == ByzNode.nodeCount&&(!hasSent))
                            {
                                StringBuilder sb = new StringBuilder();
                                foreach (int n in NeighDelay.Keys)
                                {
                                    if(n!=Nid)
                                    {
                                        sb.Append(firstSend[n]);
                                    }
                                    
                                    //await 
                                    //Console.WriteLine("{0}->[{1},{2}]->{3}:{4}", Nid, MSG_DIRECT, 0, n, NeighDelay[n]);    
                                }
                                foreach (int n in NeighDelay.Keys)
                                {
                                    //if (n < Nid)
                                    //{
                                    //    PostAsync(new ByzMessage(sb.ToString(), 2, Nid), n, NeighDelay[n]);
                                    //}
                                    //if (n > Nid)
                                    //{
                                    //    PostAsync(new ByzMessage(sb.ToString(), 2, Nid), n, NeighDelay[n-1]);
                                    //}
                                    //if (Nid == 3)
                                    //{
                                    //    Console.WriteLine("sssss");
                                    //}
                                    PostAsync(new ByzMessage(sb.ToString(), 2, Nid), n, NeighDelay[n]);
                                }
                                hasSent = true;
                            }
                            if (recNum2 == ByzNode.nodeCount)
                            {
                                break;
                            }
                        }
                    }
                    summerizeNode();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message+e.StackTrace);
                    Console.WriteLine("aa");
                }
                
                return Nid;
            }
            protected void summerizeNode()
            {
                int v0 = 0;
                int fv0 = 0;
                int v1 = 0;
                int fv1 = 0;
                for (int i = 0; i < secondSend.First().Value.Length; i++)
                {
                    int j = i;
                    //int j = 0;
                    //if (i < Nid)
                    //{
                    //    j = i;
                    //}
                    //if (i > Nid)
                    //{
                    //    j = i - 1;
                    //}
                    foreach (String v in secondSend.Values)
                    {
                        if (v[j].Equals("1"))
                        {
                            v1++;
                        }
                        if (v[j].Equals("0"))
                        {
                            v0++;
                        }
                        
                    }
                    if (myValue.Equals("1"))
                    {
                        v1++;
                    }
                    if (myValue.Equals("1"))
                    {
                        v0++;
                    }
                    if (v0 > v1)
                    {
                        fv0++;
                    }
                    else if (v0 < v1)
                    {
                        fv1++;
                    }
                }
                if (fv1 > fv0)
                {
                    myValue = "1";
                }
                else if (fv0 > fv1)
                {
                    myValue = "0";
                }
                else if (fv0 == fv1)
                {
                    myValue = defaultVal;
                }
            }
        }

        // ===========================================================
        // Please do NOT touch this library
        // ===========================================================

        static protected internal int TID() { return Thread.CurrentThread.ManagedThreadId; }

        protected internal class Node
        {
            // static members

            static protected internal bool TRACE_THREAD = false;
            static protected internal bool TRACE_INIT = true;
            static protected internal bool TRACE_POST = false;
            static protected internal bool TRACE_RECEIVE = true;
            static protected internal bool TRACE_PARENT = false;

            static protected internal Dictionary<int, Node> Vertex = new Dictionary<int, Node>();
            static protected internal int EdgeCount = 0;
            static protected internal int MessageCount = 0;

            static protected internal void Reset()
            {
                Vertex = new Dictionary<int, Node>();
                EdgeCount = 0;
                MessageCount = 0;
            }

            static protected internal void Link(int nid1, int nid2, int del12, int del21)
            {
                EdgeCount += 1;
                Vertex[nid1].NeighDelay[nid2] = del12;
                Vertex[nid2].NeighDelay[nid1] = del21;
            }

            // instance members

            protected internal int Parent = 0;
            protected internal int Nid;
            protected internal Dictionary<int, int> NeighDelay = new Dictionary<int, int>();

            protected internal Node(int nid)
            {
                Nid = nid;
                Vertex[nid] = this;
            }

            public override string ToString()
            {
                var ns = NeighDelay.Keys.Aggregate("", (a, n) => a + n + ":" + NeighDelay[n] + ", ");
                return string.Format("<Nid:{0}, Parent:{1}, NeighDelay=({2})>",
                    Nid, Parent, ns);
            }

            protected internal virtual void Init()
            {
                if (TRACE_INIT)
                {
                    if (TRACE_THREAD) Console.Write("[{0}] ", TID());
                    Console.WriteLine("{0}", this);
                }
            }

            protected internal virtual async Task<object> Run()
            {
                return null;
            }

            protected internal BufferBlock<Tuple<int, object, int>> inbox = new BufferBlock<Tuple<int, object, int>>();

            protected internal async Task<bool> PostAsync(object tok, int nid2, int dly2)
            {
                MessageCount += 1;

                if (TRACE_POST)
                {
                    if (TRACE_THREAD) Console.Write("[{0}] ", TID());
                    Console.WriteLine("{0} -> [{1}] ... {2}:{3}", Nid, tok, nid2, dly2);
                }

                await Task.Delay(dly2);
                return Vertex[nid2].inbox.Post(Tuple.Create(Nid, tok, dly2));
            }

            protected internal async Task<Tuple<int, object, int>> ReceiveAsync()
            {
                var ntok = await inbox.ReceiveAsync();

                if (TRACE_RECEIVE)
                {
                    if (TRACE_THREAD) Console.Write("[{0}] ", TID());
                    Console.WriteLine("{0} -> [{1}] -> {2}:{3}", ntok.Item1, ntok.Item2, Nid, ntok.Item3);
                }

                return ntok;
            }
        }
    }
}
