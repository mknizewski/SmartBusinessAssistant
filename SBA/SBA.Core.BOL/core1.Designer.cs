﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SBA.Core.BOL {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.6.0.0")]
    public sealed partial class core : global::System.Configuration.ApplicationSettingsBase {
        
        private static core defaultInstance = ((core)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new core())));
        
        public static core Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("127.0.0.1")]
        public string SocketServerIp {
            get {
                return ((string)(this["SocketServerIp"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("669")]
        public int SocketServerPort {
            get {
                return ((int)(this["SocketServerPort"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int MaxConnectionsAllowed {
            get {
                return ((int)(this["MaxConnectionsAllowed"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\Projekty\\SmartBusinessAssistant\\Logs\\Core\\{0}\\core.log")]
        public string LogPathPattern {
            get {
                return ((string)(this["LogPathPattern"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("005607192447384902306:qqcyt3itmec")]
        public string CseEngineId {
            get {
                return ((string)(this["CseEngineId"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AIzaSyDMMvG30YwPxuV6x8KucspB4Ga2mGlW4wY")]
        public string CseEngineApiKey {
            get {
                return ((string)(this["CseEngineApiKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.googleapis.com/customsearch/v1?q={0}&cx={1}&key={2}")]
        public string CseApiUrlPath {
            get {
                return ((string)(this["CseApiUrlPath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public long CseCountResult {
            get {
                return ((long)(this["CseCountResult"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("bf68526a-8e37-4b2d-9780-850a854e1f26")]
        public string WebAuthGuid {
            get {
                return ((string)(this["WebAuthGuid"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("cfa12e66-7850-4d14-b72f-173327e4c8a7")]
        public string DiagAuthGuid {
            get {
                return ((string)(this["DiagAuthGuid"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("cd04d90c-ba26-463a-9f2e-40d3a3859487")]
        public string ClientAuthGuid {
            get {
                return ((string)(this["ClientAuthGuid"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Dictionaries\\baza_odmian_jezyka_polskiego.txt")]
        public string VarietyFileName {
            get {
                return ((string)(this["VarietyFileName"]));
            }
        }
    }
}
