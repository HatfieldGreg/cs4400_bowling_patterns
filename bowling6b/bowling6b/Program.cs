using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bowling6b
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
            //string fileName = "";
            int curBall = 0;


            //read in file FROM INSTRUCTIONS data will be either on ONE line or split on lines so split on space and new line
            //Console.WriteLine("Enter bowling text file please:");
            //fileName = Console.ReadLine();
            
                try
                {

               // List<string> list = new List<string>();
                List<int> list = new List<int>();
                string line;
                do
                {
                    line = Console.ReadLine();
                    if (line != "END")
                    {
                        bool check = int.TryParse(line, out curBall);
                        if (check == true)
                        {
                            list.Add(curBall);
                            //Console.WriteLine(curBall);
                        }

                    }
                } while (line != "END");

                //handle the values
                Console.WriteLine("------------------------------------");
                // Console.WriteLine(list[0] + " is the Number of players");

                //get number of players for game(one for now
                //int numPlayers = list[0];
                int numPlayers = 1;
                //list.RemoveAt(0);//we don't need that number anymore, pop it
                    //int count = 0;
                    Player[] playerArray = new Player[numPlayers];
                    for (int i = 0; i < numPlayers; i++)
                    {
                        playerArray[i] = new Player();
                        playerArray[i].playerNum = i;
                    }
                    int curPlayer = 0;
                errorCount errCnt = new errorCount();
                if (numPlayers > 0)
                    {
                        curPlayer = 0;
                        int ballNum = 1;
                       // bool ballFilled = false;
                        //bool strike = false;
                        int frame = 1;
                        int tenOne = 0;
                        int tenTwo = 0;
                        int past = 0;
                        
                        bool isStrike = false;
                        bool isStrike2 = false;
                        bool isSpare = false;
                        bool isNormal = false;
                        bool skip = false;
                        bool skip2 = false;
                    
                    //valueHandler(frame, item, curPlayer, ballNum, playerArray);
                 
                    foreach (int item in list)
                        {
                            //Console.WriteLine(item + " CURRENT ITEM");
                            if (curPlayer > numPlayers - 1 && skip2 == false)
                            {
                                curPlayer = 0;
                                frame++;
                            }
                       
                            if (frame == MAXFRAME)
                            {
                                break;
                            }
                        
                       
                            if (frame < TEN)
                            {
                                switch (ballNum)
                                {
                                    case 1:
                                    if (frameOk(frame, item, 0, errCnt) == true)
                                    {
                                        
                                        //ballFilled = true;
                                        if (item != TEN)
                                        {
                                            ballNum = 2;
                                            
                                        }
                                        if (item == TEN)
                                        {
                                            valueHandler(frame, item, curPlayer, ballNum, playerArray);
                                            curPlayer++;
                                        }
                                        past = item;
                                    }                                    

                                        //Console.WriteLine("ball 1 " + item);

                                        break;
                                    case 2:
                                    // if (ballFilled == false && strike == false)
                                    // {
                                    if (frameOk(frame, item, 0, errCnt) == true)
                                    {
                                        if(frameOk(frame, past, item, errCnt) == true && past != TEN)
                                        {
                                            if (past + item <= TEN)
                                            {
                                                valueHandler(frame, past, curPlayer, 1, playerArray);
                                                valueHandler(frame, item, curPlayer, ballNum, playerArray);
                                                curPlayer++;
                                                ballNum = 1;
                                                skip2 = false;
                                            }
                                        }
                                        else
                                        {
                                            ballNum = 1;
                                            skip2 = true;
                                        }

                                        //Console.WriteLine("ball 2 " + item);
                                        // }
                                    }
                                        break;
                                }
                            }
                        //jumbled code for handling 10th frame and extra frame
                        if (frame == MAXFRAME - 1 && frameOk(frame, item, 0, errCnt) == true)
                        {
                           
                                if (isSpare == true)
                                {
                                    ballNum = 1;
                                    //Console.WriteLine("TEST");

                                    valueHandler(MAXFRAME, item, curPlayer, ballNum, playerArray);
                                    isSpare = false;
                                    curPlayer++;
                                    skip = true;
                                }
                                if (isStrike2 == true)
                                {
                                    ballNum = 2;
                                    valueHandler(MAXFRAME, item, curPlayer, ballNum, playerArray);
                                    skip = true;
                                    isStrike2 = false;
                                    ballNum = 1;
                                    curPlayer++;

                                }
                                if (isStrike == true)
                                {
                                    //Console.WriteLine("STRIKE1");
                                    ballNum = 1;
                                    valueHandler(MAXFRAME, item, curPlayer, ballNum, playerArray);
                                    isStrike = false;
                                    isStrike2 = true;
                                    skip = true;
                                }
                            isNormal = false;
                            if (frame == MAXFRAME - 1 && skip == false)
                                {
                               
                                if (ballNum == 1)
                                    {
                                    //Console.WriteLine("BALLNUMFRAME10-----Ball1--------------" + item);
                                    valueHandler(frame, item, curPlayer, ballNum, playerArray);
                                    if (item != TEN)
                                        {
                                            tenOne = item;
                                            //ballNum = 2;
                                            //break;
                                        }
                                        if (item == TEN)
                                        {
                                            isStrike = true;
                                            valueHandler(frame, item, curPlayer, ballNum, playerArray);
                                        //curPlayer++;
                                        }
                                    skip2 = false;
                                    }
                                    if (ballNum == 2)
                                    {
                                    //Console.WriteLine("BALLNUMFRAME10-----Ball2--------------" + item);
                                    tenTwo = item;
                                    if (frameOk(frame, tenOne, tenTwo, errCnt) == true)
                                    {
                                        //valueHandler(frame, tenOne, curPlayer, 1, playerArray);
                                        valueHandler(frame, item, curPlayer, ballNum, playerArray);
                                        if (tenOne + tenTwo == TEN)
                                        {
                                            isSpare = true;
                                            //ballNum = 1;
                                            //break;
                                        }
                                        if (tenOne + tenTwo < TEN)
                                        {
                                            curPlayer++;
                                            isNormal = true;
                                        }
                                        skip2 = false;
                                       
                                    }
                                    else
                                    {
                                        skip2 = true;
                                    }
                                    ballNum = 1;
                                }
                                    if (isNormal == false && skip2 == false)
                                    {
                                        ballNum = 2;
                                    }
                                }
                            }
                        
                            skip = false;                           
                        }
                        
                    }
                    printOut(playerArray, numPlayers, errCnt);
                    Console.WriteLine("------------------------------------");
                    // Console.WriteLine(count);

                }
                catch (Exception e)
                {

                    Console.WriteLine("error: '{0}'", e);
                }
            
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
        static bool frameOk(int frame, int ball1, int ball2, errorCount err)
        {
            int TEN = 10;
            if (ball1 < 0)
            {
                Console.WriteLine("Frame: " + frame +  " Roll Error: " + ball1);
                err.count++;
                return false;
            }
            if(ball1 > TEN || ball2 > TEN)
            {
                Console.WriteLine("Frame: " + frame + " Roll Error: " + ball1);
                err.count++;
                return false;
            }
            if(ball1 + ball2 > TEN)
            {
                Console.WriteLine("Frame: " + frame + " Frame Error: " + ball1 + ", " + ball2);
                err.count++;
                return false;
            }
            return true;
        }
        static void valueHandler(int frame, int value, int player, int ball, Player[] array)
        {
            //Console.WriteLine("Frame: " + frame + " Player: " + player + " Ball: " + ball + " Value: " + value);
            array[player].addToArray(frame, value, ball);
        }
        static void printOut(Player[] array, int playerAmount, errorCount err)
        {
            int total = 0;
            int frameS = 0;
            for (int i = 0; i < playerAmount; i++)
            {
                total = 0;
                for (int j = 0; j < MAXFRAME; j++)
                {
                    Console.WriteLine("Player: " + i + " FRAME: " + array[i].getFrame(j));
                    frameS = array[i].getFrameScore(j);
                    Console.WriteLine("BALL ONE: " + array[i].getBallOne(j));
                    Console.WriteLine("BALL TWO: " + array[i].getBallTwo(j));
                    Console.WriteLine("Frame Score: " + frameS);
                    total += frameS;
                }
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Player: " + i + " Total Score: " + total + " Total Errors: " + err.count );

                Console.WriteLine("------------------------------------");
            }
        }
    }
    class errorCount
    {
        public int count { set; get; }
    }
    /// <summary>
    /// create a player and have an array of frames 
    /// </summary>
    class Player
    {
        public const int MAXFRAME = 11;
        public const int TEN = 10;
        public int playerNum { get; set; }
        private Frame[] array = new Frame[MAXFRAME];

        public void addToArray(int frame, int value, int ball)
        {
            if (frame <= MAXFRAME)
            {

                array[frame - 1].frameNum = frame;
                if (ball == 1)
                {
                    //  Console.WriteLine("ADDING BALL 1" + " " + frame);
                    array[frame - 1].ballOne = value;
                }
                else if (ball == 2)
                {
                    // Console.WriteLine("ADDING BALL 2" + " " + frame);
                    array[frame - 1].ballTwo = value;
                }
            }


        }
        public int getBallOne(int value)
        {
            return array[value].ballOne;
        }

        public int getBallTwo(int value)
        {
            return array[value].ballTwo;
        }

        public int getFrame(int value)
        {
            return array[value].frameNum;
        }

        public int getFrameScore(int value)
        {
            int score = 0;
            bool strike = false;
           
            //strike
            if (array[value].ballOne == TEN && value <= MAXFRAME - 2)
            {
                array[value].calcScore();
                score += array[value].frameScore;
                strike = true;
                // Console.WriteLine("-----------STRIKE ON FRAME:" + value);
                if (array[value + 1].ballOne == TEN && value + 1 < MAXFRAME - 1)//check nextframe is strike(less than limit)
                {
                    //array[value+1].calcScore();
                    score += array[value + 1].ballOne;
                    //Console.WriteLine("-----------STRIKE ON FRAME:" + (value + 1));
                    if (array[value + 2].ballOne == TEN && value + 2 < MAXFRAME - 1)//check nextnextframe is strike (less than limit)
                    {
                        //Console.WriteLine("-----------STRIKE ON FRAME:" + (value + 2));

                        score += array[value + 2].ballOne;
                    }
                    if (array[value + 2].ballOne == TEN && value + 2 == MAXFRAME - 1)//check nextnextframe is strike (is the limit)
                    {
                        score += array[value + 1].ballOne;
                        score += array[value + 1].ballTwo;
                    }
                    if (array[value + 2].ballOne < TEN && value + 2 <= MAXFRAME - 1)//if next frame not strike add balls two from next frame
                    {
                        score += array[value + 2].ballOne;
                    }
                }
                if (array[value + 1].ballOne == TEN && value + 1 == MAXFRAME - 1)//check nextframe is strike(is the limit)
                {
                    score += array[value + 1].ballOne;
                    score += array[value + 1].ballTwo;
                }
                if (array[value + 1].ballOne < TEN && value + 1 <= MAXFRAME - 1)//if next frame not strike add balls two from next frame
                {
                    score += array[value + 1].ballOne;
                    score += array[value + 1].ballTwo;
                }
            }
            if (array[value].ballOne + array[value].ballTwo == TEN && strike == false)
            {
                //if spare
                array[value].calcScore();
                score += array[value].frameScore;
                if (value + 1 <= MAXFRAME - 1)
                {
                    score += array[value + 1].ballOne;
                }

            }
            if (array[value].ballOne + array[value].ballTwo < TEN && value + 2 < MAXFRAME)
            {
                //if normal
                //array[value].calcScore();
                //Console.WriteLine("NORMAL CALLED");
                score += array[value].ballOne;
                score += array[value].ballTwo;
                return score;

            }
            else if(array[value].ballOne + array[value].ballTwo < TEN && value == 9)
            {
                score += array[value].ballOne;
                score += array[value].ballTwo;
            }
            strike = false;
            return score;
        }


        public int getTotalScore()
        {
            int totalScore = 0;

            return totalScore;
        }
        public Player()
        {
            //Console.WriteLine("Player Created");
            for (int i = 0; i < MAXFRAME; i++)
            {
                array[i] = new Frame();
            }
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
        //getters and setters
        public int frameNum { get; set; }
        public int ballOne { get; set; }
        public int ballTwo { get; set; }
        public int frameScore { get; set; }
        public bool filled { get; set; }
        //constructor
        public Frame()
        {
            //Console.WriteLine("FRAME CREATED");
        }

        //calculate scores
        public void calcScore()
        {

            frameScore = ballOne + ballTwo;
        }

    }
}
