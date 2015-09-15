package stpaul.biblesharing.HttpClient;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;

import stpaul.biblesharing.HttpClient.Data.AccountInfo;
import stpaul.biblesharing.HttpClient.Data.OperationCode;

/**
 * Created by PBTECH on 30/07/2015.
 */
public class QuitLoginHttpSession extends HttpSession
{
    public String QuitLogin(String UserName, String PassWord)
    {
        try
        {
            AccountInfo act = new AccountInfo(UserName,PassWord, OperationCode.QuitLogin);
            String TransInfo = gson.toJson(act);
            url = new URL(HttpInfo.QuitLogInAddress);
            connection = (HttpURLConnection) url.openConnection();
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


        return null;
    }
}
