using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia_Ambiente
{
    public class clsServico
    {
        public void AbreAplicativo(string aplicativo, string nomeCaminho, bool versao12)
        {

            if (aplicativo == "RM")
            {
                Process.Start(this.GetCaminho("RM.Net\\RM.EXE", nomeCaminho));
            }
            else if (aplicativo == "Labore" && versao12 == false)
            {
                Process.Start(string.Format(@"C:\totvs\{0}\RMLabore\RMLabore.exe", nomeCaminho));
            }

            else
            {
                Process.Start(string.Format(@"C:\totvs\{0}\RM.net\RMLabore.exe", nomeCaminho));
            }


        }

        private string GetCaminho(string aplicativo, string nomeCaminho)
        {
            string str = string.Empty;

            string[] array = string.Format(@"c:/totvs/{0}", nomeCaminho).Split(new char[]
				{
					'\\'
				});
            for (int i = 0; i < array.Length - 2; i++)
            {
                str = str + array[i] + "\\";
            }

            return str + aplicativo;
        }
        public void fechartudo()
        {
            Process[] processesByName = Process.GetProcessesByName("RM");
            Process[] array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }

            processesByName = Process.GetProcessesByName("ServiceManager");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RM.HOST");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMAgilis");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMBiblios");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMBis");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMBonum");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMChronus");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMClass");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMClassisF");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMClassisNet");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMFactor");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMFLUXUS");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMJanus");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMLabore");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMLiber");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMNucleus");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMofficina");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMPortal");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMsaldus");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMSaude");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMSGI");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMSolum");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMTestis");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
            processesByName = Process.GetProcessesByName("RMVitae");
            array = processesByName;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                process.Kill();
            }
        }
        private string GetFrameworkDir()
        {
            string text = this.GetWindowsDir();
            if (Environment.Version.Major == 4)
            {
                text += "\\Microsoft.NET\\Framework\\v4.0.30319\\";
            }
            else
            {
                text += "\\Microsoft.NET\\Framework\\v2.0.50727\\";
            }
            return text;
        }
        public void ExecuteFramework(string utill, string Argument)
        {
            utill = this.GetFrameworkDir() + utill;
            this.ExecuteProcess(utill, Argument);
        }
        public void ExecuteProcess(string FileName, string Arguments)
        {
            Process process = Process.Start(new ProcessStartInfo(FileName, Arguments)
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = false,
                UseShellExecute = true
            });
            process.WaitForExit();
            process.Close();
        }
        public string GetWindowsDir()
        {
            return Environment.ExpandEnvironmentVariables("%WinDir%");
        }
        
        public void InstalaServico(string caminho)
        {
            try
            {
               this.ExecuteFramework("InstallUtil.exe", string.Format(@"{0}\RM.Net\RM.Host.Service.exe", caminho));

               this.ExecuteFramework("InstallUtil.exe", " /account=NetworkService " + "");

                //this.ExecuteFramework("InstallUtil.exe", string.Format(@"C:\totvs\{0}\RM.Net\RM.Host.Service.exe", caminho));

               // this.ExecuteFramework("InstallUtil.exe", " /account=localsystem " + "");


            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
            this.iniciarserv();
        }
         
        /*
        public void InstalaServico(string caminho)
        {
            string cmd = "explorer.exe";
            string arg = "/select, " + string.Format(@"C:\totvs\{0}\RM.net\RM.Host.ServiceManager.exe", caminho);
            Process.Start(cmd, arg);
        }
         */
        public void DesinstalaServico(string caminho)
        {
            try
            {

               // this.ExecuteProcess("net", " stop " + string.Format(@"C:\totvs\{0}\RM.Net\RM.Host.Service.exe", caminho));
                //this.ExecuteFramework("InstallUtil.exe", " /u " + string.Format(@"C:\totvs\{0}\RM.Net\RM.Host.Service.exe", caminho));

                string teste = string.Format(@"{0}\RM.Net\RM.Host.Service.exe", caminho);
                this.ExecuteProcess("net", " stop " + string.Format(@"{0}\RM.Net\RM.Host.Service.exe", caminho));
                this.ExecuteFramework("InstallUtil.exe", " /u " + string.Format(@"{0}\RM.Net\RM.Host.Service.exe", caminho));

            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);

            }
        }

        private void iniciarserv()
        {


            this.ExecuteProcess("net", " start RM.Host.Service");

        }
    }
}
