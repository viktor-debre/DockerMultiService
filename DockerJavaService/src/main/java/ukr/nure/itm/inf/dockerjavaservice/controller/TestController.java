package ukr.nure.itm.inf.dockerjavaservice.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import static org.springframework.web.bind.annotation.RequestMethod.GET;

@Controller
public class TestController {

    @RequestMapping(value = "/hello", method = GET)
    public String redirectToMainPage() {
        return "index";
    }
}
