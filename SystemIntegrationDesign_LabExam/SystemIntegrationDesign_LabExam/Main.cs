using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml;

namespace SystemIntegrationDesign_LabExam
{
    public partial class Main : Form
    {
        public Main()
        {   
            InitializeComponent();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
          


            channel.QueueDeclare(queue: "convert",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            if(xmlInput.Text.ToString().Equals(""))
            {
                MessageBox.Show("invalid input", "Invalid Input",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xmlInput.Text.Trim().ToString());

                    string jsonData = JsonConvert.SerializeXmlNode(xmlDocument);

                    var body = Encoding.UTF8.GetBytes($"{jsonData}");
                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: "convert",
                                         basicProperties: null,
                                         body: body);
                } catch(Exception err)
                {
                    MessageBox.Show("invalid XML Format", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

           
            
            
        }
    }
}
