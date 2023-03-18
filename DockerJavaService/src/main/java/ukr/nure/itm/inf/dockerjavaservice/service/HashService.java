package ukr.nure.itm.inf.dockerjavaservice.service;

import org.springframework.stereotype.Service;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

@Service
public class HashService {
    public String[] sha256Hashing(final String[] array) {
        return hashing(array, getMessageDigest("SHA-256"));
    }

    public String[] sha3256Hashing(final String[] array) {
        return hashing(array, getMessageDigest("SHA3-256"));
    }

    public String[] sha1Hashing(final String[] array) {
        return hashing(array, getMessageDigest("SHA-1"));
    }

    private String[] hashing(final String[] array,  final MessageDigest messageDigest) {
        final String[] hashingArray = new String[array.length];

        for (int i = 0; i < array.length; i++) {
            messageDigest.update(array[i].getBytes());
            byte[] hash = messageDigest.digest();

            hashingArray[i] = bytesToHex(hash);
        }

        return hashingArray;
    }

    private MessageDigest getMessageDigest(final String algorithmName) {
        try {
            return MessageDigest.getInstance(algorithmName);
        } catch (NoSuchAlgorithmException e) {
            throw new RuntimeException(e);
        }
    }

    private String bytesToHex(final byte[] bytes) {
        StringBuilder sb = new StringBuilder();
        for (byte b : bytes) {
            sb.append(String.format("%02x", b));
        }
        return sb.toString();
    }
}
