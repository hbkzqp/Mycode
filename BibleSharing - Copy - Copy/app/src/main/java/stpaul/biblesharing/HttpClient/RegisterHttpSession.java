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
import stpaul.biblesharing.InterFace.CommonInfo;


/**
 * Created by PBTECH on 26/07/2015.
 */
public class RegisterHttpSession extends HttpSession
{
    public String Register(String UserName, String PassWord)
    {
        try
        {
            AccountInfo act = new AccountInfo(UserName,PassWord, OperationCode.Register);
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
            //下面对获取到的输入流进行读取
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
            Log.e("Exception",e.getMessage());
        }


        return null;
    }
}
