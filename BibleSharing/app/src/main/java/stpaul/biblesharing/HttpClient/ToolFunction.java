package stpaul.biblesharing.HttpClient;

import android.os.Handler;
import android.util.Log;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.ProtocolException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by PBTECH on 27/07/2015.
 */
public class ToolFunction
{
    public static Handler handler = new Handler();
    public static String GetStrFromInput(InputStream stream)
    {
        StringBuilder response;
        try
        {
            BufferedReader reader = new BufferedReader(new InputStreamReader(stream));
            response = new StringBuilder();
            String line;
            while ((line = reader.readLine()) != null)
            {
                response.append(line);
            }
            return response.toString();
        }
        catch (IOException e)
        {
            Log.e("IOerror", e.getMessage());
            return null;
        }

    }
    public static void DefaultConnectionConfigure(HttpURLConnection connection)
    {
        try {
            connection.setRequestMethod("GET");
        } catch (ProtocolException e) {
            Log.e("address problem",e.getMessage());
        }
        connection.setConnectTimeout(8000);
        connection.setReadTimeout(8000);
    }
    public  static boolean emailCheck(String email)
    {
        if(email!=null)
        {

                if (null==email || "".equals(email)) return false;
                //0Pattern p = Pattern.compile("\\w+@(\\w+.)+[a-z]{2,3}"); //简单匹配
                Pattern p =  Pattern.compile("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");//复杂匹配
                Matcher m = p.matcher(email);
                return m.matches();
        }
        return false;
    }
}
