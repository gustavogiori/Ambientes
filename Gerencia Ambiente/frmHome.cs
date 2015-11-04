using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;

namespace Gerencia_Ambiente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
          
        }

        private void checkBiblioteca_CheckedChanged(object sender, EventArgs e)
        {
            clsRegraNegocio regra = new clsRegraNegocio();
            regra.verificarVisivel(txtBibliotecaCriar, btnBiblioteca, checkBiblioteca, lblBibliotecaCriar);
        }

        private void checkLabore_CheckedChanged(object sender, EventArgs e)
        {
            clsRegraNegocio regra = new clsRegraNegocio();

            if (checkVersao12.Checked)
            {
                verificarVersao12Labore();
            }

            else
            {
                regra.verificarVisivel(txtLaboreCriar, btnLabore, checkLabore, lblLaboreCriar);
            }
        }

        private void checkPortal_CheckedChanged(object sender, EventArgs e)
        {
            clsRegraNegocio regra = new clsRegraNegocio();
            regra.verificarVisivel(txtPortalCriar, btnPortal, checkPortal, lblPortalCriar);
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {

            bool deuErro = false;
            Util.clsUtil util = new Util.clsUtil();

            try
            {

                clsRegraNegocio regra = new clsRegraNegocio();

                List<Control> lst = new List<Control>();
                lst.Add(txtNome);
                if (util.verificarNulo(errorProvider1, lst))
                {

                }

                else
                {
                    if (checkBiblioteca.Checked)
                    {
                        if (regra.verificarAmbienteExiste("Biblioteca", txtNome.Text))
                        {

                        }
                        else
                        {
                            if (criarAmbiente("Biblioteca"))
                            {
                                if (txtEspecificaBiblioteca.Visible == true)
                                {

                                    if (txtEspecificaBiblioteca.Text != string.Empty)
                                    {
                                        atualizar(txtNome.Text, txtEspecificaBiblioteca.Text, "Biblioteca");
                                    }


                                }

                            }
                            else
                            {
                                deuErro = true;
                                return;
                            }
                        }


                    }


                    if (checkLabore.Checked)
                    {
                        if (txtLaboreCriar.Text != string.Empty)
                        {
                            if (criarAmbiente("Aplicativo"))
                            {

                                if (txtEspecificaLabore.Visible == true)
                                {

                                    if (txtEspecificaLabore.Text != string.Empty)
                                    {
                                        atualizar(txtNome.Text, txtEspecificaLabore.Text, regra.retornarAplicativo(txtLaboreCriar.Text));

                                    }
                                }
                            }
                            else
                            {
                                deuErro = true;
                                return;
                            }
                        }
                    }
                    if (checkPortal.Checked)
                    {

                        criarAmbiente("Portal");
                        if (txtEspecificaPortal.Visible == true)
                        {

                            if (txtEspecificaPortal.Text != string.Empty)
                            {
                                atualizar(txtNome.Text, txtEspecificaPortal.Text, "Portal");

                            }

                        }
                        //  deletarDiretorio();



                        clsServico servico = new clsServico();

                        if (checkIniciarHost.Checked)
                        {
                            servico.DesinstalaServico(txtNome.Text);
                            servico.InstalaServico(txtNome.Text);
                        }

                        if (checkIniciarLabore.Checked)
                        {
                            servico.AbreAplicativo(regra.retornarAplicativo(txtLaboreCriar.Text), txtNome.Text, checkVersao12.Checked);
                        }
                    }
                }

            }
            catch (Exception ex)
            {


                util.mensagemErro(ex.Message);
            }

            finally
            {
                deletarDiretorio(@"C:\extrair");

                if (deuErro == true)
                {
                    deletarDiretorio(string.Format(@"c:\totvs\{0}", txtNome.Text));
                    progressBar1.Value = 0;
                }

            }
        }
        public void ambienteCriadoSucesso()
        {
            Util.clsUtil util = new Util.clsUtil();
            util.mensagemSucesso("Ambiente criado com sucesso!");
            progressBar1.Value = 0;
        }

        public bool criarAmbiente(string produto)
        {
            bool criou = false;
            clsOperacoes operacoes = new clsOperacoes(progressBar1, lblTeste, checkVersao12.Checked);
            clsConfiguracao config = new clsConfiguracao();
            bool versao12 = false;
            try
            {
                if (produto == "Biblioteca")
                {
                    operacoes.setTxtDiretorio(txtBibliotecaCriar);
                    operacoes.extrairInstalador(txtBibliotecaCriar.Text, @"c:\extrair");

                    versao12 = operacoes.verificarVersao12(string.Format(@"C:\extrair\WinRoot\totvs\CorporeRM\RM.Net\RM.Version.dll", txtNome.Text));
                    operacoes.DirectoryCopy(@"C:\extrair\WinRoot\totvs\CorporeRM", txtNome
                          .Text, true, false);


                    config.criarArquivoConfig(txtNome.Text, false, versao12, txtBibliotecaCriar);
                    ambienteCriadoSucesso();
                    config.deletarBroker(txtNome.Text);
                }

                if (produto == "Aplicativo")
                {
                    operacoes.setTxtDiretorio(txtBibliotecaCriar);
                    if (Directory.Exists(@"C:\extrair"))
                    {
                        operacoes.deletarDiretorio(@"c:\extrair", true);
                    }
                    operacoes.extrairInstalador(txtLaboreCriar.Text, @"c:\extrair");

                    operacoes.DirectoryCopy(@"C:\extrair\WinRoot\totvs\CorporeRM", txtNome
                         .Text, true, false);



                    config.criarArquivoConfig(txtNome.Text, true, versao12, txtLaboreCriar);


                    clsRegraNegocio regra = new clsRegraNegocio();
                    string aplicativo = regra.retornarAplicativo(txtLaboreCriar.Text);
                        File.Copy(@"C:\Gerencia  Ambiente\Ambiente\RM.Lib.Interop.dll", string.Format(@"C:\totvs\{0}\RM{1}\RM.Lib.Interop.dll ", txtNome.Text,aplicativo), true);
                    
                    ambienteCriadoSucesso();
                }

                if (produto == "Portal")
                {
                    operacoes.setTxtDiretorio(txtPortalCriar);
                    if (Directory.Exists(@"C:\extrair"))
                    {
                        operacoes.deletarDiretorio(@"c:\extrair", true);
                    }
                    operacoes.extrairInstalador(txtPortalCriar.Text, @"c:\extrair");

                    operacoes.DirectoryCopy(@"C:\extrair\WinRoot\totvs\CorporeRM", txtNome
                      .Text, true, false);


                    config.criarArquivoConfig(txtNome.Text, false, checkVersao12.Checked, txtPortalCriar);
                    config.deletarBroker(txtNome.Text);
                    ambienteCriadoSucesso();
                }
                criou = true;
            }
            catch (Exception ex)
            {
                criou = false;
                MessageBox.Show(ex.Message);
            }

            return criou;
        }

        public int retornarQuantidadeTextBox()
        {
            List<TextBox> lst = new List<TextBox>();
            lst.Add(txtEspecificaBiblioteca);
            lst.Add(txtEspecificaLabore);
            lst.Add(txtEspecificaPortal);
            clsConfiguracao conf = new clsConfiguracao();
            return conf.verificarTextBoxSelecionados(lst);

        }
        int contador = 0;
        public void 
            atualizar(string nomeAmbiente, string caminhoArquivo, string tipoAtualizacao)
        {

            clsOperacoes operacoes = new clsOperacoes(progressBar1, lblTeste, checkVersao12.Checked);
            Util.clsUtil util = new Util.clsUtil();
            try
            {

                string extension = Path.GetExtension(this.txtEspecificaBiblioteca.Text);
                string extension2 = Path.GetExtension(this.txtEspecificaLabore.Text);
                string extension3 = Path.GetExtension(this.txtEspecificaPortal.Text);

                clsConfiguracao clsConfiguracao = new clsConfiguracao();
                util.mensagemAviso("Iniciando processo de atualização do patch");
                if (Directory.Exists("C:\\{app}") && Directory.Exists("C:\\{temp}"))
                {
                    operacoes.deletarDiretorio("C:\\{app}", true);
                    operacoes.deletarDiretorio("C:\\{temp}", true);
                }
                if (tipoAtualizacao == "Labore")
                {
                    operacoes.extrairInstalador(caminhoArquivo, "C:\\extrair\\Labore");
                    if (extension2 == ".msi")
                    {
                        operacoes.DirectoryCopy("C:\\extrair\\WinRoot\\totvs\\CorporeRM", nomeAmbiente, true, false);
                    }
                    else
                    {
                        operacoes.DirectoryCopy("C:\\extrair\\Labore\\{app}", nomeAmbiente, true, false);
                    }
                }
                else if (tipoAtualizacao == "Portal")
                {
                    if (!Directory.Exists("C:\\extrair\\Portal"))
                    {
                        Directory.CreateDirectory("C:\\extrair\\Portal");
                    }
                    operacoes.extrairInstalador(caminhoArquivo, "C:\\extrair\\Portal");
                    if (Path.GetExtension(caminhoArquivo) == ".msi")
                    {
                        operacoes.DirectoryCopy("C:\\extrair\\Portal\\WinRoot\\totvs\\CorporeRM", nomeAmbiente, true, false);
                    }
                    else
                    {
                        if (!Directory.Exists("C:\\ex\\Corpore.Net"))
                        {
                            Directory.CreateDirectory("C:\\ex\\Corpore.Net");
                        }
                        operacoes.DirectoryCopy("C:\\extrair\\Portal\\{app}", "C:\\ex\\Corpore.Net", true, true);
                        operacoes.DirectoryCopy("C:\\ex\\Corpore.Net", nomeAmbiente, true, false);
                    }
                }
                else
                {
                    operacoes.extrairInstalador(caminhoArquivo, "C:\\extrair\\Biblioteca");
                    if (Path.GetExtension(caminhoArquivo) == ".msi")
                    {
                        operacoes.DirectoryCopy("C:\\extrair\\Biblioteca\\WinRoot\\totvs\\CorporeRM", nomeAmbiente, true, false);
                    }
                    else
                    {
                        operacoes.DirectoryCopy("C:\\extrair\\Biblioteca\\{app}", nomeAmbiente, true, false);
                    }
                }
                if (tipoAtualizacao == "Biblioteca")
                {
                    if (Directory.Exists("c:\\rep\\Biblioteca"))
                    {
                        operacoes.deletarDiretorio("c:\\rep\\Biblioteca", true);
                        this.contador++;
                    }
                }
                if (tipoAtualizacao == "Portal")
                {
                    if (Directory.Exists("c:\\rep\\Portal"))
                    {
                        operacoes.deletarDiretorio("c:\\rep\\Portal", true);
                        this.contador++;
                    }
                }
                else if (tipoAtualizacao == "Labore")
                {
                    if (Directory.Exists("c:\\rep\\Labore"))
                    {
                        operacoes.deletarDiretorio("c:\\rep\\Labore", true);
                        this.contador++;
                    }
                }
                clsConfiguracao.acertarRMVersion(nomeAmbiente);
                util.mensagemSucesso("Versão atualizada com sucesso!");
                this.progressBar1.Value = 0;
                this.lblTeste.Text = "";
                this.lblTeste.Update();
                if (this.contador == this.retornarQuantidadeTextBox())
                {
                    if (Directory.Exists("C:/rep"))
                    {
                        operacoes.deletarDiretorio("C:/rep", true);
                        this.contador = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                util.mensagemErro(ex.Message);
            }

        }

        private void cbUtilizaEspecifica_CheckedChanged(object sender, EventArgs e)
        {
            verificarEspecifica();

        }


        public void deletarDiretorio(string caminhoDeletar)
        {
            clsOperacoes operacoes = new clsOperacoes();

            if (Directory.Exists(caminhoDeletar))
            {
                operacoes.deletarDiretorio(caminhoDeletar, true);
            }
        }
        public void verificarEspecifica()
        {
            clsRegraNegocio regra = new clsRegraNegocio();

            List<Label> lbl = new List<Label>();
            List<TextBox> txts = new List<TextBox>();
            List<Button> bnts = new List<Button>();
            List<CheckBox> cheBox = new List<CheckBox>();
            lbl.Add(lblBibliotecaEspecifica);
            lbl.Add(lblLaboreEspecifica);
            lbl.Add(lblEspecificaPortal);

            txts.Add(txtEspecificaBiblioteca);
            txts.Add(txtEspecificaLabore);
            txts.Add(txtEspecificaPortal);
            bnts.Add(btnEspecificaBiblioteca);
            bnts.Add(btnEspecificaLabore);
            bnts.Add(btnEspecificaPortal);

            cheBox.Add(checkBiblioteca);
            cheBox.Add(checkLabore);
            cheBox.Add(checkPortal);

            regra.habilitarEspecifica(cbUtilizaEspecifica, lbl, txts, bnts, cheBox);
        }

        private void checkBiblioteca_Click(object sender, EventArgs e)
        {
            verificarEspecifica();
        }

        private void checkLabore_Click(object sender, EventArgs e)
        {
            verificarEspecifica();
        }

        private void checkPortal_Click(object sender, EventArgs e)
        {
            verificarEspecifica();
        }

        private void btnBiblioteca_Click(object sender, EventArgs e)
        {
            abrirMsi(txtBibliotecaCriar);
        }

        public void abrirMsi(TextBox txt)
        {
            Util.clsUtil util = new Util.clsUtil();
            txt.Text = util.abrirDialigoArquivo("Biblioteca|*.msi*", "Abrir", "Biblioteca");
        }
        private void btnLabore_Click(object sender, EventArgs e)
        {
            abrirMsi(txtLaboreCriar);
        }

        private void btnPortal_Click(object sender, EventArgs e)
        {
            abrirMsi(txtPortalCriar);
        }

        public void abrirExe(TextBox txt, string tipo)
        {
            Util.clsUtil util = new Util.clsUtil();
            txt.Text = util.abrirDialigoArquivo("Arquivo Rar|*.*|Arquivo Exe|*.exe*", "Abrir", tipo);
        }

        private void btnEspecificaBiblioteca_Click(object sender, EventArgs e)
        {
            abrirExe(txtEspecificaBiblioteca, "Biblioteca");

        }

        private void btnEspecificaLabore_Click(object sender, EventArgs e)
        {
            abrirExe(txtEspecificaLabore, "Labore");
        }

        private void btnEspecificaPortal_Click(object sender, EventArgs e)
        {
            abrirExe(txtEspecificaPortal, "Portal");
        }

        private void txtAmbienteAtualizar_Click(object sender, EventArgs e)
        {
            clsRegraNegocio regra = new clsRegraNegocio();
            regra.listarAmbiente(txtAmbienteAtualizar);
        }


        public void chamarAtualizacao(string nomeAmbiente)
        {
            if (checkAtualizaBiblioteca.Checked)
            {
                atualizar(nomeAmbiente, txtAtualizarBiblioteca.Text, "Biblioteca");
            }

            if (checkAtualizaLabore.Checked)
            {
                atualizar(nomeAmbiente, txtAtualizarLabore.Text, "Aplicativo");
            }

            if (checkAtualizaPortal.Checked)
            {
                atualizar(nomeAmbiente, txtAtualizarPortal.Text, "Portal");
            }
        }

        /// <summary>
        /// Remover este metodo quando possivel 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Util.clsUtil util = new Util.clsUtil();
            List<Control> controles = new List<Control>();

            clsServico servico = new clsServico();
            if (util.verificarNulo(errorProvider1, controles))
            {

            }



            else
            {


                string nomeAmbiente = txtAmbienteAtualizar.Text.Remove(0, 9);
                if (checkPararServico.Checked)
                {
                    pararServico(nomeAmbiente);
                }

                chamarAtualizacao(nomeAmbiente);

                if (checkAtualizacaoIniciarHost.Checked)
                {
                    iniciarServico(nomeAmbiente);
                }

                if (checkAbrirLabore.Checked)
                {
                    servico.AbreAplicativo("Labore", nomeAmbiente, checkVersao12.Checked);
                }

            }
        }


        public void iniciarServico(string nomeAmbiente)
        {
            string ambienteCompleto = @"C:totvs/" + nomeAmbiente;
            clsServico servico = new clsServico();
            try
            {
                servico.InstalaServico(ambienteCompleto);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void pararServico(string nomeAmbiente)
        {
            try
            {
                string ambienteCompleto = @"C:totvs/" + nomeAmbiente;
                clsServico servico = new clsServico();
                Util.clsUtil util = new Util.clsUtil();
                util.mensagemAviso("O host será parado e todos aplicativos RM fechados");
                servico.fechartudo();
                servico.DesinstalaServico(ambienteCompleto);
            }

            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        public void verificarVersao12Labore()
        {
            clsRegraNegocio regra = new clsRegraNegocio();
            if (checkVersao12.Checked)
            {
                txtLaboreCriar.Visible = false;
                txtLaboreCriar.Text = "";
                lblLaboreCriar.Visible = false;
                btnLabore.Visible = false;
            }

            else
            {

                regra.verificarVisivel(txtLaboreCriar, btnLabore, checkLabore, lblLaboreCriar);
            }
        }

        private void checkVersao12_CheckedChanged(object sender, EventArgs e)
        {
            verificarVersao12Labore();
        }



        private void checkIniciarLabore_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkIniciarHost_CheckedChanged(object sender, EventArgs e)
        {
            /*
            Util.clsUtil util = new Util.clsUtil();
            util.mensagemAlerta("ATENÇÃO : Será aberto ao final do processo a pasta com o host para que o mesmo seja iniciado!");
             */
        }

        private void btnAtualizarBiblioteca_Click(object sender, EventArgs e)
        {
            abrirExe(txtAtualizarBiblioteca, "Biblioteca");
        }

        private void btnAtualizarLabore_Click(object sender, EventArgs e)
        {
            abrirExe(txtAtualizarLabore, "Labore");
        }

        private void btnAtualizarPortal_Click(object sender, EventArgs e)
        {
            abrirExe(txtAtualizarPortal, "Portal");
        }

        private void checkAtualizaBiblioteca_CheckedChanged(object sender, EventArgs e)
        {
            clsRegraNegocio regra = new clsRegraNegocio();
            regra.verificarVisivel(txtAtualizarBiblioteca, btnAtualizarBiblioteca, checkAtualizaBiblioteca, lblAtualizarBiblioteca);
        }

        private void checkAtualizaLabore_CheckedChanged(object sender, EventArgs e)
        {
            clsRegraNegocio regra = new clsRegraNegocio();
            regra.verificarVisivel(txtAtualizarLabore, btnAtualizarLabore, checkAtualizaLabore, lblAtualizarLabore);
        }

        private void checkAtualizaPortal_CheckedChanged(object sender, EventArgs e)
        {
            clsRegraNegocio regra = new clsRegraNegocio();
            regra.verificarVisivel(txtAtualizarPortal, btnAtualizarPortal, checkAtualizaPortal, lblAtualizarPortal);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsServico servico = new clsServico();
            Process.Start("Explorer", string.Format(@"C:\totvs\{0}\RM.net", txtNome.Text));
            //   Process.Start(string.Format(@"C:\totvs\{0}\RM.net\RM.Host.ServiceManager.exe", txtNome.Text));



        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Util.clsUtil util = new Util.clsUtil();
            util.mensagemAlerta("ATENÇÃO : Será aberto ao final do processo a pasta com o host para que o mesmo seja iniciado!");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            clsServico server = new clsServico();
            server.fechartudo();
            server.DesinstalaServico(txtAmbienteAtualizar.Text);
            server.InstalaServico(txtAmbienteAtualizar.Text);
        }




    }
}
