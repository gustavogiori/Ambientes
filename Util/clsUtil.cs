using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Util
{
   public class clsUtil
    {
        public void controleErrorProviderTela(ErrorProvider erro, bool remover, Control controle)
        {
            if (remover == true)
            {
                erro.SetError(controle, "");
            }

            else
            {
                erro.SetError(controle, "Valor deve ser preenchido!");
            }
        }

        public bool verificarNulo(ErrorProvider erro, List<Control> controles)
        {
            bool possuiValorNulo = false;
            int cout = 0;

            foreach (Control controle in controles)
            {
                if (controle is TextBox)
                {
                    if (controle.Text == string.Empty)
                    {
                        cout++;
                        controleErrorProviderTela(erro, false, controle);
                    }

                    else
                    {
                        //  cout--;
                        controleErrorProviderTela(erro, true, controle);
                    }
                }

                if (controle is ComboBox)
                {
                    if (controle.Text == string.Empty)
                    {
                        controleErrorProviderTela(erro, false, controle);
                        cout++;
                    }

                    else
                    {
                        controleErrorProviderTela(erro, true, controle);
                        //cout--;
                    }
                }
            }

            if (cout > 0)
            {
                possuiValorNulo = true;
            }
            else
            {
                possuiValorNulo = false;
            }
            return possuiValorNulo;
        }
        public void mensagemErro(string texto)
        {
            MessageBox.Show(texto, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void mensagemSucesso(string texto)
        {
            MessageBox.Show(texto, "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void mensagemAlerta(string texto)
        {
            MessageBox.Show(texto, "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        public void mensagemAviso(string texto)
        {
            MessageBox.Show(texto, "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public string salvarArquivo(string extensaoPadrao, string titulo, string filtro,string arquivo)
        {
            SaveFileDialog dialogo = new SaveFileDialog();
            string retorno = "";
            dialogo.AddExtension = true;
            dialogo.DefaultExt = extensaoPadrao;
            dialogo.Title = titulo;
            dialogo.Filter = filtro;
            dialogo.FileName = arquivo;
            dialogo.CheckPathExists = true;

            DialogResult result = dialogo.ShowDialog();

            retorno = dialogo.FileName;

            if (result == DialogResult.Cancel)
            {
                retorno = "";
            }

            return retorno;
        }
        public void deletarDiretorio(string path, bool recursive)
        {
            try
            {
                // Apagar sub-pastas e ficheiros?
                if (recursive)
                {
                    // Sim... Então vamos a isso
                    var subfolders = Directory.GetDirectories(path);
                    foreach (var s in subfolders)
                    {
                        deletarDiretorio(s, recursive);
                    }
                }

                // Obtém todos os ficheiros da pasta
                var files = Directory.GetFiles(path);
                foreach (var f in files)
                {
                    // Obtém os atributos do ficheiro
                    var attr = File.GetAttributes(f);

                    // O ficheiro é 'read-only'?
                    if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        // Sim... Remove o atributo 'read-only' do ficheiro
                        File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                    }

                    // Apaga o ficheiro
                    File.Delete(f);
                }

                // Ao chegar aqui, todos os ficheiros da pasta
                // foram apagados... É só apagar a pasta vazia
                Directory.Delete(path);
            }

            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }

        public string abrirDialigoArquivo(string filtro, string titulo,string Tipo)
        {
            string retorno = "";
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = filtro;
            dialogo.Title = titulo;
            dialogo.InitialDirectory = @"\\engenharia";
            dialogo.ShowDialog();

            if (dialogo.FileName != string.Empty)
            {
                DirectoryInfo info = new DirectoryInfo(dialogo.FileName);

                if (info.Extension == ".zip")
                {
                    if (Tipo == "Biblioteca")
                    {
                        if (Directory.Exists(@"C:\rep\Biblioteca"))
                        {
                            deletarDiretorio(@"C:\rep\Biblioteca", true);
                        }
                        mensagemAviso("Arquivo selecionado .zip , o mesmo será extraido para C:\\rep\\Biblioteca");
                        ZipFile.ExtractToDirectory(dialogo.FileName, @"C:\rep\Biblioteca");
                        // mensagemAviso("Altere o caminho para c:\\Extrair e selecione o arquivo desejado");
                        retorno = "";
                        dialogo.InitialDirectory = @"C:\rep\Biblioteca";
                        dialogo.ShowDialog();
                    }
                    if (Tipo == "Labore")
                    {
                        if (Directory.Exists(@"C:\rep\Labore"))
                        {
                            deletarDiretorio(@"C:\rep\Labore", true);
                        }
                        mensagemAviso("Arquivo selecionado .zip , o mesmo será extraido para C:\\extrair\\Labore");
                        ZipFile.ExtractToDirectory(dialogo.FileName, @"C:\rep\Labore");
                        // mensagemAviso("Altere o caminho para c:\\Extrair e selecione o arquivo desejado");
                        retorno = "";
                        dialogo.InitialDirectory = @"C:\rep\Labore";
                        dialogo.ShowDialog();
                    }
                     if (Tipo == "Portal")
                    {
                        if (Directory.Exists(@"C:\rep\Portal"))
                        {
                            deletarDiretorio(@"C:\rep\Portal", true);
                        }
                        mensagemAviso("Arquivo selecionado .zip , o mesmo será extraido para C:\\extrair\\Portal");
                        ZipFile.ExtractToDirectory(dialogo.FileName, @"C:\rep\Portal");
                        // mensagemAviso("Altere o caminho para c:\\Extrair e selecione o arquivo desejado");
                        retorno = "";
                        dialogo.InitialDirectory = @"C:\rep\Portal";
                        dialogo.ShowDialog();
                    }
                }
            }
            retorno = dialogo.FileName;

            if (retorno == string.Empty)
            {
               // mensagemAviso("Processo cancelado!");
                retorno = "";
            }

            return retorno;
        }

        public string localDiretorio(FolderBrowserDialog dialogo)
        {
            string caminho = "";
            dialogo.ShowDialog();
            caminho = dialogo.SelectedPath;

            return caminho;
        }

        public string salvarDiretorio(SaveFileDialog dialogo, string filter, string titulo)
        {
            string name = "";

            dialogo.Filter = filter;
            dialogo.Title = titulo;
            if (dialogo.ShowDialog() == DialogResult.OK)
            {


                if (name == string.Empty)
                {
                  
                    return null;
                }

                else
                {
                    dialogo.FileName = name;
                }
            }
            return name;
        }
    }
}
