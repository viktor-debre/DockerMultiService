package ukr.nure.itm.inf.dockerjavaservice.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseBody;
import ukr.nure.itm.inf.dockerjavaservice.model.HashData;
import ukr.nure.itm.inf.dockerjavaservice.service.HashService;

@Controller
public class HashController {
    private final HashService hashService;

    public HashController(HashService hashService) {
        this.hashService = hashService;
    }

    @PostMapping("/sha-256")
    @ResponseBody
    public HashData sha256Hashing(@RequestBody HashData data) {
        long startTime = System.nanoTime();
        final String[] array = hashService.sha256Hashing(data.getArray());
        long endTime = System.nanoTime();

        return new HashData((endTime - startTime), array);
    }

    @PostMapping("/sha3-256")
    @ResponseBody
    public HashData sha3256Hashing(@RequestBody HashData data) {
        long startTime = System.nanoTime();
        final String[] array = hashService.sha3256Hashing(data.getArray());
        long endTime = System.nanoTime();

        return new HashData((endTime - startTime), array);
    }

    @PostMapping("/sha-1")
    @ResponseBody
    public HashData sha1Hashing(@RequestBody HashData data) {
        long startTime = System.nanoTime();
        final String[] array = hashService.sha1Hashing(data.getArray());
        long endTime = System.nanoTime();

        return new HashData((endTime - startTime), array);
    }
}
