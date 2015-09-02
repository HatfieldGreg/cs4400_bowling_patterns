using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bowling4
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


            //read in file FROM INSTRUCTIONS data will be either on ONE line or split on lines so split on space and new line
            Console.WriteLine("Enter bowling text file please:");
            fileName = Console.ReadLine();
            if (File.Exists(fileName))
            {
                try
                {

                    List<int> list = new List<int>();//create list
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string line;//each line in file in case it's split into different lines
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] numbers = line.Split(' ', '\n', '\r');//use an array to store each line
                            //Console.WriteLine(line); // Write to console.
                            foreach (string num in numbers)// go through each number and parse it
                            {
                                //if valid, add to numList, not valid ignore
                                bool check = int.TryParse(num, out curBall);
                                if (check == true)
                                {
                                    list.Add(curBall);
                                }

                            }
                        }
                    }

                    //handle the values
                    Console.WriteLine("------------------------------------");
                   // Console.WriteLine(list[0] + " is the Number of players");

                    //get number of players for game
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

                    if (numPlayers > 0)
                    {
                        curPlayer = 0;
                        int ballNum = 1;
                        bool ballFilled = false;
                        bool strike = false;
                        int frame = 1;
                        int tenOne = 0;
                        int tenTwo = 0;
                        bool isStrike = false;
                        bool isStrike2 = false;
                        bool isSpare = false;
                        bool isNormal = false;
                        bool skip = false;
                        //valueHandler(frame, item, curPlayer, ballNum, playerArray);

                        foreach (int item in list)
                        {
                            //Console.WriteLine(item + " CURRENT ITEM");
                            if (curPlayer > numPlayers - 1)
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
                                        valueHandler(frame, item, curPlayer, ballNum, playerArray);
                                        //ballFilled = true;
                                        if (item != TEN)
                                        {
                                            ballNum = 2;
                                        }
                                        if (item == TEN)
                                        {
                                            curPlayer++;
                                        }


                                        //Console.WriteLine("ball 1 " + item);

                                        break;
                                    case 2:
                                        // if (ballFilled == false && strike == false)
                                        // {
                                        valueHandler(frame, item, curPlayer, ballNum, playerArray);
                                        curPlayer++;
                                        ballNum = 1;
                                        //Console.WriteLine("ball 2 " + item);
                                        // }

                                        break;
                                }
                            }
                            //jumbled code for handling 10th frame and extra frame
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
                            if (frame == TEN && skip == false)
                            {
                                isNormal = false;
                                if (ballNum == 1)
                                {
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
                                        //curPlayer++;
                                    }

                                }
                                if (ballNum == 2)
                                {
                                    tenTwo = item;
                                    valueHandler(frame, item, curPlayer, ballNum, playerArray);
                                    if (tenOne + tenTwo == TEN)
                                    {
                                        isSpare = true;
                                        //ballNum = 1;
                                        //break;
                                    }
                                    if (tenOne + tenTwo != TEN)
                                    {
                                        curPlayer++;
                                        isNormal = true;
                                    }

                                    ballNum = 1;
                                }
                                if (isNormal == false)
                                {
                                    ballNum = 2;
                                }
                            }
                            skip = false;
                        }
                    }
                    printOut(playerArray, numPlayers);

                    roller(playerArray);
                    Console.WriteLine("------------------------------------");
                    // Console.WriteLine(count);

                }
                catch (Exception e)
                {

                    Console.WriteLine("File could not be opened, following error: '{0}'", e);
                }
            }
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

        static void valueHandler(int frame, int value, int player, int ball, Player[] array)
        {
            //Console.WriteLine("Frame: " + frame + " Player: " + player + " Ball: " + ball + " Value: " + value);
            array[player].addToArray(frame, value, ball);
        }
        static void printOut(Player[] array, int playerAmount)
        {
            int total = 0;
            int frameS = 0;
            for (int i = 0; i < playerAmount; i++)
            {
                total = 0;
                for (int j = 0; j < MAXFRAME; j++)
                {
                   // Console.WriteLine("Player: " + i + " FRAME: " + array[i].getFrame(j));
                    frameS = array[i].getFrameScore(j);
                    //Console.WriteLine("BALL ONE: " + array[i].getBallOne(j));
                   // Console.WriteLine("BALL TWO: " + array[i].getBallTwo(j));
                   // Console.WriteLine("Frame Score: " + frameS);
                    total += frameS;
                }
               // Console.WriteLine("------------------------------------");
               // Console.WriteLine("Player: " + i + " Total Score: " + total);
               // Console.WriteLine("------------------------------------");
            }
        }
        /// <summary>
        /// randomly roll till the program is done
        /// </summary>
        /// <param name="array"></param>
        static void roller(Player[] array)
        {
            Random randNum = new Random();
            blackBoard bb = new blackBoard();
            bb.playerArray(array);
            int value = 0;
            int totalScore = 0;
            int realTotal = 0;
            while(bb.doneYet == false)
            {
                value = randNum.Next(0, 11);
                if (bb.getFilled(value) == false)
                {
                    Console.WriteLine("Adding Frame # " + value + " into array with values " + array[0].getBallOne(value) + " " + array[0].getBallTwo(value));
                   
                    totalScore += array[0].getBallOne(value) + array[0].getBallTwo(value);
                    realTotal += array[0].getTotalScore();
                    bb.setFilled(value);
                    Console.WriteLine("Frames Total Score: " + totalScore);
                    
                }
                
                if (bb.allFilled() == true)
                {
                    bb.doneYet = true;

                }
                if (bb.doneYet == false)
                {
                    Console.WriteLine("It is not done yet");
                }
            }
            Console.WriteLine("It is Done");
            Console.WriteLine("Game Total Score: " + array[0].getTotalScore());
        }
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
        private int totalScore;
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
            if (array[value].ballOne + array[value].ballTwo < TEN && value + 2 < MAXFRAME - 1)
            {
                //if normal
                array[value].calcScore();
                score += array[value].frameScore;

            }
            strike = false;
            totalScore += score;
            return score;
        }


        public int getTotalScore()
        {           

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

        public void bbSet(int value)
        {
            array[value].bbCheck = true;
        }
        public bool bbGet(int value)
        {
            return array[value].bbCheck;
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

        public bool bbCheck { get; set; }
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

    class blackBoard
    {
        public const int MAXFRAME = 11;
        private Player[] array = new Player[1];
        public bool doneYet { set; get; }
        public int totalScore { set; get; }
        public void playerArray(Player[] value )
        {
            array = value;
        }
        
        public string isItDone()
        {
            string notDone = "It is not Done";
            string Done = "It is Done!";
            if (doneYet == false)
            {
                return notDone;
            }
            else
            {
                return Done;
            }            

        }
        public void setFilled (int value)
        {
            array[0].bbSet(value);
        }

        public bool allFilled()
        {
            for(int i = 0; i < MAXFRAME; i++)
            {
                
                if(array[0].bbGet(i) == false)
                {
                    //Console.WriteLine("FALSE");
                    return false;
                }
            }
            //Console.WriteLine("TRUE");
            return true;
        }
        public bool getFilled(int value)
        {
            return array[0].bbGet(value);
        }
    }
    
}

