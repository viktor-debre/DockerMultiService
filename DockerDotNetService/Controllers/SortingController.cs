using Microsoft.AspNetCore.Mvc;

namespace DockerDotNetService.Controllers
{
    [ApiController]
    public class SortingController : ControllerBase
    {
        private readonly ILogger<SortingController> _logger;

        public SortingController(ILogger<SortingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("api/sorting/bubble")]
        public IEnumerable<int> SortBubble([FromBody] List<int> array)
        {
            BubbleSort(array);
            return array;
        }

        [HttpPost]
        [Route("api/sorting/heap")]
        public IEnumerable<int> SortHeap([FromBody] List<int> array)
        {
            HeapSort(array);
            return array;
        }

        [HttpPost]
        [Route("api/sorting/quick")]
        public IEnumerable<int> SortQuick([FromBody] List<int> array)
        {
            QuickSort(array, 0, array.Count - 1);
            return array;
        }

        public void BubbleSort(List<int> array)
        {
            int n = array.Count;
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

        public void HeapSort(List<int> list)
        {
            int n = list.Count;

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

        // To heapify a subtree rooted with node i which is
        // an index in list. n is size of heap
        public void Heapify(List<int> list, int n, int i)
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

        public void QuickSort(List<int> array, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(array, low, high);

                QuickSort(array, low, pi - 1);
                QuickSort(array, pi + 1, high);
            }
        }

        static int Partition(List<int> array, int low, int high)
        {
            int pivot = array[high];
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;
            return (i + 1);
        }
    }
}
