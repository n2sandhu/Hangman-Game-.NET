using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;

using System.Text.RegularExpressions;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    string word;
    string maskedWord;
    int attemptCount = 5;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            game();
        }
        else {
           word = (string)ViewState["word"];
           maskedWord = (string)ViewState["maskedWord"];
           attemptCount = (int)ViewState["attemptCount"];            
        }
    }

    protected void Page_PreRender(Object sender, EventArgs e)
    {
        ViewState["word"] = word;
        ViewState["maskedWord"] = maskedWord;
        ViewState["attemptCount"] = attemptCount;
    }


    protected void game()
    {
        A.Enabled = true;        B.Enabled = true;        C.Enabled = true;        D.Enabled = true;        E.Enabled = true;
        F.Enabled = true;        G.Enabled = true;        H.Enabled = true;        I.Enabled = true;        J.Enabled = true;
        K.Enabled = true;        L.Enabled = true;        M.Enabled = true;        N.Enabled = true;        O.Enabled = true;
        P.Enabled = true;        Q.Enabled = true;        R.Enabled = true;        S.Enabled = true;        T.Enabled = true;
        U.Enabled = true;        V.Enabled = true;        W.Enabled = true;        X.Enabled = true;        Y.Enabled = true;
        Z.Enabled = true;        Button1.Enabled = false;
        attemptCount = 5;
        Label2.Text = "Number Of attempts Left: ";
        ArrayList words = new ArrayList();
        Random random = new Random();
        try
        {
            Array userData = File.ReadAllLines(Server.MapPath("TextFile.txt"));
            foreach (string dataLine in userData) 
            {
                dataLine.ToString();
                words.Add(dataLine);
            }
            string[] wordss = words.ToArray(typeof(string)) as string[];
            word = wordss[random.Next(wordss.Length)];
            maskedWord = word;
            maskedWord = Regex.Replace(word, "[A-z]", "-");
            Label1.Text = maskedWord;
            Label3.Text = attemptCount.ToString();
            Label4.Text = "";
           
        }
        catch (Exception ex)
        {
            Label1.Text = "Cannot read the file "+ ex;
        }
       
    }
    protected void LetterGuessed(object sender, EventArgs e)
    {

        if (attemptCount != 0 && (Label4.Text.Equals("")))
        {
            string letter = ((Control)sender).ID.ToString().ToLower();
            Button b1 = (Button)sender;
            b1.Enabled = false;
            int startPosition = word.IndexOf(letter);
            if (!(startPosition == -1))
            {
                do
                {
                    maskedWord = maskedWord.Remove(startPosition, 1).Insert(startPosition, letter);
                    Label1.Text = maskedWord;
                    startPosition = word.IndexOf(letter, startPosition + 1);
                }
                while (startPosition != -1);
            }
            else
            {
                attemptCount--;
                Label3.Text = attemptCount.ToString();
            }
            if (maskedWord.Equals(word))
            {
                Label1.Text = word;
                Label4.Text = "You Win";
                Button1.Enabled = true;
                Label2.Text = "You won the game with " + (attemptCount).ToString() + " attempts left";
                Label3.Text = ""; 
            }

            if (attemptCount == 0 && !(maskedWord.Equals(word)))
            {
                Label4.Text = "You Lost";
                Label1.Text = word;
                Button1.Enabled = true;
                Label2.Text = "No more attempts left";
                Label3.Text = "";
            }

        }
        else
        { 
            //Do nothing on letter button click
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        game();
    }
}