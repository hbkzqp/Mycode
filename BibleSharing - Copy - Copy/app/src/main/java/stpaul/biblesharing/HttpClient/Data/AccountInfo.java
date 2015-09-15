package stpaul.biblesharing.HttpClient.Data;
import com.google.gson.Gson;
/**
 * Created by PBTECH on 30/07/2015.
 */
public class AccountInfo extends BaseInfo
{
    public AccountInfo()
    {

    }
    public AccountInfo(String UserName, String PassWord,int operation)
    {
        this.setUserName(UserName);
        this.setPassword(PassWord);
        this.setOperationCode(operation);
    }
    private String userName;
    private String passWord;
    public String getPassword() {
        return passWord;
    }

    public void setPassword(String password) {
        passWord = password;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }
}
