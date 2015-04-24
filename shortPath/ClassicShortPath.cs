using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace shortPath
{
    class Program
    {
        static int length =0;
        static int noPath = 2000;
        static int MaxSize = 1000;
       // static int[,] G = { { noPath, noPath, noPath, noPath, noPath, noPath }, { noPath, noPath, noPath, noPath, noPath, noPath }, { noPath, noPath, noPath, noPath, noPath, noPath }, { noPath, noPath, noPath, noPath, noPath, noPath }, { noPath, noPath, noPath, noPath, noPath, noPath }, { noPath, noPath, noPath, noPath, noPath, noPath } };
      //  static int[,] BD = { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };            
       // static int[] path1 = new int[length];
        static int R, L, C, BandWidth;
        static int[] S = new int[10];
        static int[] D = new int[10];
        static int[] ConstraintBD = new int[10];

        static void Main(string[] args)
        {
            string InPath = args[0];
            string OutPath = args[1];
            //string InPath = "Input.txt";
            //string OutPath = "Output.txt";
            string[] SS;//存放分隔的每一项ArrayList中的元素
            int i = -1;
            List<int> ver = new List<int>();//存放不同的顶点号
            ArrayList arrayList = new ArrayList();
            StreamReader srReadFile = new StreamReader(InPath);
            // 读取流直至文件第一部分末尾结束
            string strReadLine = srReadFile.ReadLine().Trim(); //第一行为字段
            while (!srReadFile.EndOfStream)
            {
                strReadLine = srReadFile.ReadLine().Trim(); //读取每行数据
                if (strReadLine == ""||strReadLine ==";")
                    break;
                arrayList.Add(strReadLine);
            }
            //提取节点数(ver的长度就是顶点的数量)
            foreach (String row in arrayList)
            {
                SS = row.Trim().Split(',');
                R = Int32.Parse(SS[0]);
                L = Int32.Parse(SS[1]);
                if (!ver.Contains(R))
                {
                    ver.Add(R);
                }
                if (!ver.Contains(L))
                {
                    ver.Add(L);
                }
            }
            //初始化Cost矩阵和bindwidth矩阵
            length = ver.Count;
            int[,] G = new int[length, length];//Cost矩阵
            int[,] BD = new int[length, length];//bindwidth矩阵
            int[] path1 = new int[length];
            for (int m = 0; m < ver.Count; m++)
            {
                for (int n = 0; n < ver.Count; n++)
                {
                    G[m, n] = noPath;
                    BD[m, n] = 0;
                }
            }
            //为矩阵赋值
            foreach (String  Row in arrayList )
            {
                SS = Row.Trim().Split(',');
                R = Int32.Parse(SS[0]);
                L = Int32.Parse(SS[1]);
                C = Int32.Parse(SS[2]);
                BandWidth = Int32.Parse(SS[3]);
                G[R-1, L-1] = C;
                BD[R-1, L-1] = BandWidth;
            }
            //读取Input.txt文件的第二部分找出源节点和目的节点
            strReadLine = srReadFile.ReadLine().Trim();
            while (!srReadFile.EndOfStream)
            {
                strReadLine = srReadFile.ReadLine().Trim();
                if (strReadLine == "srcNodeID,dstNodeID,bandwidth")
                {
                    //  strReadLine = srReadFile.ReadLine().Trim();
                    while (!srReadFile.EndOfStream)
                    {
                        strReadLine = srReadFile.ReadLine().Trim();
                        i++;
                        SS = strReadLine.Trim().Split(',');
                        S[i] = Int32.Parse(SS[0]);
                        D[i] = Int32.Parse(SS[1]);
                        ConstraintBD[i] = Int32.Parse(SS[2]);
                        //Console.WriteLine(strReadLine);
                    }
                }
            }
            #region
            //if (strReadLine != "leftnodeID,rightnodeID,bandwidth" && strReadLine != ";"&&strReadLine !="srcNodeID,dstNodeID,bandwidth"&&strReadLine !="")
                //{
                //    //if (strReadLine != ";")
                //    //{
                //        SS = strReadLine.Trim().Split(',');
                //        R = Int32.Parse(SS[0]);
                //        L = Int32.Parse(SS[1]);
                //        if (!ver.Contains(R))
                //        {
                //            ver.Add(R);
                //        }
                //        if (!ver.Contains(L))
                //        {
                //            ver.Add(L);
                //        }
                //        BindWidth = Int32.Parse(SS[2]);
                //        if (R != L)
                //        {
 
                //        }

                //        G[R, L] = 1;
                //        BD[R, L] = BindWidth;
                    //}
                    //else
                    //{
                    //    strReadLine = srReadFile.ReadLine();
                    //    if (strReadLine == "leftnodeID,rightnodeID,bandwidth")
                    //    {
                    //        strReadLine = srReadFile.ReadLine();
                    //        SS = strReadLine.Split(',');
                    //        S = Convert.ToInt16(SS[0]);
                    //        D = Convert.ToInt16(SS[1]);
                    //        ConstraintBD = Convert.ToInt16(SS[2]);
                    //    }
                    //}
               // }
                //Console.WriteLine(strReadLine); //屏幕打印每行数据
                //if (strReadLine == "srcNodeID,dstNodeID,bandwidth")
                //{
                //  //  strReadLine = srReadFile.ReadLine().Trim();
                //    while (!srReadFile.EndOfStream)
                //    {
                //        strReadLine = srReadFile.ReadLine();
                //        i++;
                //        SS = strReadLine.Trim().Split(',');
                //        S[i] = Int32.Parse(SS[0]);
                //        D[i] = Int32.Parse(SS[1]);
                //        ConstraintBD[i] = Int32.Parse(SS[2]);
                //        Console.WriteLine(strReadLine);                       
                //    }
            //}
            #endregion
            // 关闭读取流文件
            srReadFile.Close();
            //Console.ReadKey();
            StreamWriter swWriteFile = File.CreateText(OutPath);
            for (int k = 0; k <= i; k++)
            {
                for (int p = 0; p < length; p++)
                {
                    path1[p] = -1;
                }
                int dist1 = getShortedPath(G, BD, S[k]-1, D[k]-1, ConstraintBD[k], path1);
                if (dist1 > 1000)
                {
                 // Console.Write("没有满足条件的路线！");
                    //swWriteFile.Write("No Route");
                    //swWriteFile.Close();
                    continue;
                    //Console.ReadKey();                  
                }
                //Console.WriteLine("点0到点5路径:");
                //foreach (int s in path1)
                //{
                //    if (s == -1)
                //        continue;
                    
                //    swWriteFile.Write(s + "\t");
                //}
                String str = "";
                for (int j = 0; j < path1.Length; j++)
                {
                    int value = Int32.Parse(path1[j].ToString());
                    if (value != -1)
                    {
                        value++;
                        str += Convert.ToString(value)+',';
                       // swWriteFile.Write(value + ",");
                    }                 
                }
                str = str.Substring(0, str.Length - 1);
                swWriteFile.WriteLine(str);
                //swWriteFile.WriteLine();
            }
            swWriteFile.Close();
            //Console.ReadKey();
        }

            //for (int i = 0; i < path1.Length; i++)
            //{
            //   // Console.Write(path1[i].ToString() + " ");                       
            //    swWriteFile.WriteLine(path1[i].ToString()); //写入读取的每行数据
            //}
          
            //Console.WriteLine("长度:" + dist1);
            //Console.ReadKey();
           
                           
           
            //int[] pathdist = getShortedPath(G, 0, path2);
            //Console.WriteLine("点0到任意点的路径:");
            //for (int j = 0; j < pathdist.Length; j++)
            //{
            //    Console.WriteLine("点0到" + j + "的路径:");
            //    for (int i = 0; i < length; i++)
            //        Console.Write(path2[j, i].ToString() + " ");
            //    Console.WriteLine("长度:" + pathdist[j]);
            //}
           // Console.ReadKey();
        
        //从某一源点出发，找到到某一结点的最短路径
        static int getShortedPath(int[,] G,int [,]BD, int start, int end, int bandwidth, int[] path)
        {
            bool[] s = new bool[length]; //表示找到起始结点与当前结点间的最短路径
            int min;  //最小距离临时变量
            int curNode = 0; //临时结点，记录当前正计算结点
            int[] dist = new int[length];
            int[] prev = new int[length];

            //初始结点信息
            for (int v = 0; v < length; v++)
            {
                s[v] = false;
                dist[v] = G[start, v];
                if (BD[start, v] < bandwidth && BD[start, v] > 0)
                {
                    dist[v] = 1500;
                }               
                if (dist[v] > MaxSize)
                    prev[v] = 0;
                else
                    prev[v] = start;
            }
            path[0] = end;
            dist[start] = 0;
            s[start] = true;
            //主循环
            for (int i = 1; i < length; i++)
            {
                min = MaxSize;
                for (int w = 0; w < length; w++)
                {
                    if (!s[w] && dist[w] < min)//将除源点以外的点并入到集合中
                    {
                        curNode = w;
                        min = dist[w];
                    }
                }
                s[curNode] = true;
                for (int j = 0; j < length; j++)//寻找集合内的点到集合外某一点的最短路径
                    if (!s[j] && min + G[curNode, j] < dist[j])
                    {
                        if (BD[curNode, j] < bandwidth)
                            continue;                        
                        dist[j] = min + G[curNode, j];
                        prev[j] = curNode;//记录最短路径上的点（过程点）
                    }
            }
            //输出路径结点
            int e = end, step = 0;
            try
            {
                while (e != start)
                {
                    step++;
                    path[step] = prev[e];
                    e = prev[e];
                }
            }
            catch
            {
                //Console.WriteLine("没有路径！");
                step = 0;
            }
            for (int i = step; i > step / 2; i--)
            {
                int temp = path[step - i];
                path[step - i] = path[i];
                path[i] = temp;
            }
            return dist[end];
        }
        #region
        ////从某一源点出发，找到到所有结点的最短路径
        //static int[] getShortedPath(int[,] G, int start, int[,] path)
        //{
        //    int[] PathID = new int[length];//路径（用编号表示）
        //    bool[] s = new bool[length]; //表示找到起始结点与当前结点间的最短路径
        //    int min;  //最小距离临时变量
        //    int curNode = 0; //临时结点，记录当前正计算结点
        //    int[] dist = new int[length];
        //    int[] prev = new int[length];
        //    //初始结点信息
        //    for (int v = 0; v < length; v++)
        //    {
        //        s[v] = false;
        //        dist[v] = G[start, v];
        //        if (dist[v] > MaxSize)
        //            prev[v] = 0;
        //        else
        //            prev[v] = start;
        //        path[v, 0] = v;
        //    }
        //    dist[start] = 0;
        //    s[start] = true;
        //    //主循环
        //    for (int i = 1; i < length; i++)
        //    {
        //        min = MaxSize;
        //        for (int w = 0; w < length; w++)//将除源点以外的点并入到集合中
        //        {
        //            if (!s[w] && dist[w] < min)
        //            {
        //                curNode = w;
        //                min = dist[w];
        //            }
        //        }
        //        s[curNode] = true;
        //        for (int j = 0; j < length; j++)//寻找集合内的点到集合外某一点的最短路径
        //            if (!s[j] && min + G[curNode, j] < dist[j])
        //            {
        //                dist[j] = min + G[curNode, j];
        //                prev[j] = curNode;//记录最短路径上的点（过程点）
        //            }
        //    }
        //    //输出路径结点
        //    for (int k = 0; k < length; k++)
        //    {
        //        int e = k, step = 0;
        //        while (e != start)
        //        {
        //            step++;
        //            path[k, step] = prev[e];
        //            e = prev[e];
        //        }
        //        for (int i = step; i > step / 2; i--)
        //        {
        //            int temp = path[k, step - i];
        //            path[k, step - i] = path[k, i];
        //            path[k, i] = temp;
        //        }
        //    }
        //    return dist;
        //}
        #endregion
    }
}
    

