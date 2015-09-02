///
//Greg Hatfield
//Bowling 2
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace bowling2
{
    class Program
    {
        //constants
       const int TEN = 10;
       const int TWO = 2;
       const int MAXFRAME = 11;
       
       int frameCnt = 0;

     
        static void Main(string[] args)
        {
            //vars
            string fileName = "";
            int curBall = 0;
            int prevBall = 0;
            int counter = 0;
            int subcnt = 0;
            bool start = true;
            bool strike = false;
            bool spare = false;
            int totalScore = 0;
            Frame[] frameList = new Frame[MAXFRAME];
            Strike[] strikeList = new Strike[MAXFRAME];
            Spare[] spareList = new Spare[MAXFRAME];    

            for (int i = 0; i < MAXFRAME; i++ )
            {
                frameList[i] = new Frame()
                {
                    FrameNum = i,
                    BallOne = 0,
                    BallTwo = 0,
                    Filled = false
                };
                strikeList[i] = new Strike()
                {
                    FrameNum = 0,
                    BallOne = 0,
                    BallTwo = 0,
                    Filled = false
                };

                spareList[i] = new Spare()
                {
                    FrameNum = 0,
                    BallOne = 0,
                    BallTwo = 0,
                    Filled = false
                };
            }
 
            List<string> list = new List<string>();
            List<int> numList = new List<int>();
            
            
            //read in file FROM INSTRUCTIONS data will be either on ONE line or split on lines so split on space and new line
            Console.WriteLine("Enter bowling text file please:");
            fileName = Console.ReadLine();
            if(File.Exists(fileName))
            {
                try
                {

                    foreach (string line in File.ReadLines(fileName))
                    {
                        string[] numbers = line.Split(' ', '\n','\r');
                        foreach(string num in numbers)
                        {
                            //if valid, add to numList, not valid ignore
                            bool check = int.TryParse(num, out curBall);
                            if (check == true)
                            {
                                numList.Add(curBall);
                            }
                                                       
                            list.Add(num);
                           // Console.WriteLine("from array " + curBall);
                        }
                           //Console.WriteLine(line);
                    }                    
                    Console.WriteLine("---------------------------------------"); 
                    //go through each item in list and assign to frame objects                   
                      foreach (int item in numList)
                        {
                        //Console.WriteLine(item);
                        if (counter > MAXFRAME-1)
                        {

                            break;
                        }
                        
                        //Console.WriteLine(item);
                        curBall = item;
                        //for bonus frame stuff
                        if (counter == MAXFRAME -1)
                        {
                            if(subcnt == 0)
                            {
                                frameList[counter].FrameNum = counter;
                                frameList[counter].BallOne = curBall;
                                subcnt++;
                                
                            }
                            else if(subcnt == 1 && frameList[counter].Filled == true)
                            {
                                frameList[counter].BallTwo = curBall;                                
                                counter++;                                
                            }
                            if(counter == MAXFRAME-1)
                            frameList[counter].Filled = true;
                        }
                        //normal 10 frames
                        if (counter < MAXFRAME - 1)
                        {
                            
                            //handle strike scenario
                            if (item == TEN)
                            {
                                //Console.WriteLine("strike");

                                if (strikeList[counter].Filled == false)
                                {
                                    strikeList[counter].FrameNum = counter;
                                    strikeList[counter].BallOne = curBall;
                                    strikeList[counter].Filled = true;
                                    // Console.WriteLine("FRAME #" + (strikeList[counter].FrameNum+1) + " BALL IN FRAME " + strikeList[counter].BallOne);
                                    strike = true;
                                    //get frame list of all frames
                                    frameList[counter].FrameNum = counter;
                                    frameList[counter].BallOne = curBall;
                                    frameList[counter].BallTwo = 0;
                                    frameList[counter].Filled = true;
                                    counter++;

                                }

                            }
                            //handle spare scenario
                            else if (prevBall + curBall == TEN && strike == false)
                            {
                                //Console.WriteLine("Spare!");
                                if (spareList[counter].Filled == false)
                                {
                                    spareList[counter].FrameNum = counter;
                                    spareList[counter].BallOne = prevBall;
                                    spareList[counter].BallTwo = curBall;
                                    spareList[counter].Filled = true;
                                    //Console.WriteLine("FRAME #" + (spareList[counter].FrameNum+1) + " BALLS IN FRAME " + spareList[counter].BallOne + " " + spareList[counter].BallTwo);

                                    spare = true;
                                    //fill framelist with ALL frames
                                    frameList[counter].FrameNum = counter;
                                    frameList[counter].BallOne = prevBall;
                                    frameList[counter].BallTwo = curBall;
                                    frameList[counter].Filled = true;

                                    counter++;

                                }

                            }
                            //handle normal frame(non strike/spare) scenario
                            else
                            {
                                if (frameList[counter].Filled == false && start == false && strike == false && spare == false && curBall + prevBall < TEN)
                                {
                                    frameList[counter].FrameNum = counter;
                                    frameList[counter].BallOne = prevBall;
                                    frameList[counter].BallTwo = curBall;
                                    frameList[counter].Filled = true;
                                    if (frameList[counter].FrameNum + 1 == MAXFRAME)
                                    {
                                        // Console.WriteLine("BONUS FRAME #" + (frameList[counter].FrameNum + 1) + " BALLS IN FRAME " + frameList[counter].BallOne + " " + frameList[counter].BallTwo);
                                    }
                                    else
                                        // Console.WriteLine("FRAME #" + (frameList[counter].FrameNum+1) + " BALLS IN FRAME " + frameList[counter].BallOne + " " + frameList[counter].BallTwo);

                                        counter++;

                                }
                            }
                            prevBall = item;
                            start = false;
                            spare = false;
                            strike = false;
                        }
                    }
                    //loop through the list and do score/nextball stuff
                    for(int i = 0; i < MAXFRAME; i++)
                    {
                        //calc spare scores
                        if(spareList[i].Filled == true)
                        {
                            //handle score stuff
                            spareList[i].NextFrame(frameList);
                            spareList[i].calcScore();
                            frameList[i].FrameScore = spareList[i].FrameScore;
                            
                        }
                        //calc strike scores
                        if (strikeList[i].Filled == true)
                        {
                            if(i == MAXFRAME-1)
                            {
                                break;
                            }
                            //handle score stuff
                            strikeList[i].NextFrame(frameList);
                            strikeList[i].calcScore();
                            frameList[i].FrameScore = strikeList[i].FrameScore;
                        }
                        else
                        {
                            //calc normal scores
                            if(spareList[i].Filled == false && strikeList[i].Filled == false)
                            frameList[i].calcScore();
                        }
                    }
                    
                    //display results
                    for(int i = 0; i < MAXFRAME; i ++)
                    {
                        if(strikeList[i].Filled == true)
                        {
                            totalScore += frameList[i].FrameScore;
                            Console.WriteLine("Frame #" + (i+1) + " Strike! FrameScore: " + frameList[i].FrameScore + " Total Score: " + totalScore);
                        }
                        if(spareList[i].Filled == true)
                        {
                            totalScore += frameList[i].FrameScore;
                            Console.WriteLine("Frame #" + (i+1) + " Spare! Ball1:" + frameList[i].BallOne + " Ball2:" + frameList[i].BallTwo +  " FrameScore: " + frameList[i].FrameScore + " Total Score: " + totalScore);
                            
                        }
                        else
                        {
                            if(frameList[i].FrameNum == MAXFRAME -1)
                            {
                            Console.WriteLine("BONUS Frame #" + (i + 1) + " Ball1:" + frameList[i].BallOne + " Ball2:" + frameList[i].BallTwo + " FrameScore: " + frameList[i].FrameScore + " Total Score: " + totalScore);
                                break;
                            }
                            if(strikeList[i].Filled == false && spareList[i].Filled == false)
                            {
                                totalScore += frameList[i].FrameScore;
                                Console.WriteLine("Frame #" + (i + 1) + " Ball1:" + frameList[i].BallOne + " Ball2:" + frameList[i].BallTwo + " FrameScore: " + frameList[i].FrameScore + " Total Score: " + totalScore);

                            }
                        }

                        Console.WriteLine("---------------------------------------------");
                    }
 
                }
                catch (Exception e)
                {
                    Console.WriteLine("File could not be opened, following error: '{0}'", e);
                }
            }
            Console.WriteLine("Hit Enter To EXIT");
            Console.ReadLine(); 
        }

        
    }
    /// <summary>
    /// all frames created and read from list which is from file
    /// </summary>
    class Frame
    {
        //constants
       public const int TEN = 10;
        public const int TWO = 2;
        public const int MAXFRAME = 11;

        private int frameNum;
        private int ballOne;
        private int ballTwo;
        private int frameScore;
        private bool filled;

        //setters and getters
        public int FrameNum
        {
            get { return frameNum;
                }
            set { frameNum = value;           
                }
            
        }

        public int BallOne
        {
            get { return ballOne; }
            set { ballOne = value; }
        }

        public int BallTwo
        {
            get { return ballTwo; }
            set { ballTwo = value; }
        }

        public bool Filled
        {
            get { return filled; }
            set { filled = value; }
        }

        public int FrameScore
        {
            get { return frameScore; }
            set { frameScore = value; }
        }

        //calculate scores
        public void calcScore()
        {
            FrameScore = BallOne + BallTwo;
        }

    }

    //if the score from file is 10
    class Strike : Frame
    {        
       private int nextBall1;
       private int nextBall2;
        public Strike()
        {
            //Console.WriteLine("Strike FRAME OBJECT CREATED!");
        }
        //setters and getters
        public int NextBall1
        {
            get { return nextBall1; }
            set { nextBall1 = value; }
        }

        public int NextBall2
        {
            get { return nextBall2; }
            set { nextBall2 = value; }
        }

        //handle next frame operation for strike (next 2 balls)
        public void NextFrame(Frame[] frameArry)
        {
            int curFrame = FrameNum;
            int nexFrame = frameArry[FrameNum + 1].FrameNum;
            NextBall1 = frameArry[nexFrame].BallOne;
            //Console.WriteLine("GET NEXT BALL FROM FRAME #" + (nexFrame + 1) + " Which is: " + NextBall1);
            //handle special case if first ball is strike go to next frame
            if (NextBall1 == TEN)
            {
                if (nexFrame + 1 < MAXFRAME - 1)
                {
                    NextBall2 = frameArry[nexFrame + 1].BallOne;
                }
                else if (nexFrame + 1 == MAXFRAME - 1)
                {
                    NextBall2 = frameArry[nexFrame + 1].BallOne;
                }
                else if (nexFrame == MAXFRAME - 1)
                {
                    NextBall2 = frameArry[nexFrame].BallTwo;
                }

            }
            else
            {
                NextBall2 = frameArry[nexFrame].BallTwo;
                //Console.WriteLine("AND " + NextBall2);
            }

        }

        //calculate the score for the frame
        public void calcScore()
        {
            //Console.WriteLine("SCore DEBUG " + BallOne + NextBall1 + NextBall2 + (BallOne + NextBall1 + NextBall2) + "FRAME: " + FrameNum);
            FrameScore = BallOne + NextBall1 + NextBall2;
        }


    }

    /// <summary>
    ///if score from frame ball 1 and ball 2 = 10 its a spare
    /// </summary>
    class Spare : Frame
    {
       private int nextBall1;
        public Spare()
        {
            //Console.WriteLine("SPARE FRAME OBJECT CREATED!");
        }

        public int NextBall
        {
            get { return nextBall1; }
            set { nextBall1 = value; }
            
        }
        //handle the next frame operation
        public void NextFrame(Frame[] frameArry)
        {
            int curFrame = FrameNum;
            if (curFrame < MAXFRAME - 1)
            {
                int nexFrame = frameArry[FrameNum + 1].FrameNum;
                NextBall = frameArry[nexFrame].BallOne;
            }
           // Console.WriteLine(NextBall + " FOR DEBUG");
           // Console.WriteLine("GET NEXT BALL FROM FRAME #" + (nexFrame+1) + " Which is: " + NextBall);
        }
        //calculate the score for the frame
        public void calcScore()
        {
            //Console.WriteLine((NextBall + BallOne + BallTwo) + " FOR DEBUG BALL 1 = " +BallOne + " BALL 2 = " + BallTwo);

            FrameScore = BallOne + BallTwo + NextBall;

        }
    }

    
}
