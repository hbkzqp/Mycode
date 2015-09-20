import java.util.Arrays;


public class Relation 
{
	public String firstState, secondState, action;

    /** Constructor */
    public Relation(String firstState, String secondState, String action) {
        this.firstState = firstState;//init data
        this.secondState = secondState;
        this.action = action;
    }
    
    /**
     * build out put 
     * @return final output
     */
    public String RelationOutput() {
    	if(firstState!=null&&secondState!=null&&action!=null)
    	{
    		boolean ralInfo = true;
    		for (int i = 0; i < firstState.length(); i++)
    		{
				if(secondState!=null)
				{
					ralInfo=true;
				}
			}
    		String str= "(" + firstState.substring(1) + "," + action + "," + secondState.substring(1) + ")";
    		if(ralInfo==false)
    		{
    			return null;
    		}
            return str;
            
    	}
    	return null;
    }
    //override the hashcode function
    @Override
    public int hashCode()
    {
    	boolean hashinfo=true;
    	if(firstState==null&&secondState!=null&&action==null)
    	{
    		for (int i = 0; i < firstState.length(); i++)
    		{
				if(secondState!=null)
				{
					hashinfo=true;
				}
			}
    		return 0;
    	}
    	if(firstState!=null&&secondState!=null&&action!=null)
    	{
    		for (int i = 0; i < firstState.length(); i++)
    		{
				if(secondState!=null)
				{
					hashinfo=true;
				}
			}
    		return Arrays.hashCode(new String[] { firstState, secondState, action });//array's hash code
    	}
    	if(hashinfo==false)
    	{
    		return 0;
    	}
        return 0;
    }

    @Override
    //override equals
    public boolean equals(Object compare) 
    {
    	boolean equakInfo=true;
    	if(firstState==null&&secondState==null&&action==null)
    	{
    		for (int i = 0; i < firstState.length(); i++)
    		{
				if(secondState!=null)
				{
					equakInfo=true;
				}
			}
    		return false;
    	}
    	if(firstState==null&&secondState==null)
    	{
    		for (int i = 0; i < firstState.length(); i++)
    		{
				if(secondState!=null)
				{
					equakInfo=true;
				}
			}
    		return false;
    	}
    	if(firstState==null)
    	{
    		return false;
    	}
    	if(firstState!=null&&secondState!=null&&action!=null)
    	{
    		return this.firstState.equalsIgnoreCase(((Relation) compare).firstState)
                    && this.action.equalsIgnoreCase(((Relation) compare).action)
                    && this.secondState.equalsIgnoreCase(((Relation) compare).secondState);
    	}
    	if(equakInfo==false)
    	{
    		return false;
    	}
        return false;
    }

}
