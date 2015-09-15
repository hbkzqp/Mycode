package stpaul.biblesharing;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Handler;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.util.logging.LogRecord;

import stpaul.biblesharing.HttpClient.Data.AccountBackInfo;
import stpaul.biblesharing.HttpClient.HttpSession;
import stpaul.biblesharing.HttpClient.LoginHttpSession;
import stpaul.biblesharing.HttpClient.RegisterHttpSession;
import stpaul.biblesharing.HttpClient.ToolFunction;
import stpaul.biblesharing.InterFace.CommonInfo;


public class LoginActivity extends ActionBarActivity {


    private EditText userArea ;
    private EditText pwArea;
    private Button loginBtn;
    private Button vistorBtn;
    private TextView registerText;
    private TextView forgetPw;

    private EditText passwordArea;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        initWidget();
        //httpTest();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_login, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
    public void saveAccount(String ID,String pw)
    {
        String appname = getString(R.string.app_name);
        SharedPreferences share = getSharedPreferences(appname, MODE_PRIVATE);
        SharedPreferences.Editor edit = share.edit();
        edit.putString(CommonInfo.USER_NAME,ID);
        edit.putString(CommonInfo.PASSWORD, pw);
    }
    public boolean checkShare()
    {
        String appname = getString(R.string.app_name);
        SharedPreferences share = getSharedPreferences(appname, MODE_PRIVATE);
        if(share.getString(CommonInfo.USER_NAME,"").equals("")||share.getString(CommonInfo.PASSWORD,"").equals(""))
        {
            return  false;
        }
        Login(share.getString(CommonInfo.USER_NAME,""),share.getString(CommonInfo.PASSWORD,""));
        return true;
    }
    private void  Login(final String ID, final String pw)
    {
        Thread thread = new Thread(new Runnable() {
            @Override
            public void run()
            {
                LoginHttpSession lhs = new LoginHttpSession();
                String result = lhs.Login(ID, pw);

                if(result!=null)
                {
                    final AccountBackInfo anbi = HttpSession.gson.fromJson(result,AccountBackInfo.class);
                    if(anbi.status==0)
                    {
                        ToolFunction.handler.post(new Runnable() {
                            @Override
                            public void run()
                            {
                                Intent intent = new Intent(LoginActivity.this, BibleActivity.class);
                                startActivity(intent);
                            }
                        });
                    }
                    else
                    {
                        ToolFunction.handler.post(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(LoginActivity.this, anbi.message, Toast.LENGTH_LONG).show();
                            }
                        });

                    }
                }

            }
        });
        thread.start();
    }
    private void initWidget()
    {
        userArea = (EditText) findViewById(R.id.UserNameArea);
        setForgetPw();
        setRegisterBtn();
        setLoginBtn();
        setTextArea();
        //passwordArea = (EditText) findViewById(R.id.PassWordArea);
        //userArea.getBackground().setAlpha(0);
        //passwordArea.getBackground().setAlpha(45);
    }
    private void setTextArea()
    {
        passwordArea = (EditText) findViewById(R.id.PasswordArea);
        userArea = (EditText) findViewById(R.id.UserNameArea);
    }
    private void setLoginBtn()
    {
        loginBtn = (Button) findViewById(R.id.LoginBtn);


        loginBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v)
            {
                if(userArea.getText()==null||passwordArea.getText()==null)
                {
                    Toast.makeText(LoginActivity.this, "Plesse fill in username and password", Toast.LENGTH_LONG).show();
                    return;
                }
                final String username  = userArea.getText().toString();
                final String password = passwordArea.getText().toString();
                if(username!=null&&password!=null)
                {
                    if(ToolFunction.emailCheck(username)&&password.length()>=6)
                    {
                        Toast.makeText(LoginActivity.this, "Login....", Toast.LENGTH_LONG).show();
                        Thread thread = new Thread(new Runnable() {
                            @Override
                            public void run()
                            {
                                LoginHttpSession lhs = new LoginHttpSession();
                                String result = lhs.Login(username, password);

                                if(result!=null)
                                {
                                    final AccountBackInfo anbi = HttpSession.gson.fromJson(result,AccountBackInfo.class);
                                    if(anbi.status==0)
                                    {
                                        ToolFunction.handler.post(new Runnable() {
                                            @Override
                                            public void run()
                                            {
                                                saveAccount(username, password);
                                                Intent intent = new Intent(LoginActivity.this, BibleActivity.class);
                                                startActivity(intent);
                                            }
                                        });
                                    }
                                    else
                                    {
                                        ToolFunction.handler.post(new Runnable() {
                                            @Override
                                            public void run() {
                                                Toast.makeText(LoginActivity.this, anbi.message, Toast.LENGTH_LONG).show();
                                            }
                                        });

                                    }
                                }

                            }
                        });
                        thread.start();

                    }
                    else if(!ToolFunction.emailCheck(username))
                    {
                        Toast.makeText(LoginActivity.this, "User name should be Email, Please", Toast.LENGTH_LONG).show();
                    }
                    else if(!(password.length()>=6))
                    {
                        Toast.makeText(LoginActivity.this, "Password should be longer than 6", Toast.LENGTH_LONG).show();
                    }
                }
                else
                {
                    Toast.makeText(LoginActivity.this, "Plesse fill in username and password", Toast.LENGTH_LONG).show();
                }
            }
        });
    }
    private void setRegisterBtn()
    {
        registerText = (TextView) findViewById(R.id.RegisterBtn);
        registerText.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v)
            {
                Intent intent = new Intent(LoginActivity.this, RegisterActivity.class);
                startActivity(intent);
            }
        });
    }
    private  void  setForgetPw()
    {
        forgetPw = (TextView) findViewById(R.id.ForgetBtn);
        forgetPw.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v)
            {
                Intent intent = new Intent(LoginActivity.this, ForgetPasswordActivity.class);
                startActivity(intent);
            }
        });
    }
    private void httpTest()
    {
        final RegisterHttpSession rht = new RegisterHttpSession();
        Thread thread = new Thread(new Runnable() {
            @Override
            public void run()
            {
                String result = rht.Register("haha","hahah");
                if(result!=null)
                {
                    Log.e("register", result);
                }

            }
        });
        thread.start();
    }
}
