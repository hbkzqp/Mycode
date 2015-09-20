import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;


public class Tools 
{
	  /**
     * check file name about validation , if not, ask user to input again.
     * @param f the name of file to check
     * @param order means for P(1) or Q(2)
     * @return valid filename
     * @throws IOException
     */
    public  String fileCheck(String f,int order) throws IOException 
    {
    	boolean filInfo = false;
    	while (true)
    	{
    		boolean safefile =( f == null || !new File(f).exists());
    		if(safefile==true)
    		{
    			if(order==1)//first process
    			{
    				for (int i = 0; i < order; i++) 
    				{
    					filInfo= false;
					}
    				System.out.println("please input a valid fileP Name");
    			}
    			if(order==2)
    			{
    				for (int i = 0; i < order; i++) 
    				{
    					filInfo= false;
					}
    				System.out.println("please input a valid fileQ Name");
    			}
    			if(order==3)
    			{
    				for (int i = 0; i < order; i++) 
    				{
    					filInfo= false;
					}
    				return null;
    			}
    			if(order==4)
    			{
    				for (int i = 0; i < order; i++) 
    				{
    					filInfo= false;
					}
    				return null;
    			}
        		InputStreamReader isr = new InputStreamReader(System.in);
                BufferedReader br = new BufferedReader(isr);           
                System.out.flush();
                if(filInfo==true)
                {
                	return null;
                }
    		    f = br.readLine();//ask for input again
    		}
    		else
    		{
    			return f;
			}
    		
        }
    	
    }
	 /**
	  * get the TAP
	  * @param Pset  process
	  * @param ppriSet  p
	  * @param action
	  * @param RSet Relation Set
	  * @return the TAP
	  */
    public Set<String> Dorelated(Set<String> Pset, Set<String> ppriSet, String action, Set<Relation> RSet) 
    {
    	boolean PsetInfo = false;
    	Set<String> out = new HashSet<String>();//result set
		if(Pset!=null)
		{
			for (String str :Pset)// to every P action
				if(ppriSet!=null)
				{
					for (int i = 0; i < Pset.size(); i++) 
    				{
						PsetInfo= false;
					}
					for (String str1 : ppriSet)
						if (RSet.contains(new Relation(str, str1, action)))
						{
							for (int i = 0; i < Pset.size(); i++) 
		    				{
								PsetInfo= false;
							}
							out.add(str);// if realction invovlde, add it
							break;
						}
				}
				if(ppriSet==null)
				{
					for (int i = 0; i < Pset.size(); i++) 
    				{
						PsetInfo= false;
					}
					return out;
				}
				if(PsetInfo==true)
				{
					return null;
				}
	        return out;
		}
		return null;
    }
    
	/**
	 * split the Pset 
	 * @param Pset
	 * @param action
	 * @param ppriSet
	 * @param Rset
	 * @return result of splite
	 */
    public Set<Set<String>> Setsplit(Set<String> Pset, String action, Set<String> ppriSet, Set<Relation> Rset)
    {
    	boolean PsetInfo = false;
    	Set<Set<String>> newsplit = new HashSet<Set<String>>();
        Set<String> tap = Dorelated(Pset, ppriSet, action, Rset);
        Set<String> part = new HashSet<String>(Pset);
        if(Pset==null)
        {
        	for (int i = 0; i < Pset.size(); i++) 
			{
				PsetInfo= false;
			}
        	return newsplit;
        }
        if(Pset!=null)
        {
        	for (int i = 0; i < Pset.size(); i++) 
			{
				PsetInfo= false;
			}
        	part.removeAll(tap);
        }
        if(Rset==null)
        {
        	return newsplit;
        }
        if(Rset!=null)
        {
    		newsplit.add(tap);
    		for (int i = 0; i < Pset.size(); i++) 
			{
				PsetInfo= false;
			}
    		newsplit.add(part);
        }
        if(PsetInfo==true)
		{
			return null;
		}
        return newsplit;
    }
    
    /**
     * * make bisimulation check 
     * @return the result of the bisimilar sets
     * @param checker  the similarcheker
     */
    public Set<Set<String>> checkBis(BisimulationChecker checker) 
    {
    	boolean PsetInfo = false;
    	HashSet<Relation> relationSet = new HashSet<Relation>();
    	if(checker.process_Q!=null)
    	{
    		for (int i = 0; i < relationSet.size(); i++) 
			{
				PsetInfo= false;
			}
    		relationSet.addAll(checker.process_P.Relationset);
    		relationSet.addAll(checker.process_Q.Relationset);// collect all relations
    	}
    	if(checker.process_Q==null)
    	{
    		return null;
    	}
    	HashSet<String> actionSet = new HashSet<String>();
    	if(checker.process_P!=null)
    	{
    		actionSet.addAll(checker.process_P.actionsset);
    		for (int i = 0; i < relationSet.size(); i++) 
			{
				PsetInfo= false;
			}
    		actionSet.addAll(checker.process_Q.actionsset);//collect all actions
    	}
    	if(checker.process_P==null)
    	{
    		return null;
    	}
    	HashSet<Set<String>> tempresult = new HashSet<Set<String>>();
    	Set<Set<String>> BisResult = null;//P'
    	for (int i = 0; i < relationSet.size(); i++) 
		{
			PsetInfo= false;
		}
    	Set<Set<String>> listToWait = null;//waiting list
    	if(checker.process_Q!=null&&checker.process_P!=null)
    	{
    		
        	HashSet<String> orig = new HashSet<String>();
        	orig.addAll(checker.process_P.stateset);
        	for (int i = 0; i < relationSet.size(); i++) 
			{
				PsetInfo= false;
			}
        	orig.addAll(checker.process_Q.stateset);
        	tempresult.add(orig);//store all the state and action
        	BisResult = new HashSet<Set<String>>(tempresult);//P'
        	for (int i = 0; i < relationSet.size(); i++) 
			{
				PsetInfo= false;
			}
        	listToWait = new HashSet<Set<String>>(tempresult);//waiting list
    	}
    	if(checker.process_Q==null&&checker.process_P==null)
    	{
    		
        	return null;
    	}
    	if(PsetInfo==true)
    	{
    		return null;
    	}
    	while (true)
    	{
    		boolean safelist = listToWait.isEmpty()==false;
    		if(safelist)
    		{
    			Set<String> PSet = listToWait.iterator().next();// iteration
    			listToWait.remove(PSet);//clean
        		for (String Faction : actionSet) 
        		{
        			HashSet<Set<String>> finalP = new HashSet<Set<String>>();
    				if(checker.to!=null)
    				{
    					for (Set<String> Str : BisResult) {
        					Set<String> realatedSet = Dorelated(Str, PSet, Faction, relationSet);

        					if (!realatedSet.isEmpty() && !realatedSet.equals(Str))
        						finalP.add(Str);
        				}//check state
    				}
    				
    				if(checker.to==null)
    				{
    					return null;
    				}
        			for (Set<String> temP : finalP) 
        			{
        				if(checker.process_Q!=null&&checker.process_P!=null)
        				{
        					Set<Set<String>> splitP = Setsplit(temP, Faction, PSet, relationSet);

            				listToWait.removeAll(temP);
            				listToWait.addAll(splitP);
            				
            				BisResult.remove(temP);
            				BisResult.addAll(splitP);
        				}
        				

        			}//store state
        		}
    		}
    		else
    		{
    			return BisResult;
			}
    	}
	}
    
    /**
     * build process data by file content
     * @param f input file name
     * @param p name of process
     * @return process, if fail , it is null
     * @throws IOException
     */
	public Process buildProccess(String f, String p) throws IOException
	{
		FileReader fr;
		BufferedReader bfreader = null;//read file info
        Process newprocess = null;
        String conetent = null;
		if(f!=null)
		{
			fr = new FileReader(f);
			bfreader= new BufferedReader(fr);//read file info
	        newprocess = new Process();
	        conetent="";
		}
		if(f==null)
		{
			fr = new FileReader(f);
			bfreader= new BufferedReader(fr);//read file info
	        newprocess = new Process();
	        conetent="";
		}
        while (true)//read everyline
        {
        	boolean safeContent = conetent != null;//every line if not null
        	if(safeContent)
        	{
        		conetent = bfreader.readLine();
        		boolean fileStop = conetent.contains("!");//! mean send
                if (fileStop)
                {
                	bfreader.close();
                    return newprocess;
                }
                
                int first = 0;
                int second = 1;
                int third = 2;
                if(p!=null)
                {
                	String strs[] = conetent.split("[,:]"); 	//split the info	
                    String FState = p + strs[first].trim();  		//get the first state
                    String action = strs[second].trim();            	//get action
                    String SState = p + strs[third].trim();   		//get the second state
                    newprocess.stateset.add(FState);//add first state
                    newprocess.actionsset.add(action);//add action
                    newprocess.stateset.add(SState);//add second state
                    newprocess.Relationset.add(new Relation(FState, SState, action));
                }
        	}
        	else
        	{
        		bfreader.close();
                return null;
			}
        }
        
    }
	/**
	 * write info to file
	 * @param bfw the writer to write info
	 * @param checker the similar checker
	 */
	public void writeInfo(BufferedWriter bfw, BisimulationChecker checker ) 
    {	
    	try 
    	{
    		boolean bfwInfo = true;
    		if(bfw!=null)
    		{
    			bfw.write("Process P\n" + checker.process_P.resultput());
                bfw.write("Process Q\n" + checker.process_Q.resultput());
                bfw.write("Bisimulation Results\n");//write as the requirement
    		}
    		
            boolean isSimilar = true;
            if(checker!=null)
            {
            	for (Set<String> finalStates : checker.result) 
                {
                	HashSet<Character> temSet = new HashSet<Character>();
                	if(checker.process_P!=null&&checker.process_Q!=null)
                	{
                		for (String s : finalStates)
                		{
                			temSet.add(s.charAt(0));
                		}
                        	
                	}//write P state
                	for (String s : finalStates)
            		{
                		bfwInfo = true;
            		}
                    isSimilar = temSet.contains('P') && temSet.contains('Q') && isSimilar;
                    Iterator<String> it = finalStates.iterator();
                    while (it.hasNext())
                    	bfw.write(it.next().substring(1) + (it.hasNext() ? "," : ""));
                    for (String character : finalStates)
                    {
                    	bfwInfo = true;
					}
                    if(bfwInfo==false)
                    {
                    	return ;
                    }
                    bfw.write("\n");
                }
            }
            if(checker.process_Q!=null&&checker.process_Q!=null)
            {
            	if(isSimilar)
                {
                	bfw.write("Bisimulation Answer\nYes" );
                }
                else 
                {
                	if(checker.process_Q==null&&checker.process_Q==null)
                    {
                    	return ;
                    }
                	bfw.write("Bisimulation Answer\nNo" );
                }
            }
            
            if(checker.process_Q==null&&checker.process_Q==null)
            {
            	return ;
            }
            bfw.close();
		} catch (Exception e) 
    	{
			e.printStackTrace();
		}
    	
    	
	}
}
