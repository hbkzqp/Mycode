package stpaul.biblesharing;

import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import stpaul.biblesharing.HttpClient.EmailVerifyHttpSession;
import stpaul.biblesharing.HttpClient.ToolFunction;


public class ForgetPasswordActivity extends ActionBarActivity {
    private EditText usernameText ;
    private Button verifyBtn;
    private EditText verifyText;
    private EditText passwordText;
    private EditText c_passwordText;
    private Button resetPasswordBtn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_forget_password);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_forget_password, menu);
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
    private void initTextArea()
    {
        usernameText = (EditText) findViewById(R.id.FUserNameArea);
        passwordText = (EditText) findViewById(R.id.FPasswordArea);
        c_passwordText = (EditText) findViewById(R.id.FCPasswordArea);
        verifyText  = (EditText) findViewById(R.id.FVerifyArea);
    }
    private  void initVerifyBtn()
    {
        verifyBtn = (Button) findViewById(R.id.Get_VerificationBtn);
        if(!ToolFunction.emailCheck(usernameText.getText().toString()))
        {
            return;
        }
        verifyBtn.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                if(!ToolFunction.emailCheck(usernameText.getText().toString()))
                {
                    Toast.makeText(ForgetPasswordActivity.this, "Username should be Email", Toast.LENGTH_LONG).show();
                    return;
                }
                Toast.makeText(ForgetPasswordActivity.this, "Please check your username Email....", Toast.LENGTH_LONG).show();
                Thread thread = new Thread(new Runnable()
                {
                    @Override
                    public void run()
                    {
//                        String username = usernameText.getText().toString();
//                        EmailVerifyHttpSession emailVerifyHttpSession = new EmailVerifyHttpSession();
//                        String result = emailVerifyHttpSession.EmailVerify(username);
                    }
                });
            }
        });
    }
}
