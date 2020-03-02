using System;
using System.Windows.Forms;

namespace ValidarCPF_CNPJ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string mensagem = "";

        private void btnValidar_Click_1(object sender, EventArgs e)
        {
            string valor = maskValor.Text;
            if (rdb_cnpj.Checked == false && rdb_cpf.Checked == false)
            {
                toolTip1.ToolTipTitle = "Nenhum número informado";
                toolTip1.Show("Por favor selecione um tipo de documento e insira seu número para validação.", maskValor, maskValor.Location, 2000);
            }
            else if (rdb_cnpj.Checked == true || rdb_cpf.Checked == true)
            {
                if (maskValor.MaskFull == false)
                {
                    toolTip1.ToolTipTitle = "Documento inválido";
                    toolTip1.Show("Número do documento deve ser preenchido corretamente.", maskValor, maskValor.Location, 2000);
                    maskValor.Focus();
                    maskValor.Text = "";
                }
                else 
                {
                    if (rdb_cnpj.Checked == true)
                    {
                        if (Validacao.ValidaCNPJ.IsCnpj(valor))
                        {
                            mensagem = "O número é um CNPJ Válido!";
                            maskValor.Focus();
                            maskValor.SelectAll();
                        }
                        else
                        {
                            mensagem = "O número é um CNPJ Inválido!";
                            maskValor.Focus();
                            maskValor.SelectAll();
                        }
                    }
                    else if (rdb_cpf.Checked == true)
                    {
                        if (Validacao.ValidaCPF.IsCpf(valor))
                        {
                            mensagem = "O número é um CPF Válido!";
                            maskValor.Focus();
                            maskValor.SelectAll();
                        }
                        else
                        {
                            mensagem = "O número é um CPF Inválido!";
                            maskValor.Focus();
                            maskValor.SelectAll();
                        }
                    }
                    MessageBox.Show(mensagem, "Retorno");
                }
            }
        }

        void maskValor_MaskInputRejected_1(object sender, MaskInputRejectedEventArgs e)
        {
            if (rdb_cnpj.Checked == false || rdb_cpf.Checked == false)
            {
                toolTip1.ToolTipTitle = "Número informado inválido";
                toolTip1.Show("Somente digitos de (0-9) são permitidos.", maskValor, maskValor.Location, 2000);
            }
        }

        private void rdb_cnpj_CheckedChanged_1(object sender, EventArgs e)
        {
            maskValor.Text = "";
            maskValor.Mask = "00,000,000/0000-00";
            this.maskValor.Size = new System.Drawing.Size(150, 20);
            maskValor.MaskInputRejected += new MaskInputRejectedEventHandler(maskValor_MaskInputRejected_1);
            if (maskValor.Visible == false || maskValor.CanFocus)
            {
                maskValor.Visible = true;
                maskValor.Focus();
                maskValor.Select(0, 0);
            }
        }

        private void rdb_cpf_CheckedChanged(object sender, EventArgs e)
        {
            maskValor.Text = "";
            maskValor.Mask = "000,000,000-00";
            maskValor.MaskInputRejected += new MaskInputRejectedEventHandler(maskValor_MaskInputRejected_1);
            this.maskValor.Size = new System.Drawing.Size(120, 20);
            if (maskValor.Visible == false || maskValor.CanFocus)
            {
                maskValor.Visible = true;
                maskValor.Focus();
                maskValor.Select(0, 0);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            if (maskValor.Text != "")
            {
                maskValor.Text = "";
                maskValor.Focus();
                maskValor.Select(0, 0);
            }
            else if (maskValor.Text == "" && maskValor.Visible == true)
            {
                rdb_cnpj.Checked = false;
                rdb_cpf.Checked = false;
                this.maskValor.Visible = false;
            }
            else if (maskValor.Visible == false)
            {
                toolTip1.ToolTipTitle = "Procedimento inválido";
                toolTip1.Show("Não há informação a ser limpa.", maskValor, maskValor.Location, 2000);
            }
        }

        private void btnLimpar_MouseEnter(object sender, EventArgs e)
        {
            if (rdb_cpf.Checked == true)
            {
                toolTip1.ToolTipTitle = "Procedimento inválido";
                toolTip1.Show("Não há informação a ser limpa.", maskValor, maskValor.Location, 2000);
            }
        }
        private void maskValor_MouseEnter(object sender, EventArgs e)
        {
            if (rdb_cpf.Checked == true && maskValor.Text == "")
            {
                toolTip1.ToolTipTitle = "Preenchimento CPF";
                toolTip1.Show("CPF deve ser preenchido com 11 digitos.", maskValor, maskValor.Location, 2000);
            }
            else if (rdb_cnpj.Checked == true && maskValor.Text == "")
            {
                toolTip1.ToolTipTitle = "Preenchimento CNPJ";
                toolTip1.Show("CNPJ deve ser preenchido com 14 digitos.", maskValor, maskValor.Location, 2000);
            }
        }
    }
}