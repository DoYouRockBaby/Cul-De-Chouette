using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        NetworkClient NetworkClient = new NetworkClient();
        public Form1()
        {
            InitializeComponent();
            NetworkClient.OnMessage = OnMessage;
            NetworkClient.Start("127.0.0.1", 8888);
        }

        public void OnMessage(string message)
        {
            ListBox.Items.Add(message);
        }

        private void GrelotteCaPicoteButton_Click(object sender, EventArgs e)
        {
            NetworkClient.Send("Grelotte ca picote !");
        }

        private void PasMouLeCaillouButton_Click(object sender, EventArgs e)
        {
            NetworkClient.Send("Pas mou le caillou !");
        }

        private void ThrowCubesButton_Click(object sender, EventArgs e)
        {
            NetworkClient.Send("Lancer les des");
        }
    }
}
