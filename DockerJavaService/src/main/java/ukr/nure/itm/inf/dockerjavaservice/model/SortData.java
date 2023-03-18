package ukr.nure.itm.inf.dockerjavaservice.model;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class SortData {
    private int[] array;
    private long timeElapsed;
}
