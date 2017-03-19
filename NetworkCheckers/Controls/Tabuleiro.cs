using System.ComponentModel;
using System.Windows.Forms;


namespace NetworkCheckers.Controls
{
    public partial class Tabuleiro : UserControl
    {
        private delegate void RefreshDelegate();

        private delegate void RenderMoveDelegate();

        public Tabuleiro()
        {
            InitializeComponent();
        }

        public Tabuleiro(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
