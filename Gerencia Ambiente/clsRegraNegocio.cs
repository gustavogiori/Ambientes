using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Gerencia_Ambiente
{
    public class clsRegraNegocio
    {


        public void listarAmbiente(ComboBox cbAmbiente)
        {
            cbAmbiente.DataSource = Directory.GetDirectories(@"C:\totvs");
        }

        public string retornarAplicativo(string arquivo)
        {
            if (arquivo.Contains("Incorporação") || arquivo.Contains("SGI") || arquivo.Contains("Incorporacao"))
            {
                return "SGI";
            }

            if (arquivo.Contains("Projetos") || arquivo.Contains("Solum") || arquivo.Contains("Obras"))
            {
                return "Solum";
            }

            if (arquivo.Contains("Pessoas") || arquivo.Contains("Vitae") || arquivo.Contains("PESSOAS"))
            {
                return "Vitae";
            }

            else if (arquivo.Contains("Pagamento") || arquivo.Contains("Labore") || arquivo.Contains("PAGAMENTO") || arquivo.Contains("pagamento"))
            {
                return "Labore";
            }

            else if (arquivo.Contains("Ponto") || arquivo.Contains("Chronus") || arquivo.Contains("PONTO") || arquivo.Contains("ponto"))
            {

                return "Chronus";
            }
            else
            {
                return "";
            }
        }

        public void habilitarEspecifica(CheckBox cbEspecifica, List<Label> lbls, List<TextBox> txts, List<Button> btns, List<CheckBox> checks)
        {

            if (cbEspecifica.Checked)
            {

                for (int i = 0; i < checks.Count; i++)
                {
                    if (checks[i].Name == "checkBiblioteca")
                    {
                        if (checks[i].Checked)
                        {


                            txts[0].Visible = true;
                            lbls[0].Visible = true;
                            btns[0].Visible = true;
                        }

                        else
                        {
                            txts[0].Visible = false;
                            lbls[0].Visible = false;
                            btns[0].Visible = false;
                        }
                    }
                    if (checks[i].Name == "checkLabore")
                    {

                        if (checks[i].Checked)
                        {
                            txts[1].Visible = true;
                            lbls[1].Visible = true;
                            btns[1].Visible = true;
                        }

                        else
                        {
                            txts[1].Visible = false;
                            lbls[1].Visible = false;
                            btns[1].Visible = false;
                        }
                    }


                    if (checks[i].Name == "checkPortal")
                    {
                        if (checks[i].Checked)
                        {
                            txts[2].Visible = true;
                            lbls[2].Visible = true;
                            btns[2].Visible = true;
                        }
                        else
                        {
                            txts[2].Visible = false;
                            lbls[2].Visible = false;
                            btns[2].Visible = false;
                        }
                    }
                }

            }
            else
            {
                for (int i = 0; i < checks.Count; i++)
                {
                    txts[i].Visible = false;
                    lbls[i].Visible = false;
                    btns[i].Visible = false;
                }
            }
        }


        public bool verificarAmbienteExiste(string produto, string nomeAmbiente)
        {
            Util.clsUtil util = new Util.clsUtil();
            bool diretorioExiste = false;

            if (produto == "Folha de Pagamento")
            {
                if (Directory.Exists(string.Format(@"C:\totvs\{0}\RMLabore", nomeAmbiente)))
                {

                    util.mensagemErro("Diretorio já existe , Caso queira poderá atualizar o mesmo usando o menu de atualizar ambiente.");
                    diretorioExiste = true;
                }
            }

            else if (produto == "Portal")
            {
                if (Directory.Exists(string.Format(@"C:\totvs\{0}\Corpore.Net", nomeAmbiente)))
                {
                    util.mensagemErro("Diretorio já existe , Caso queira poderá atualizar o mesmo usando o menu de atualizar ambiente.");
                    diretorioExiste = true;
                }

            }

            else
            {

                if (Directory.Exists(string.Format(@"C:\totvs\{0}\Corpore.Net", nomeAmbiente)))
                {
                    util.mensagemErro("Diretorio já existe , Caso queira poderá atualizar o mesmo usando o menu de atualizar ambiente.");
                    diretorioExiste = true;
                }

            }
            return diretorioExiste;
        }


        public void verificarVisivel(TextBox txtAtual, Button btnAtual, CheckBox checkAtual, Label lblAtual)
        {
            if (checkAtual.Checked)
            {
                txtAtual.Visible = true;
                btnAtual.Visible = true;
                lblAtual.Visible = true;
            }
            else
            {
                txtAtual.Visible = false;
                btnAtual.Visible = false;
                lblAtual.Visible = false;
            }
        }

    }
}
