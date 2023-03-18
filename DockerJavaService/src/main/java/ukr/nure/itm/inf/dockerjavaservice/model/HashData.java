package ukr.nure.itm.inf.dockerjavaservice.model;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class HashData {
    private long timeElapsed;
    private String[] array;
}
