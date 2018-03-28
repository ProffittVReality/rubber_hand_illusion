Hello, fellow RA!

####################
#    QUICKSTART    #
####################

RA Controls:
-------------------------------------------------------
Key			Operation
-------------------------------------------------------
Right Arrow		Next stage
Up/Down Arrow		Increase/decrease hand+arm size
Numpad 1		Set a good position
Numpad 2		Use the good position
Numpad 4		Block hand
Numpad 5		Regular hand
DIRECTOR > Lag		Change delay of arm in frames
-------------------------------------------------------


Participant Controls:
-------------------------------------------------------
Key			Operation
-------------------------------------------------------
W/S			Increase/decrease slider value
A/D			Change marker/guess location



########################
#    DETAILED GUIDE    #
########################

(Make sure "HandIllusionRoom.unity" is open, and hit play or Ctrl+P.)

1) Adjust the arm size with the up/down arrow keys until the participant
   feels like the arm size is correct.

   Have the participant align their arm with the virtual arm. The participant
   should be shifting around to make sure the virtual shoulder feels like it
   is where the real shoulder should be. They are free to move their arm 
   around during this process. Once they have found a position where the arm
   fits well, ask them to stay there and press "1" on the numpad.

   The participant may return to a comfortable sitting position, regardless
   of where the arm is. Make sure they know that THEY SHOULD NOT MOVE THEIR 
   BODY AFTER THIS POINT. Once they are ready, press "2" on the numpad.

   Repeat 1) until they feel the arm is in a correct position and they are
   comfortable. Prepare the Oculus Touch controller in your hands for the
   next stage of the experiment, and press the right arrow key ONCE.

2) Tell the participant to use the W/S keys to adjust how real their virtual
   hand feels while you carry out this stage.

   Stroke the hand using the Oculus Touch controller while stroking their real
   hand. Relative to the participant's forward, the virtual hand is .2 meters
   to the left of the real hand. If you are sitting opposite of the participant,
   the virtual hand is .2 meters to your right.

   After completing the stroke section, press the right arrow key once.

3) Tell the participant to use the A/D keys to locate where their real hand is.
   once they have made their estimate, press the right arrow key once.

Congratulations, you have completed the experiment. If multiple participants
need to be tested in a row, simply start at 1) again; the experiment resets
itself. All data is collected in the Data folder, under "RUBBERHAND.txt".

The center of the right-most box has a marker position of 0.5, for reference.

To switch between a block and normal hand, refer to the Quickstart section.
To enable a lag in the virtual arm, change the value of Lag (found in the
inspector, under the DIRECTOR object). 90 ~ 1 second, and Avglag will change to
tell you the specific delay length in seconds. To remove lag, set Lag back to
0.