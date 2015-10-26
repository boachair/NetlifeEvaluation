using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TestProgram
{
    
    public partial class Form_Main : Form
    {
        int nWidth=5;
        int nHeight=5;
        const int MAXINT = 30000;
        const int MAX_WIDTH = 10;
        const int MAX_HEIGHT = 10;
        int cellwidth = 66;
        int cellheight = 32;
        int nOffsetX = 250;
        int nOffsetY = 10;
        /* information on each cell */
        string[,] pWord = new string[MAX_WIDTH, MAX_HEIGHT];    //word on the cell(x,y)
        int[,] pState = new int[MAX_WIDTH, MAX_HEIGHT]; //0,not visited; 1,visited; 2, invalid

        int[] px = new int[] { 1, -1, 0, 0, 1, -1, 1, -1 };   // x axis offset
        int[] py = new int[] { 0, 0, 1, -1, 1, 1, -1, -1 };   // y axis offset

        int MaxSize = 10000;
        int[,] G = new int[MAX_WIDTH * MAX_HEIGHT, MAX_WIDTH * MAX_HEIGHT];

        public Form_Main()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            for (int j = 0; j < MAX_HEIGHT; j++)
            {
                for (int i = 0; i < MAX_WIDTH; i++)
                {
                    pWord[i, j] = " ";
                    pState[i, j] = 0;
                }
            }
            for (int j = 0; j < MAX_WIDTH * MAX_HEIGHT; j++)
            {
                for (int i = 0; i < MAX_WIDTH * MAX_HEIGHT; i++)
                {
                    G[i, j] = MAXINT;
                }
            }
        }


        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            PaintGrid(e.Graphics);
        }
        private void PaintGrid(Graphics g)
        {
            // Clear the graphs and variables before paint
            g.Clear(Color.White);
            for (int j = 0; j < MAX_WIDTH * MAX_HEIGHT; j++)
            {
                for (int i = 0; i < MAX_WIDTH * MAX_HEIGHT; i++)
                {
                    G[i, j] = MAXINT;
                }
            }
            for (int j = 0; j < MAX_HEIGHT; j++)
            {
                for (int i = 0; i < MAX_WIDTH; i++)
                {
                    pWord[i, j] = " ";
                    pState[i, j] = 0;
                }
            }

            int num_words = 0;
            string IdOrder;
            string[] array = new string[MAX_WIDTH * MAX_HEIGHT];
            int index = 0;
            int gridx = 0;
            int gridy = 0;
            int lastx = 0;
            int lasty = 0;
            int tmpx = 0;
            int tmpy = 0;
            // Read the input from textBox
            if (!string.IsNullOrEmpty(Convert.ToString(textBox1.Text)) )
            {
                IdOrder = Convert.ToString(textBox1.Text);
                //replacing "enter" i.e. "\n" by ","
                string temp = IdOrder.Replace("\r\n", "@");
                string[] ArrIdOrders = Regex.Split(temp, "@");

                for (int l = 0; l < ArrIdOrders.Length; l++)
                {
                    
                    //read one row one time and save each word to one array
                    string[] tmp_array = ArrIdOrders[l].Split(' ');
                    num_words = tmp_array.Length;

                    try
                    {
                        for (int i = 0; i < num_words; i++)
                        {
                            array[i] = tmp_array[i];
                        }

                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        MessageBox.Show("IndexOutOfRangeException source: {0}", ex.Source);
                    }
                    finally
                    {
                    }
                    if (nWidth != 0)
                    {
                        if (num_words < 1)
                        {
                            lastx = 0;
                            lasty = 0;
                        }
                        else
                        {
                            lastx = (num_words - 1) % nWidth;
                            lasty = (num_words - 1) / nWidth;
                        }
                    }
                    else
                    {
                        lastx = 0;
                        lasty = 0;
                    }
                    // Draw the grid, draw each word to one cell
                    for (int j = 0; j < nHeight; j++)
                    {
                        for (int i = 0; i < nWidth; i++)
                        {
                            gridx = nOffsetX + (cellwidth+2) * i + 1;
                            gridy = nOffsetY + (cellheight+2) * j + 1;
                            index = j * nWidth + i;
                            pState[i, j] = 0;
                            pWord[i, j] = array[index];

                            g.FillRectangle(Brushes.YellowGreen, new Rectangle(gridx, gridy, cellwidth, cellheight));
                            g.DrawString(array[index], new Font("Consolas", 8), new SolidBrush(Color.Blue), gridx, gridy);
                        }
                    }
                    //Obtain the cost for each cell
                    for (int j = 0; j < nHeight; j++)
                    {
                        for (int i = 0; i < nWidth; i++)
                        {
                            GridCost(i, j);  //Calculate the cost between two points
                        }
                    }
                    //Highlight the optimal path
                    int[] path1 = new int[num_words];
                    if (num_words <= 0)
                    {
                    }
                    else
                    {
                        
                        int dist1 = GetShortestPath(G, 0, lastx + nWidth * lasty, path1);
                    }
                    for (int i = 0; i < path1.Length; i++)
                    {
                        if (nWidth != 0)
                        {
                            tmpx = path1[i] % nWidth;
                            tmpy = path1[i] / nWidth;
                            g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Cyan)), new Rectangle(nOffsetX + (cellwidth + 2) * (tmpx) + 1, nOffsetY + (cellheight+2) * (tmpy) + 1, cellwidth, cellheight));
                        }
                        else
                        {
                            tmpx = 0;
                            tmpy = 0;
                        }
                    }
                    Array.Clear(array, 0, array.Length);
                    Array.Clear(path1,0,path1.Length);
                    
                }//end of "for (int l = 0; l < ArrIdOrders.Length; l++)"
            }//end of "if (!string.IsNullOrEmpty(Convert.ToString(textBox1.Text)))"
        }
        private void GridCost(int sx, int sy)
        {
            pState[sx, sy] = 1; // visit the cell
            for (int i = 0; i < 8; i++)
            {
                // neighbor location
                int x = sx + px[i];
                int y = sy + py[i];
                if (x >= 0 && x < nWidth && y >= 0 && y < nHeight && (pState[x, y] == 0)) // cell locates in the region and is not visited
                {
                    if ((!string.IsNullOrEmpty(pWord[sx, sy]))&&(!string.IsNullOrEmpty(pWord[x, y])))
                    {
                        G[sx + sy * nWidth, x + y* nWidth] = Cost(pWord[sx,sy].ToUpper(),pWord[x,y].ToUpper());  
                    }
                }
            }
        }
        /* Calculate the cost moving from one point to the other points*/
        private int Cost(string str1, string str2)
        {
            byte[] asciiBytes1 = Encoding.ASCII.GetBytes(str1);
            byte[] asciiBytes2 = Encoding.ASCII.GetBytes(str2);
            return Math.Abs((int)(asciiBytes1[0]) - (int)(asciiBytes2[0]));
        }
       
        private int GetShortestPath(int[,] G, int start, int end, int[] path)
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            nWidth = (trackBar1.Value);
            this.Refresh();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            nHeight = (trackBar2.Value);
            this.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

    }
}
