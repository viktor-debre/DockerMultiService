package ukr.nure.itm.inf.dockerjavaservice.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseBody;
import ukr.nure.itm.inf.dockerjavaservice.model.TaxData;
import ukr.nure.itm.inf.dockerjavaservice.model.TaxResponse;
import ukr.nure.itm.inf.dockerjavaservice.model.Status;
import ukr.nure.itm.inf.dockerjavaservice.service.TaxService;

@Controller
public class TaxController {

    private final TaxService taxService;

    public TaxController(TaxService taxService) {
        this.taxService = taxService;
    }

    @PostMapping("/tax")
    @ResponseBody
    public TaxResponse getTax(@RequestBody final TaxData data) {
        final double tax = taxService.calculateFederalIncomeTax(data.getIncome(), data.getStatus());
        return new TaxResponse(tax);
    }
}
