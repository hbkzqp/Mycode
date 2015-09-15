package stpaul.biblesharing.HttpClient;

import com.google.gson.Gson;

import java.net.HttpURLConnection;
import java.net.URL;

/**
 * Created by PBTECH on 27/07/2015.
 */
public class HttpSession
{
    protected URL url;
    protected HttpURLConnection connection;
    public static Gson gson = new Gson();
}
