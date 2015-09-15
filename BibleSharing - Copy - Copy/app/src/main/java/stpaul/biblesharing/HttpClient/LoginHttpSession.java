package stpaul.biblesharing.HttpClient;

import android.util.Log;

import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;

import stpaul.biblesharing.HttpClient.Data.AccountInfo;
import stpaul.biblesharing.HttpClient.Data.OperationCode;
import stpaul.biblesharing.HttpClient.HttpSession;

/**
 * Created by PBTECH on 16/08/2015.
 */
public class LoginHttpSession extends HttpSession
{
    public String Login(String UserName, String PassWord)
    {
        try
        {
            AccountInfo act = new AccountInfo(UserName,PassWord, OperationCode.Login);
            String TransInfo = gson.toJson(act);
            url = new URL(HttpInfo.RegisterAddress);
            connection = (HttpURLConnection) url.openConnection();
            connection.setDoOutput(true);
            connection.setDoInput(true);
            connection.setRequestMethod("POST");
            connection.setUseCaches(false);
            connection.setInstanceFollowRedirects(true);
            connection.setRequestProperty("Content-Type",
                    "application/x-www-form-urlencoded");

            connection.connect();
            DataOutputStream out = new DataOutputStream(connection.getOutputStream());
            out.writeBytes(TransInfo);
            out.flush();
            out.close();
            InputStream in = connection.getInputStream();
            //??????????????
            return ToolFunction.GetStrFromInput(in);
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (ProtocolException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        catch (Exception e)
        {
            Log.e("Exception", e.getMessage());
        }


        return null;
    }
}
