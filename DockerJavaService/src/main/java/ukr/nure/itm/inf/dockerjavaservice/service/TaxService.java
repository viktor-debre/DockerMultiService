package ukr.nure.itm.inf.dockerjavaservice.service;

import org.springframework.stereotype.Service;
import ukr.nure.itm.inf.dockerjavaservice.model.Status;

@Service
public class TaxService {
    public double calculateFederalIncomeTax(double income, Status status) {
        double tax = 0;
        double[] rate = new double[] { 10, 12, 22, 24, 32, 35, 37 };
        double[] single = new double[] { 0, 10275, 41775, 89075, 170050, 215950, 539900 };
        double[] marriedJoint = new double[] { 0, 20550, 83550, 178150, 340100, 431900, 647850 };
        double[] marriedSeparate = new double[] { 0, 10275, 41775, 89075, 170050, 215950, 323925 };
        double[] headOfHousehold = new double[] { 0, 13250, 50400, 130150, 210800, 413350, 441000 };
        double[] brackets;

        switch (status) {
            case SINGLE:
                brackets = single;
                break;
            case MARRIED_JOINT:
                brackets = marriedJoint;
                break;
            case MARRIED_SEPARATE:
                brackets = marriedSeparate;
                break;
            case HEAD_OF_HOUSEHOLD:
                brackets = headOfHousehold;
                break;
            default:
                return -1;
        }

        for (int i = rate.length - 1; i >= 0; i--) {
            if (income > brackets[i]) {
                tax += (income - brackets[i]) * rate[i] / 100;
                income = brackets[i];
            }
        }

        return tax;
    }
}
