import java.util.Arrays;
import java.util.Comparator;
import java.util.HashSet;
import java.util.Iterator;

/**
 * process object to store actions and states
 * @author qzha994
 *
 */
public class Process
{
	public HashSet<String> stateset =  new HashSet<String>() ;//states
	public HashSet<String> actionsset =  new HashSet<String>();//actions
    HashSet<Relation> Relationset = new HashSet<Relation>();//relataions between them

    /** Constructor */
    public Process() 
    {
    	
    }

    /**
     * build info string of this process
     * @param sarray  actions
     * @param isallowed ok to outout
     * @return info about this process
     */
    private String buildString(String[] sarray, boolean isallowed)
    {
    	if(sarray!=null)//array should not be null
    	{
    		boolean arrayinfo = true;
    		 
    		String result = "";//init result
            Iterator<String> s = Arrays.asList(sarray).iterator();
            for (String string : sarray) 
            {
            	 
 			 	if(result==null)
 			 	{
 			 		arrayinfo=false;
 			 	}
 			 	else 
 			 	{
 			 		arrayinfo=true;
 			 		break;
 			 	}
			}
           
   			 	
            if(arrayinfo==false)
            {
            	return null;
            }
            while (true) //for every action
            {
            	boolean safeit = s.hasNext();
            	if(safeit)
            	{
            		if(isallowed)
            		{
            			result +=  s.next().substring(1);//put into result
            		}
            		else
            		{
            			result = result+s.next();
    				}
            		if(s.hasNext() )
            		{
            			result = result+"," ;
            		}
            	}
            	else
            	{
                    return result;
    			}
            	for (String string : sarray) 
                {
                	 
     			 	if(result==null)
     			 	{
     			 		arrayinfo=false;
     			 	}
     			 	else 
     			 	{
     			 		arrayinfo=true;
     			 		break;
     			 	}
    			}
                
            }
    	}
        return null;

    }
    
    /**
     * get info and build final out put
     * @return final output
     */
    public String resultput()
    {
        String result = "S = ";// init result
        boolean resultInfo = true;
        String[] statesetOut = stateset.toArray(new String[stateset.size()]);
        if(stateset!=null)
        {
        	 Arrays.sort(statesetOut);//sort items 
             result += buildString(statesetOut, true);//build result
             
             result += "\nA = ";
        }
        int a = actionsset.size();
        String[] actionssetOut = actionsset.toArray(new String[a]);
        if(Relationset!=null)
        {
        	 Arrays.sort(actionssetOut);//sort actions
             result = result+ buildString(actionssetOut, false);//build result
             
             result = result+ "\nT = ";// init result
             a= Relationset.size();
             Relation[] RelationsOut = Relationset.toArray(new Relation[a]);
             Arrays.sort(RelationsOut, new Comparator<Relation>() {
                 @Override
                 public int compare(Relation x, Relation y) {
                     if (!x.firstState.equalsIgnoreCase(y.firstState))
                         return x.firstState.compareTo(y.firstState);//compare by state

                     if (!x.action.equalsIgnoreCase(y.action))
                         return x.action.compareTo(y.action);//compared by action

                     return x.secondState.compareTo(y.secondState);
                 }
             });// override the sort
             if(actionsset!=null)
             {
            	 Iterator<Relation> rts = Arrays.asList(RelationsOut).iterator();//
                 while (true)
                 {
                	if(statesetOut!=null)
                	{
                		for (String relation : statesetOut) 
                		{
                			if(result!=null)
                			{
                				resultInfo = true;
                			}
						}
                		
                	}
                 	boolean isnext = rts.hasNext();
                 	if(isnext)
                 	{
                 		result =result+ rts.next().RelationOutput();// combine info to result
                 		if(rts.hasNext())
                 		{
                 			result =result+ ",";
                 		}
                 	}
                 	else
                 	{
                 		return result += "\n";
         			}
                 	if(resultInfo==false)
                 	{
                 		return null;
                 	}
                 }
             }
            
             
        }
        return null;
    }
}
