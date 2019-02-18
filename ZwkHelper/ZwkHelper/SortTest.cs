using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CoreHelper;

namespace ZwkHelper
{
    public static class SortTest
    {
        #region 产生新数组

        public static List<int> NewArray(int length)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < length; i++)
            {
                result.Add(RandomHelper.GetNumber(0, 100000));
            }
            return result;
        }

        #endregion

        #region 冒泡排序

        /// <summary>
        /// a,b,c,d,e
        /// 将a与b比较，a大，则交换数值，再a与c比较
        /// 再将b与c比较，以此类推
        /// </summary>
        /// <param name="array"></param>
        public static void BubbleSort(List<int> array)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < array.Count; i++)
            {
                for (int j = i + 1; j < array.Count; j++)
                {
                    if (array[i] > array[j])
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("冒泡排序------" + time.TotalMilliseconds);
        }

        #endregion

        #region 选择排序

        /// <summary>
        /// 从第一位开始，按顺序选出最小的数，交换到第一位
        /// 再从第二位开始，按顺序选出最小的数，交换到第二位
        /// 以此类推
        /// </summary>
        /// <param name="array"></param>
        public static void SelectionSort(List<int> array)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < array.Count; i++)
            {
                var index = i;
                for (int j = i + 1; j < array.Count; j++)
                {
                    if (array[index] > array[j])
                        index = j;
                }
                var num = array[i];
                array[i] = array[index];
                array[index] = num;
            }
            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("选择排序------" + time.TotalMilliseconds);
        }

        #endregion

        #region 插入排序

        /// <summary>
        /// 从第二个数开始，将每个数与前置数字比较，若当前数小，则交换到前面
        /// 再从第三个数开始，以此类推
        /// 优化方式：2分查询，将当前数字与前置数字比较时使用2分法，大数据性能有提升，小数据没用
        /// </summary>
        public static void InsertionSort(List<int> array)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 1; i < array.Count; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    var num = array[j + 1];
                    if (num < array[j])
                    {
                        array[j + 1] = array[j];
                        array[j] = num;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("插入排序------" + time.TotalMilliseconds);
        }

        #endregion

        #region 希尔排序 

        /// <summary>
        /// 有序数组的插入排序性能很高
        /// 希尔排序就是控制间隔，使数组基本有序
        /// 类似于预处理使数组基本有序，再进行最后一次的插入排序
        /// 核心在于间隔序列的设定。既可以提前设定好间隔序列，也可以动态的定义间隔序列
        /// 间隔序列设置的好，性能提升，否则性能可能落后
        /// </summary>
        public static void ShellSort(List<int> array)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //设置增量 目前仅在5000比插入快
            for (int i = array.Count / 3; i > 0; i = i / 3)
            {
                ShellSort(array, i);
            }
            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("希尔排序------" + time.TotalMilliseconds);
        }

        public static void ShellSort(List<int> array, List<int> gaps)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var gap in gaps)
            {
                ShellSort(array, gap);
            }

            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("希尔排序------" + time.TotalMilliseconds);
        }

        private static void ShellSort(List<int> array, int gap)
        {
            for (int i = gap; i < array.Count; i++)
            {
                var temp = array[i];
                if (temp < array[i - gap])
                {
                    for (int j = 0; j < i; j += gap)
                    {
                        if (temp < array[j])
                        {
                            temp = array[j];
                            array[j] = array[i];
                            array[i] = temp;
                        }
                    }
                }
            }
        }

        #endregion

        #region 快速排序

        /// <summary>
        /// 取数组第一位数据，与后面数字比较并交换，使这个数字前位置的数比他小，后面的数字比他大
        /// 再将这个位置前后两部分以此类推
        /// </summary>
        /// <param name="array"></param>
        public static void QuickSort(List<int> array)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            QuickSort(array, 0, array.Count - 1);
            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("快速排序------" + time.TotalMilliseconds);
        }

        private static int Division(List<int> array, int left, int right)
        {
            //基准数据
            var num = array[left];
            while (left < right)
            {
                if (num > array[left + 1])
                {
                    array[left] = array[left + 1];
                    array[left + 1] = num;
                    left++;
                }
                else
                {
                    var temp = array[right];
                    array[right] = array[left + 1];
                    array[left + 1] = temp;
                    right--;
                }
            }
            return left;
        }

        private static void QuickSort(List<int> array, int left, int right)
        {
            //排序并找出一个左边小于他，右侧大于他的数字
            var i = Division(array, left, right);
            if (left < i - 1)
                //排序左侧
                QuickSort(array, left, i - 1);
            if (i + 1 < right)
                //排序右侧
                QuickSort(array, i + 1, right);
        }

        #endregion

        #region 归并排序

        /// <summary>
        /// 将数组从中间分为左右两部分，再左右分为两部分，递归，直至每个部分仅有一个数
        /// 比较两部分的数字大小，从小到大插入到新数组返回
        /// 最终返回一个排序好的数组，按顺序赋值给输入的数组即可
        /// </summary>
        /// <param name="array"></param>
        public static void MergeSort(List<int> array)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = Merge(array);
            array.RemoveAll(p => true);
            foreach (var i in result)
            {
                array.Add(i);
            }

            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("归并排序------" + time.TotalMilliseconds);
        }

        private static List<int> Merge(List<int> array)
        {
            var len = array.Count;
            if (len < 2)
                return array;
            //拆分数组
            var mid = array.Count / 2;
            var left = array.GetRange(0, mid);
            var right = array.GetRange(mid, array.Count - mid);
            //合并左右两部分
            var result = Merge(Merge(left), Merge(right));
            return result;
        }

        //合并数组
        private static List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();
            while (left.Count > 0 && right.Count > 0)
            {
                if (left[0] < right[0])
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }
            }
            while (left.Count > 0)
            {
                result.Add(left[0]);
                left.RemoveAt(0);
            }
            while (right.Count > 0)
            {
                result.Add(right[0]);
                right.RemoveAt(0);
            }
            return result;
        }

        #endregion

        #region 堆排序

        /// <summary>
        /// 建立2叉树，满足上面的数字大于下面的数字
        /// 将顶上的数字与底部数字交换，2叉树大小减1，重新整理2叉树
        /// </summary>
        /// <param name="array"></param>
        public static void HeapSort(List<int> array)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Heap(array);

            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("  堆排序------" + time.TotalMilliseconds);
        }

        private static void Heap(List<int> array)
        {
            var heapSize = array.Count;
            for (int i = heapSize / 2 - 1; i >= 0; i--)
            {
                Heapify(array, i, heapSize);
            }
            for (var j = heapSize - 1; j >= 1; j--)
            {
                var temp = array[0];
                array[0] = array[j];
                array[j] = temp;
                heapSize--;
                Heapify(array, 0, heapSize);
            }
        }

        private static void Heapify(List<int> array, int x, int length)
        {
            var l = 2 * x;
            var r = 2 * x + 1;
            var largest = x;
            if (l < length && array[l] > array[largest])
            {
                largest = l;
            }
            if (r < length && array[r] > array[largest])
            {
                largest = r;
            }
            if (largest != x)
            {
                var temp = array[x];
                array[x] = array[largest];
                array[largest] = temp;
                Heapify(array, largest, length);
            }
        }

        #endregion

        #region 计数排序

        /// <summary>
        /// 限定数字范围
        /// 创建数字范围数组range[]，循环输入数据input，若出现数字1231，则range[1231]++
        /// 清空input，循环range[]
        /// 若游标i为1，则input.add(i)，为2，则input.add(i)，input.add(i)
        /// </summary>
        /// <param name="array"></param>
        public static void CountingSort(List<int> array)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Counting(array, 100000);

            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("计数排序------" + time.TotalMilliseconds);
        }

        private static void Counting(List<int> array, int range)
        {
            int[] temp = new int[range];

            for (int i = 0; i < array.Count; i++)
            {
                temp[array[i]]++;
            }

            array.RemoveAll(p => true);
            for (int i = 0; i < temp.Length; i++)
            {
                while (temp[i] > 0)
                {
                    array.Add(i);
                    temp[i]--;
                }
            }
        }

        #endregion

        #region 桶排序

        //计数排序的改版
        //数组分组，各组单独排序，再连接在一起，组内可以有自己的排序实现方式

        #endregion

        #region 基数排序

        /// <summary>
        /// 基数排序是按照低位先排序，然后收集；再按照高位排序，然后再收集；依次类推，直到最高位。
        /// 有时候有些属性是有优先级顺序的，先按低优先级排序，再 按高优先级排序。
        /// 最后的次序就是高优先级高的在前，高优先级相同的低优先级高的在前。
        /// 基数排序基于分别排序，分别收集，所以是稳定的
        /// 
        /// 取得数组中的最大数，并取得位数；
        /// arr为原始数组，从最低位开始取每个位组成radix数组；
        /// 对radix进行计数排序（利用计数排序适用于小范围数的特点）；
        /// </summary>
        /// <param name="array"></param>
        public static void RadixSort(List<int> array, int maxDigit)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            

            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            Console.WriteLine("基数排序------" + time.TotalMilliseconds);
        }

        #endregion






    }
}
