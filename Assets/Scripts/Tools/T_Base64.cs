using System;

public class T_Base64 {

    static public string EncodeBase64String(string from) {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(from);
        var dest = Convert.ToBase64String(bytes);
        return dest;
    }
}
