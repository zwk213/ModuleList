using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FileHelper;
using NPOI.HSSF.Record;
using WebHelper;

namespace ZwkHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 5000;

            List<int> array = SortTest.NewArray(num);
            SortTest.BubbleSort(array);

            array = SortTest.NewArray(num);
            SortTest.SelectionSort(array);

            array = SortTest.NewArray(num);
            SortTest.QuickSort(array);

            array = SortTest.NewArray(num);
            SortTest.InsertionSort(array);

            array = SortTest.NewArray(num);
            SortTest.ShellSort(array);

            array = SortTest.NewArray(num);
            SortTest.MergeSort(array);

            array = SortTest.NewArray(num);
            SortTest.HeapSort(array);

            array = SortTest.NewArray(num);
            SortTest.CountingSort(array);

            Console.ReadLine();
        }
    }

}
