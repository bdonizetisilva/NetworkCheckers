using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.IO;

namespace NetworkCheckers.Controls
{
    /// <summary>
    /// Implementação do player de aúdio
    /// </summary>
    public class Win32Audio
    {
        // Estáticos
        #region Static

        /// <summary>
        /// Sons em cache
        /// </summary>
        private static Dictionary<string, string> _CachedWavs = new Dictionary<string, string>();

        /// <summary>
        /// Sons bloqueados
        /// </summary>
        private static readonly object _SyncLock = new object();

        /// <summary>
        /// Métodos nativos
        /// </summary>
        private static class NativeMethods
        {
            /// <summary>
            /// Reproduz um arquvio de audio usando uma chamada externa
            /// </summary>
            [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool PlaySound(string pszSound, IntPtr hmod, uint fdwSound);
        }

        /// <summary>
        /// Flags de modos de reprodução de um som
        /// </summary>
        private static class SoundFlags
        {
            /// <summary>
            /// Reprodução assincrona
            /// </summary>
            public static uint SND_ASYNC = 0x0001;        

            /// <summary>
            /// Não para nenhum som que esteja sendo reproduzido
            /// </summary>
            public static uint SND_NOSTOP = 0x0010;       
        }

        #endregion

        // Variáveis
        #region Variables

        private TempFileCollection _TempFiles = new TempFileCollection();

        #endregion

        #region Public

        /// <summary>
        /// Libera os recursos deste objeto
        /// </summary>
        public  virtual void Dispose()
        {
            // Tenta recuperar arquivos como IDisposable
            IDisposable disposableTempFiles = this._TempFiles as IDisposable;

            // Se recuperou, realiza a liberação de recursos
            disposableTempFiles?.Dispose();

            // Chama garbage collector para liberar os recursos
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private

        private string GetWavResource(string wav)
        {
            if (!_CachedWavs.ContainsKey(wav))
            {
                lock (_SyncLock)
                {
                    if (!_CachedWavs.ContainsKey(wav))
                    {
                        // get the namespace 
                        string wavResource = "Okorodudu.Checkers.View.Providers.Win.Resources.Audio." + wav;

                        // get the resource into a stream
                        using (Stream wavInputStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(wavResource))
                        {
                            string tempfile = Path.GetTempFileName();
                            this._TempFiles.AddFile(tempfile, false);
                            byte[] wavData = new byte[wavInputStream.Length];
                            wavInputStream.Read(wavData, 0, (int)wavInputStream.Length);
                            File.WriteAllBytes(tempfile, wavData);
                            _CachedWavs.Add(wav, tempfile);
                        }
                    }
                }
            }

            string tempWavFile = (_CachedWavs.ContainsKey(wav)) ? _CachedWavs[wav] : string.Empty;
            return tempWavFile;
        }

        #endregion
    }
}
