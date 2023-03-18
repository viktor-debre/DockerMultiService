using DockerDotNetService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DockerDotNetService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SortingController : ControllerBase
    {
        private readonly ILogger<SortingController> _logger;

        public SortingController(
            ILogger<SortingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ActionName("bubbleSort")]
        public ActionResult<SortingResponce> SortBubble([FromBody] SortData request)
        {
            var array = request.array;
            var timer = new Stopwatch();
            timer.Start();
            BubbleSort(array);
            timer.Stop();
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            var responce = new SortingResponce()
            {
                array = array.ToList(),
                timeElapsed = timer.ElapsedTicks * nanosecPerTick,
            };

            return Ok(responce);
        }

        [HttpPost]
        [ActionName("heapSort")]
        public ActionResult<SortingResponce> SortHeap([FromBody] SortData request)
        {
            var array = request.array;
            var timer = new Stopwatch();
            timer.Start();
            HeapSort(array);
            timer.Stop();
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            var responce = new SortingResponce()
            {
                array = array.ToList(),
                timeElapsed = timer.ElapsedTicks * nanosecPerTick,
            };

            return Ok(responce);
        }

        [HttpPost]
        [ActionName("quickSort")]
        public ActionResult<SortingResponce> SortQuick([FromBody] SortData request)
        {
            var array = request.array;
            var timer = new Stopwatch();
            timer.Start();
            QuickSort(array, 0, array.Length - 1);
            timer.Stop();
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            var responce = new SortingResponce()
            {
                array = array.ToList(),
                timeElapsed = timer.ElapsedTicks * nanosecPerTick,
            };

            return Ok(responce);
        }


        public void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        // swap array[j] and array[j+1]
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        public void HeapSort(int[] list)
        {
            int n = list.Length;

            // Build heap (rearrange list)
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(list, n, i);

            // One by one extract an element from heap
            for (int i = n - 1; i >= 0; i--)
            {
                // Move current root to end
                int temp = list[0];
                list[0] = list[i];
                list[i] = temp;

                // call max heapify on the reduced heap
                Heapify(list, i, 0);
            }
        }

        private void Heapify(int[] list, int n, int i)
        {
            int largest = i; // Initialize largest as root
            int left = 2 * i + 1; // left = 2*i + 1
            int right = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (left < n && list[left] > list[largest])
                largest = left;

            // If right child is larger than largest so far
            if (right < n && list[right] > list[largest])
                largest = right;

            // If largest is not root
            if (largest != i)
            {
                int swap = list[i];
                list[i] = list[largest];
                list[largest] = swap;

                // Recursively heapify the affected sub-tree
                Heapify(list, n, largest);
            }
        }

        public int[] QuickSort(int[] array, int left, int right)
        {
            int i = left, j = right;
            int pivot = array[left + (right - left) / 2];

            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }
                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                QuickSort(array, left, j);
            }
            if (i < right)
            {
                QuickSort(array, i, right);
            }

            return array;
        }
    }
}
