using ff14bot;
using ff14bot.AClasses;
using Switcharoo.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ff14bot.Managers;
using Switcharoo.Models;
using RoutineManager = ff14bot.Managers.RoutineManager;

#pragma warning disable 4014
#pragma warning disable CS1998

namespace Switcharoo
{
    public class SwitcharooPlugin
    {
        private static DateTime _pulseLimiter;
        private static bool _initialized;
        private static readonly string VersionPath = Path.Combine(Environment.CurrentDirectory, @"Plugins\Switcharoo\version.txt");

        public SwitcharooPlugin()
        {
            if (PluginManager.GetEnabledPlugins().Contains("Switcharoo"))
            {
                OnInitialize();
            }
        }

        public static void OnButtonPress()
        {
            RoutineManager.PickRoutineFired += RoutinePickFired;
            Logger.SwitcharooLog(@"Opening Switcharoo Settings!");
            FormManager.OpenForms();
        }

        public void OnInitialize()
        {
            TreeRoot.OnStart += OnBotStart;
            TreeRoot.OnStop += OnBotStop;
            RoutineManager.PickRoutineFired += RoutinePickFired;

            //Logger.SwitcharooLog($"Initializing Version: {File.ReadAllText(VersionPath)}");
            Logger.SwitcharooLog($"Initializing Version: GitHub 1.0.0");
            FormManager.SaveFormInstances();
            _initialized = true;
        }

        private void OnBotStop(BotBase bot)
        {
            Logger.SwitcharooLog(@"Stopping Switcharoo!");
            FormManager.SaveFormInstances();
            RoutineManager.PickRoutineFired -= RoutinePickFired;
        }

        private void OnBotStart(BotBase bot)
        {
            if (!_initialized) OnInitialize();

            Logger.SwitcharooLog(@"Starting Switcharoo!");
            FormManager.SaveFormInstances();
            RoutineManager.PickRoutineFired += RoutinePickFired;
        }

        public void OnShutdown()
        {
            FormManager.SaveFormInstances();
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            RoutineManager.PickRoutineFired -= RoutinePickFired;
        }

        public void OnEnabled()
        {
            if (!_initialized) OnInitialize();

            FormManager.SaveFormInstances();
            TreeRoot.OnStart += OnBotStart;
            TreeRoot.OnStop += OnBotStop;
            RoutineManager.PickRoutineFired += RoutinePickFired;
        }

        public void OnDisabled()
        {
            FormManager.SaveFormInstances();
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            RoutineManager.PickRoutineFired -= RoutinePickFired;
        }

        private static void RoutinePickFired(object sender, EventArgs eventArgs)
        {
            FormManager.SaveFormInstances();
            RoutineManager.PreferedRoutine = Utilities.RoutineManager.GetRoutine();
            Logger.SwitcharooLog(@"New Routine selected, resetting logic.");
        }

        public void OnPulse()
        {
            if (!_initialized) OnInitialize();

            if (PulseCheck())
                FormManager.SaveFormInstances();
        }

        public static bool PulseCheck()
        {
            if (DateTime.Now < _pulseLimiter) return false;
            if (DateTime.Now > _pulseLimiter)
            {
                _pulseLimiter = DateTime.Now.Add(TimeSpan.FromMinutes(1));
            }

            return true;
        }
    }
}