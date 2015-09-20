1.Test method

(1). Open command line and make the path to current location

(2). Input “javac BisimulationChecker.java”

(3). Input “java BisimulationChecker”

(4). Input first process info file name when “please input a valid fileP Name” is shown.

(5). Input second process info file name when “please input a valid fileP Name” is shown.

(6). Input output file name when “Please input a filename for output:” is shown.

(7). Check output file for result.


2. Code introduction
There are four classes in total to implement this program.

(1). BisimulationChecker
The entrance class which is required and involves 3 functions to read input, check Bisimulation, write output and the constructor. 

public BisimulationChecker(): constructor, guarantees everything has been initialled

public static void main(String[] args): entry of the program

public void readInput(String fileP, String fileQ):get input file name to handle, if files are not valid,  it will ask for input on the command line.
Parameters:
fileP - - Process P filename
fileQ - - Process Q filename

public void performBisimulation():make bisimulation check, first check whether data is valid, then do process check.

public void writeOutput(String filename): output the bisimulation info to exact file
Parameters:
filename - the file to output

(2). Process 
The data structure of Process which contains states actions and relations between them.

public String resultput(); get info and build final out put
Returns: final output

(3). Relation
The data structure of relation which contains the start and end states and the action between them.

public String RelationOutput(): build output
Returns: final output
 
(4). Tools
An object which contains functions to do file, string process and data Structure process.  

public String fileCheck(String f,int order): check file name about validation , if not, ask user to input again.
Parameters:
f - the name of file to check
order - means for P(1) or Q(2)
Returns: valid filename

public  Set< String> Dorelated(Set< String> Pset,Set< String> ppriSet,String action,Set<Relation> RSet): get the TAP
Returns: the TAP

public  Set< Set< String>> Setsplit( Set< String> Pset,String action,Set< String> ppriSet,Set<Relation> Rset): split the Pset
Returns: result of splite

public  Set< Set< String>> checkBis(BisimulationChecker checker): make bisimulation check
Returns: the result of the bisimilar sets

public Process buildProccess( String f,String p)
build process data by file content
Parameters:
f - input file name
p - name of process
Returns: process, if fail , it is null

public void writeInfo(java.io.BufferedWriter bfw, BisimulationChecker checker): write info to file
Parameters:
bfw - the writer to write info
checker - the similar checker

