using ff14bot.AClasses;
using ff14bot.Helpers;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Media;
using Clio.Utilities;

namespace SwitcharooLoader
{
    public class SwitcharooLoader : BotPlugin
    {
        private const string ProjectName = "Switcharoo";
        private const string ProjectMainType = "Switcharoo.SwitcharooPlugin";
        private const string ProjectAssemblyName = "Switcharoo.dll";
        private static readonly Color LogColor = Color.FromRgb(109, 139, 225);
        public override string Description => "Routine Switcher.";
        public override string Author => "Omninewb";
        public override string ButtonText => "Settings";
        public override Version Version => new Version("1.0.0");
        public override bool WantButton => true;

        private static readonly object Locker = new object();
        private static readonly string ProjectAssembly = Path.Combine(Environment.CurrentDirectory, $@"Plugins\{ProjectName}\{ProjectAssemblyName}");
        private static readonly string GreyMagicAssembly = Path.Combine(Environment.CurrentDirectory, @"GreyMagic.dll");
        //private static readonly string VersionPath = Path.Combine(Environment.CurrentDirectory, $@"Plugins\{ProjectName}\version.txt");
        private static bool _loaded;

        private static object Product { get; set; }

        private static MethodInfo InitFunc { get; set; }
        private static MethodInfo ButtonFunc { get; set; }
        private static MethodInfo PulseFunc { get; set; }

        private static MethodInfo EnabledFunc { get; set; }
        private static MethodInfo DisabledFunc { get; set; }
        private static MethodInfo ShutDownFunc { get; set; }

        #region Overrides

        public override string Name => ProjectName;

        public override void OnInitialize()
        {
            if (!_loaded && Product == null) { LoadProduct(); }
            if (Product != null) { InitFunc.Invoke(Product, null); }
        }

        public override void OnButtonPress()
        {
            if (!_loaded && Product == null) { LoadProduct(); }
            if (Product != null) { ButtonFunc.Invoke(Product, null); }
        }

        public override void OnPulse()
        {
            if (!_loaded && Product == null) { LoadProduct(); }
            if (Product != null) { PulseFunc.Invoke(Product, null); }
        }

        public override void OnEnabled()
        {
            if (!_loaded && Product == null) { LoadProduct(); }
            if (Product != null) { EnabledFunc.Invoke(Product, null); }
        }

        public override void OnDisabled()
        {
            if (!_loaded && Product == null) { LoadProduct(); }
            if (Product != null) { DisabledFunc.Invoke(Product, null); }
        }

        public override void OnShutdown()
        {
            if (!_loaded && Product == null) { LoadProduct(); }
            if (Product != null) { ShutDownFunc.Invoke(Product, null); }
        }

        #endregion Overrides

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(string name);

        public static bool Unblock(string fileName)
        {
            return DeleteFile(fileName + ":Zone.Identifier");
        }

        public static void RedirectAssembly()
        {
            ResolveEventHandler handler = (sender, args) =>
            {
                string name = Assembly.GetEntryAssembly().GetName().Name;
                var requestedAssembly = new AssemblyName(args.Name);
                return requestedAssembly.Name != name ? null : Assembly.GetEntryAssembly();
            };

            AppDomain.CurrentDomain.AssemblyResolve += handler;

            ResolveEventHandler greyMagicHandler = (sender, args) =>
            {
                var requestedAssembly = new AssemblyName(args.Name);
                return requestedAssembly.Name != "GreyMagic" ? null : Assembly.LoadFrom(GreyMagicAssembly);
            };

            AppDomain.CurrentDomain.AssemblyResolve += greyMagicHandler;
        }

        private static string CompiledAssembliesPath => Path.Combine(Utilities.AssemblyDirectory, "CompiledAssemblies");

        private static Assembly LoadAssembly(string path)
        {
            if (!File.Exists(path)) { return null; }
            if (!Directory.Exists(CompiledAssembliesPath))
            {
                Directory.CreateDirectory(CompiledAssembliesPath);
            }

            var t = DateTime.Now.Ticks;
            var name = $"{Path.GetFileNameWithoutExtension(path)}{t}.{Path.GetExtension(path)}";
            var pdbPath = path.Replace(Path.GetExtension(path), "pdb");
            var pdb = $"{Path.GetFileNameWithoutExtension(path)}{t}.pdb";
            var capath = Path.Combine(CompiledAssembliesPath, name);
            if (File.Exists(capath))
            {
                try
                {
                    File.Delete(capath);
                }
                catch (Exception)
                {
                    //
                }
            }
            if (File.Exists(pdb))
            {
                try
                {
                    File.Delete(pdb);
                }
                catch (Exception)
                {
                    //
                }
            }

            if (!File.Exists(capath))
            {
                File.Copy(path, capath);
            }

            if (!File.Exists(pdb) && File.Exists(pdbPath))
            {
                File.Copy(pdbPath, pdb);
            }

            Assembly assembly = null;
            Unblock(path);
            try { assembly = Assembly.LoadFrom(path); }
            catch (Exception e) { Logging.WriteException(e); }

            return assembly;
        }

        private static object Load()
        {
            Log("Loading...");

            RedirectAssembly();

            var assembly = LoadAssembly(ProjectAssembly);
            if (assembly == null) { return null; }

            Type baseType;
            try { baseType = assembly.GetType(ProjectMainType); }
            catch (Exception e)
            {
                Log(e.ToString());
                return null;
            }

            object bb;
            try { bb = Activator.CreateInstance(baseType); }
            catch (Exception e)
            {
                Log(e.ToString());
                return null;
            }

            if (bb != null) { Log(ProjectName + " was loaded successfully."); }
            else { Log("Could not load " + ProjectName + ". This can be due to a new version of Rebornbuddy being released. An update should be ready soon."); }

            return bb;
        }

        private static void LoadProduct()
        {
            lock (Locker)
            {
                if (Product != null) { return; }
                Product = Load();
                _loaded = true;
                if (Product == null) { return; }

                PulseFunc = Product.GetType().GetMethod("OnPulse");
                EnabledFunc = Product.GetType().GetMethod("OnEnabled");
                DisabledFunc = Product.GetType().GetMethod("OnDisabled");
                ShutDownFunc = Product.GetType().GetMethod("ShutDown");
                ButtonFunc = Product.GetType().GetMethod("OnButtonPress");
                InitFunc = Product.GetType().GetMethod("OnInitialize", new[] { typeof(int) });
                if (InitFunc != null)
                {
#if RB_CN
                Log($"{ProjectName} CN loaded.");
                InitFunc.Invoke(Product, new[] {(object)2});
#else
                    Log($"{ProjectName} 64 loaded.");
                    InitFunc.Invoke(Product, new[] { (object)1 });
#endif
                }
            }
        }

        private static void Log(string message)
        {
            message = "[Auto-Updater][" + ProjectName + "] " + message;
            Logging.Write(LogColor, message);
        }
    }
}