/**
 * BisimulationChecker.java
 * Author: Qipeng Zhang
 */
import java.io.*;
import java.util.Set;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Arrays;
import java.util.Comparator;

/**
 * The main Class of the program, including all logic: commond line, file process, data process
 * @author qzha994
 *
 */
public class BisimulationChecker {
    private static final String P="P";//name of process
    private static final String Q="Q";
    public Tools to = new Tools();// the tools class
	public Process process_P = null;//init the two process as null
	public Process	process_Q = null;
	public Set<Set<String>> result = new HashSet<Set<String>>();//result
	/**
	 * entry of the program 
	 * @param args
	 */
    public static void main(String[] args) 
    {
        BisimulationChecker checker = new BisimulationChecker();
        checker.readInput(null, null);//if null , it will ask for input
        checker.performBisimulation();//compute
        checker.writeOutput(null);//if null it will ask for out put file
    }
    /**
     *constructor 
     *every thing has been inited
     */
    public BisimulationChecker() 
    {
    }
    /**
     * get input file name to handle
     * @param fileP - Process P filename
     * @param fileQ - Process Q filename
     */
    public void readInput(String fileP, String fileQ) {
    	try
    	{	
    		fileP= to.fileCheck(fileP,1);//check whether it is valid
    		fileQ= to.fileCheck(fileQ,2);
    		this.process_P=to.buildProccess(fileP, P);// build file info to process data
    		this.process_Q = to.buildProccess(fileQ, Q);
		}
    	catch(Exception e)
    	{
    		readInput(null, null);
    	}
    }
  

    

	/**
     *make bisimulation check, first check whether data is valid, the do process check.
     */
    public void performBisimulation() {
    	if (process_P == null )// process should not be null
    	{
    		System.err.println("Sorry, first input file is not valid");
    	}
    	if (process_Q == null )
    	{
    		System.err.println("Sorry, second input file is not valid");
    	}	
    	else
    		this.result = to.checkBis(this);// make Bis compute
    }
	
  
    
   

    /**
     * output the bisimulation info to exact file
     * @param filename  the file to output
     */
    public void writeOutput(String filename) 
    {
    	try {
	        while (true) 
	        {
	        	boolean isfileSafe = filename == null || filename.length() < 1;
	        	//if file name is not valid ask to input again
	        	if(isfileSafe)
	        	{
	        		InputStreamReader insr = new InputStreamReader(System.in);
		            BufferedReader bfr = new BufferedReader(insr);//read file name
		            System.out.println("Please input a filename for output: ");// get Input
		            System.out.flush();
		            filename = bfr.readLine();
		            bfr.close();
	        	}
	        	else
	        	{
	        		break;
				}
	        }
	        FileWriter fw= new FileWriter(filename);//writer
	        BufferedWriter bfw = new BufferedWriter(fw);//write file
	        to.writeInfo(bfw, this);//start write
    	}catch(IOException e){ 
    		e.printStackTrace();
    	}
    }
    
    
   
}
