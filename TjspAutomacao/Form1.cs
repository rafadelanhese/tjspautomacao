using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TjspAutomacao
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            InicializaDataGridView();
        }   
        
        private void InicializaDataGridView()
        {
            dgvProcessos.Columns.Add("Núm. Processo", "Núm. Processo");
            dgvProcessos.Columns.Add("Foro", "Foro");
            dgvProcessos.Columns.Add("Competência", "Competência");
        }
    }
}
