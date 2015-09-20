As illustrated in the assignment requirement, four components are designed in this model. 

1. Train

There are 3 states for Train which are:

Idle: train is still far away. 

Appr: train is approaching. It will cost 15 to 30 seconds (calculate by clock) to approach. Once it reaches this state, it will send "appr[trainId]!".

Cross:train has left the railway. It will cost 15 to 30 seconds to cross. Once it finishes this state, it will send "leave [trainId]! ".

2. Control

It has 1 variable “num” means the account of trains on the railway. 

There are 2 states for Control which are:

Ready: means no train is on the railway. Once it gets "appr[trainId]? ", it will send "close!" to start to close the gate or "turnR!"  to let the light start to turn red as soon as possible by "Committed location" depending on the current states of gate and light. Then the state will go to "Passing" and num =num+1.

Passing: means railway is occupied. Once it gets "appr[trainId]? ", it will stay in this state and num = num+1. Once it get "leave[trainId]? ", if num ==1, num= num -1 and it send "open!" to open gate as soon as possible by "Committed location" (gate will send "turnR! " to let light turn green after open.) Then the state will go back to "Ready". If num!=1 it will stay in this state and num = num-1.

There are two "Committed Location" to send "close!" and "turnG"

3. Gate

It has 1 variable “isInter” to indicate that whether gate opening is interrupted. 

There are 5 states for Gate which are:

Off: means the gate is closed. Once it gets "open?", it will turn to state “Opening” to start open the gate.

Opening: means the gate is opening. It will last for 5 seconds(using clock). Once it gets "close?", it will stay and go to “On” state then change the value of “isInter”. If "close?" does not come during 5 seconds, it will send “turnG!” to let light turn green then turn state On or turn On directly by "Committed location" depending on the state of light. 

On: means the gate is open. Once this state is reached, it will check the value “isInter” to determine whether close immediately. If yse, it will turn to "Closing". If no, it will wait for "close?” to close the gate. Once "close?" comes, it will turn to "Closing".

stayOn: means the gate stays "On", so that the light can change to "green". It will react to "close!" to start closing gate as to state"colsing". Or it will go to state "On". Because state "On" is Committed location, to avoid it stop light state change, I add this state. 

Closing: means the gate is closing. It will last for 5 seconds. Then turn to "Off"

There on 2 "Committed Location", one is used to send"turnG" and the other one is state "On",  if "close!" comes when opening, it will still turn to "On" but will close immediately..

4.Light 

There are 4 states for Light which are:

red: means the light is red. Once it gets "turnG?" Which means light should turn green now.  It will turn to state "turningG".

turningG: means the light is turning green. This state will cost 2 seconds if "turnR?" does not come, then it will turn to state green. However, if  "turnR?" comes during the 2 seconds, it will return state "red" directly and send "close!" to let gate close by "Committed location".

green: means the light is green.  Once it gets "turnR?", it will take 2 seconds to state "yellow". 

yellow: means the light is yellow. This state will just stay 2 seconds then turn to "red" and send "close!" by "Committed location".

There are 2 "Commnitted Location" to send "close!".



Declarations: 
Global:

const int TrNum=3;//trace number
int gateState = 2;//the state for every train,0 means closed 1 means open, 2means off, 3 means on
int lightState = 0;//the state for light, 0 means red, 1 means yellow, 2 means green,3 means turning 
typedef  int[0,TrNum-1] trainId;// trainId;
chan appr[3],cross[3],leave[3];//action of train 
urgent chan turnR;// light states to turn red immediately 
urgent chan turnG; // light states to turn green immediately
urgent chan open; // gate states to turn open immediately
urgent chan close;// // gate states to turn close immediately

Train:
clock x;

Control:
int [0,TrNum] num;

Gate:

clock x;
int isInter=0;

Light:
clock x;



Verification:(all verification passed)
1. A[] not deadlock
The model is deadlock free.

2. Gate.opening --> Gate.On; Gate.closing--> Gate.Off
Gates are never instructed to raise or lower in th mid-way of closing/opening
	
3. A[] (Light.green) imply (Gate.On or Gate.stayOn)
Traffic lights are never turned green before opening gates.

4. A[]  (Gate.Off or Gate.closing)  imply (Light.red)
Traffic lights are always turned red before closing the gates.

5. A[] forall(i:trainId) Train(i).Cross imply (Gate.Off or Gate.closing)
Gates are never open when a train is inside crossing.

6. E<> (Light.green)
Road traffic can pass through.


Colour:

The blue lines mean to send channel signal and orange mean waiting for channel signal.
The colourd states means important states in this model.