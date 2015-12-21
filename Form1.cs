using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 象棋
{
    enum player
    {
        blank,
        red,
        blue,   
    };
    enum chesstype
    {
        blank,
        jiang,
        che,
        ma,
        pao,
        xiang,
        zu,
        shi      
    };
    struct chess
    {
        public player side;
        public chesstype type;
    };

    struct block
    {
        public PictureBox container;
        public chess item;
    };
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureboxlist = new List<PictureBox>(81);
            Matrix=new block[10][];
            int i,j;
            for (i = 0; i < 10;i++ )
            {
                Matrix[i] = new block[9];
            }
            for(i=0;i<10;i++)
            {
                for(j=0;j<9;j++)
                {
                    Control[] col = this.Controls.Find("pictureBox" + (i*9+j+1), false);
                    Matrix[i][j].container=col[0] as PictureBox;
                    Matrix[i][j].container.Location = new Point(60 * j, 60 * i);                    
                }
            }        
            redcoll = new collecter();
            bluecool = new collecter();
            for (i = 91; i < 107;i++ )
            {
                Control[] col = this.Controls.Find("pictureBox" + i, false);
                bluecool.add(col[0] as PictureBox);
            }
            for (i = 107; i < 123;i++ )
            {
                Control[] col = this.Controls.Find("pictureBox" + i, false);
                redcoll.add(col[0] as PictureBox);
            }
                resetground();
        }
        List<PictureBox> pictureboxlist;
        block[][] Matrix;
        collecter redcoll;
        collecter bluecool;
        int chozenX;
        int chozenY;
        player currentside;
        bool beenchozen;
        bool clickswitch;
        private void click1(object sender, EventArgs e)
        {
            if(!clickswitch)
            {
                resetground();
                return;
            }
            string name = (sender as PictureBox).Name;
            string number = name.Substring(10);
            int index = Convert.ToInt32(number);
            int i,j;
            bool flag = false;
            i=(index-1)/9;
            j=(index-1)%9;
            //下载于www.51aspx.com
            if (beenchozen)
            {
                Matrix[chozenX][chozenY].container.BorderStyle = BorderStyle.None;
                Matrix[chozenX][chozenY].container.BackColor = Color.Transparent;
                beenchozen = false;
                if(Matrix[chozenX][chozenY].item.side==Matrix[i][j].item.side)
                {
                    return;
                }
                if (Matrix[chozenX][chozenY].item.side != player.blank)
                {
                    if(Matrix[i][j].item.type== chesstype.jiang)
                    {
                        flag=true;
                    }
                    if(!movechess(i, j))
                    {
                        return;
                    }
                    if(flag)
                    {
                        if (currentside == player.red)
                        {
                            MessageBox.Show("红方胜利！点击任意处重新开局");
                        }
                        else
                        {
                            MessageBox.Show("蓝方胜利！点击任意处重新开局");
                        }
                        clickswitch = false;
                    }
                    
                }
                if (currentside == player.red)
                {
                    currentside = player.blue;
                    label1.Text = "蓝方";
                    label1.ForeColor = Color.Blue;
                }
                else
                {
                    currentside = player.red;
                    label1.Text = "红方";
                    label1.ForeColor = Color.Red;
                }
            }
            else if(Matrix[i][j].item.side== currentside)
            {
                Matrix[i][j].container.BorderStyle = BorderStyle.FixedSingle;
                Matrix[i][j].container.BackColor = Color.Brown;
                chozenX = i;
                chozenY = j;
                beenchozen = true;    
            }
        }
        private void resetground()
        {
            int i, j;
            for (i = 0; i < 10;i++ )
            {
                for(j=0;j<9;j++)
                {
                    Matrix[i][j].container.Image = null;
                    Matrix[i][j].item.side = player.blank;
                    Matrix[i][j].item.type = chesstype.blank;
                }
            }
            beenchozen = false;
            clickswitch = true;
            currentside = player.red;
            label1.Text = "红方";
            label1.ForeColor = Color.Red;
            redcoll.clear();
            bluecool.clear();
            Matrix[0][0].container.Image = global::象棋.Properties.Resources.蓝车;
            Matrix[0][1].container.Image = global::象棋.Properties.Resources.蓝马;
            Matrix[0][2].container.Image = global::象棋.Properties.Resources.蓝象;
            Matrix[0][3].container.Image = global::象棋.Properties.Resources.蓝士;
            Matrix[0][4].container.Image = global::象棋.Properties.Resources.蓝将;
            Matrix[0][5].container.Image = global::象棋.Properties.Resources.蓝士;
            Matrix[0][6].container.Image = global::象棋.Properties.Resources.蓝象;
            Matrix[0][7].container.Image = global::象棋.Properties.Resources.蓝马;
            Matrix[0][8].container.Image = global::象棋.Properties.Resources.蓝车;
            Matrix[2][1].container.Image = global::象棋.Properties.Resources.蓝炮;
            Matrix[2][7].container.Image = global::象棋.Properties.Resources.蓝炮;
            Matrix[3][0].container.Image = global::象棋.Properties.Resources.蓝卒;
            Matrix[3][2].container.Image = global::象棋.Properties.Resources.蓝卒;
            Matrix[3][4].container.Image = global::象棋.Properties.Resources.蓝卒;
            Matrix[3][6].container.Image = global::象棋.Properties.Resources.蓝卒;
            Matrix[3][8].container.Image = global::象棋.Properties.Resources.蓝卒;
            Matrix[6][0].container.Image = global::象棋.Properties.Resources.红卒;
            Matrix[6][2].container.Image = global::象棋.Properties.Resources.红卒;
            Matrix[6][4].container.Image = global::象棋.Properties.Resources.红卒;
            Matrix[6][6].container.Image = global::象棋.Properties.Resources.红卒;
            Matrix[6][8].container.Image = global::象棋.Properties.Resources.红卒;
            Matrix[7][1].container.Image = global::象棋.Properties.Resources.红炮;
            Matrix[7][7].container.Image = global::象棋.Properties.Resources.红炮;
            Matrix[9][0].container.Image = global::象棋.Properties.Resources.红车;
            Matrix[9][1].container.Image = global::象棋.Properties.Resources.红马;
            Matrix[9][2].container.Image = global::象棋.Properties.Resources.红象;
            Matrix[9][3].container.Image = global::象棋.Properties.Resources.红士;
            Matrix[9][4].container.Image = global::象棋.Properties.Resources.红将;
            Matrix[9][5].container.Image = global::象棋.Properties.Resources.红士;
            Matrix[9][6].container.Image = global::象棋.Properties.Resources.红象;
            Matrix[9][7].container.Image = global::象棋.Properties.Resources.红马;
            Matrix[9][8].container.Image = global::象棋.Properties.Resources.红车;
            //下载于www.51aspx.com
            Matrix[0][0].item.side = player.blue;
            Matrix[0][1].item.side = player.blue;
            Matrix[0][2].item.side = player.blue;
            Matrix[0][3].item.side = player.blue;
            Matrix[0][4].item.side = player.blue;
            Matrix[0][5].item.side = player.blue;
            Matrix[0][6].item.side = player.blue;
            Matrix[0][7].item.side = player.blue;
            Matrix[0][8].item.side = player.blue;
            Matrix[2][1].item.side = player.blue;
            Matrix[2][7].item.side = player.blue;
            Matrix[3][0].item.side = player.blue;
            Matrix[3][2].item.side = player.blue;
            Matrix[3][4].item.side = player.blue;
            Matrix[3][6].item.side = player.blue;
            Matrix[3][8].item.side = player.blue;
            Matrix[6][0].item.side = player.red;
            Matrix[6][2].item.side = player.red;
            Matrix[6][4].item.side = player.red;
            Matrix[6][6].item.side = player.red;
            Matrix[6][8].item.side = player.red;
            Matrix[7][1].item.side = player.red;
            Matrix[7][7].item.side = player.red;
            Matrix[9][0].item.side = player.red;
            Matrix[9][1].item.side = player.red;
            Matrix[9][2].item.side = player.red;
            Matrix[9][3].item.side = player.red;
            Matrix[9][4].item.side = player.red;
            Matrix[9][5].item.side = player.red;
            Matrix[9][6].item.side = player.red;
            Matrix[9][7].item.side = player.red;
            Matrix[9][8].item.side = player.red;

            Matrix[0][0].item.type = chesstype.che;
            Matrix[0][1].item.type = chesstype.ma;
            Matrix[0][2].item.type = chesstype.xiang;
            Matrix[0][3].item.type = chesstype.shi;
            Matrix[0][4].item.type = chesstype.jiang;
            Matrix[0][5].item.type = chesstype.shi;
            Matrix[0][6].item.type = chesstype.xiang;
            Matrix[0][7].item.type = chesstype.ma;
            Matrix[0][8].item.type = chesstype.che;
            Matrix[2][1].item.type = chesstype.pao;
            Matrix[2][7].item.type = chesstype.pao;
            Matrix[3][0].item.type = chesstype.zu;
            Matrix[3][2].item.type = chesstype.zu;
            Matrix[3][4].item.type = chesstype.zu;
            Matrix[3][6].item.type = chesstype.zu;
            Matrix[3][8].item.type = chesstype.zu;
            Matrix[6][0].item.type = chesstype.zu;
            Matrix[6][2].item.type = chesstype.zu;
            Matrix[6][4].item.type = chesstype.zu;
            Matrix[6][6].item.type = chesstype.zu;
            Matrix[6][8].item.type = chesstype.zu;
            Matrix[7][1].item.type = chesstype.pao;
            Matrix[7][7].item.type = chesstype.pao;
            Matrix[9][0].item.type = chesstype.che;
            Matrix[9][1].item.type = chesstype.ma;
            Matrix[9][2].item.type = chesstype.xiang;
            Matrix[9][3].item.type = chesstype.shi;
            Matrix[9][4].item.type = chesstype.jiang;
            Matrix[9][5].item.type = chesstype.shi;
            Matrix[9][6].item.type = chesstype.xiang;
            Matrix[9][7].item.type = chesstype.ma;
            Matrix[9][8].item.type = chesstype.che;
        }
        private bool movechess(int X,int Y)
        {
            int i, j, k, n=0;
            switch(Matrix[chozenX][chozenY].item.type)
            {
                case chesstype.che:                    
                    if(chozenX==X)
                    {
                        i = chozenY < Y ? chozenY : Y;
                        j = chozenY > Y ? chozenY : Y;
                        for(k=i+1;k<j;k++)
                        {
                            if(Matrix[X][k].item.side!= player.blank)
                            {
                                return false;
                            }
                        }                       
                    }
                    if (chozenY == Y)
                    {
                        i = chozenX < X ? chozenX : X;
                        j = chozenX > X ? chozenX : X;
                        for (k = i + 1; k < j; k++)
                        {
                            if (Matrix[k][Y].item.side != player.blank)
                            {
                                return false;
                            }
                        }
                    }
                    setmove(X, Y);
                    return true;
                case chesstype.jiang:                   
                    if(Matrix[X][Y].item.type== chesstype.jiang&&chozenY==Y)
                    {
                        i = chozenX < X ? chozenX : X;
                        j = chozenX > X ? chozenX : X;
                        for (k = i + 1; k < j; k++)
                        {
                            if (Matrix[k][Y].item.side != player.blank)
                            {
                                return false;
                            }
                        }
                        setmove(X, Y);
                        return true;
                    }
                    if (Matrix[chozenX][chozenY].item.side== player.blue)
                    { 
                      
                       if(Y<3||Y>5||X>2)
                       {
                           return false;
                       }
                    }
                    else
                    {
                        if(Y<3||Y>5||X<7)
                        {
                            return false;
                        }
                    }
                    if((chozenX-X)*(chozenX-X)+(chozenY-Y)*(chozenY-Y)!=1)
                    {
                        return false;
                    }
                    setmove(X, Y);
                    return true;
                case chesstype.ma:                    
                    if (Math.Abs(chozenX - X) == 1 && Math.Abs(chozenY - Y) == 2)
                    {
                        if (Matrix[chozenX][chozenY + (Y - chozenY) / Math.Abs(Y - chozenY)].item.side!= player.blank)
                        {
                            return false;
                        }
                    }
                    else if (Math.Abs(chozenX - X) == 2 && Math.Abs(chozenY - Y) == 1)
                    {
                        if (Matrix[chozenX + (X - chozenX) / Math.Abs(X - chozenX)][chozenY].item.side != player.blank)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    setmove(X, Y);
                    return true;
                case chesstype.pao:                    
                    n = 0;
                    if(chozenX==X)
                    {
                        i = chozenY < Y ? chozenY : Y;
                        j = chozenY > Y ? chozenY : Y;
                        n = 0;
                        for(k=i+1;k<j;k++)
                        {
                            if(Matrix[X][k].item.side!= player.blank)
                            {
                                n++;
                            }
                        }          
                        if(n>1)
                        {
                            return false;
                        }
                    }
                    else if (chozenY == Y)
                    {
                        i = chozenX < X ? chozenX : X;
                        j = chozenX > X ? chozenX : X;
                        n = 0;
                        for (k = i + 1; k < j; k++)
                        {
                            if (Matrix[k][Y].item.side != player.blank)
                            {
                                n++;
                            }
                        }
                        if (n > 1)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    if(n==0&&Matrix[X][Y].item.side!= player.blank)
                    {
                        return false;
                    }
                    if(n==1&&Matrix[X][Y].item.side== player.blank)
                    {
                        return false;
                    }
                    setmove(X, Y);
                    return true;
                case chesstype.shi:                    
                    if (Matrix[chozenX][chozenY].item.side== player.blue)
                    { 
                       if(Y<3||Y>5||X>2)
                       {
                           return false;
                       }
                    }
                    else
                    {
                        if(Y<3||Y>5||X<7)
                        {
                            return false;
                        }
                    }
                    if(Math.Abs(X-chozenX)!=1||Math.Abs(chozenY-Y)!=1)
                    {
                        return false;
                    }
                    setmove(X, Y);
                    return true;
                case chesstype.xiang:                   
                    if (Matrix[chozenX][chozenY].item.side == player.blue)
                    {
                        if (X>4)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (X<5)
                        {
                            return false;
                        }
                    }
                    if ((X - chozenX) * (X - chozenX) + (chozenY - Y) * (chozenY - Y) != 8)
                    {
                        return false;
                    }                                       
                    if(Matrix[(X+chozenX)/2][(Y+chozenY)/2].item.side!= player.blank)
                    {
                        return false;
                    }
                    setmove(X, Y);
                    return true;
                case chesstype.zu: 
                    if(X!=chozenX&&Y!=chozenY)
                    {
                        return false;
                    }
                    if (Matrix[chozenX][chozenY].item.side == player.blue)
                    {
                        if (chozenX<5&&X-chozenX!=1)
                        {
                            return false;
                        }
                        if(chozenX>4)
                        {
                            if(X==chozenX&&Math.Abs(Y-chozenY)!=1)
                            {
                                return false;
                            }
                            if(Y==chozenY&&X-chozenX!=1)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (chozenX>4&&chozenX-X!=1)
                        {
                            return false;
                        }
                        if (chozenX <5)
                        {
                            if (X == chozenX && Math.Abs(Y - chozenY) != 1)
                            {
                                return false;
                            }
                            if (Y == chozenY &&  chozenX-X != 1)
                            {
                                return false;
                            }
                        }
                    }
                    setmove(X,Y);
                    return true;
            }
            return false;
        }

        private void setmove(int X,int Y)
        {
            if (Matrix[X][Y].item.side== player.red)
            {
                redcoll.push(Matrix[X][Y].container.Image);
            }
            else if (Matrix[X][Y].item.side == player.blue)
            {
                bluecool.push(Matrix[X][Y].container.Image);
            }
            Matrix[X][Y].container.Image = Matrix[chozenX][chozenY].container.Image;
            Matrix[X][Y].item = Matrix[chozenX][chozenY].item;
            Matrix[chozenX][chozenY].container.Image = null;
            Matrix[chozenX][chozenY].item.side = player.blank;
            Matrix[chozenX][chozenY].item.type= chesstype.blank;
        }
    }
    class collecter
    {
        public PictureBox[] container;
        public int number;
        public int chessnum;
        public collecter()
        {
            number = 0;
            container = new PictureBox[16];
            chessnum = 0;
        }
        public void add(PictureBox box)
        {
            container[number++] = box;
        }
        public void push(Image ima)
        {
            container[chessnum++].BackgroundImage = ima;
        }
        public void clear()
        {
            for (int i = 0; i < chessnum; i++)
            {
                container[i].BackgroundImage = null;
            }
            chessnum = 0;
        }
    };
}
