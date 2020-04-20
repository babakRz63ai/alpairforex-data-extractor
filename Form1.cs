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
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using WebSocket4Net;

using alpairdotcomextractor.models;

namespace alpairdotcomextractor
{
    public partial class Form1 : Form
    {
        private const string webSocketURI = "wss://alpariforex.org/bo_guest/";
        
        // Use this offset to convert Unix time into .Net time
        // private const long InitialUnixDateTicks = 621355968000000000;
        
        private WebSocket ws;
        //private JsonSerializerSettings jsonSettings;
        private int requestID;
        
        private bool isRunning = false;


        public Form1()
        {
            InitializeComponent();
            ws = new WebSocket(webSocketURI);
            ws.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            ws.Closed += new EventHandler(websocket_Closed);
            //jsonSettings = new JsonSerializerSettings();
        }
        //-------------------------------------------
        private void websocket_Closed()
        {
        	btnStartStop.Text = "Start";
            btnStartStop.Enabled = true;
        }
		//-------------------------------------------
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!isRunning) {
            	btnStartStop.Text = "Stop";
            	isRunning = true;
            	openAndReadSocket();
            }
            else
            {
            	isRunning = false;
            	btnStartStop.Text = "Wait...";
            	btnStartStop.Enabled = false;
            	ws.Close();
            }
            
        }
        //-------------------------------------------
        private void displayData(RatesAll rates)
        {
            dataGridView.Rows.Clear();
            
            for (int i=0;i<rates.Data.length;i++)
            {
            	dataGridView.Rows.Add(rates.Data[i][0], rates.Data[i][1]);
            }
            
        }
        //-------------------------------------------
        private void showConnectionError(string msg)
        {
            MessageBox.Show(msg, "Error on connecting");
            Invoke(new Action(() => { 
                btnStartStopStop.Text = "Start";
                UseWaitCursor = false;
            }));
           
        }
        //-----------------------------------
        
        private void sendRequest(string data)
        {

            while (this.ws.State == WebSocketState.Connecting) { };
            if (this.ws.State != WebSocketState.Open)
            {
                showConnectionError("Connection is not open.");
                return;
            }
           	
            ws.Send(data);
#if DEBUG
            Console.WriteLine("The request has been sent: ");
            Console.WriteLine(data);
            Console.WriteLine("\r\n\r\n");
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
            
            requestID = 1;
            
            #if DEBUG
            Console.WriteLine("The connection is established!\r\n");
            #endif
        }
        //-----------------------------------

        private async Task openAndReadSocket()
        {
            
            if (txtUsername.Text.Length==0)
            {
            	errorProvider.SetError(txtUsername,"Provide your user name");
            	return;
            }
            else
            	errorProvider.SetError(txtUsername,null);
            
            if (txtPassword.Text.Length==0)
            {
            	errorProvider.SetError(txtPassword,"Provide your password");
            	return;
            }
            else
            	errorProvider.SetError(txtPassword,null);
            	
            // TODO run HTTP requests(s) to authenticate and get a valid token
            
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

        private string generateAuth(string token, long instance_id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            JsonTextWriter writer = new JsonTextWriter(new StringWriter(stringBuilder));
            writer.WriteStartObject();
            writer.WritePropertyName("rid");
            writer.WriteValue(requestID++);
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
            writer.WriteValue(instance_id);
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WritePropertyName("type");
            writer.WriteValue("REQUEST");
            writer.WritePropertyName("v");
            writer.WriteValue(3);
            writer.WriteEndObject();                   
            return stringBuilder.ToString();
       
        }
        

        private void websocket_MessageReceived(object skrewer, MessageReceivedEventArgs eventargs)
        {     
            
            JObject message = JObject.Parse(eventargs.Message);
            
            if (!message.ContainsKey("type"))
            	return;
            JToken typeToken = message["type"];
            if (!"EVENT".Equals(typeToken)
            	return;
            	
            if (!message.ContainsKey("action"))
            	return;
            JToken actionToken = message["action"];
            if (!"rates/all".Equals(actionToken))
            	return;
            
            if (!message.ContainsKey("body"))
            	return;
            JToken body = message["body"];
            if (body==null)
            	return;
            	
           	RatesAll rates = body.ToObject<RatesAll>();
           	if (rates==null || rates.Data==null)
           		return;
           	
           	Invoke(new Action(() =>
                {
                    displayData(rates);
                }
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            dataGridView.SelectAll();
        }
    }   
}
