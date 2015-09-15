package stpaul.biblesharing;

import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import stpaul.biblesharing.HttpClient.Data.AccountBackInfo;
import stpaul.biblesharing.HttpClient.HttpSession;
import stpaul.biblesharing.HttpClient.LoginHttpSession;
import stpaul.biblesharing.HttpClient.RegisterHttpSession;
import stpaul.biblesharing.HttpClient.ToolFunction;


public class RegisterActivity extends ActionBarActivity {

    private EditText usernameArea;
    private EditText passwordArea;
    private EditText c_passwordArea;
    private Button registerBtn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        setTextArea();
        setRBtn();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_register, menu);
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
    private void setTextArea()
    {
        usernameArea = (EditText) findViewById(R.id.RUserNameArea);
        passwordArea = (EditText) findViewById(R.id.RPasswordArea);
        c_passwordArea = (EditText) findViewById(R.id.RCPasswordArea);
    }
    private void setRBtn()
    {
        registerBtn = (Button) findViewById(R.id.RRegisterBtn);
        registerBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v)
            {
                if(!ToolFunction.emailCheck(usernameArea.getText().toString()))
                {
                    Toast.makeText(RegisterActivity.this, "User name should be Email, Please", Toast.LENGTH_LONG).show();
                    return;
                }
                if((passwordArea.getText().length()<6))
                {
                    Toast.makeText(RegisterActivity.this, "password should be longer than 6", Toast.LENGTH_LONG).show();
                    return;
                }
                if(!(passwordArea.getText().toString().equals(c_passwordArea.getText().toString())))
                {
                    Toast.makeText(RegisterActivity.this, "password should be perfectly confirmed", Toast.LENGTH_LONG).show();
                    return;
                }
                Toast.makeText(RegisterActivity.this, "Registering....", Toast.LENGTH_LONG).show();
                final String username = usernameArea.getText().toString();
                final String password = passwordArea.getText().toString();
                Thread thread = new Thread(new Runnable() {
                    @Override
                    public void run()
                    {
                        RegisterHttpSession lhs = new RegisterHttpSession();
                        String result = lhs.Register(username, password);

                        if(result!=null)
                        {
                            final AccountBackInfo anbi = HttpSession.gson.fromJson(result,AccountBackInfo.class);
                            if(anbi.status==0)
                            {
                                ToolFunction.handler.post(new Runnable() {
                                    @Override
                                    public void run() {
                                        Intent intent = new Intent(RegisterActivity.this, BibleActivity.class);
                                        startActivity(intent);
                                    }
                                });
                            }
                            else
                            {
                                ToolFunction.handler.post(new Runnable() {
                                    @Override
                                    public void run() {
                                        Toast.makeText(RegisterActivity.this, anbi.message, Toast.LENGTH_LONG).show();
                                    }
                                });

                            }
                        }

                    }
                });
                thread.start();
            }
        });
    }
}
