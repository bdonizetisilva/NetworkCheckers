using System.Windows.Forms;
using NetworkCheckers.Rede.Servicos;

namespace NetworkCheckers
{
    public partial class Form1 : Form
    {
        private ServerPublisher _Publisher;

        public Form1()
        {
            InitializeComponent();

            ServiceInfo service = new ServiceInfo("Dama", 87);

            
            ServerInfo info = new ServerInfo();

            info.Add(service);

            _Publisher = new ServerPublisher(info, 60);
            
        }
    }
}
