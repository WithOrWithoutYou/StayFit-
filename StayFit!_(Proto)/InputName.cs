using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StayFit___Proto_
{
    public partial class AddColumns : Form
    {
        public AddColumns(string sTitle = "input")
        {

            //lblInput.Text = sTitle;      // 초기화되지 않은 상태에서 값을 전하는 우를 범하였구나
            STITLE = sTitle;
            InitializeComponent();
        }
        private void AddColumns_Load(object sender, EventArgs e)
        {
            lblInput.Text = STITLE;
        }

        public string sInput; // " " 
        string STITLE;

        public void btnInput_Click(object sender, EventArgs e)
        {
            if (tbInput.Text != null)
            {
                this.DialogResult = DialogResult.OK;
                sInput = tbInput.Text;
            }
            else this.DialogResult = DialogResult.Cancel; // 이건 뭘까...
            this.Close(); // 폼을 닫는 명령어 입니다. // 이걸 해주면 연결된 버튼을누를 때 닫기 수행 // 아하~
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {/*
            if (e.KeyCode != Keys.Enter) return; // 

            if (e.KeyCode == Keys.Enter)
            {
                if (tbInput.Text != null)
                {
                    this.DialogResult = DialogResult.OK;
                    sInput = tbInput.Text;
                }
                else this.DialogResult = DialogResult.Cancel;
            }
            */
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                sInput = tbInput.Text;
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                Close();
            }



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
                
        }

        private void btnFileNew_Click(object sender, EventArgs e)
        {
            if (tbInput.Text != null)
            {
                this.DialogResult = DialogResult.OK;
                sInput = tbInput.Text;
            }
            else this.DialogResult = DialogResult.Cancel; // 이건 뭘까...
            this.Close(); // 폼을 닫는 명령어 입니다. // 이걸 해주면 연결된 버튼을누를 때 닫기 수행 // 아하~
        }
    }
}
