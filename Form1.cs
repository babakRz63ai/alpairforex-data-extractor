using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Net.WebSockets;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using WebSocket4Net;

namespace binarydotcomanalyzer
{
    public partial class Form1 : Form
    {
        private const string uri = "wss://alpariforex.org/bo_guest/";
        
        private WebSocket ws;
        private TickItem[] itemsBuffer;
        private int itemsBottom = 0;
        private JsonSerializerSettings jsonSettings;
        private string marketSymbol;

        public Form1()
        {
            InitializeComponent();
            ws = new WebSocket(uri);
            ws.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            jsonSettings = new JsonSerializerSettings();
            //jsonSettings.DateParseHandling = DateParseHandling.
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (ticksCount.Value<1)
            {
                errorProvider.SetError(ticksCount, "set a non-zero value");
                return;
            }

            if (symbolSelector.SelectedItem==null)
            {
                errorProvider.SetError(symbolSelector, "Choose on of the symbols");
                return;
            }

            btnStart.Enabled = false;
            panel1.Visible = false;
            UseWaitCursor = true;
            progressBar.Value = 0;
            progressBar.Maximum = (int)ticksCount.Value;
            progressBar.Visible = true;
            marketSymbol = symbolSelector.SelectedItem.ToString();
            string endTickStr = textBoxEndTick.Text.Trim();
            if (endTickStr.Equals("latest"))
                getHistory(marketSymbol, (int)ticksCount.Value, -1);
            else
            {
                long endTick;
                if (long.TryParse(endTickStr, out endTick) && endTick>=0)
                    getHistory(marketSymbol, (int)ticksCount.Value, endTick);
                else
                    errorProvider.SetError(textBoxEndTick, "Invalid tick value");
            }
        }
        //-------------------------------------------
        private void analyzeData()
        {
            dataGridView.Rows.Clear();
            foreach (TickItem tick in itemsBuffer)
            {
                DataGridViewRow row = new DataGridViewRow();
                dataGridView.Rows.Add(dataGridView.Rows.Count + 1,
                    tick.Tick.ToString(),
                    tick.Time.ToString(), tick.Price);
            }
            panel1.Visible = true;
            btnStart.Enabled = true;
            UseWaitCursor = false;
            progressBar.Visible = false;
        }
        //-------------------------------------------
        private void showConnectionError(string msg)
        {
            MessageBox.Show(msg, "Error on connecting");
            Invoke(new Action(() => { 
                btnStart.Enabled = true;
                UseWaitCursor = false;
                progressBar.Visible = false;
            }));
           
        }
        //-----------------------------------
        

        private void sendRequest(string data)
        {

            while (this.ws.State == WebSocketState.Connecting) { };
            if (this.ws.State != WebSocketState.Open)
            {
               
                throw new Exception("Connection is not open.");

            }
            else
            {
                MessageBox.Show("asdasd");
            }

            ws.Send(data);
#if DEBUG
            Console.WriteLine("The request has been sent: ");
            Console.WriteLine(data);
            Console.WriteLine("\r\n \r\n");
#endif
        }
        //-----------------------------------

       
        //-----------------------------------

        private void ConnectWebsocket()
        {
            #if DEBUG
            Console.WriteLine("Prepare to connect to: " + uri.ToString());
            Console.WriteLine("\r\n");
            #endif

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            ws.Open();
            while (ws.State == WebSocketState.Connecting) ;
            #if DEBUG
            Console.WriteLine("The connection is established!\r\n");
           #endif
        }
        //-----------------------------------

        private async Task getHistory(string symbol, int count, long endTick)
        {
            itemsBottom = count-1;
            itemsBuffer = new TickItem[count];
            if (ws.State != WebSocketState.Open)
                ConnectWebsocket();

            if (ws.State != WebSocketState.Open)
            {
                showConnectionError("Connection is not open.");
                return;
            }

            // the first part of history
            sendRequest(generateAuth("pe939d733363684f131eb0ae1f98a25be39c55064",86633458 ));

           
        }
        //-----------------------------------

        private string generateAuth(string token, long id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            JsonTextWriter writer = new JsonTextWriter(new StringWriter(stringBuilder));
            writer.WriteStartObject();
            writer.WritePropertyName("rid");
            writer.WriteValue(3);
            writer.WritePropertyName("action");
            writer.WriteValue("auth");
            writer.WritePropertyName("body");
            writer.WriteStartObject();
            writer.WritePropertyName("token");
            writer.WriteValue(token);
            writer.WritePropertyName("client");
            writer.WriteStartObject();
            writer.WritePropertyName("app");
            writer.WriteValue("gtt.web.wl");
            writer.WritePropertyName("version");
            writer.WriteValue("2.2+49");
            writer.WritePropertyName("timezone_offset");
            writer.WriteValue(10800);
            writer.WritePropertyName("instance_id");
            writer.WriteValue(id);      
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WritePropertyName("type");
            writer.WriteValue("REQUEST");
            writer.WritePropertyName("v");
            writer.WriteValue(3);
            writer.WriteEndObject();                   
            return stringBuilder.ToString();
       
        }
        
        static readonly long InitialJavaScriptDateTicks = 621355968000000000;

        private void websocket_MessageReceived(object skrewer, MessageReceivedEventArgs eventargs)
        {
           
            HistoryMessage history = JsonConvert.DeserializeObject<HistoryMessage>(eventargs.Message);
            int count = history.History.Prices.Length;
            for (int i=1;i<=count;i++)
                itemsBuffer[i+itemsBottom-count] = new TickItem {
                    Price = history.History.Prices[i-1],
                    Tick = history.History.Times[i-1],
                    Time = new DateTime(InitialJavaScriptDateTicks + 10000000 * history.History.Times[i-1], DateTimeKind.Utc)
                };
            itemsBottom -= count;

            // request for rest of them or show the results
            if (itemsBottom > 0) {
                Invoke(new Action(() =>
                {
                    progressBar.Value += count;
                }));
                sendRequest(generateAuth("pe939d733363684f131eb0ae1f98a25be39c55064", 86633458));
            }
            else
                Invoke(new Action(() =>
                {
                    progressBar.Visible = false;
                    analyzeData();
                }
            ));
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            dataGridView.SelectAll();
        }
    }   
}
