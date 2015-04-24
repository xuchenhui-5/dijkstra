using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shortPath
{
    class Program
    {
        static int length = 6;
        static string[] shortedPath = new string[length];
        static int noPath = 2000;
        static int MaxSize = 1000;
        static int[,] G = { { noPath, noPath, 10, noPath, 30, 100 }, { noPath, noPath, 5, noPath, noPath, noPath }, { noPath, noPath, noPath, 50, noPath, noPath }, { noPath, noPath, noPath, noPath, noPath, 10 }, { noPath, noPath, noPath, 20, noPath, 60 }, { noPath, noPath, noPath, noPath, noPath, noPath } };
        static string[] PathResult = new string[length];

        static int[] path1 = new int[length];
        static int[,] path2 = new int[length, length];
        static int[] distance2 = new int[length];

        static void Main(string[] args)
        {
            int dist1 = getShortedPath(G, 0, 5, path1);
            Console.WriteLine("点0到点5路径:");
            for (int i = 0; i < path1.Length; i++)
                Console.Write(path1[i].ToString() + " ");
            Console.WriteLine("长度:" + dist1);
            Console.ReadKey();

            //int[] pathdist = getShortedPath(G, 0, path2);
            //Console.WriteLine("点0到任意点的路径:");
            //for (int j = 0; j < pathdist.Length; j++)
            //{
            //    Console.WriteLine("点0到" + j + "的路径:");
            //    for (int i = 0; i < length; i++)
            //        Console.Write(path2[j, i].ToString() + " ");
            //    Console.WriteLine("长度:" + pathdist[j]);
            //}
            //Console.ReadKey();
        }
        //从某一源点出发，找到到某一结点的最短路径
        static int getShortedPath(int[,] G, int start, int end, int[] path)
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
                        dist[j] = min + G[curNode, j];
                        prev[j] = curNode;//记录最短路径上的点（过程点）
                    }
            }
            //输出路径结点
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
        //        path[v,0] = v;
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
        //            path[k,step] = prev[e];
        //            e = prev[e];
        //        }
        //        for (int i = step; i > step / 2; i--)
        //        {
        //            int temp = path[k,step - i];
        //            path[k,step - i] = path[k,i];
        //            path[k,i] = temp;
        //        }
        //    }
        //    return dist;
        //}
        #endregion
    }
}
    

