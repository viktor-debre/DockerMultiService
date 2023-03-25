from flask import Flask, request, jsonify
import time
import hashlib

app = Flask(__name__)
app.config['JSON_SORT_KEYS'] = False

@app.route('/bubbleSort', methods=['POST'])
def bubble_sort():
    array = request.json.get('array')
    start_time_ns = time.perf_counter_ns()
    bubble_sort_f(array)
    elapsed_time_ns = time.perf_counter_ns() - start_time_ns

    data = {
        "timeElapsed": elapsed_time_ns,
        "array": array
    }

    return jsonify(data)

def bubble_sort_f(arr):
    n = len(arr)

    # Traverse through all elements in the list
    for i in range(n):
        # Last i elements are already sorted, so we don't need to check them
        for j in range(0, n - i - 1):
            # Swap if the element found is greater than the next element
            if arr[j] > arr[j + 1]:
                arr[j], arr[j + 1] = arr[j + 1], arr[j]

@app.route('/heapSort', methods=['POST'])
def heap_sort():
    array = request.json.get('array')
    start_time_ns = time.perf_counter_ns()
    heap_sort_f(array)
    elapsed_time_ns = time.perf_counter_ns() - start_time_ns

    data = {
        "timeElapsed": elapsed_time_ns,
        "array": array
    }

    return jsonify(data)

def heap_sort_f(arr):
    n = len(arr)

    # Build max heap
    for i in range(n // 2 - 1, -1, -1):
        heapify(arr, n, i)

    # Extract elements from heap
    for i in range(n - 1, 0, -1):
        arr[i], arr[0] = arr[0], arr[i]  # Swap the root with the last element
        heapify(arr, i, 0)  # Heapify the reduced heap

def heapify(arr, n, i):
    largest = i  # Initialize the largest as root
    left = 2 * i + 1
    right = 2 * i + 2

    # Check if left child exists and is greater than root
    if left < n and arr[largest] < arr[left]:
        largest = left

    # Check if right child exists and is greater than the largest so far
    if right < n and arr[largest] < arr[right]:
        largest = right

    # If the largest is not the root, swap and continue heapifying
    if largest != i:
        arr[i], arr[largest] = arr[largest], arr[i]
        heapify(arr, n, largest)

@app.route('/quickSort', methods=['POST'])
def quick_sort():
    array = request.json.get('array')
    start_time_ns = time.perf_counter_ns()
    quick_sort_f(array, 0, len(array) - 1)
    elapsed_time_ns = time.perf_counter_ns() - start_time_ns

    data = {
        "timeElapsed": elapsed_time_ns,
        "array": array
    }

    return jsonify(data)

def partition(arr, low, high):
    pivot = arr[high]
    i = low - 1

    for j in range(low, high):
        if arr[j] <= pivot:
            i += 1
            arr[i], arr[j] = arr[j], arr[i]

    arr[i + 1], arr[high] = arr[high], arr[i + 1]
    return i + 1


def quick_sort_f(arr, low, high):
    if low < high:
        pi = partition(arr, low, high)

        quick_sort_f(arr, low, pi - 1)
        quick_sort_f(arr, pi + 1, high)


@app.route('/sha-256', methods=['POST'])
def hash_sha256():
    array = request.json.get('array')
    start_time_ns = time.perf_counter_ns()
    result = hash_sha256_f(array)
    elapsed_time_ns = time.perf_counter_ns() - start_time_ns

    data = {
        "timeElapsed": elapsed_time_ns,
        "array": result
    }

    return jsonify(data)

def hash_sha256_f(strings):
    hashed_strings = []
    for s in strings:
        hashed_str = hashlib.sha256(str.encode(s)).hexdigest()
        hashed_strings.append(hashed_str)
    return hashed_strings

@app.route('/sha3-256', methods=['POST'])
def hash_sha3_256():
    array = request.json.get('array')
    start_time_ns = time.perf_counter_ns()
    result = hash_sha3_256_f(array)
    elapsed_time_ns = time.perf_counter_ns() - start_time_ns

    data = {
        "timeElapsed": elapsed_time_ns,
        "array": result
    }

    return jsonify(data)

def hash_sha3_256_f(strings):
    hashed_strings = []
    for s in strings:
        hashed_str = hashlib.sha3_256(str.encode(s)).hexdigest()
        hashed_strings.append(hashed_str)
    return hashed_strings

@app.route('/sha-1', methods=['POST'])
def hash_sha1():
    array = request.json.get('array')
    start_time_ns = time.perf_counter_ns()
    result = hash_sha1_f(array)
    elapsed_time_ns = time.perf_counter_ns() - start_time_ns

    data = {
        "timeElapsed": elapsed_time_ns,
        "array": result
    }

    return jsonify(data)

def hash_sha1_f(strings):
    hashed_strings = []
    for s in strings:
        hashed_str = hashlib.sha1(str.encode(s)).hexdigest()
        hashed_strings.append(hashed_str)
    return hashed_strings


@app.route('/tax', methods=['POST'])
def calculate_federal_income_tax():
    income = request.json.get('income')
    status = request.json.get('status')
    tax = calculate_federal_income_tax(income,status)
    
    data = {
        "tax": tax
    }

    return jsonify(data)

def calculate_federal_income_tax(income, status):
    tax = 0
    rate = [10, 12, 22, 24, 32, 35, 37]
    single = [0, 10275, 41775, 89075, 170050, 215950, 539900]
    married_joint = [0, 20550, 83550, 178150, 340100, 431900, 647850]
    married_separate = [0, 10275, 41775, 89075, 170050, 215950, 323925]
    head_of_household = [0, 13250, 50400, 130150, 210800, 413350, 441000]

    if status == 1:
        brackets = single
    elif status == 2:
        brackets = married_joint
    elif status == 3:
        brackets = married_separate
    elif status == 4:
        brackets = head_of_household
    else:
        return -1

    for i in range(len(rate) - 1, -1, -1):
        if income > brackets[i]:
            tax += (income - brackets[i]) * rate[i] / 100
            income = brackets[i]

    return tax


if __name__ == '__main__':
    app.run()
