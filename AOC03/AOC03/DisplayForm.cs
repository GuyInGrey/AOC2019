using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AOC03
{
    public partial class DisplayForm : Form
    {
        public List<(int, int)> Path1;
        public List<(int, int)> Path2;
        public Bitmap Image;

        public DisplayForm(List<(int,int)> line1, List<(int, int)> line2)
        {
            InitializeComponent();
            Path1 = line1;
            Path2 = line2;
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {

        }
    }
}
