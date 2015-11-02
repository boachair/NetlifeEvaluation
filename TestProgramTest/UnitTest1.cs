using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProgramTest
{
    [TestClass]
    public class CostUnitTest
    {
        [TestMethod]
        //Function to test: private int Cost(string str1, string str2). This function is to calculate the absolute value of the integer difference between the corresponding first character codes (case insensitive) of two strings.
        //Input: normal string, exclude Null or Empty string
        public void NormalStringToValidateCost()    
        {
            TestProgram _testProgram = new TestProgram();
            string str1 = "fu";
            string str2 = "chen";
            int expected = 0;
            if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
            {
                byte[] asciiBytesStr1 = Encoding.ASCII.GetBytes(str1.ToUpper());
                byte[] asciiBytesStr2 = Encoding.ASCII.GetBytes(str2.ToUpper());
                expected = Math.Abs((int)(asciiBytesStr1[0]) - (int)(asciiBytesStr2[0]));
            }
            var actual = _testProgram.Cost(str1, str2);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test: private int Cost(string str1, string str2). This function is to calculate the absolute value of the integer difference between the corresponding first character codes (case insensitive) of two strings.
        //Input: abnormal input, empty string
        public void EmptyStringToValidateCost()
        {
            TestProgram _testProgram = new TestProgram();
            string str1 = "";
            string str2 = "";
            int expected = 0;
            var actual = _testProgram.Cost(str1, str2);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test: private void GridCost(int sx, int sy). This function is to calculate the cost of one grid cell located at (sx,sy). 
        //Input: input string is "Present a big text input area in which a reasonably large text can be written or pasted."
        //The grid is default setting, 5x5 layout. 
        /*View example:
         *  Present | a     | big   | text | input 
         *  area    | in    | which | a    | reasonably
         *  large   | text  | can   | be   | written
         *  or      | pasted|
         */

        //This test is an edge test. The target grid cell under test is the left upper corner of the grid. 
        public void LeftUpperToValidateGridCost()
        {
            TestProgram _testProgram = new TestProgram();
            int sx = 0;
            int sy = 0;
            int[] sCost = new int[8];
            const int MAXINT = 3000;
            string str1 = "Present";
            string str2 = "a";
            string str3 = "area";
            string str4 = "in";
            for (int i = 0; i < 8; i++)
            {
                sCost[i] = MAXINT;
            }
            _testProgram.pWord[sx, sy] = str1;
            _testProgram.pWord[sx+1, sy] = str2;
            _testProgram.pWord[sx, sy+1] = str3;
            _testProgram.pWord[sx+1, sy+1] = str4;
            if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2)
                && !string.IsNullOrEmpty(str3) && !string.IsNullOrEmpty(str4))
            {
                sCost[0] = _testProgram.Cost(str1.ToUpper(), str2.ToUpper());
                sCost[2] = _testProgram.Cost(str1.ToUpper(), str3.ToUpper());
                sCost[4] = _testProgram.Cost(str1.ToUpper(), str4.ToUpper());
            }

            int expected = 0;
            int actual = 0;
            _testProgram.GridCost(sx, sy);
            if ((sCost[0] == _testProgram.G[0,1]) 
                && (sCost[2] == _testProgram.G[0,_testProgram.nWidth])
                && (sCost[4] == _testProgram.G[0,1+_testProgram.nWidth]))
            {
                actual = 0;
            }
            else
                actual = 1;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test: private void GridCost(int sx, int sy). This function is to calculate the cost of one grid cell located at (sx,sy). 
        //Input: input string is "Present a big text input area in which a reasonably large text can be written or pasted."
        //The grid is default setting, 5x5 layout. 
        //This test is an edge test. The target grid cell under test is the right upper corner of the grid. 
        public void RightUpperToValidateGridCost()
        {
            TestProgram _testProgram = new TestProgram();
            
            int sx = 4;
            int sy = 0;
            int[] sCost = new int[8];
            const int MAXINT = 3000;
            string str1 = "input";
            string str2 = "text";
            string str3 = "reasonably";
            string str4 = "a";
            for (int i = 0; i < 8; i++)
            {
                sCost[i] = MAXINT;
            }
            _testProgram.pWord[sx, sy] = str1;
            _testProgram.pWord[sx - 1, sy] = str2;
            _testProgram.pWord[sx, sy + 1] = str3;
            _testProgram.pWord[sx - 1, sy + 1] = str4;
            if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2)
                && !string.IsNullOrEmpty(str3) && !string.IsNullOrEmpty(str4))
            {
                sCost[1] = _testProgram.Cost(str1.ToUpper(), str2.ToUpper());
                sCost[2] = _testProgram.Cost(str1.ToUpper(), str3.ToUpper());
                sCost[5] = _testProgram.Cost(str1.ToUpper(), str4.ToUpper());
            }
            int expected = 0;
            int actual = 0;
            _testProgram.GridCost(sx, sy);
            if ((sCost[1] == _testProgram.G[sx + sy * _testProgram.nWidth, _testProgram.nWidth - 2])
                && (sCost[2] == _testProgram.G[sx + sy * _testProgram.nWidth, 2 * _testProgram.nWidth - 1])
                && (sCost[5] == _testProgram.G[sx + sy * _testProgram.nWidth, 2 * _testProgram.nWidth - 2]))
            {
                actual = 0;
            }
            else
                actual = 1;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test: private void GridCost(int sx, int sy). This function is to calculate the cost of one grid cell located at (sx,sy). 
        //Input: input string is "Present a big text input area in which a reasonably large text can be written or pasted."
        //The grid is default setting, 5x5 layout. 
        //This test is an edge test. The target grid cell under test is the left lower corner of the grid. 
        public void LeftLowerToValidateGridCost()
        {
            TestProgram _testProgram = new TestProgram();
            //	Present a big text input area in which a reasonably large text can be written or pasted.
            int sx = 0;
            int sy = 3;
            int[] sCost = new int[8];
            const int MAXINT = 3000;
            string str1 = "or";
            string str2 = "pasted";
            string str3 = "large";
            string str4 = "text";
            for (int i = 0; i < 8; i++)
            {
                sCost[i] = MAXINT;
            }
            _testProgram.pWord[sx, sy] = str1;
            _testProgram.pWord[sx + 1, sy] = str2;
            _testProgram.pWord[sx, sy - 1] = str3;
            _testProgram.pWord[sx + 1, sy - 1] = str4;
            if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2)
                && !string.IsNullOrEmpty(str3) && !string.IsNullOrEmpty(str4))
            {
                sCost[0] = _testProgram.Cost(str1.ToUpper(), str2.ToUpper());
                sCost[3] = _testProgram.Cost(str1.ToUpper(), str3.ToUpper());
                sCost[6] = _testProgram.Cost(str1.ToUpper(), str4.ToUpper());
            }

            int expected = 0;
            int actual = 0;
            _testProgram.GridCost(sx, sy);
            if ((sCost[0] == _testProgram.G[sx + sy * _testProgram.nWidth, sx+1+ sy * _testProgram.nWidth])
                && (sCost[3] == _testProgram.G[sx + sy * _testProgram.nWidth, sx + (sy-1)* _testProgram.nWidth])
                && (sCost[6] == _testProgram.G[sx + sy * _testProgram.nWidth, sx+1 + (sy - 1) * _testProgram.nWidth]))
            {
                actual = 0;
            }
            else
                actual = 1;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test: private void GridCost(int sx, int sy). This function is to calculate the cost of one grid cell located at (sx,sy). 
        //Input: input string is "Present a big text input area in which a reasonably large text can be written or pasted."
        //The grid is default setting, 5x5 layout. 
        /*View example:
         *  Present | a     | big   | text | input 
         *  area    | in    | which | a    | reasonably
         *  large   | text  | can   | be   | written
         *  or      | pasted|
         */
        //This test is an edge test. The target grid cell under test is the right lower corner of the grid. 
        public void RightLowerToValidateGridCost()
        {
            TestProgram _testProgram = new TestProgram();
            //	Present a big text input area in which a reasonably large text can be written or pasted.
            int sx = 1;
            int sy = 3;
            int[] sCost = new int[8];
            const int MAXINT = 3000;
            string str1 = "pasted";
            string str2 = "or";
            string str3 = "text";
            string str4 = "large";
            for (int i = 0; i < 8; i++)
            {
                sCost[i] = MAXINT;
            }
            _testProgram.pWord[sx, sy] = str1;
            _testProgram.pWord[sx - 1, sy] = str2;
            _testProgram.pWord[sx, sy - 1] = str3;
            _testProgram.pWord[sx - 1, sy - 1] = str4;
            if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2)
                && !string.IsNullOrEmpty(str3) && !string.IsNullOrEmpty(str4))
            {
                sCost[1] = _testProgram.Cost(str1.ToUpper(), str2.ToUpper());
                sCost[3] = _testProgram.Cost(str1.ToUpper(), str3.ToUpper());
                sCost[7] = _testProgram.Cost(str1.ToUpper(), str4.ToUpper());
            }

            int expected = 0;
            int actual = 0;
            _testProgram.GridCost(sx, sy);
            if ((sCost[1] == _testProgram.G[sx + sy * _testProgram.nWidth, sx - 1 + sy * _testProgram.nWidth])
                && (sCost[3] == _testProgram.G[sx + sy * _testProgram.nWidth, sx + (sy - 1) * _testProgram.nWidth])
                && (sCost[7] == _testProgram.G[sx + sy * _testProgram.nWidth, sx - 1 + (sy - 1) * _testProgram.nWidth]))
            {
                actual = 0;
            }
            else
                actual = 1;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test: private void GridCost(int sx, int sy). This function is to calculate the cost of one grid cell located at (sx,sy). 
        //Input: input string is "Present a big text input area in which a reasonably large text can be written or pasted."
        //The grid is default setting, 5x5 layout. 
        //This test is a normal test. The target grid cell under test is the middle cell of the grid. 
        public void MiddleToValidateGridCost()
        {
            TestProgram _testProgram = new TestProgram();
            //	Present a big text input area in which a reasonably large text can be written or pasted.
            int sx = 2;
            int sy = 1;
            int[] sCost = new int[8];
            const int MAXINT = 3000;
            
            string str0 = "which";
            string str1 = "a";
            string str2 = "in";
            string str3 = "can";
            string str4 = "big";
            string str5 = "be";
            string str6 = "text";
            string str7 = "text";
            string str8 = "a";
            for (int i = 0; i < 8; i++)
            {
                sCost[i] = MAXINT;
            }
            _testProgram.pWord[sx, sy] = str0;
            _testProgram.pWord[sx + 1, sy] = str1;
            _testProgram.pWord[sx-1, sy] = str2;
            _testProgram.pWord[sx, sy + 1] = str3;
            _testProgram.pWord[sx, sy - 1] = str4;
            _testProgram.pWord[sx + 1, sy + 1] = str5;
            _testProgram.pWord[sx - 1, sy + 1] = str6;
            _testProgram.pWord[sx + 1, sy - 1] = str7;
            _testProgram.pWord[sx - 1, sy - 1] = str8;

            if (!string.IsNullOrEmpty(str0) && !string.IsNullOrEmpty(str1)
                && !string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str3)
                && !string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str5)
                && !string.IsNullOrEmpty(str6) && !string.IsNullOrEmpty(str7)
                && !string.IsNullOrEmpty(str8))
            {  
                sCost[0] = _testProgram.Cost(str0.ToUpper(), str1.ToUpper());
                sCost[1] = _testProgram.Cost(str0.ToUpper(), str2.ToUpper());
                sCost[2] = _testProgram.Cost(str0.ToUpper(), str3.ToUpper());
                sCost[3] = _testProgram.Cost(str0.ToUpper(), str4.ToUpper());
                sCost[4] = _testProgram.Cost(str0.ToUpper(), str5.ToUpper());
                sCost[5] = _testProgram.Cost(str0.ToUpper(), str6.ToUpper());
                sCost[6] = _testProgram.Cost(str0.ToUpper(), str7.ToUpper());
                sCost[7] = _testProgram.Cost(str0.ToUpper(), str8.ToUpper());
            }

            int expected = 0;
            int actual = 0;
            _testProgram.GridCost(sx, sy);
            int[] px = new int[] { 1, -1, 0, 0, 1, -1, 1, -1 };   // x axis offset
            int[] py = new int[] { 0, 0, 1, -1, 1, 1, -1, -1 };   // y axis offset
            if ((sCost[0] == _testProgram.G[sx + sy * _testProgram.nWidth, sx + 1 + sy * _testProgram.nWidth])
                && (sCost[1] == _testProgram.G[sx + sy * _testProgram.nWidth, sx-1 + sy * _testProgram.nWidth])
                && (sCost[2] == _testProgram.G[sx + sy * _testProgram.nWidth, sx + (sy + 1) * _testProgram.nWidth])
                && (sCost[3] == _testProgram.G[sx + sy * _testProgram.nWidth, sx + (sy - 1) * _testProgram.nWidth])
                && (sCost[4] == _testProgram.G[sx + sy * _testProgram.nWidth, sx+1 + (sy + 1) * _testProgram.nWidth])
                && (sCost[5] == _testProgram.G[sx + sy * _testProgram.nWidth, sx-1 + (sy + 1) * _testProgram.nWidth])
                && (sCost[6] == _testProgram.G[sx + sy * _testProgram.nWidth, sx + 1 + (sy - 1) * _testProgram.nWidth])
                && (sCost[7] == _testProgram.G[sx + sy * _testProgram.nWidth, sx-1 + (sy - 1) * _testProgram.nWidth]))
            {
                actual = 0;
            }
            else
                actual = 1;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test:  private int GetShortestPath(int[,] G, int start, int end, int[] path). This function is to calculate the shortest path from start to end.
        //Input: input string is "Present"
        //The grid is default setting, 5x5 layout. 
        //This test is an edge test for one single input word.
        public void OneWordValidategetShortestPath()
        {
            TestProgram _testProgram = new TestProgram();
            string expected = "";
            string actual = "";
            int num_words = 1;
            _testProgram.InitG();
            int[] path1 = new int[num_words];
            int dist1 = _testProgram.getShortestPath(_testProgram.G, 0, num_words - 1, path1);
            expected = "0";
            for (int i = 0; i < path1.Length; i++)
                actual += path1[i].ToString();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test:  private int GetShortestPath(int[,] G, int start, int end, int[] path). This function is to calculate the shortest path from start to end.
        //Input: input string is "Present a big text"
        //The grid is default setting, 5x5 layout. 
        //This test is a normal test for one single input line.
        public void OneLineValidategetShortestPath()
        {
            TestProgram _testProgram = new TestProgram();
            string expected = "";
            string actual = "";
            int num_words = 4;
            _testProgram.InitG();
            _testProgram.G[0, 1] = 15;
            _testProgram.G[1, 0] = 15;
            _testProgram.G[1, 2] = 1;
            _testProgram.G[2, 1] = 1;
            _testProgram.G[2, 3] = 18;
            _testProgram.G[3, 2] = 18;
            int[] path1 = new int[num_words];
            int dist1 = _testProgram.getShortestPath(_testProgram.G, 0, num_words-1, path1);
            expected = "0123";
            for (int i = 0; i < path1.Length; i++)
                actual+=path1[i].ToString();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //Function to test:  private int GetShortestPath(int[,] G, int start, int end, int[] path). This function is to calculate the shortest path from start to end. 
        //Input: input string is "Present a big text input area in which a reasonably large text can be written or pasted."
        /*View example:
        *  Present | a     | big   | text | input 
        *  area    | in    | which | a    |
        */
        //The grid is default setting, 5x5 layout. 
        //This test is a normal test for multiple input lines.
        public void MultipleLineValidategetShortestPath()
        {
            TestProgram _testProgram = new TestProgram();
            string expected = "";
            string actual = "";
            int num_words = 9;
            _testProgram.InitG();
            _testProgram.G[0, 1] = 15;
            _testProgram.G[0, 5] = 15;
            _testProgram.G[0, 6] = 7;
            _testProgram.G[1, 0] = 15;
            _testProgram.G[1, 2] = 1;
            _testProgram.G[1, 5] = 0;
            _testProgram.G[1, 6] = 8;
            _testProgram.G[1, 7] = 22;
            _testProgram.G[2, 1] = 1;
            _testProgram.G[2, 3] = 18;
            _testProgram.G[2, 6] = 7;
            _testProgram.G[2, 7] = 21;
            _testProgram.G[2, 8] = 1;
            _testProgram.G[3, 2] = 18;
            _testProgram.G[3, 4] = 11;
            _testProgram.G[3, 7] = 3;
            _testProgram.G[3, 8] = 19;
            _testProgram.G[4, 3] = 11;
            _testProgram.G[4, 8] = 8;
            _testProgram.G[5, 0] = 15;
            _testProgram.G[5, 1] = 0;
            _testProgram.G[5, 6] = 8;
            _testProgram.G[6, 0] = 7;
            _testProgram.G[6, 1] = 8;
            _testProgram.G[6, 2] = 7;
            _testProgram.G[6, 5] = 8;
            _testProgram.G[6, 7] = 14;
            _testProgram.G[7, 1] = 22;
            _testProgram.G[7, 2] = 21;
            _testProgram.G[7, 3] = 3;
            _testProgram.G[7, 6] = 14;
            _testProgram.G[7, 8] = 22;
            _testProgram.G[8, 2] = 1;
            _testProgram.G[8, 3] = 19;
            _testProgram.G[8, 4] = 8;
            _testProgram.G[8, 7] = 22;
            int[] path1 = new int[num_words];
            int dist1 = _testProgram.getShortestPath(_testProgram.G, 0, num_words-1, path1);
            expected = "062800000";
            for (int i = 0; i < path1.Length; i++)
                actual += path1[i].ToString();

            Assert.AreEqual(expected, actual);
        }
    }

    public class TestProgram
    {
        public int nWidth = 5;
        public int nHeight = 5;
        const int MAXINT = 30000;
        const int MAX_WIDTH = 10;
        const int MAX_HEIGHT = 10;

        /* information on each cell */
        public string[,] pWord = new string[MAX_WIDTH, MAX_HEIGHT];    //word on the cell(x,y)
        public int[,] pState = new int[MAX_WIDTH, MAX_HEIGHT]; //0,not visited; 1,visited; 2, invalid
        int[] px = new int[] { 1, -1, 0, 0, 1, -1, 1, -1 };   // x axis offset
        int[] py = new int[] { 0, 0, 1, -1, 1, 1, -1, -1 };   // y axis offset

        int MaxSize = 10000;
        public int[,] G = new int[MAX_WIDTH * MAX_HEIGHT, MAX_WIDTH * MAX_HEIGHT];

        //int[] path1 = new int[MAX_WIDTH * MAX_HEIGHT];
        public void InitG()
        {
            for (int j = 0; j < MAX_WIDTH * MAX_HEIGHT; j++)
            {
                for (int i = 0; i < MAX_WIDTH * MAX_HEIGHT; i++)
                {
                    G[i, j] = MAXINT;
                }
            }
        }

        public int Cost(string str1, string str2)
        {
            if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
            {
                byte[] asciiBytes1 = Encoding.ASCII.GetBytes(str1);
                byte[] asciiBytes2 = Encoding.ASCII.GetBytes(str2);
                return Math.Abs((int)(asciiBytes1[0]) - (int)(asciiBytes2[0]));
            }
            return 0;
        }
        public int GridCost(int sx, int sy)
        {
            pState[sx, sy] = 1; // visit the cell
            for (int i = 0; i < 8; i++)
            {
                // neighbor location
                int x = sx + px[i];
                int y = sy + py[i];
                if (x >= 0 && x < nWidth && y >= 0 && y < nHeight && (pState[x, y] == 0)) // cell locates in the region and is not visited
                {
                    if ((!string.IsNullOrEmpty(pWord[sx, sy])) && (!string.IsNullOrEmpty(pWord[x, y])))
                    {
                        G[sx + sy * nWidth, x + y * nWidth] = Cost(pWord[sx, sy].ToUpper(), pWord[x, y].ToUpper());
                    }               
                }
            }
            return 0;
        }
        public int getShortestPath(int[,] G, int start, int end, int[] path)
        {
            bool[] s = new bool[MAX_WIDTH * MAX_HEIGHT];
            int min;
            int curNode = 0;
            int[] dist = new int[MAX_WIDTH * MAX_HEIGHT];
            int[] prev = new int[MAX_WIDTH * MAX_HEIGHT];

            for (int v = 0; v < MAX_WIDTH * MAX_HEIGHT; v++)
            {
                s[v] = false;
                dist[v] = G[start, v];
                if (dist[v] > MaxSize)
                    prev[v] = 0;
                else
                    prev[v] = start;
            }
            path[0] = end;
            dist[start] = 0;
            s[start] = true;
            for (int i = 1; i < MAX_WIDTH * MAX_HEIGHT; i++)
            {
                min = MaxSize;
                for (int w = 0; w < MAX_WIDTH * MAX_HEIGHT; w++)
                {
                    if (!s[w] && dist[w] < min)
                    {
                        curNode = w;
                        min = dist[w];
                    }
                }

                s[curNode] = true;

                for (int j = 0; j < MAX_WIDTH * MAX_HEIGHT; j++)
                    if (!s[j] && min + G[curNode, j] < dist[j])
                    {
                        dist[j] = min + G[curNode, j];
                        prev[j] = curNode;
                    }

            }
            int e = end, step = 0;
            while (e != start)
            {
                step++;
                path[step] = prev[e];
                e = prev[e];
            }
            for (int i = step; i > step / 2; i--)
            {
                int temp = path[step - i];
                path[step - i] = path[i];
                path[i] = temp;
            }
            return dist[end];
        }
    }
}
