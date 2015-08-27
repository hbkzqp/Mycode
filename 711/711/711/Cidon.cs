//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Threading.Tasks.Dataflow;

//namespace _711
//{
//    class Cidon
//    {
//        public static void Main(String[] args)//init print prob
//        {
//            //if (args == null || args.Length == 0)
//            //{
//            //    Console.Error.WriteLine("args problem, not valad");
//            //}
//            try
//            {
//                //string[] lines = System.IO.File.ReadAllLines(arg);
//                string[] lines = System.IO.File.ReadAllLines("hehe.txt");
//                Cidon cidon = new Cidon();
//                cidon.buildNodes(lines[0]);
//                for (int i = 1; i < lines.Length; i++)
//                {
//                    cidon.buildPath(lines[i]);
//                }
//                var sw = Stopwatch.StartNew();
//                cidon.startCidon().Wait();
//                sw.Stop();
//                Console.WriteLine("total time {0} ms", sw.ElapsedMilliseconds);
//                Console.WriteLine("node count {0}", cidon.getNodeCount());
//                Console.WriteLine("edge count {0}", (int)(Node.EdgeCount));//edge count question
//                Console.WriteLine("message count {0}", Node.MessageCount);
//                Console.WriteLine("\n\nNode Parent");
//                foreach (int nid in Node.Vertex.Keys)
//                {
//                    Console.WriteLine("{0} {1}", Node.Vertex[nid].Nid, Node.Vertex[nid].Parent);
//                }
//                Node.Reset();
//                Console.WriteLine("fk");
//            }
//            catch (Exception e)
//            {
//                Console.Error.WriteLine("problem happens in Node building, message is {0}.", e.Message);
//            }
//            //foreach (String arg in args)
//            //{
//            //    try
//            //    {
//            //        //string[] lines = System.IO.File.ReadAllLines(arg);
//            //        string[] lines = System.IO.File.ReadAllLines("haha.txt");
//            //        Cidon cidon = new Cidon();
//            //        cidon.buildNodes(lines[0]);
//            //        foreach (String li in lines)
//            //        {
//            //            cidon.buildPath(li);
//            //        }
//            //        var sw = Stopwatch.StartNew();
//            //        cidon.startCidon().Wait();
//            //        sw.Stop();
//            //        Console.WriteLine("total time {0} ms",sw.ElapsedMilliseconds);
//            //        Console.WriteLine("node count {0}",cidon.getNodeCount());
//            //        Console.WriteLine("edge count {0}",(int)(Node.MessageCount/2));
//            //        Console.WriteLine("message count {0}",Node.MessageCount);
//            //        Console.WriteLine("\n\nNode Parent");
//            //        foreach(int nid in Node.Vertex.Keys)
//            //        {
//            //            Console.WriteLine("{0} {1}", Node.Vertex[nid], Node.Vertex[nid].Parent);
//            //        }
//            //        Console.WriteLine("fk");
//            //    }
//            //    catch (Exception e)
//            //    {
//            //        Console.Error.WriteLine("problem happens in Node building, message is {0}.", e.Message);
//            //    }

//            //}
//        }

//        private void buildNodes(String nodeInfo)
//        {
//            String[] nodesNames = nodeInfo.Split(' ');
//            if (nodesNames.Length != 0)
//            {
//                for (int i = 0; i < nodesNames.Length; i++)
//                {

//                    try
//                    {
//                        int id = Int32.Parse(nodesNames[i]);
//                        //new node
//                        if (0 == i)
//                        {
//                            InitiatorNode iNode = new InitiatorNode(id);
//                        }
//                        else
//                        {
//                            CommonNode cNode = new CommonNode(id);
//                        }

//                    }
//                    catch (Exception e)
//                    {
//                        //TODO name problem
//                        Console.Error.WriteLine("sorry, node id is not legal");
//                    }
//                }
//            }


//        }
//        private void buildPath(String pathInfo)
//        {
//            String[] info = pathInfo.Split(' ');
//            try
//            {
//                if (info.Length == 4)
//                {
//                    Node.Link(Int32.Parse(info[0]), Int32.Parse(info[1]), Int32.Parse(info[2]), Int32.Parse(info[3]));
//                }
//                else
//                {
//                    throw new Exception();
//                }
//            }
//            catch (Exception e)
//            {
//                Console.Error.WriteLine("sorry, path build problem, text file is not legal");
//            }

//        }
//        private int getNodeCount()
//        {
//            return ((CidonNode)(Node.Vertex.First().Value)).nodeCount;
//        }
//        public async Task startCidon()
//        {
//            await Network.Start();
//        }
//        protected internal sealed class Network
//        {
//            static internal async Task Start()
//            {
//                int source = Node.Vertex.First().Key;
//                var tasks = new List<Task>();
//                foreach (int nid in Node.Vertex.Keys)
//                {
//                    if (nid != source)
//                        tasks.Add(Node.Vertex[nid].Run()); // no await yet!
//                }

//                await Node.Vertex[source].Run();
//                await Task.WhenAll(tasks);

//            }
//        }

//        // --- your actual Cidon implementation ---

//        protected internal class CidonMessage
//        {
//            protected internal CidonMessage(int code, int nodenum, int source)
//            {
//                this.transferCode = code;
//                this.nodeNum = nodenum;
//                this.sourceNode = source;
//            }

//            protected internal int transferCode;
//            protected internal int nodeNum;
//            protected internal int sourceNode;

//            public override string ToString()
//            {
//                return string.Format("[{0},{1}]", transferCode, nodeNum);
//            }
//        }

//        protected internal class CidonNode : Node
//        {
//            static protected internal readonly int MSG_notify = 0;
//            static protected internal readonly int MSG_forward = 1;
//            static protected internal readonly int MSG_back = 2;

//            protected internal int nodeCount = 1;
//            protected internal int currentChild = 0;

//            protected internal Dictionary<int, int> noWayNode = new Dictionary<int, int>();
//            protected internal void incNodeCount(int source)
//            {
//                //Interlocked.Increment(ref nodeCount);
//                nodeCount = source + nodeCount;
//            }
//            protected internal CidonNode(int nid)
//                : base(nid)
//            {
//            }
//            protected void moveNode(CidonMessage msg)
//            {
//                if (!(noWayNode.Keys.Contains(msg.sourceNode)))
//                {
//                    noWayNode.Add(msg.sourceNode, NeighDelay[msg.sourceNode]);
//                    NeighDelay.Remove(msg.sourceNode);
//                }
//            }
//        }

//        protected internal class InitiatorNode : CidonNode
//        {
//            protected internal InitiatorNode(int nid)
//                : base(nid)
//            {
//            }

//            protected internal override async Task<object> Run()
//            {
//                Init();

//                foreach (int n in NeighDelay.Keys)
//                {
//                    //await 
//                    //Console.WriteLine("{0}->[{1},{2}]->{3}:{4}", Nid, MSG_DIRECT, 0, n, NeighDelay[n]);
//                    PostAsync(new CidonMessage(MSG_notify, 0, Nid), n, NeighDelay[n]);
//                }
//                PostAsync(new CidonMessage(MSG_forward, 0, Nid), NeighDelay.First().Key, NeighDelay.First().Value);
//                currentChild = NeighDelay.First().Key;
//                int rec = 0;
//                var count = NeighDelay.Count();


//                while (rec < count)
//                {
//                    var ntok = await ReceiveAsync();

//                    if (ntok.Item2 is CidonMessage)
//                    {
//                        CidonMessage msg = (CidonMessage)(ntok.Item2);
//                        //if (msg.transferCode == 0)
//                        //{

//                        //}////
//                        if (MSG_forward == msg.transferCode)
//                        {
//                            if (Parent == 0)
//                            {
//                                Parent = msg.sourceNode;
//                                foreach (int id in NeighDelay.Keys)
//                                {
//                                    if (id != Parent)
//                                    {
//                                        PostAsync(new CidonMessage(MSG_notify, 0, Nid), NeighDelay.First().Key, NeighDelay[NeighDelay.First().Key]);
//                                    }
//                                }

//                            }
//                        }
//                        if (MSG_notify == msg.transferCode)
//                        {
//                            try
//                            {
//                                moveNode(msg);
//                                if (currentChild == msg.sourceNode)
//                                {
//                                    PostAsync(new CidonMessage(MSG_forward, 0, Nid), NeighDelay.First().Key, NeighDelay[NeighDelay.First().Key]);
//                                    rec = rec - 1;
//                                }
//                            }
//                            catch (Exception e)
//                            {
//                                Console.WriteLine(e.Message);
//                            }
//                        }
//                        if (MSG_back == msg.transferCode)
//                        {
//                            incNodeCount(msg.nodeNum);
//                            moveNode(msg);
//                            if (NeighDelay.Count != 0)
//                            {
//                                PostAsync(new CidonMessage(MSG_forward, 0, Nid), NeighDelay.First().Key, NeighDelay[NeighDelay.First().Key]);
//                                rec = rec - 1;
//                            }
//                        }
//                    }
//                    rec += 1;

//                }

//                if (TRACE_THREAD) Console.Write("[{0}] ", TID());
//                Console.WriteLine("\n\nsource {0} decides {1}", Nid, Vertex.Keys.Count);

//                return Nid;
//            }
//        }

//        protected internal class CommonNode : CidonNode
//        {
//            protected internal CommonNode(int nid)
//                : base(nid)
//            {
//            }

//            protected internal override async Task<object> Run()
//            {
//                Init();

//                if (TRACE_PARENT)
//                {
//                    if (TRACE_THREAD) Console.Write("[{0}] ", TID());
//                    Console.WriteLine("{0} => {1}", Parent, Nid);
//                }


//                int rec = 0;
//                var count = NeighDelay.Count() + 1;
//                while (rec < count)
//                {
//                    try
//                    {
//                        var ntok = await ReceiveAsync();
//                        //if (Nid == 3)
//                        //{
//                        //    Console.WriteLine("sss5");
//                        //}
//                        //if (Nid == 4)
//                        //{
//                        //    Console.WriteLine("sss4");
//                        //}
//                        if (ntok.Item2 is CidonMessage)
//                        {
//                            CidonMessage msg = (CidonMessage)(ntok.Item2);
//                            //if (msg.transferCode == 0)
//                            //{

//                            //}////
//                            if (MSG_forward == msg.transferCode)
//                            {
//                                if (Parent == 0)
//                                {
//                                    Parent = msg.sourceNode;
//                                    moveNode(msg);
//                                    if (NeighDelay.Count != 0)
//                                    {
//                                        foreach (int id in NeighDelay.Keys)
//                                        {
//                                            if (id != Parent)
//                                            {
//                                                PostAsync(new CidonMessage(MSG_notify, 0, Nid), id, NeighDelay[id]);
//                                            }

//                                        }
//                                    }
//                                    if (noWayNode.Count != 0)
//                                    {
//                                        foreach (int id in noWayNode.Keys)
//                                        {
//                                            if (id != Parent)
//                                            {
//                                                PostAsync(new CidonMessage(MSG_notify, 0, Nid), id, noWayNode[id]);
//                                            }
//                                        }
//                                    }

//                                    postForward();

//                                }
//                                else
//                                {
//                                    rec--;
//                                }

//                            }
//                            if (MSG_notify == msg.transferCode)
//                            {

//                                moveNode(msg);
//                                if (currentChild == msg.sourceNode)
//                                {
//                                    PostAsync(new CidonMessage(MSG_forward, 0, Nid), NeighDelay.First().Key, NeighDelay[NeighDelay.First().Key]);
//                                }


//                            }
//                            if (MSG_back == msg.transferCode)
//                            {
//                                incNodeCount(msg.nodeNum);
//                                moveNode(msg);
//                                if (NeighDelay.Count != 0)
//                                {
//                                    PostAsync(new CidonMessage(MSG_forward, 0, Nid), NeighDelay.First().Key, NeighDelay[NeighDelay.First().Key]);
//                                }
//                            }
//                        }
//                        rec += 1;
//                        //if (rec == count)
//                        //{
//                        //    Console.WriteLine("aaa{0}", Nid);
//                        //}

//                        //await 
//                        //Console.WriteLine("{0}->[{1},{2}]->{3}:{4}", Nid, MSG_REFLECT, nodeCount, Parent, dly);


//                    }
//                    catch (Exception e)
//                    {
//                        Console.WriteLine(e.Message + e.StackTrace + e.Source);
//                    }

//                }
//                int dly = noWayNode[Parent];
//                PostAsync(new CidonMessage(MSG_back, nodeCount, Nid), Parent, dly);
//                return Nid;

//            }

//            private void postForward()
//            {
//                if (NeighDelay.Count != 0)
//                {
//                    PostAsync(new CidonMessage(MSG_forward, 0, Nid), NeighDelay.First().Key, NeighDelay[NeighDelay.First().Key]);
//                }
//            }


//        }

//        // ===========================================================
//        // Please do NOT touch this library
//        // ===========================================================

//        static protected internal int TID() { return Thread.CurrentThread.ManagedThreadId; }

//        protected internal class Node
//        {
//            // static members

//            static protected internal bool TRACE_THREAD = false;
//            static protected internal bool TRACE_INIT = true;
//            static protected internal bool TRACE_POST = false;
//            static protected internal bool TRACE_RECEIVE = true;
//            static protected internal bool TRACE_PARENT = false;

//            static protected internal Dictionary<int, Node> Vertex = new Dictionary<int, Node>();
//            static protected internal int EdgeCount = 0;
//            static protected internal int MessageCount = 0;

//            static protected internal void Reset()
//            {
//                Vertex = new Dictionary<int, Node>();
//                EdgeCount = 0;
//                MessageCount = 0;
//            }

//            static protected internal void Link(int nid1, int nid2, int del12, int del21)
//            {
//                EdgeCount += 1;
//                Vertex[nid1].NeighDelay[nid2] = del12;
//                Vertex[nid2].NeighDelay[nid1] = del21;
//            }

//            // instance members

//            protected internal int Parent = 0;
//            protected internal int Nid;
//            protected internal Dictionary<int, int> NeighDelay = new Dictionary<int, int>();

//            protected internal Node(int nid)
//            {
//                Nid = nid;
//                Vertex[nid] = this;
//            }

//            public override string ToString()
//            {
//                var ns = NeighDelay.Keys.Aggregate("", (a, n) => a + n + ":" + NeighDelay[n] + ", ");
//                return string.Format("<Nid:{0}, Parent:{1}, NeighDelay=({2})>",
//                    Nid, Parent, ns);
//            }

//            protected internal virtual void Init()
//            {
//                if (TRACE_INIT)
//                {
//                    if (TRACE_THREAD) Console.Write("[{0}] ", TID());
//                    Console.WriteLine("{0}", this);
//                }
//            }

//            protected internal virtual async Task<object> Run()
//            {
//                return null;
//            }

//            protected internal BufferBlock<Tuple<int, object, int>> inbox = new BufferBlock<Tuple<int, object, int>>();

//            protected internal async Task<bool> PostAsync(object tok, int nid2, int dly2)
//            {
//                MessageCount += 1;

//                if (TRACE_POST)
//                {
//                    if (TRACE_THREAD) Console.Write("[{0}] ", TID());
//                    Console.WriteLine("{0} -> [{1}] ... {2}:{3}", Nid, tok, nid2, dly2);
//                }

//                await Task.Delay(dly2);
//                return Vertex[nid2].inbox.Post(Tuple.Create(Nid, tok, dly2));
//            }

//            protected internal async Task<Tuple<int, object, int>> ReceiveAsync()
//            {
//                var ntok = await inbox.ReceiveAsync();

//                if (TRACE_RECEIVE)
//                {
//                    if (TRACE_THREAD) Console.Write("[{0}] ", TID());
//                    Console.WriteLine("{0} -> [{1}] -> {2}:{3}", ntok.Item1, ntok.Item2, Nid, ntok.Item3);
//                }

//                return ntok;
//            }
//        }
//    }
//}
