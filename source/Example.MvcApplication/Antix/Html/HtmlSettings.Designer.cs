﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Antix.Html {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    public sealed partial class HtmlSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static HtmlSettings defaultInstance = ((HtmlSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new HtmlSettings())));
        
        public static HtmlSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\"")]
        public string HtmlAttributeQuote {
            get {
                return ((string)(this["HtmlAttributeQuote"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("br,hr,meta,link,img,video,audio,param,col")]
        public string HtmlNonContainers {
            get {
                return ((string)(this["HtmlNonContainers"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("em,strong,q,cite,dfn,i,b,sup,sub,small,code,mark,del,s,ins,abbr,span,br")]
        public string HtmlInlineElements {
            get {
                return ((string)(this["HtmlInlineElements"]));
            }
        }
    }
}
