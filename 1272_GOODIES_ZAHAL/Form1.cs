
using _1272_GOODIES_ZAHAL.Email;
using GoodiModels_1272.DataModel;
using GoodiModels_1272.RestFull;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1272_GOODIES_ZAHAL
{
    public partial class Form1 : Form
    {
        ManualResetEvent m_stopThreadsEvent = new ManualResetEvent(false);

        private readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Form1));
        private readonly int _timeToWait = int.Parse(ConfigurationManager.AppSettings["timeToWait"]);
        private readonly int _textBoxLines = int.Parse(ConfigurationManager.AppSettings["textBoxLines"]);
        public Form1()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = false;
            StopBtn.Enabled = true;
            m_stopThreadsEvent = new ManualResetEvent(false);
            Thread t1 = new Thread(new ThreadStart(Run));
            t1.Start();
        }


        [Obsolete("Message")]
        private void Run()
        {
            AppendText("Start Running");
            while (true)
            {
                try
                {
                   
                    Token token = GoodiModels_1272.DataBase.DBParser.Instance().GetToken();
                    if (token == null || token.Stemp_Tar < DateTime.Now || !RestApi.Instance().IsTokenValid(token.TokenNumber))
                    {
                        token = RestApi.Instance().GetNewToken();
                        if (token !=null)
                        {
                            bool res = GoodiModels_1272.DataBase.DBParser.Instance().UpdateToken(token);
                        }
                        else
                        {
                            AppendText("Did not susseeded get token");
                            throw new Exception("Did not susseeded get token");
                        }
                    }
                   
                    IEnumerable<ExecuteTransaction> transactions = GoodiModels_1272.DataBase.DBParser.Instance().GetAllTransactions();
                    List<ExecuteTransaction> updateOk = new List<ExecuteTransaction>();
                    List<ExecuteTransaction> errorUpdate = new List<ExecuteTransaction>();
                    foreach (ExecuteTransaction transaction in transactions)
                    {
                        AppendText("Start transaction Goodi system");
                        string errorMessage = "";
                        string errorCode = "";
                        bool isUpdate = false;
                        ExecuteTransactionResponse executeTransactionResponse = null;
                        bool result = RestApi.Instance().ExecuteTransaction(transaction, token.TokenNumber,out errorMessage, out errorCode, out executeTransactionResponse);
                        if (!result)
                        {
                            transaction.ErrorMessage = $"Station order  {transaction.Station_order} did not succeeded update in Goodi system";
                            AppendText(errorMessage);
                            isUpdate = GoodiModels_1272.DataBase.DBParser.Instance().UpdateTransaction(transaction.Id, errorMessage,errorCode);
                            errorUpdate.Add(transaction);
                        }
                        else
                        {
                            AppendText($"Successfully update Station order {transaction.Station_order}  in goodi system");
                            isUpdate = GoodiModels_1272.DataBase.DBParser.Instance().UpdateTransaction(transaction.Id, transactionResponse: executeTransactionResponse);
                            updateOk.Add(transaction);
                        }
                        if (isUpdate)
                        {
                            AppendText($"Successfully update Station order status {transaction.Station_order}  in data base"); 
                        }
                        else
                        {
                            transaction.ErrorMessage = $"Station order {transaction.Station_order} did not succeeded update order status in Data base";
                            AppendText(transaction.ErrorMessage);
                        }
                        AppendText("Finish Transaction to Goodi system");
                    }
                    if (updateOk.Count > 0 || errorUpdate.Count > 0)
                    {
                        AppendText($"{updateOk.Count} selers where update and {errorUpdate.Count} selers getting error");
                        //send mail
                        string title = $" ממשקי גודי דלקנים של צהל";
                        string body = $"<div  style='direction:rtl' > שלום  <br /> מספר התדלוקים של צהל שעודכנו בגודי {updateOk.Count} <br /> מספר התדלוקים של צהל שלא עדכנו בגודי {errorUpdate.Count} </div>";
                        if (errorUpdate.Count > 0)
                        {
                            string errorSeler = "<div  style='direction:rtl' ><br />תדלוקים שלא עודכנו בגודי<br /></div>";
                            foreach (ExecuteTransaction transaction in errorUpdate)
                            {
                                errorSeler += $"<div  style='direction:rtl' ><br />{transaction.ToString()}<br /></div>";
                            }
                            body += errorSeler;
                        }
                        SendMail.Instance().Email(body, title);
                    }
                    
                }
                catch (Exception ex)
                {
                    AppendText($"Error when try to update selers, error message: {ex.Message}");
                }
                if (m_stopThreadsEvent.WaitOne(_timeToWait))
                {
                    break;
                }
            }
        }

        delegate void AppendTextDelegate(string text);

        private void AppendText(string text)
        {
            if (LogtextBox.InvokeRequired)
            {
                LogtextBox.Invoke(new AppendTextDelegate(this.AppendText), new object[] { text });
            }
            else
            {
                LogtextBox.Text += $"{DateTime.Now.ToString()}: {text}\r\n";
                log.Debug(text);
                if (LogtextBox.Lines.Count() > _textBoxLines)
                {
                    LogtextBox.Lines = LogtextBox.Lines.Skip(1).ToArray();
                }

            }
        }


        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopBtn.Enabled = false;
            StartBtn.Enabled = true;
            m_stopThreadsEvent.Set();
            AppendText("Stop Co9nnection to Goodi system");
        }

        private void OpenLogBtn_Click(object sender, EventArgs e)
        {
            string FileName = "";
            try
            {
                FileName = @"c:\Log\" + DateTime.Now.ToString("yyyy-MM-dd") + "_GoodiSystem.txt";//ClsApi.FldLog + "\\Log" + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "notepad++.exe";
                proc.StartInfo.Arguments = FileName;
                proc.Start();
            }
            catch (Exception ex)
            {

                log.Error($"Rrror when try to open log {FileName}, error message: {ex.Message}");

            }
        }

        private void OpenLogFolderBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = "";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = @"c:\Log\";
                proc.StartInfo.Arguments = FileName;
                proc.Start();
            }
            catch (Exception ex)
            {
                log.Error($"Rrror when try to open log folder c:\\Log\\, error message: {ex.Message}"); ;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_stopThreadsEvent.Set();
            Thread.Sleep(2000);
        }
    }
}
