﻿using System;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;
using FluentSharp.Git;
using FluentSharp.Git.APIs;

namespace FluentSharp.O2Platform
{
    public class O2_Platform_Config
    {
        public static O2_Platform_Config Current  { get; set; }
        
        public string CurrentUser_AppData   { get; set;}
        public string Folder_Root           { get; set;}
        public string Folder_Scripts        { get; set;}
        public string Version_Name          { get; set;}

        static O2_Platform_Config()
        {
            Current = new O2_Platform_Config();
        }

        public O2_Platform_Config()
        {
            SetUpDefaultValues();
        }
        public O2_Platform_Config SetUpDefaultValues()
        {
            CurrentUser_AppData        = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Version_Name = "OWASP O2 Platform ".append_Version_FluentSharp_Short();     //"O2 Platform v.5.3";
            Folder_Root     = CurrentUser_AppData.pathCombine(Version_Name.replace(" ","_"));
            Folder_Scripts  = Folder_Root.pathCombine("O2.Platform.Scripts");
            Folder_Root.createDir();
            PublicDI.config.MapFolders_And_Set_DefaultValues(Folder_Root);            
            return this;
        }

        
    }
    public class O2_Platform_Scripts
    {
        public API_NGit nGit;
        
        public bool SetUp()
        {
            return Clone_Or_Open_O2_Platform_Scripts_Repository();
        }
        public bool Clone_Or_Open_O2_Platform_Scripts_Repository()
        {            
            var sourceRepository = O2_Platform_Consts.GIT_HUB_O2_PLATFORM_SCRIPTS;
            var targetFolder     = O2_Platform_Config.Current.Folder_Scripts;
            nGit = new API_NGit().open_or_Clone(sourceRepository, targetFolder);
            return nGit.isGitRepository();
        }
        public string ScriptsFolder()
        {
            return O2_Platform_Config.Current.Folder_Scripts;
        }

        

    }

    public class O2_Platform_Consts
    {
        public static string   GIT_HUB_O2_PLATFORM_SCRIPTS { get; set; }

        static O2_Platform_Consts()
        {
            GIT_HUB_O2_PLATFORM_SCRIPTS = "https://github.com/o2platform/O2.Platform.Scripts.git";
        }

    }
}
