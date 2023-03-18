package ukr.nure.itm.inf.dockerjavaservice.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseBody;
import ukr.nure.itm.inf.dockerjavaservice.model.SortData;
import ukr.nure.itm.inf.dockerjavaservice.service.SortService;

@Controller
public class SortController {

    private final SortService sortService;

    public SortController(SortService sortService) {
        this.sortService = sortService;
    }

    @PostMapping("/bubbleSort")
    @ResponseBody
    public SortData bubbleSort(@RequestBody SortData data) {
        long startTime = System.nanoTime();
        final int[] array = sortService.bubbleSort(data.getArray());
        long endTime = System.nanoTime();

        return new SortData(array, (endTime - startTime));
    }

    @PostMapping("/heapSort")
    @ResponseBody
    public SortData heapSort(@RequestBody SortData data) {
        long startTime = System.nanoTime();
        final int[] array = sortService.heapSort(data.getArray());
        long endTime = System.nanoTime();

        return new SortData(array, (endTime - startTime));
    }

    @PostMapping("/quickSort")
    @ResponseBody
    public SortData quickSort(@RequestBody SortData data) {
        long startTime = System.nanoTime();
        final int[] array = sortService.quickSort(data.getArray(), 0, data.getArray().length - 1);
        long endTime = System.nanoTime();

        return new SortData(array, (endTime - startTime));
    }
}
